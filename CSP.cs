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
            var schedule = new Schedule(GetClasses(Data.Groups.Select(g=>g.Clone()).ToList(), new List<Class>(), 0, Data.Times[0]));
            return schedule;
        }

        private IEnumerable<Class> GetClasses(List<Group> groups, List<Class> assignedClasses, int counter, Time time)
        {
            
            if (counter != 0 && 1.0 * counter / Data.Groups.Count == counter / Data.Groups.Count)
            {
                groups = Data.Groups.Select(g => g.Clone()).ToList();
                foreach (var g in groups)
                {
                    var excludedCoursesIds = assignedClasses.Where(c => c.Group.Id == g.Id).Select(c => c.Course.Id).ToList();
                    foreach (var excludedId in excludedCoursesIds)
                    {
                        var courseObj = g.PossibleCourses.First(x => x.Id == excludedId);
                        g.PossibleCourses.Remove(courseObj);
                    }
                }
                if (AllGroupsOutOfCourses(groups)) return assignedClasses;

                time = Data.Times[counter / Data.Groups.Count ];
            }
            counter++;


            var clas = new Class();
            clas.Time = time;
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
            if (clas.Group.PossibleCourses.Count == 0) return null;
            var courseDict = new Dictionary<int, int>();
            var leftCourses = Data.Groups.Select(g => g.Clone()).ToList();
            foreach( var leftCourse in leftCourses)
            {
                var usedCourses = assignedClasses.Where(c=>c.Group.Id == leftCourse.Id).ToList();
                foreach ( var usedCourse in usedCourses)
                {
                    var thisCourse = leftCourse.PossibleCourses.First(c=>c.Id == usedCourse.Course.Id);
                    leftCourse.PossibleCourses.Remove(thisCourse);
                }
            }
            foreach (var course in clas.Group.PossibleCourses.Where(c=>c.Teachers.Count()>0))
            {
                courseDict.Add(
                                    course.Id,
                                    leftCourses.SelectMany(x => x.PossibleCourses)
                                               .Where(t => t.Id == course.Id)
                                               .Count()
                                );
            }
            var maxCourseId = courseDict.OrderByDescending(x => x.Value).First(x => x.Value > 0).Key;
            clas.Course = clas.Group.PossibleCourses.First(x=>x.Id == maxCourseId);
            var teacherDict = new Dictionary<int, int>();
            foreach (var teacher in clas.Course.Teachers)
            {
                teacherDict.Add(
                                    teacher.Id,
                                    groups.SelectMany(x => x.PossibleCourses)
                                            .SelectMany(x => x.Teachers)
                                            .Where(t => t.Id == teacher.Id)
                                            .Count()
                                );
            }
            try
            {
                var minTeacher = teacherDict.OrderBy(x => x.Value).First(x=>x.Value>0);
                var minTeacherId = minTeacher.Key;
                clas.Teacher = clas.Course.Teachers.First(t => t.Id == minTeacherId);
                assignedClasses.Add(clas);

                var newGroups = groups.Select(g => g.Clone()).Where(g => g.Id != minGroupId).ToList();
                for (var i = 0; i < newGroups.Count(); i++)
                {
                    for (var j = 0; j < newGroups[i].PossibleCourses.Count(); j++)
                    {
                        newGroups[i].PossibleCourses[j].Teachers.Remove(clas.Teacher);
                    }
                }
                var newClasses = GetClasses(newGroups, assignedClasses.Select(c=>c.Clone()).ToList(), counter, time);
                if (newClasses == null)
                {
                    return null;
                }

                    
                return newClasses;
            }
            catch(Exception)
            {
                return null;
            }
        }

        private bool AllGroupsOutOfCourses(List<Group> groups)
        {
            foreach(var group in groups)
            {
                if (group.PossibleCourses.Any()) return false;
            }
            return true;
        }
    }
}
