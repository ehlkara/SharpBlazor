using Sharp_Models;

namespace Sharp_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public CategoryDto Create(CategoryDto objDTO);
        public CategoryDto Update(CategoryDto objDTO);
        public int Delete(int id);
        public CategoryDto GetById(int id);
        public IEnumerable<CategoryDto> GetAll();
    }
}
