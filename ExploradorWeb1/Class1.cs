using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExploradorWeb1
{
    public class HistorialURL
    {
        public string Address { get; set; }
        public int VisitCount { get; set; }
        public DateTime LastAccessed { get; set; }

        public HistorialURL(string address)
        {
            Address = address;
            VisitCount = 1;
            LastAccessed = DateTime.Now;
        }

        public HistorialURL(string address, int visitCount, DateTime lastAccessed)
        {
            Address = address;
            VisitCount = visitCount;
            LastAccessed = lastAccessed;
        }
    }

}
