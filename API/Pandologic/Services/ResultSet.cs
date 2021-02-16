using System;
using System.Runtime.Serialization;

namespace Services
{
    [DataContract]
    [Serializable]
    public class ResultSet<TItem>
    {
        public static readonly DateTime MinimalTimeStamp = new DateTime(1900, 1, 1);

        [DataMember(EmitDefaultValue = false)]
        public DateTime TimeStamp { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TItem[] Items { get; set; }

        
        public ResultSet()
        {
            TimeStamp = MinimalTimeStamp;
        }
    }
}
