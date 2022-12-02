﻿using EcommerceApi.Domain.Entities.Common.Concrete;

namespace EcommerceApi.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }

    public ICollection<Order> Orders { get; set; }
}