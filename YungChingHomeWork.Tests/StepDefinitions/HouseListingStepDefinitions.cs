using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using YungChingHomeWork.Models;
using YungChingHomeWork.Services;
using Xunit;

[Binding]
public class HouseListingStepDefinitions : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private HouseListing _houseListing;
    private HttpResponseMessage _response;
    private readonly JsonSerializerOptions _jsonOptions;

    public HouseListingStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Given(@"I have a new house listing with the following details:")]
    public void GivenIHaveANewHouseListingWithTheFollowingDetails(Table table)
    {
        var row = table.Rows[0];
        _houseListing = new HouseListing
        {
            Name = row["Name"],
            Address = row["Address"],
            Price = decimal.Parse(row["Price"])
        };
    }

    [When(@"I submit the house listing")]
    public async Task WhenISubmitTheHouseListing()
    {
        var json = JsonSerializer.Serialize(_houseListing, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _response = await _client.PostAsync("/api/houselisting", content);
    }

    [Then(@"the house listing should be created successfully")]
    public void ThenTheHouseListingShouldBeCreatedSuccessfully()
    {
        Assert.Equal(System.Net.HttpStatusCode.Created, _response.StatusCode);
    }

    [Then(@"the response should contain the house listing ID")]
    public async Task ThenTheResponseShouldContainTheHouseListingID()
    {
        var responseContent = await _response.Content.ReadAsStringAsync();
        var createdListing = JsonSerializer.Deserialize<HouseListing>(responseContent, _jsonOptions);
        Assert.True(createdListing.Id > 0);
    }

    [Given(@"there are existing house listings in the system")]
    public async Task GivenThereAreExistingHouseListingsInTheSystem()
    {
        // 預先建立一些測試資料
        var testListing = new HouseListing
        {
            Name = "測試房屋",
            Address = "測試地址",
            Price = 1000000
        };
        
        var json = JsonSerializer.Serialize(testListing, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _client.PostAsync("/api/houselisting", content);
    }

    [When(@"I request all house listings")]
    public async Task WhenIRequestAllHouseListings()
    {
        _response = await _client.GetAsync("/api/houselisting");
    }

    [Then(@"I should receive a list of house listings")]
    public async Task ThenIShouldReceiveAListOfHouseListings()
    {
        Assert.Equal(System.Net.HttpStatusCode.OK, _response.StatusCode);
        
        var responseContent = await _response.Content.ReadAsStringAsync();
        var listings = JsonSerializer.Deserialize<List<HouseListing>>(responseContent, _jsonOptions);
        Assert.NotEmpty(listings);
    }
}