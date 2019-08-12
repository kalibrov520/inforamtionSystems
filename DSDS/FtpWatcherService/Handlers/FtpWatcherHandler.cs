using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Camunda.Worker;
using FileLoader.FileSystem;
using FileLoader.FTP;
using File = System.IO.File;

namespace FtpWatcherService.Handlers
{
    [HandlerTopics("FtpWatcher")]
    public class FtpWatcherHandler : ExternalTaskHandler
    {
        private bool _updateStatus = false;

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            var timer = new System.Timers.Timer(10000); // every 10 seconds

            var fileLoader = new FtpFileLoader("ftp://spb-mdspoc01.internal.corp", "ftpUser", "password123");

            var patternsForExtension = new List<string>
            {
                "*.txt"
            };

            var filesOnFtp = fileLoader.GetFilesWithFileExtensionPattern(patternsForExtension);

            timer.Elapsed += (source, e) =>
            {
                var newFiles = fileLoader.GetFilesWithFileExtensionPattern(patternsForExtension).ToList();

                if (!filesOnFtp.SequenceEqual(newFiles))
                {
                    _updateStatus = true;
                }
            };

            timer.Enabled = true;

            while (!_updateStatus)
            {

            }

            timer.Enabled = false;

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
            {
                ["batFile"] = Variable.Bytes(File.ReadAllBytes("C:\\Users\\ikalibrov\\Desktop\\Autoload_Process.bat"))
            }));
        }
    }
}