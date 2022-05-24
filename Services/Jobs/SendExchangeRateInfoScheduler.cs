using Quartz;
using Quartz.Impl;

namespace Services.Jobs
{
    public class SendExchangeRateInfoScheduler
    {
        public static async Task Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = (JobFactory)serviceProvider.GetService(typeof(JobFactory));
            await scheduler.Start();
            
            IJobDetail jobDetail = JobBuilder.Create<SendExchangeRateInfoJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("SendExchangeRateInfoJob", "default")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(1)
                    .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
