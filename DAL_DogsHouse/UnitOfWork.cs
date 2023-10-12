﻿using DAL_DogsHouse.Interfaces;
using DAL_DogsHouse.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DogsHouse
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DogsHouseDbContext _context;
        private DogRepository dogRepository;

        public UnitOfWork(DogsHouseDbContext context)
        {
            _context = context;
        }

        public IDogRepository DogRepository 
        {
            get
            {
                if (dogRepository == null)
                    dogRepository = new DogRepository(_context);
                return dogRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
