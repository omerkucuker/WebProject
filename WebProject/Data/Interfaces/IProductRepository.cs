﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Data.interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; set; }
       // IEnumerable<Product> 

        //Product GetProduct
    }
}
