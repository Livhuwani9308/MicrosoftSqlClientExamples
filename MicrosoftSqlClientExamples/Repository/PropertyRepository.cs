using Microsoft.Data.SqlClient;
using MicrosoftSqlClientExamples.Models;
using MicrosoftSqlClientExamples.Repository.Interfaces;

namespace MicrosoftSqlClientExamples.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly string _connectionString;

        public PropertyRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> AddPropertyAsync(Property property)
        {
            const string query = @"
            INSERT INTO Properties (Name, Price, Bedroom, Bathroom, Parking, Description, Street)
            VALUES (@Name, @Price, @Bedroom, @Bathroom, @Parking, @Description, @Street)";

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@Name", property.Name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", property.Price);
            cmd.Parameters.AddWithValue("@Bedroom", property.Bedroom);
            cmd.Parameters.AddWithValue("@Bathroom", property.Bathroom);
            cmd.Parameters.AddWithValue("@Parking", property.Parking);
            cmd.Parameters.AddWithValue("@Description", property.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Street", property.Street ?? (object)DBNull.Value);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            const string query = @"SELECT PropertyId, Name, Price, Bedroom, Bathroom, Parking, Description, Street, CreatedDate FROM Properties";
            var list = new List<Property>();

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new Property
                {
                    PropertyId = reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    Bedroom = reader.GetDecimal(3),
                    Bathroom = reader.GetDecimal(4),
                    Parking = reader.GetInt32(5),
                    Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Street = reader.IsDBNull(7) ? null : reader.GetString(7),
                    CreatedDate = reader.GetDateTime(8)
                });
            }

            return list;
        }

        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            const string query = @"SELECT PropertyId, Name, Price, Bedroom, Bathroom, Parking, Description, Street, CreatedDate 
                               FROM Properties WHERE PropertyId = @Id";

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Property
                {
                    PropertyId = reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    Bedroom = reader.GetDecimal(3),
                    Bathroom = reader.GetDecimal(4),
                    Parking = reader.GetInt32(5),
                    Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Street = reader.IsDBNull(7) ? null : reader.GetString(7),
                    CreatedDate = reader.GetDateTime(8)
                };
            }

            return null;
        }

        public async Task<int> UpdatePropertyAsync(Property property)
        {
            const string query = @"
            UPDATE Properties 
            SET Name=@Name, Price=@Price, Bedroom=@Bedroom, Bathroom=@Bathroom, 
                Parking=@Parking, Description=@Description, Street=@Street
            WHERE PropertyId=@Id";

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@Id", property.PropertyId);
            cmd.Parameters.AddWithValue("@Name", property.Name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", property.Price);
            cmd.Parameters.AddWithValue("@Bedroom", property.Bedroom);
            cmd.Parameters.AddWithValue("@Bathroom", property.Bathroom);
            cmd.Parameters.AddWithValue("@Parking", property.Parking);
            cmd.Parameters.AddWithValue("@Description", property.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Street", property.Street ?? (object)DBNull.Value);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeletePropertyAsync(int id)
        {
            const string query = "DELETE FROM Properties WHERE PropertyId=@Id";

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }
    }
}
