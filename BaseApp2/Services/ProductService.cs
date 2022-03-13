﻿using DataAccess;
using DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApp2.Services
{
    public class ProductService : IProductService
    {
        private readonly IDalService DalService;
        public ProductService(IDalService dalService)
        {
            DalService = dalService;
        }

        public async Task<List<Product>> FindAll(string filter, int pIdx, int pAmt, long categoryId = 0)
        {
            try
            {
                string _QR = @"select * from Products where Products.Deleted = 0";

                List<SqlParameter> Parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(filter))
                {
                    _QR += " and (Products.Name like @Filter)";
                    Parameters.Add(new SqlParameter("@Filter", string.Format("%{0}%", filter)));
                }
                if (categoryId > 0)
                {
                    _QR += " and Products.CategoryId = @categoryId";
                    Parameters.Add(new SqlParameter("@categoryId", categoryId));
                }

                _QR += " order by Products.Name asc";

                _QR += " OFFSET @pagSkip ROWS FETCH NEXT @pagAmt ROWS ONLY";
                Parameters.Add(new SqlParameter("@pagSkip", pIdx * pAmt));
                Parameters.Add(new SqlParameter("@pagAmt", pAmt));

                return DalService.DBContext.Products.FromSqlRaw(_QR, Parameters.ToArray()).ToList();
            }
            catch (Exception ex)
            { }

            return null;
        }

        public async Task<List<Product>> GetAll()
        {
            return DalService.GetAll<Product>();
        }

        public async Task<Product> Get(long Id)
        {
            var result = DalService.Get<Product>(Id);

            return result;
        }

        public async Task<Product> Save(Product data)
        {
            return DalService.Save(data);
        }

        public async Task<Product> Delete(long Id)
        {
            return DalService.Delete<Product>(Id);
        }

        public async Task<int> Count()
        {
            return DalService.Count<Product>();
        }
    }
}

