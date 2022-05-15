using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sharp_Business.Repository.IRepository;
using Sharp_DataAccess;
using Sharp_DataAccess.Data;
using Sharp_Models;

namespace Sharp_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CategoryDto Create(CategoryDto objDTO)
        {
            var obj = _mapper.Map<CategoryDto, Category>(objDTO);
            obj.CreatedDate = DateTime.Now;
            var addedObj = _context.Add(obj);
            _context.SaveChanges();
            return _mapper.Map<Category, CategoryDto>(addedObj.Entity);
        }

        public int Delete(int id)
        {
            var obj = _context.Categories.FirstOrDefault(c => c.Id==id);
            if(obj != null)
            {
                _context.Categories.Remove(obj);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_context.Categories);
        }

        public CategoryDto GetById(int id)
        {
            var obj = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                _mapper.Map<Category, CategoryDto>(obj);
            }
            return new CategoryDto();
        }

        public CategoryDto Update(CategoryDto objDTO)
        {
            var objFromDb = _context.Categories.FirstOrDefault(c => c.Id == objDTO.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                _context.Categories.Update(objFromDb);
                _context.SaveChanges();
                return _mapper.Map<Category, CategoryDto>(objFromDb);
            }
            return objDTO;
        }
    }
}
