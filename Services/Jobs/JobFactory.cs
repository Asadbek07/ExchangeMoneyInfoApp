using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Services.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) =>
            _serviceProvider.GetRequiredService<JobRunner>();

        public void ReturnJob(IJob job)
        {
        }
    }
}
