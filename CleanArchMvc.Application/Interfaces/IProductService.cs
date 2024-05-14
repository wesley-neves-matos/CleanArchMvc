using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductService : IEntityService<ProductDTO>
    {
        Task DeleteImage(ProductDTO productDTO, string folderPath);

        Task SaveOrUpdateImage(ProductDTO productDTO, string destinationFolderPath);
    }
}
