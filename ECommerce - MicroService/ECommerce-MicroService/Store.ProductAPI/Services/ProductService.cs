using AutoMapper;
using Store.ProductAPI.DTOs;
using Store.ProductAPI.Models;
using Store.ProductAPI.Repositories;

namespace Store.ProductAPI.Services;

public class ProductService : IProductService
{
    private IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task AddProduct(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        productEntity.NameFees = "Taxa de Importação";
        productEntity.Amount = 10.00m;
        await _productRepository.Create(productEntity);
        productDTO.CategoryId = productEntity.CategoryId;
    }

    public async Task UpdateProduct(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Update(productEntity);
    }  

    public async Task RemoveProduct(int id)
    {
        var productEntity = _productRepository.GetById(id).Result;
        await _productRepository.Delete(productEntity.CategoryId);
    }

   
}
