using System;
using System.Collections.Generic;
using System.Text;

namespace FileChecker
{
    public interface IFileChecker
    {
        bool FileChanged();
    }
}
