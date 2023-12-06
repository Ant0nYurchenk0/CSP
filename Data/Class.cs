using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    internal struct Class
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public Time Time { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }

        public Class(Time time, Group group, Teacher teacher, Course course)
        {
            Id = _id++;
            Time = time;
            Group = group;
            Teacher = teacher;
            Course = course;
        }
    }
}
