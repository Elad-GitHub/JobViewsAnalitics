using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Services
{
    public class DbCommandArguments
    {
        private readonly object m_source;

        public DbCommandArguments(object source)
        {
            m_source = source;
        }

        public object Output { get; set; }

        public static object ValueOrDBNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        private void AddCommandParameters(DbCommand command, object source, ParameterDirection direction = ParameterDirection.Input)
        {

            foreach(var property in source.GetType().GetProperties())
            {
                var name = "@" + property.Name;
                if (command.Parameters.Cast<DbParameter>().All(p => p.ParameterName != name))
                {
                    var value = property.GetValue(source, null);

                    if (property.PropertyType == typeof(DateTime))
                    {

                        value = ((DateTime)value).ToUniversalTime();
                    }
                    else if (property.PropertyType == typeof(DateTime?))
                    {
                        value = (((DateTime?)value).HasValue ? ((DateTime?)value).Value.ToUniversalTime() : ValueOrDBNull(default(DateTime?)));
                    }
                    else if (property.PropertyType.IsEnum)
                    {
                        if (value != null)
                        {
                            value = (int)value;
                        }
                    }

                    if (value == null)
                    {
                        value = DBNull.Value;
                    }


                    if (property.PropertyType == typeof(DataTable))
                    {
                        var table = value as DataTable;
                        if (table == null)
                        {
                            throw new ArgumentException("Table parameter (name: {0}) must have valid table value.", name);
                        }
                        AddTableParameter(command, name, table.TableName, table);
                    }
                }
            }

        }

        private void AddTableParameter(DbCommand command, string parameterName, string typeName, DataTable value)
        {
            var parameter = new SqlParameter(parameterName, SqlDbType.Structured);

            parameter.TypeName = typeName;
            parameter.Value = value;

            command.Parameters.Add(parameter);
        }

        public void AddCommandParameters(DbCommand command)
        {
            if (Output != null)
            {
                AddCommandParameters(command, Output, ParameterDirection.Output);
            }

            if (m_source != null)
            {
                AddCommandParameters(command, m_source);
            }
        }
    }
}
