using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sharp_Business.Repository.IRepository;
using Sharp_DataAccess;
using Sharp_DataAccess.Data;
using Sharp_Models;

namespace Sharp_Business.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public ProductRepository(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductDto> Create(ProductDto objDTO)
		{
			var obj = _mapper.Map<ProductDto, Product>(objDTO);
			var addedObj = _context.Add(obj);
			await _context.SaveChangesAsync();
			return _mapper.Map<Product, ProductDto>(addedObj.Entity);
		}

		public async Task<int> Delete(int id)
		{
			var obj = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
			if (obj != null)
			{
				_context.Products.Remove(obj);
				return await _context.SaveChangesAsync();
			}
			return 0;
		}

		public async Task<ProductDto> Get(int id)
		{
			var obj = await _context.Products.Include(u=>u.Category).Include(p=>p.ProductPrices).FirstOrDefaultAsync(c => c.Id == id);
			if (obj == null)
			{
				throw new NotImplementedException();
			}
			return _mapper.Map<ProductDto>(obj);
		}

		public async Task<IEnumerable<ProductDto>> GetAll()
		{
			return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_context.Products.Include(u => u.Category).Include(p => p.ProductPrices));
		}

		public async Task<ProductDto> Update(ProductDto objDTO)
		{
			var objFromDb = await _context.Products.FirstOrDefaultAsync(c => c.Id == objDTO.Id);
			if (objFromDb != null)
			{
				objFromDb.Name = objDTO.Name;
				objFromDb.Description = objDTO.Description;
				objFromDb.ImageUrl = objDTO.ImageUrl;
				objFromDb.CategoryId = objDTO.CategoryId;
				objFromDb.Color = objDTO.Color;
				objFromDb.ShowFavorites = objDTO.ShowFavorites;
				objFromDb.CustomerFavorites = objDTO.CustomerFavorites;
				_context.Products.Update(objFromDb);
				await _context.SaveChangesAsync();
				return _mapper.Map<Product, ProductDto>(objFromDb);
			}
			return objDTO;
		}
	}
}
