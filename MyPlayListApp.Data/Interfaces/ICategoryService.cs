﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Interfaces
{
    public interface ICategoryService
    {
        CategoryItemResult GetCategories(CategoriesFilter filter);
        ResultBase AddNewCategory(CategoryDTO category);
        ResultBase UpdateCategory(CategoryDTO category);
        ResultBase DeleteCategory(Guid categoryId);
        List<Category> GetCategoriesList();
    }
}
