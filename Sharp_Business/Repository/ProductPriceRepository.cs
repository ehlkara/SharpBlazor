using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sharp_Business.Repository.IRepository;
using Sharp_DataAccess;
using Sharp_DataAccess.Data;
using Sharp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductPriceRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductPriceDto> Create(ProductPriceDto objDTO)
        {
            var obj = _mapper.Map<ProductPriceDto, ProductPrice>(objDTO);
            var addedObj = _context.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _context.ProductPrices.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _context.ProductPrices.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDto> Get(int id)
        {
            var obj = await _context.ProductPrices.Include(u => u.Product).FirstOrDefaultAsync(c => c.Id == id);
            if (obj == null)
            {
                throw new NotImplementedException();
            }
            return _mapper.Map<ProductPriceDto>(obj);
        }

        public async Task<IEnumerable<ProductPriceDto>> GetAll(int? id = null)
        {
            if(id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDto>>
                    (_context.ProductPrices.Where(p => p.ProductId==id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDto>>(_context.ProductPrices);
            }
        }

        public async Task<ProductPriceDto> Update(ProductPriceDto objDTO)
        {
            var objFromDb = await _context.ProductPrices.FirstOrDefaultAsync(c => c.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.ProductId = objDTO.ProductId;
                objFromDb.Size = objDTO.Size;
                objFromDb.Price = objDTO.Price;
                _context.ProductPrices.Update(objFromDb);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDto>(objFromDb);
            }
            return objDTO;
        }
    }
}
