using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace Services
{
    public class DbReaderMapper<T> : IDataReaderMapper
    {
        private readonly Func<T> m_create;


        public DbReaderMapper()
            : this(Activator.CreateInstance<T>)
        {
        }

        public DbReaderMapper(Func<T> create)
        {
            m_create = create;
        }


        public List<T> Results { get; set; }

        protected virtual object MapField(string name, string dataTypeName, Type propertyType, DbDataReader reader)
        {
            var value = GetSafeValue(reader[name], default(object));
            if (propertyType == typeof(DateTime))
            {
                if(value != null)
                {
                    value = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
                }
            }
            if (propertyType == typeof(DateTime?))
            {
                if (value != null)
                {
                    value = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
                }
            }
            
            return value;
        }

        public void Read(DbDataReader reader)
        {
            var items = new List<T>();
            var schema = reader.GetSchemaTable();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var propertiesMap = new Dictionary<string, PropertyInfo>();

            foreach (var item in properties)
            {
                propertiesMap[item.Name] = item;
            }

            while (reader.Read())
            {
                var item = m_create();
                foreach (var column in schema.Rows.Cast<DataRow>())
                {
                    var name = column["ColumnName"] as string;
                    var dataTypeName = column["DataTypeName"] as string;

                    PropertyInfo property;
                    if (propertiesMap.TryGetValue(name, out property) &&
                        property.CanWrite)
                    {
                        var value = MapField(name, dataTypeName, property.PropertyType, reader);
                        property.SetValue(item, value, null);
                    }
                }

                items.Add(item);
            }

            Results = items;
        }

        /// Get safe value, if value not exists (DBNull) return the default value
        public static TType GetSafeValue<TType>(object value, TType defaultValue)
        {
            if (Convert.IsDBNull(value))
            {
                return defaultValue;
            }
            else
            {
                if (value is DateTime)
                {
                    value = ((DateTime)value).ToUniversalTime().ToLocalTime();
                }


                return (TType)value;
            }
        }
    }
}
