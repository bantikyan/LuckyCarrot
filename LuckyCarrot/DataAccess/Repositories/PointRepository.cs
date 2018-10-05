using AutoMapper;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PointRepository : IPointRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PointRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PointTransferModel>> GetTransfers(int companyId)
        {
            var items = await (from p in _context.PointTransfers
                               where p.CompanyId == companyId
                               orderby p.CreateDate descending
                               select p).ProjectTo<PointTransferModel>(_mapper.ConfigurationProvider).ToListAsync();

            return items;
        }

        public async Task<List<ReasonModel>> GetReasons(int companyId)
        {
            var items = await (from p in _context.Reasons
                               where p.CompanyId == companyId
                               select p).ProjectTo<ReasonModel>(_mapper.ConfigurationProvider).ToListAsync();

            return items;
        }

        public async Task Give(PointTransferModel model)
        {
            var item = new PointTransfer();
            _mapper.Map(model, item);
            _context.PointTransfers.Add(item);

            var fromUser = await _context.Users.SingleOrDefaultAsync(p => p.Id == model.FromUserId);
            fromUser.Points -= model.Points;

            var toUser = await _context.Users.SingleOrDefaultAsync(p => p.Id == model.ToUserId);
            toUser.ReceivedPoints += model.Points;

            await _context.SaveChangesAsync();
        }
    }
}
