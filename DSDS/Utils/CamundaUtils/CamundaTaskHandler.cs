using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camunda.Worker;

namespace CamundaUtils
{
    public abstract class CamundaTaskHandler : IExternalTaskHandler
    {
        public ExternalTask ExternalTask { get; private set; }

        public Guid RunId { get; set; }

        public Guid DataFeedId { get; private set; }

        public virtual async Task HandleAsync(IExternalTaskContext context)
        {
            IExecutionResult executionResult;
            try
            {
                ParseDefaultContext(context.Task);
                ParseContext(context.Task);
                await Process();
                try
                {
                    executionResult = await this.GetExecutionResult();
                }
                catch (Exception e)
                {
                    executionResult = new BpmnErrorResult("11", e.ToString());
                }
                
            }
            catch (Exception ex)
            {
                executionResult = (IExecutionResult)new FailureResult(ex);
            }
            await executionResult.ExecuteResultAsync(context);
        }

        private void ParseDefaultContext(ExternalTask externalTask)
        {
            ExternalTask = externalTask;
            DataFeedId = Guid.Parse(externalTask.ProcessDefinitionId.Split(':').Last());
            if (externalTask.Variables.ContainsKey("runId"))
            {
                RunId = Guid.Parse(externalTask.Variables["runId"].AsString());
            }
        }

        public abstract void ParseContext(ExternalTask externalTask);

        public abstract Task Process();


        public abstract Task<IExecutionResult> GetExecutionResult();
    }
}
