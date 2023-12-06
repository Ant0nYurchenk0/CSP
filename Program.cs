namespace CSP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new Data();
            var CSP = new CSP(data);
            var schedule = CSP.GetSchedule();
            ScheduleWriter.Write(schedule);
        }
    }
}