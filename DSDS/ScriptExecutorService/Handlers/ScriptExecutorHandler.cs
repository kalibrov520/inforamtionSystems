﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Camunda.Worker;
using System.Diagnostics;

namespace ScriptExecutorService.Handlers
{
    [HandlerTopics("ScriptExecutor")]
    public class ScriptExecutorHandler : ExternalTaskHandler
    {
        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            foreach (var (variableKey, variableValue) in externalTask.Variables)
            {
                using (var fileWriter = new StreamWriter(".\\" + variableKey + ".bat"))
                {
                    fileWriter.Write(System.Text.Encoding.Default.GetString(variableValue.AsBytes()));
                }

                System.Diagnostics.Process.Start(variableKey + ".bat");
            }

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}