using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyPlayListApp.Data.Context;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly MyPlayListAppContext _context;

        public CategoryRepository(MyPlayListAppContext context) : base(context)
        {
            _context = context;
        }

        public ResultBase AddNewCategory(Category category)
        {
            var result = new ResultBase();
            try
            {
                var addedcategory = Add(category);
                if (addedcategory != null)
                {
                    result.Success = true;
                    result.Message = "Category Added Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed To Add Category: {ex.Message}";
            }
            return result;
        }

        public ResultBase DeleteCategory(Guid categoryId)
        {
            var result = new ResultBase();
            try
            {
                var isDelelted = Delete(categoryId);
                if (isDelelted)
                {
                    result.Success = true;
                    result.Message = "Category Deleted Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to Delete Category: {ex.Message}";
            }
            return result;
        }

        public CategoryItemResult GetCategories(CategoriesFilter filter)
        {
            var result = new CategoryItemResult();
            var categories = _context.Categories.Select(c => new
            {
                c.Id,
                c.Name
            });
            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                categories = categories.Where(c => c.Name.Contains(filter.CategoryName));
            }

            categories = categories.OrderBy(s => s.Name);
            result.TotalCount = categories.Count();
            result.TotalPages = (int)Math.Ceiling((double)result.TotalCount / (double)filter.PageSize);
            categories = categories.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);
            result.Categories = categories.Select(c => new CategoryDetailes()
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
            }).ToList();

            return result;
        }

        public bool IsCategoryExists(Guid categoryId)
        {
            var isExists = Any(s => s.Id == categoryId);
            return isExists;
        }

        public ResultBase UpdateCategory(Category category)
        {
            var result = new ResultBase();
            try
            {
                var updatedCategory = Update(category);
                if (updatedCategory != null)
                {
                    result.Success = true;
                    result.Message = "Category Updated Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to Update Category: {ex.Message}";
            }
            return result;
        }
        public bool HasSongs(Guid categoryId)
        {
            var hasSongs = Any(c => c.Id == categoryId && c.Songs.Any(s => s.CategoryId == categoryId));
            return hasSongs;
        }
    }
}
