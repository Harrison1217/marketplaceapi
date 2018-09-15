﻿using Marketplace.Data;
using Marketplace.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class ProductService
    {
        private readonly Guid _userId;
        private readonly int _productID;

        public ProductService(Guid userid)
        {
            _userId = userid;
        }

        public ProductDetails GetProductbyId(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductId == productId && e.OwnerId == _userId);
                return
                    new ProductDetails
                    {
                        ProductId = entity.ProductId,
                        ProductName = entity.ProductName,
                        ProductDescription = entity.ProductDescription,
                        ProductPrice = entity.ProductPrice,
                        ProductImage = entity.ProductImage,
                        ProductCategory = entity.ProductCategory,
                        ProductUpc = entity.ProductUpc,
                        RetailerId = entity.RetailerId,
                        OwnerId = entity.OwnerId,
                    };
            }
        }

        public bool CreateProduct(ProductCreate model)
        {
            var entity =
                new Product
                {
                    ProductId = _productID,
                    ProductName = model.ProductName,
                    ProductCategory = model.ProductCategory,
                    ProductCost = model.ProductCost,
                    ProductDescription = model.ProductDescription,
                    ProductImage = model.ProductImage,
                    ProductOnSale = model.ProductOnSale,
                    ProductPrice = model.ProductPrice,
                    ProductProfit = model.ProductProfit,
                    ProductQuantity = model.ProductQuantity,
                    ProductUpc = model.ProductUpc,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Products.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public ICollection<ProductListItem> GetAllProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var products =
                ctx
                    .Products
                    .Select(
                    e => new ProductListItem()
                    {
                        ProductId = e.ProductId,
                        ProductCategory = e.ProductCategory,
                        ProductDescription = e.ProductDescription,
                        ProductImage = e.ProductImage,
                        ProductName = e.ProductName,
                        ProductOnSale = e.ProductOnSale,
                        ProductPrice = e.ProductPrice,
                        ProductQuantity = e.ProductQuantity,
                        ProductUpc = e.ProductUpc,
                    });
                return products.ToList();
            }
        }

        public bool UpdateProduct(ProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductId == model.ProductId && e.OwnerId == _userId);

                entity.ProductCategory = model.ProductCategory;
                entity.ProductCost = model.ProductCost;
                entity.ProductDescription = model.ProductDescription;
                entity.ProductId = model.ProductId;
                entity.ProductImage = model.ProductImage;
                entity.ProductName = model.ProductName;
                entity.ProductOnSale = model.ProductOnSale;
                entity.ProductPrice = model.ProductPrice;
                entity.ProductProfit = model.ProductProfit;
                entity.ProductQuantity = model.ProductQuantity;
                entity.ProductUpc = model.ProductUpc;
                entity.ProductUpc = model.ProductUpc;
                entity.RetailerId = model.RetailerId;
                entity.OwnerId = model.OwnerId;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductId == productId && e.OwnerId == _userId);

                ctx.Products.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}