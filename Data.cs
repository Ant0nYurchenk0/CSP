namespace CSP
{
    internal struct Data
    {
        public Data()
        {
            Times = new();
            Groups = new();
            Courses = new();
            Teachers = new();
            Population = new();

            createTimes();
            createTeachers();
            createCourses();
            createGroups();
        }
        internal List<Time> Times { get; private set; }
        internal List<Teacher> Teachers { get; private set; }
        internal List<Group> Groups { get; private set; }
        internal List<Course> Courses { get; private set; }
        internal List<Schedule> Population { get; set; }
        private void createTimes()
        {
            Times.Add(new Time("09.00-10.30"));
            Times.Add(new Time("10.30-12.00"));
            Times.Add(new Time("12.00-13.30"));
            Times.Add(new Time("13.30-15.00"));
        }
        private void createGroups()
        {
            Groups.Add(new Group("K-14", Courses.Select(c=>c.Clone()).ToList()));
            Groups.Add(new Group("K-24", Courses.Select(c=>c.Clone()).ToList()));
            Groups.Add(new Group("MI-3", Courses.Select(c=>c.Clone()).ToList()));
            Groups.Add(new Group("MI-4", Courses.Select(c=>c.Clone()).ToList()));
        }
        private void createTeachers()
        {
            Teachers.Add(new Teacher("Molodtsov"));
            Teachers.Add(new Teacher("Rubliov"));
            Teachers.Add(new Teacher("Stavrovskyi"));
            Teachers.Add(new Teacher("Rabanovych"));
            Teachers.Add(new Teacher("Rybalko"));
            Teachers.Add(new Teacher("Yakymiv"));
            Teachers.Add(new Teacher("Marynych"));
            Teachers.Add(new Teacher("Koval"));
        }
        private void createCourses()
        {
            Courses.Add(new Course("Mat. analysis", Teachers.Where(t => t.Name == "Molodtsov" || t.Name == "Rubliov").ToList()));
            Courses.Add(new Course("Operations Research", Teachers.Where(t => t.Name == "Yakymiv" || t.Name == "Marynych").ToList()));
            Courses.Add(new Course("Linear algebra", Teachers.Where(t => t.Name == "Rabanovych" || t.Name == "Yakymiv" || t.Name == "Rybalko").ToList()));
            Courses.Add(new Course("Programming", Teachers.Where(t => t.Name == "Koval" || t.Name == "Stavrovskyi").ToList()));
        }
    }
}