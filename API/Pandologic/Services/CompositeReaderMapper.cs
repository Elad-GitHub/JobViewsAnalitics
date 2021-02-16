using System.Data.Common;
using System.Linq;

namespace Services
{
    public class CompositeReaderMapper
    {
        private readonly IDataReaderMapper[] m_readers;

        public CompositeReaderMapper(params IDataReaderMapper[] readers)
        {
            m_readers = readers;
        }

        public void Read(DbDataReader reader)
        {
            m_readers.First().Read(reader);
            var currentReader = 1;

            while (currentReader < m_readers.Length &&
                   reader.NextResult())
            {
                m_readers[currentReader].Read(reader);
                currentReader++;
            }
        }
    }
}
