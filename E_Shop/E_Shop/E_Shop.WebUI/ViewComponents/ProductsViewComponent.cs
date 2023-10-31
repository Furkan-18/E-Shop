﻿using E_Shop.Business.Services;
using E_Shop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.WebUI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {

        private readonly IProductService _productService;
        public ProductsViewComponent(IProductService productService)
        {
                _productService = productService;
        }

        public IViewComponentResult Invoke(int? categoryId = null)
        {
            var productDtos = _productService.GetProductsByCategoryId(categoryId);

            var viewModel = productDtos.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryName = x.CategoryName,
                CategoryId = x.CategoryId,
                ImagePath = x.ImagePath,



            }).ToList();
            return View(viewModel);
        }
    }
}
