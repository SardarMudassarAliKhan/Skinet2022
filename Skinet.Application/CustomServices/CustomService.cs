using Skinet.Application.ICustomServices;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Application.CustomServices
{
    public class CustomService : ICustomService<Products>
    {
        public void Delete(Products entity)
        {
            try
            {
              

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Products> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Products entity)
        {
            throw new NotImplementedException();
        }

        public Task<Products> Update(Products entity)
        {
            throw new NotImplementedException();
        }
    }
}
