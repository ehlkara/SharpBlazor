using Sharp_Models;

namespace Sharp_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDto> Create(CategoryDto objDTO);
        public Task<CategoryDto> Update(CategoryDto objDTO);
        public Task<int> Delete(int id);
        public Task<CategoryDto> Get(int id);
        public Task<IEnumerable<CategoryDto>> GetAll();
    }
}
