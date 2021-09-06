using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCoreMVC.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext _dbContext;

		public CategoryRepository(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public IEnumerable<Category> AllCategories()
		{
			return _dbContext.Categories;
		}

		public Category GetCategoryById(int id)
		{
			return _dbContext.Categories
				.FirstOrDefault(c => c.CategoryId == id);
		}
	}
}