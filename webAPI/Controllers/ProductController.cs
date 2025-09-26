using DataAccess;
using DataAccess.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using webAPI.Core;
using webAPI.Core.Validators;
using webAPI.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TerminContext _context;

        public ProductController(TerminContext context)
        {
            _context = context;
        }

        public class SearchDto
        {
            public string? Name { get; set; }
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto search)
        {
           try
            {
                var productsQuery = _context.Products.AsQueryable();
                if (search.Name != "" && search.Name != null)
                {
                    productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(search.Name.ToLower()));
                }
                if (search.MinPrice.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.Price >= search.MinPrice);
                }
                if (search.MaxPrice.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.Price <= search.MaxPrice);
                }
                var results = productsQuery.ToList().Adapt<List<ProductDto>>();
                
                return Ok(results);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }// Returns OK 200 or Bad Request 400 in case reqired parameters are missing, 500 in case of server error

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            return product == null ? NotFound() : Ok(product.Adapt<ProductDto>());
        }// Return Ok 200 with the entity or 404 not found, 500 for error

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto dto, [FromServices]CreateProductValidator validator)
        {
            var validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                //var errors = validationResult.Errors.Adapt<List<Core.ClientError>>();
                //return UnprocessableEntity(new
                //{
                //    Errors = errors
                //});
                return validationResult.AsClientErrors();
            }
            var product = dto.Adapt<Product>();
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } // 201 Created
          // 422 unprocessable entity for validation errors
          // 409 conflict, order attempt with insufficient funds for example
          // 500

        // Update
        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDto dto, [FromServices] UpdateProductValidator validator)
        {
            dto.Id = id;

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var result = validator.Validate(dto);
            if (!result.IsValid)
            {
                return result.AsClientErrors();
            }
            dto.Adapt(product);
            try
            {
                _context.SaveChanges();
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }//204 no content
         //404 not found
         //422
         //409
         //500

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            product.IsDeleted = true;
            product.isActive = false;
            product.DeletedAt = DateTime.Now;

            try
            {
                _context.SaveChanges();
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }//204, 404, 409, 500
    }
}
