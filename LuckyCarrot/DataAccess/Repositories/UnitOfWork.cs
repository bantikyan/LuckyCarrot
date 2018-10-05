using AutoMapper;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DbContextOptions<AppDbContext> options, IMapper mapper)
        {
            _context = new AppDbContext(options);
            _mapper = mapper;
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_context, _mapper);
                }
                return userRepository;
            }
        }

        private IPointRepository pointRepository;
        public IPointRepository PointRepository
        {
            get
            {
                if (pointRepository == null)
                {
                    pointRepository = new PointRepository(_context, _mapper);
                }
                return pointRepository;
            }
        }

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
