using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auditore.Models
{
    public class MyTask
    {
        //[JsonPropertyName("_id")]
        //public string Id { get; set; }

        //[JsonPropertyName("name")]
        //public string Name { get; set; }

        //[JsonPropertyName("description")]
        //public string Description { get; set; }

        //[JsonPropertyName("categoryId")]
        //public string CategoryId { get; set; }

        //[JsonPropertyName("userId")]
        //public string UserId { get; set; }

        //[JsonPropertyName("startDate")]
        //public DateTimeOffset StartDate { get; set; }

        //[JsonPropertyName("endDate")]
        //public DateTimeOffset EndDate { get; set; }

        //[JsonPropertyName("__v")]
        //public long V { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public long V { get; set; }
    }
}
