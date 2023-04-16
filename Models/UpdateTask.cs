using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Models
{
    [AddINotifyPropertyChangedInterface]
    public class UpdateTask
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string categoryId { get; set; }
        public string userId { get; set; }
        public DateTimeOffset startDate { get; set; }
        public DateTimeOffset endDate { get; set; }
        public bool completed { get; set; } = false;
        public string taskColor { get; set; }
        public long __v { get; set; }
    }
}
