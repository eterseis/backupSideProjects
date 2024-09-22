using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
    internal class Program
    {
        const string connectionString = "Server=Hive;Database=balta;Integrated Security=SSPI;TrustServerCertificate=True";

        static void Main(string[] args)
        {
            #region Categories
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "SHIT ASS";
            category.Url = "amazon";
            category.Summary = "AWS Cloud";
            category.Order = 8;
            category.Description = "Categoria destinada a serviços do AWS";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoriaNova";
            category2.Summary = "Categoria Nova";
            category2.Order = 9;
            category2.Description = "Categoria nova";
            category2.Featured = true;
            #endregion
            using (var connection = new SqlConnection(connectionString))
            {
                OneToOne(connection);
            }
        }

        static void GetCategory(SqlConnection connection, string id)
        {
            var getQuery = "SELECT [Id], [Title] FROM [Category] WHERE [Id] = @id";
            var item = connection.QueryFirst(getQuery, new
            {
                id
            });
            Console.WriteLine($"{item.Id} :: {item.Title}");
        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]").ToHashSet();
            foreach (var item in categories)
            {
                Console.WriteLine(item.Id + " :: " + item.Title);
            }
        }

        static void CreateCategory(SqlConnection connection, Category category)
        {
            var insertSql = @"INSERT INTO 
                                [Category]
                                VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";
            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void DeleteCategory(SqlConnection connection, string id)
        {
            var deleteQuery = @"DELETE FROM [Category] WHERE [Id] = @Id";
            var rows = connection.Execute(deleteQuery, new { id });
            Console.WriteLine(rows + " linhas removidas");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = @"UPDATE [Category] SET [Title] = @Title WHERE [Id] = @Id";
            var rows = connection.Execute(updateQuery, new
            {
                Title = "Reverse Engineering",
                Id = "70417066-b6c3-4fa6-b6d8-266581323470"
            });
            Console.WriteLine(rows + " linhas alteradas");
        }

        static void CreateManyCategory(SqlConnection connection, Category[] category)
        {
            var insertSql = @"INSERT INTO 
                                [Category]
                                VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";
            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                    category[0].Id,
                    category[0].Title,
                    category[0].Url,
                    category[0].Summary,
                    category[0].Order,
                    category[0].Description,
                    category[0].Featured
                },
                new
                {
                    category[1].Id,
                    category[1].Title,
                    category[1].Url,
                    category[1].Summary,
                    category[1].Order,
                    category[1].Description,
                    category[1].Featured
                }
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "[spDeleteStudent]";
            var pars = new { StudentId = "b2a6dcef-6dd6-4f47-9614-25e8ce1a7955" };
            var effectedLines = connection.Execute(
                procedure,
                pars,
                commandType: System.Data.CommandType.StoredProcedure
            );
            Console.WriteLine(effectedLines + " effected lines");
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
            var courses = connection.Query(procedure, pars, commandType: System.Data.CommandType.StoredProcedure);

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Id} :: {course.Title}");
            }
        }

        static void ExecuteScalar(SqlConnection connection, Category category)
        {
            var insertSql = @"INSERT INTO 
                                [Category]
                                OUTPUT inserted.[Id]
                                VALUES(
                                NEWID(), 
                                @Title, 
                                @Url, 
                                @Summary, 
                                @Order, 
                                @Description, @Featured)";
            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"{id} linhas inseridas");
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"SELECT *
                        FROM [CareerItem]
                        INNER JOIN [Course]
                        ON [CareerItem].[CourseId] = [Course].[Id]";
            var items = connection.Query<CareerItem, Course, CareerItem>
                (sql,
                (careerItem, course) => { careerItem.Course = course; return careerItem; },
                splitOn: "Id");
            foreach (var item in items)
            {
                Console.WriteLine(item.Course.Title);
            }
        }

        static void OneToMany(SqlConnection connection)
        {

        }
    }
}
