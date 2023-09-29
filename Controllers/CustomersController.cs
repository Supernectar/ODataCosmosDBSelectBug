using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using U4PIM.InvoiceManagementAPI.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CustomersController : ODataController
{
    private readonly ICosmosService _cosmosService;

    public CustomersController(ICosmosService cosmosService)
    {
        _cosmosService = cosmosService;
    }

    public async Task<ActionResult<IEnumerable<Customer>>> Get(ODataQueryOptions<Customer> queryOptions)
    {
        IEnumerable<Customer> result = await _cosmosService.GetCustomers(queryOptions);
        return Ok(result);
    }
}