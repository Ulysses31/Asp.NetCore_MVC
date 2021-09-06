using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCoreMVC.Models
{
	public class MockCategoryRepository : ICategoryRepository
	{
		private IEnumerable<Category> categories;

		public MockCategoryRepository()
		{
			categories = new List<Category>() {
				new Category() {
					CategoryId = 1,
					CategoryName = "Fruit pies",
					Description = "All-fruity pies"
				},
				new Category() {
					CategoryId = 2,
					CategoryName = "Cheese cakes",
					Description = "Cheesy all the way"
				},
				new Category() {
					CategoryId = 3,
					CategoryName = "Seasonal pies",
					Description = "Get in the mood for a seasonal pie"
				}
			};
		}

		public IEnumerable<Category> AllCategories()
		{
			return categories;
		}

		public Category GetCategoryById(int id)
		{
			return categories.FirstOrDefault(c => c.CategoryId == id);
		}
	}
}