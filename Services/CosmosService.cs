using Microsoft.AspNetCore.OData.Query;
using Microsoft.Azure.Cosmos;
using WebApplication1.Models;
using Microsoft.Azure.Cosmos.Linq;

namespace U4PIM.InvoiceManagementAPI.Services;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _cosmosClient;

    public CosmosService(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;

    }

    private Container Container
    {
        get => _cosmosClient.GetContainer("TestDB", "test");
    }

    public async Task<IEnumerable<Customer>> GetCustomers(ODataQueryOptions<Customer> queryOptions)
    {
        var customerList = new List<Customer>();

        var customerQueryable = Container.GetItemLinqQueryable<Customer>();

        ODataQuerySettings querySettings = new();

        customerQueryable = queryOptions.ApplyTo(customerQueryable, querySettings) as IOrderedQueryable<Customer>;

        using FeedIterator<Customer> setIterator = customerQueryable.ToFeedIterator();

        while (setIterator.HasMoreResults)
        {
            var result = await setIterator.ReadNextAsync();
            customerList.AddRange(result.Resource);
        }

        return customerList;
    }
}
