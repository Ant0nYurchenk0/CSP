namespace CSP
{
    internal class ScheduleWriter
    {
        internal static void Write(Schedule goodSchedule)
        {
            Console.WriteLine();
            var groups = goodSchedule.Classes.Select(c => c.Group).Distinct();
            foreach (var group in groups)
            {
                Console.WriteLine("=========================================================================");
                Console.WriteLine($"{group.Name}");
                var groupClasses = goodSchedule.Classes.Where(c => c.Group.Id == group.Id).OrderBy(c => c.Time.Id);
                foreach (var groupClass in groupClasses)
                {
                    Console.WriteLine($"{groupClass.Time.TimeStr} - {groupClass.Course.Name} ({groupClass.Teacher.Name})");
                }
            }

        }
    }
}