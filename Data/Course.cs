using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    internal struct Course
    {
        private static int _id = 0;

        public Course(string name, IEnumerable<Teacher> teachers)
        {
            Id = _id++;
            Name = name;
            Teachers = teachers.ToList();
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }

        internal Course Clone()
        {
            return new Course()
            {
                Id = Id,
                Name = Name,
                Teachers = Teachers.Select(t => t.Clone()).ToList(),
            };
        }
    }
}
