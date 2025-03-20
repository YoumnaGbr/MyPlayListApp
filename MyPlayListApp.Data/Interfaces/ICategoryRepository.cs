using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        CategoryItemResult GetCategories(CategoriesFilter filter);
        ResultBase AddNewCategory(Category category);
        ResultBase UpdateCategory(Category category);
        ResultBase DeleteCategory(Guid categoryId);
        bool IsCategoryExists(Guid categoryId);
    }
}
