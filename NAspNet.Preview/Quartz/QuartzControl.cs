using Quartz;
using Quartz.Impl;

namespace NAspNet.Quartz
{
    public class QuartzControl
    {
        private readonly IScheduler Scheduler;

        public QuartzControl()
        {
            Scheduler = new StdSchedulerFactory().GetScheduler().Result;
            Scheduler.Start();
        }

        public void Start() => Scheduler.Start();
        public void Shutdown() => Scheduler.Shutdown();

        public void AddJob<TJob>(string cron) where TJob : IJob
        {
            var jobDetail = JobBuilder.Create<TJob>().Build();
            var trigger = TriggerBuilder.Create().StartNow().WithCronSchedule(cron).Build();
            Scheduler.ScheduleJob(jobDetail, trigger).Wait();
        }

    }
}
