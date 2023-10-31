﻿using E_Shop.Business.Dtos;
using E_Shop.Business.Services;
using E_Shop.Data.Entities;
using E_Shop.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Managers
{
    public class ProductManager : IProductService
    {

        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<CategoryEntity> _categoryRepository;
        public ProductManager(IRepository<ProductEntity>productRepository, IRepository<CategoryEntity>categoryRepository)
        {
            
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public bool AddProduct(AddProductDto addProductDto)
        {
            var hasProduct = _productRepository.GetAll(X => X.Name.ToLower() == addProductDto.Name.ToLower()).ToList();
        
            if (hasProduct.Any())
            {
                return false;
            }
            var productEntity = new ProductEntity()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                UnitInStock = addProductDto.UnitInStock,
                UnitPrice = addProductDto.UnitPrice,
                CategoryId = addProductDto.CategoryId,
                ImagePath = addProductDto.ImagePath

            };
        _productRepository.Add(productEntity);
            return true;

        
        
        }

        public void DeleteProduct(int id)
        {
           _productRepository.Delete(id);
        }

        public void EditProduct(EditProductDto editProductDto)
        {
            var productEntity = _productRepository.GetById(editProductDto.Id);
            productEntity.Name = editProductDto.Name;
       productEntity.Description = editProductDto.Description;
            productEntity.UnitPrice = editProductDto.UnitPrice;
            productEntity.UnitInStock = editProductDto.UnitInStock;
            productEntity.CategoryId = editProductDto.CategoryId;
        
        if (editProductDto.ImagePath is not null) {

                productEntity.ImagePath = editProductDto.ImagePath;
            
            
            }

            _productRepository.Update(productEntity);
        }

        public EditProductDto GetProductById(int id)
        {
            var productEntity =_productRepository.GetById(id);
            var editProductDto = new EditProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
            
                CategoryId = productEntity.CategoryId,
                ImagePath = productEntity.ImagePath
            
            
            };
        return editProductDto;
        
        
        }

        public ProductDetailDto GetProductDetailById(int id)
        {
            var productEntity = _productRepository.GetById(id);
            var productDetailDto = new ProductDetailDto()
            {
                ProductId = productEntity.Id,
                ProductName = productEntity.Name,
                Description = productEntity.Description,
                UnitPrice = productEntity.UnitPrice,
               UnitInStock = productEntity.UnitInStock,
               ImagePath = productEntity.ImagePath,CategoryId = productEntity.CategoryId,
               CategoryName = _categoryRepository.GetById(productEntity.CategoryId).Name
            
            };
        
        return productDetailDto;


        
        }

        public List<ListProductDto> GetProducts()
        {
           var productEntities = _productRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name);
            var productDtoList = productEntities.Select(x => new ListProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath,

                
            }).ToList();
        
        
            return productDtoList;
        
        
        
        }

        public List<ListProductDto> GetProductsByCategoryId(int? categoryId)
        {
            if(categoryId.HasValue)//is not null

            {
                var productsEntities = _productRepository.GetAll(x => x.CategoryId ==categoryId).OrderBy(x => x.Name);

                var productDtos = productsEntities.Select(x => new ListProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitInStock = x.UnitInStock,
                    UnitPrice = x.UnitPrice,
                    CategoryId = x.CategoryId,
                    CategoryName=x.Category.Name,
                    ImagePath = x.ImagePath,



                }).ToList();

                return productDtos;
            

            
            }
            return GetProducts();        

                    }
    }
}
