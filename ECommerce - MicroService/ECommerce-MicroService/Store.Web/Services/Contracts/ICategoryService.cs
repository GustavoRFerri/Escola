using Store.Web.Models;

namespace Store.Web.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();


    }
}
