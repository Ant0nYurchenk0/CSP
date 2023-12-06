using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    internal struct Schedule
    {
        public List<Class> Classes  {get; set; }
        public Schedule(IEnumerable<Class> classes)
        {
            Classes = classes.ToList();
        }

    }
}
