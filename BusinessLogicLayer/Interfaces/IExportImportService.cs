using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IExportImportService
    {
        public Task<byte[]> Export();
    }
}
