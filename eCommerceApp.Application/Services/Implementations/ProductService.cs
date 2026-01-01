using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations
{
    public class ProductService(IGeneric<Product> productInterface) : IProductService
    {
        public Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetProduct> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
