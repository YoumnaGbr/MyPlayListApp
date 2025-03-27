using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ResultBase AddNewCategory(CategoryDetailes category)
        {
            var addedCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = category.CategoryName
            };
            return _categoryRepository.AddNewCategory(addedCategory);
        }

        public ResultBase DeleteCategory(Guid categoryId)
        {
            var result = new ResultBase();
            var isExists = _categoryRepository.IsCategoryExists(categoryId);
            var hasSongs = _categoryRepository.HasSongs(categoryId);
            if (!isExists)
            {
                result.Success = false;
                result.Message = "Category Not Found.";
                return result;
            }
            if (hasSongs)
            {
                result.Success = false;
                result.Message = "Cannot Delele Category Has Songs";
                return result;
            }
            result = _categoryRepository.DeleteCategory(categoryId);
            return result;
        }

        public CategoryItemResult GetCategories(CategoriesFilter filter)
        {
            return _categoryRepository.GetCategories(filter);
        }

        public ResultBase UpdateCategory(CategoryDetailes category)
        {
            var result = new ResultBase();

            var isExists = _categoryRepository.IsCategoryExists(category.CategoryId);
            if (!isExists)
            {
                result.Success = false;
                result.Message = "Category Not Found.";
                return result;
            }
            var updatedCategory = new Category
            {
                Id = category.CategoryId,
                Name = category.CategoryName
            };
            result = _categoryRepository.UpdateCategory(updatedCategory);
            return result;
        }
        public List<Category> GetCategoriesList()
        {
            return _categoryRepository.GetAll();
        }
    }
}
