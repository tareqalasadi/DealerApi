using DealerApi.AppDb;
using DealerApi.DomenClass;
using DealerApi.Helper.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DealerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public readonly IRedisService _IRedisService;

        public HomeController(AppDbContext appDbContext, IRedisService redisService)
        {
            _context = appDbContext;
            _IRedisService= redisService;
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(PropertyCategory category)
        {
            if (category == null)
                return BadRequest("Invalid category data.");

            _context.PropertyCategories.Add(category);
            await _context.SaveChangesAsync();

            await _IRedisService.RemoveAsync("property:categories");

            return Ok(new { message = "Category added successfully" });
        }

        [HttpPost("AddProperty")]
        public async Task<IActionResult> AddProperty([FromBody] PropertyDto propertyDto)
        {
            if (propertyDto == null || propertyDto.Property == null)
                return BadRequest("Invalid property data.");

            var exists = await _context.PropertyCategories.AnyAsync(c => c.Id == propertyDto.Property.CategoryId);
            if (!exists)
                return NotFound("Category not found.");

            _context.Properties.Add(propertyDto.Property);
            await _context.SaveChangesAsync();

            await _IRedisService.RemoveAsync("properties:all");
            await _IRedisService.RemoveAsync($"properties:category:{propertyDto.Property.CategoryId}");

            if (propertyDto.Images != null && propertyDto.Images.Count > 0)
            {
                foreach (var imgBytes in propertyDto.Images)
                {
                    var propertyImage = new PropertyImage
                    {
                        ImageData = imgBytes,
                        PropertyId = propertyDto.Property.Id
                    };
                    _context.PropertyImages.Add(propertyImage);
                }
                await _context.SaveChangesAsync();

                await _IRedisService.RemoveAsync($"property:images:{propertyDto.Property.Id}");
            }

            return Ok(new { message = "Property added successfully" });
        }


        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            string cacheKey = "property:categories";

            var cachedData = await _IRedisService.GetAsync<List<PropertyCategory>>(cacheKey);
            if (cachedData != null)
                return Ok(cachedData);

            var categories = await _context.PropertyCategories
                .AsNoTracking()
                .Select(c => new
                {
                    c.Id,
                    c.NameEn,
                    c.NameAr,
                    c.Image
                })
                .ToListAsync();

            if (categories == null || !categories.Any())
                return NotFound("No categories found.");

            await _IRedisService.SetAsync(cacheKey, categories, TimeSpan.FromDays(365));

            return Ok(categories);
        }


        [HttpGet("GetProperties/{categoryId}")]
        public async Task<IActionResult> GetProperties(int categoryId)
        {
            try
            {
                string cacheKey = categoryId == 0 ? "properties:all" : $"properties:category:{categoryId}";

                var cachedProperties = await _IRedisService.GetAsync<List<Property>>(cacheKey);
                if (cachedProperties != null)
                    return Ok(cachedProperties);

                List<Property> properties;

                if (categoryId != 0)
                {
                    properties = await _context.Properties
                        .Where(p => p.CategoryId == categoryId)
                        .AsNoTracking()
                        .ToListAsync();
                }
                else
                {
                    properties = await _context.Properties
                        .AsNoTracking()
                        .ToListAsync();
                }

                if (properties == null || properties.Count == 0)
                    return NotFound("No properties found.");

                await _IRedisService.SetAsync(cacheKey, properties, TimeSpan.FromDays(365));

                return Ok(properties);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpGet("GetPropertiesImg/{PropertyId}")]
        public async Task<IActionResult> GetPropertiesImg(int PropertyId)
        {
            if (PropertyId == 0)
                return NotFound();

            string cacheKey = $"property:images:{PropertyId}";

            var cachedImages = await _IRedisService.GetAsync<List<PropertyImage>>(cacheKey);
            if (cachedImages != null)
                return Ok(cachedImages);

            var propertiesImg = await _context.PropertyImages
                .Where(p => p.PropertyId == PropertyId)
                .AsNoTracking()
                .ToListAsync();

            if (propertiesImg == null || propertiesImg.Count == 0)
                return NotFound("No images found.");

            await _IRedisService.SetAsync(cacheKey, propertiesImg, TimeSpan.FromDays(365));

            return Ok(propertiesImg);
        }

    }

}

