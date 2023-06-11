using Dapper;
using SimApi.Base;
using SimApi.Data.Context;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SimApi.Data.Repository;

public class DapperRepository<Entity> : IDapperRepository<Entity> where Entity : BaseModel
{
    protected readonly SimDapperDbContext dbContext;
    private readonly string tableName;
    private bool disposed;
    private IEnumerable<PropertyInfo> GetProperties => typeof(Entity).GetProperties();

    public DapperRepository(SimDapperDbContext dbContext, string tableName)
    {
        this.dbContext = dbContext;
        this.tableName = tableName;
    }
    public void DeleteById(int id)
    {
        var sql = $"DELETE FROM dbo.\"{tableName}\" WHERE \"Id\"=@Id";
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            connection.Execute(sql, new { Id = id });
            connection.Close();
        }
    }

    public List<Entity> Filter(string sql)
    {
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Entity>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public List<Entity> GetAll()
    {
        var sql = $"SELECT * FROM dbo.\"{tableName}\"";
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Entity>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public Entity GetById(int id)
    {
        var sql = $"SELECT * FROM dbo.\"{tableName}\" WHERE \"Id\"=@Id";
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            var result = connection.QuerySingleOrDefault<Entity>(sql, new { Id = id });
            connection.Close();
            return result;
        }
    }

    public void Insert(Entity entity)
    {
        var sql = GenerateInsertQuery();
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            connection.Execute(sql, entity);
            connection.Close();
        }
    }

    public void Update(Entity entity)
    {
        var sql = GenerateUpdateQuery();
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            connection.Execute(sql, entity);
            connection.Close();
        }
    }


    private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
    {
        return (from prop in listOfProperties
                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                select prop.Name).ToList();
    }


    private string GenerateInsertQuery()
    {
        var insertQuery = new StringBuilder($"INSERT INTO dbo.\"{tableName}\" ");

        insertQuery.Append("(");

        var properties = GenerateListOfProperties(GetProperties);
        properties.ForEach(prop =>
        {
            if (!prop.Equals("Id"))
            {
                insertQuery.Append($"\"{prop}\",");
            }
        });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(") VALUES (");

        properties.ForEach(prop =>
        {
            if (!prop.Equals("Id"))
            {

                insertQuery.Append($"@{prop},");
            }
        });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(")");

        return insertQuery.ToString();
    }

    private string GenerateUpdateQuery()
    {
        var updateQuery = new StringBuilder($"UPDATE dbo.\"{tableName}\" SET ");
        var properties = GenerateListOfProperties(GetProperties);

        properties.ForEach(property =>
        {
            if (!property.Equals("Id"))
            {
                updateQuery.Append($"\"{property}\"=@{property},");
            }
        });

        updateQuery.Remove(updateQuery.Length - 1, 1);
        updateQuery.Append(" WHERE \"Id\"=@Id");

        return updateQuery.ToString();
    }
}
