using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service;

[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class DapperCategoryController : ControllerBase
{
    private readonly IDapperCategoryService categoryService;
    public DapperCategoryController(IDapperCategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public ApiResponse<List<CategoryResponse>> GetAll()
    {
        return categoryService.GetAll();
    }

    [HttpGet("{id}")]
    public ApiResponse<CategoryResponse> GetById(int id)
    {
        return categoryService.GetById(id);
    }

    [HttpPost]
    public ApiResponse Post(CategoryRequest categoryRequest)
    {
        return categoryService.Insert(categoryRequest);
    }

    [HttpPut("{id}")]
    public ApiResponse Put(int id, CategoryRequest categoryRequest)
    {
        return categoryService.Update(id, categoryRequest); 
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        return categoryService.Delete(id);
    }
}
