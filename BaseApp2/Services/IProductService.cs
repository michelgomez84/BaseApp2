﻿using DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseApp2.Services
{
    public interface IProductService
    {
        Task<List<Product>> FindAll(string filter, int pIdx, int pAmt, long categoryId=0);
        Task<List<Product>> GetAll();
        Task<Product> Get(long Id);
        Task<Product> Save(Product data);
        Task<Product> Delete(long Id);
        Task<int> Count();
    }
}
