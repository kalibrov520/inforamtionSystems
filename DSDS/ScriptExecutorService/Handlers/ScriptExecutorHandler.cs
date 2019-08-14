using System.Threading.Tasks;
using Camunda.Worker;

namespace ScriptExecutorService.Handlers
{
    [HandlerTopics("ScriptExecutor")]
    public class ScriptExecutorHandler : ExternalTaskHandler
    {
        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            throw new System.NotImplementedException();
        }
    }
}