using AutoMapper;
using Serilog;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SimApi.Operation;

public class DapperCategoryService : BaseService<Category, CategoryRequest, CategoryResponse>, IDapperCategoryService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public DapperCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public override ApiResponse Delete(int Id)
    {
        try
        {
            var entity = unitOfWork.DapperRepository<Category>("Category").GetById(Id);
            if (entity is null)
            {
                Log.Warning("Record not found for Id " + Id);
                return new ApiResponse("Record not found");
            }

            unitOfWork.DapperRepository<Category>("Category").DeleteById(Id);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Delete Exception");
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse<List<CategoryResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.DapperRepository<Category>("Category").GetAll();
            var mapped = mapper.Map<List<Category>, List<CategoryResponse>>(entityList);
            return new ApiResponse<List<CategoryResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<CategoryResponse>>(ex.Message);
        }
    }

    public override ApiResponse<CategoryResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.DapperRepository<Category>("Category").GetById(id);
            if (entity is null)
            {
                Log.Warning("Record not found for Id " + id);
                return new ApiResponse<CategoryResponse>("Record not found");
            }

            var mapped = mapper.Map<Category, CategoryResponse>(entity);
            return new ApiResponse<CategoryResponse>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetById Exception");
            return new ApiResponse<CategoryResponse>(ex.Message);
        }
    }

    public override ApiResponse Insert(CategoryRequest request)
    {
        try
        {
            var entity = mapper.Map<CategoryRequest, Category>(request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = "sim@sim.com";

            unitOfWork.DapperRepository<Category>("Category").Insert(entity);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Insert Exception");
            return new ApiResponse(ex.Message);
        }
    }

    public override ApiResponse Update(int Id, CategoryRequest request)
    {
        try
        {
            var entity = mapper.Map<CategoryRequest, Category>(request);

            var exist = unitOfWork.DapperRepository<Category>("Category").GetById(Id);
            if (exist is null)
            {
                Log.Warning("Record not found for Id " + Id);
                return new ApiResponse("Record not found");
            }

            entity.Id = Id;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "sim@sim.com";

            unitOfWork.DapperRepository<Category>("Category").Update(entity);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Update Exception");
            return new ApiResponse(ex.Message);
        }
    }
}
