using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> CategoryInterface, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await CategoryInterface.AddAsync(mappedData);
            return result > 0
                ? new ServiceResponse { Success = true, Message = "Category added successfully." }
                : new ServiceResponse { Success = false, Message = "Failed to add category." };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await CategoryInterface.DeleteAsync(id);
            return result > 0 
                ? new ServiceResponse { Success = true, Message = "Category deleted." }
                : new ServiceResponse { Success = false, Message = "Category not found or failed to delete." };
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await CategoryInterface.GetAllAsync();
            if (!rawData.Any())
                return [];
            return mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await CategoryInterface.GetByIdAsync(id);
            if (rawData == null)
                return new GetCategory();
            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await CategoryInterface.UpdateAsync(mappedData);
            return result > 0
                ? new ServiceResponse { Success = true, Message = "Category updated successfully." }
                : new ServiceResponse { Success = false, Message = "Failed to update category." };
        }
    }
}
