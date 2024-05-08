using CleanArchMvc.Application.DTOs;


namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductService : IServices<ProductDTO>
    {
        //Task<ProductDTO> GetByIdWithCategoryAsync(int? id);
    }
}
