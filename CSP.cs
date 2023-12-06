using CSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    internal class CSP
    {
        public Data Data { get; }

        public CSP(Data data)
        {
            Data = data;
        }

        public Schedule GetSchedule()
        {
            var schedule = new Schedule(new List<Class>());
            foreach(var time in Data.Times)
            {
                var classes = GetClasses(Data.Groups.Select(g=>g.Clone()).ToList()).ToList();
                foreach (var clas in classes)
                {
                    schedule.Classes.Add(new Class(time, clas.Group, clas.Teacher, clas.Course));
                }
                for (var i = 0; i < Data.Groups.Count(); i++)
                {
                    var courseId = classes.First(c => c.Group.Id == Data.Groups[i].Id).Course.Id;
                    var course = Data.Groups[i].PossibleCourses.First(c => c.Id == courseId);
                    Data.Groups[i].PossibleCourses.Remove(course);
                    
                }
            }
            return schedule;
        }

        private IEnumerable<Class> GetClasses(List<Group> groups)
        {
            var clas = new Class();
            var minGroupId = groups.ToDictionary
                                    (
                                        g => g.Id, 
                                        g => groups.First(x=>x.Id == g.Id)
                                                   .PossibleCourses
                                                   .Count()
                                    )
                                   .OrderBy(x=>x.Value)
                                   .First()
                                   .Key;
            clas.Group = groups.First(g => g.Id == minGroupId);
            clas.Course = groups.First(g => g.Id == minGroupId).PossibleCourses.First();
            var teacherDict = new Dictionary<int, int>();
            foreach (var teacher in clas.Course.Teachers)
            {
                teacherDict.Add(
                                    teacher.Id, 
                                    groups.SelectMany(x=>x.PossibleCourses)
                                          .SelectMany(x=>x.Teachers)
                                          .Where(t=>t.Id == teacher.Id)
                                          .Count()
                                );
            }
            var minTeacherId = teacherDict.OrderBy(x => x.Value).First().Key;
            clas.Teacher = clas.Course.Teachers.First(t => t.Id == minTeacherId);

            var nextClasses = new List<Class>
            {
                clas
            };

            var newGroups = groups.Select(g => g.Clone()).Where(g => g.Id != minGroupId).ToList();
            if (newGroups.Count == 0) return nextClasses;
            for (var i = 0; i < newGroups.Count(); i++) 
            {
                for (var j = 0; j < newGroups[i].PossibleCourses.Count(); j++)
                {
                    newGroups[i].PossibleCourses[j].Teachers.Remove(clas.Teacher);
                    if(newGroups[i].PossibleCourses[j].Teachers.Count() == 0)
                    {
                        newGroups[i].PossibleCourses.Remove(newGroups[i].PossibleCourses[j]);
                    }
                    
                }
            }
            
            return nextClasses.Union(GetClasses(newGroups));
        }
    }
}
