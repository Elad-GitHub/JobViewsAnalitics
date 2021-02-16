using System.Data.Common;

namespace Services
{
    public interface IDataReaderMapper
    {
        void Read(DbDataReader reader);
    }
}
