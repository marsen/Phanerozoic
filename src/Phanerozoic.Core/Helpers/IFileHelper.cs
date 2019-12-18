namespace Phanerozoic.Core.Helpers
{
    public interface IFileHelper
    {
        bool Exists(string path);

        string ReadAllText(string path);
    }
}