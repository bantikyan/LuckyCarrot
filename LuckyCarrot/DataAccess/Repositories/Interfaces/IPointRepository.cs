using DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPointRepository
    {
        Task<List<PointTransferModel>> GetTransfers(int companyId);
        Task<List<ReasonModel>> GetReasons(int companyId);
    }
}
