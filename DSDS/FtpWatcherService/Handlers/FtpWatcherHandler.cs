using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Camunda.Worker;

namespace FtpWatcherService.Handlers
{
    [HandlerTopics("FtpWatcher")]
    public class FtpWatcherHandler : ExternalTaskHandler
    {
        private bool _updateStatus = false;

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            using (var watcher = new FileSystemWatcher())
            {
                watcher.Path = "path";
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName
                                       | NotifyFilters.DirectoryName;

                watcher.Filter = "*.txt";

                watcher.Created += OnCreate;

                watcher.EnableRaisingEvents = true;


                while (!_updateStatus)
                {

                }

                return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
                {
                    ["batFile"] = Variable.Bytes(File.ReadAllBytes("path"))
                }));
            }
        }

        private void OnCreate(object source, EventArgs e)
        {
            _updateStatus = true;
        }
    }
}