using System;
using System.Runtime.Serialization;

namespace Pandologic
{
    [DataContract]
    public class JobsMappingQuery
    {
        [DataMember]
        public DateTime? Date { get; set; }
        
        [DataMember]
        public int? NumberOfActiveJobs { get; set; }
        
        [DataMember]
        public int? NumberOfJobViews { get; set; }
        
        [DataMember]
        public int? NumberOfPredictedJobViews { get; set; }
    }
}
