using System.Collections.Generic;

namespace Asp.NetCoreMVC.Models
{
	public interface ICategoryRepository
	{
		public IEnumerable<Category> AllCategories();

		public Category GetCategoryById(int id);
	}
}