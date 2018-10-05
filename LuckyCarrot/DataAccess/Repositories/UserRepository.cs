using AutoMapper;
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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> Get(int companyId)
        {
            var items = await (from p in _context.Users
                               where p.CompanyId == companyId
                               select p).ProjectTo<UserModel>(_mapper.ConfigurationProvider).ToListAsync();

            return items;
        }
    }
}
