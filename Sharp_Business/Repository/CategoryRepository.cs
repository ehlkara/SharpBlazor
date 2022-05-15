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
            var mappedDto = _mapper.Map<Category>(objDTO);
            mappedDto.CreatedDate = DateTime.Now;
            var categoryResult = await _context.AddAsync(mappedDto);
            _mapper.Map<CategoryDto>(categoryResult);
            return objDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _context.Categories.Remove(obj);
                await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_context.Categories);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _mapper.Map<Category, CategoryDto>(obj);
            }
            return new CategoryDto();
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
