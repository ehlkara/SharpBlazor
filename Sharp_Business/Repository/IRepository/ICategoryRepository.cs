using Sharp_Models;

namespace Sharp_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> Create(CategoryDto objDTO);
        Task<CategoryDto> Update(CategoryDto objDTO);
        Task<bool> Delete(int id);
        Task<CategoryDto> GetById(int id);
        Task<IEnumerable<CategoryDto>> GetAll();
    }
}
