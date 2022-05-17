using Sharp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Business.Repository.IRepository
{
	public interface IProductRepository
	{
        public Task<ProductDto> Create(ProductDto objDTO);
        public Task<ProductDto> Update(ProductDto objDTO);
        public Task<int> Delete(int id);
        public Task<ProductDto> Get(int id);
        public Task<IEnumerable<ProductDto>> GetAll();
    }
}
