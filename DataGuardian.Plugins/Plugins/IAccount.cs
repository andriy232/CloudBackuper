using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;

namespace DataGuardian.Plugins.Plugins
{
    public interface IAccount: IDbModel
    {
        ICloudStorageProvider CloudStorageProvider { get; }

        string Name { get; }
        string ConnectionSettings { get; }
    }

    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}