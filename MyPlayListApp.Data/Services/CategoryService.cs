using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return _categoryRepository.DeleteCategory(categoryId);
        }

        public CategoryItemResult GetCategories(CategoriesFilter filter)
        {
            return _categoryRepository.GetCategories(filter);
        }

        public ResultBase UpdateCategory(CategoryDetailes category)
        {
            var updatedCategory = new Category
            {
                Id = category.CategoryId,
                Name = category.CategoryName
            };
            return _categoryRepository.UpdateCategory(updatedCategory);
        }
        public List<Category> GetCategoriesList()
        {
            return _categoryRepository.GetAll();
        }
    }
}
