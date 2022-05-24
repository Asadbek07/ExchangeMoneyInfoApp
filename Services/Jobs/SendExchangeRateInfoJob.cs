using Quartz;

namespace Services.Jobs
{
    public class SendExchangeRateInfoJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Job is executed!");
        }
    }
}
