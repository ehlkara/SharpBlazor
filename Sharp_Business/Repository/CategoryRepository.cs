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

        public async Task<CategoryDto> Create(CategoryDto objDTO)
        {
            var obj = _mapper.Map<CategoryDto, Category>(objDTO);
            obj.CreatedDate = DateTime.Now;
            var addedObj = _context.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(c => c.Id==id);
            if(obj != null)
            {
                _context.Categories.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_context.Categories);
        }

        public async Task<CategoryDto> Get(int id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (obj == null)
            {
                return null;
            }
            return _mapper.Map<CategoryDto>(obj);
        }

        public async Task<CategoryDto> Update(CategoryDto objDTO)
        {
            var objFromDb = await _context.Categories.FirstOrDefaultAsync(c => c.Id == objDTO.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                _context.Categories.Update(objFromDb);
                await _context.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDto>(objFromDb);
            }
            return objDTO;
        }
    }
}
