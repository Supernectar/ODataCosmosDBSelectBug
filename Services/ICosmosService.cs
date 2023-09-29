using Microsoft.AspNetCore.OData.Query;
using Microsoft.Azure.Cosmos;
using WebApplication1.Models;

namespace U4PIM.InvoiceManagementAPI.Services;

public interface ICosmosService
{
    Task<IEnumerable<Customer>> GetCustomers(ODataQueryOptions<Customer> queryOptions);
}
