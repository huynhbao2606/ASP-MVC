﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models
{
    public class BasketItem
    {
        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
