﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ProductModel
{
    public class UpdateProduct
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
