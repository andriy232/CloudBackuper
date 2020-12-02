using System.IO;
using System.Threading.Tasks;

namespace DataGuardian.Server.Plugins
{
    public interface IStorageManager
    {
        Task SaveFileToFileSystem(string fileName, Stream stream);
    }
}