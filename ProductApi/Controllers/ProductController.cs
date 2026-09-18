namespace ProductApi.Controller;

using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Model;

[ApiController]
[Route("api/product")]

public class ProductController : ControllerBase
{
  private readonly ProductRepository _productRepository;

  public ProductController(ProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  [HttpPost]
  public IActionResult Create(CreateProduct newProduct)
  {
    var product = _productRepository.Create(newProduct);

    return Ok(product);
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var products = _productRepository.GetAll();

    return Ok(products);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var product = _productRepository.GetById(id);

    return Ok(product);
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteById(int id)
  {
    _productRepository.DeleteById(id);

    return NoContent();
  }
}