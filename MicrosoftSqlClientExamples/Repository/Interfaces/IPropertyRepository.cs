using MicrosoftSqlClientExamples.Models;

namespace MicrosoftSqlClientExamples.Repository.Interfaces
{
    public interface IPropertyRepository
    {
        Task<int> AddPropertyAsync(Property property);
        Task<List<Property>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(int id);
        Task<int> UpdatePropertyAsync(Property property);
        Task<int> DeletePropertyAsync(int id);
    }
}
