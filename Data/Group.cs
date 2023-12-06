using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    internal struct Group
    {
        private static int _id;
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<Course> PossibleCourses { get; set; }
        public Group(string name, List<Course> courses)
        {
            PossibleCourses = courses;
            Name = name;
            Id = _id++;
        }
        public Group Clone()
        {
            return new Group()
            {
                Id = Id,
                Name = Name,
                PossibleCourses = PossibleCourses.Select(c => c.Clone()).ToList(),
            };
        }
    }
}
