using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException();
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categoriesEntity = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetByIdAsync(int? id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task CreateAsync(CategoryDTO dto)
        {
            var categoryEntity = _mapper.Map<Category>(dto);
            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            await _categoryRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(CategoryDTO dto)
        {
            var categoryEntity = _mapper.Map<Category>(dto);
            await _categoryRepository.UpdateAsync(categoryEntity);
        }
    }
}
