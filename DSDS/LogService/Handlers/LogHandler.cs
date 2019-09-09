using System.Threading.Tasks;
using Camunda.Worker;

namespace LogService.Handlers
{
    [HandlerTopics("LogService")]
    public class LogHandler : ExternalTaskHandler
    {
        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            throw new System.NotImplementedException();
        }
    }
}