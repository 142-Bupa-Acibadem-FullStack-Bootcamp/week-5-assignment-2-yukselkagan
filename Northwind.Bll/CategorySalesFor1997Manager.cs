using Northwind.Bll.Base;
using Northwind.Dal.Abstract;
using Northwind.Entity.Dto;
using Northwind.Entity.Models;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.Bll
{
    public class CategorySalesFor1997Manager : BllBase<CategorySalesFor1997, DtoCategorySalesFor1997>, ICategorySalesFor1997Service
    {
        private readonly ICategorySalesFor1997Repository categorySalesFor1997Repository;

        public CategorySalesFor1997Manager(IServiceProvider service) : base(service)
        {
            categorySalesFor1997Repository = service.GetService<ICategorySalesFor1997Repository>();
        }

        public IQueryable CategorySalesFor1997Report()
        {
            return categorySalesFor1997Repository.CategorySalesFor1997Report();
        }


    }
}
