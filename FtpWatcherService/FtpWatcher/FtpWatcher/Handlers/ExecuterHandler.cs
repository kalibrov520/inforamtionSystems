using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Camunda.Worker;

namespace FtpWatcher.Handlers
{
    [HandlerTopics("Executer")]
    public class ExecuterHandler : ExternalTaskHandler
    {
        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                var fs = new FileStream("path", FileMode.Create, FileAccess.Write);
                fs.Write(externalTask.Variables["batFile"].AsBytes());
            }
            catch (Exception ex)
            {
                // ignored
            }

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}