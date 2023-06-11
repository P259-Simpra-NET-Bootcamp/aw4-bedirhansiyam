using SimApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApi.Data.Repository;

public class DapperCategoryRepository : DapperRepository<Category>, IDapperCategoryRepository
{
    public DapperCategoryRepository(SimDapperDbContext dbContext, string tableName) : base(dbContext, tableName)
    {
    }
}
