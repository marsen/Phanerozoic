using System.IO;

namespace Phanerozoic.Core.Helpers
{
    public class FileHelper : IFileHelper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}