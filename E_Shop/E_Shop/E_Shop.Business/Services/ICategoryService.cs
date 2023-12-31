﻿using E_Shop.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Services
{
     public interface ICategoryService
    {

        bool AddCategory(AddCategoryDto addCategoryDto);

        List<ListCategoryDto> GetCategories();

        EditCategoryDto GetCategoryById(int id);

        void EditCategory(EditCategoryDto editCategoryDto);

        void DeleteCategory(int id);




    }
}
