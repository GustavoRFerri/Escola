using Microsoft.AspNetCore.Mvc;
using Store.ProductAPI.DTOs;
using Store.ProductAPI.Services;

namespace Store.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{  
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var products = await _productService.GetProducts();
        if (products == null)            
            return NotFound(" Products not found ");            
        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound(" Product not found ");
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] ProductDTO productDTO)
    {
        if (productDTO == null)
        {
            return BadRequest(" Data Invalid ");
        }

        await _productService.AddProduct(productDTO);
        return new CreatedAtRouteResult("GetProductById", new { id = productDTO.Id }, productDTO);
    }

    [HttpPut()]
    public async Task<ActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
    {        
        if (productDTO == null)        
            return BadRequest(" Data Invalid ");
        
        await _productService.UpdateProduct(productDTO);
        return Ok(productDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound(" Product not found ");
        }
        await _productService.RemoveProduct(id);
        return Ok(product);
    }
}
