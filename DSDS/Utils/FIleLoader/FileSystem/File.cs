using System;
using System.Numerics;

namespace FileLoader.FileSystem
{
    public class File : IFileSystemItem, IEquatable<File>
    {
        public double Size { get; set; }

        public string FullPath { get; set; }

        public string Name { get; set; }

        public DateTime LastModified { get; set; }
        public string Extension { get; set; }

        public bool Equals(File other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Size.Equals(other.Size) && string.Equals(FullPath, other.FullPath) && string.Equals(Name, other.Name) && LastModified.Equals(other.LastModified) && string.Equals(Extension, other.Extension);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((File) obj);
        }
    }
}