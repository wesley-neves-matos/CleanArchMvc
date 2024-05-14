using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var productsQuery = new GetProductsQuery();

            ValidateEntity(productsQuery);

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var ProductByIdQuery = new GetProductByIdQuery(id ?? 0);

            ValidateEntity(ProductByIdQuery);

            var result = await _mediator.Send(ProductByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO dto)
        {
            DefineExtensionImage(dto);

            var productCreateCommand = _mapper.Map<ProductCreateCommand>(dto);

            var result = await _mediator.Send(productCreateCommand);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task RemoveAsync(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id ?? 0);

            await _mediator.Send(productRemoveCommand);
        }

        public async Task UpdateAsync(ProductDTO dto)
        {
            DefineExtensionImage(dto);

            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(dto);

            await _mediator.Send(productUpdateCommand);
        }

        public async Task DeleteImage(ProductDTO productDTO, string folderPath)
        {
            ProductDTO oldProductDTO = await GetByIdAsync(productDTO.Id);

            FileService fileService = new FileService(folderPath, oldProductDTO.ImageName());

            fileService.DeleteCurrentFile();
        }

        public async Task SaveOrUpdateImage(ProductDTO productDTO, string destinationFolderPath)
        {
            ProductDTO oldProductDTO = await GetByIdAsync(productDTO.Id);

            FileService fileService = new FileService(destinationFolderPath, productDTO.ImageName(), oldProductDTO?.ImageName());

            fileService.SaveCurrentFile(productDTO.FileToCopyForImage);
        }

        private static void ValidateEntity(object productsQuery)
        {
            if (productsQuery == null)
                throw new Exception("Entity could not be loaded.");
        }

        private static void DefineExtensionImage(ProductDTO dto)
        {
            if (dto.FileToCopyForImage != null)
            {
                dto.ExtensionImage = Path.GetExtension(dto.FileToCopyForImage);
            }
        }
    }
}
