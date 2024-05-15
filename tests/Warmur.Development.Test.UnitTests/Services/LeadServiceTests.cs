using System.Net;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using Shouldly;
using Warmur.Development.Test.Models.Configuration;
using Warmur.Development.Test.Services;

namespace Warmur.Development.Test.UnitTests.Services;

public class LeadServiceTests
{
    [Fact]
    public async Task Lead_Endpoint_Returns_Forbidden_Without_Api_Key_Header()
    {
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<LeadsApiConfig>>();

        mockHttp.When("https://warmur-interview-api.azurewebsites.net/api/Leads/0")
            .WithHeaders(new Dictionary<string, string>
            {
                { "x-api-key", string.Empty }
            })
            .Throw(new HttpRequestException("Forbidden", null, HttpStatusCode.Forbidden));

        mockOptions.Setup(x => x.Value).Returns(new LeadsApiConfig
        {
            BaseUri = "https://warmur-interview-api.azurewebsites.net/api/",
            Key = string.Empty
        });
        

        var sut = new LeadService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetLead(0);
        
        result.IsFailed.ShouldBeTrue();
        result.Errors.ShouldContain(x => x.Message == "Forbidden");
    }
    
    [Fact]
    public async Task Lead_Endpoint_Returns_Success_With_Valid_Api_Key()
    {
        var validGuid = Guid.NewGuid().ToString();
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<LeadsApiConfig>>();

        mockHttp.When("https://warmur-interview-api.azurewebsites.net/api/Leads/0")
            .WithHeaders(new Dictionary<string, string>
            {
                { "x-api-key", validGuid}
            })
            .Respond("application/json", "{\"Id\":\"0\"}");

        mockOptions.Setup(x => x.Value).Returns(new LeadsApiConfig
        {
            BaseUri = "https://warmur-interview-api.azurewebsites.net/api/",
            Key = validGuid
        });
        

        var sut = new LeadService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetLead(0);
        
        result.IsSuccess.ShouldBeTrue();
        result.Value.Id.ShouldBe(0);
    }
    
    [Fact]
    public async Task Indicators_Endpoint_Returns_Forbidden_Without_Api_Key_Header()
    {
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<LeadsApiConfig>>();

        mockHttp.When("https://warmur-interview-api.azurewebsites.net/api/Leads/0/Indicators")
            .WithHeaders(new Dictionary<string, string>
            {
                { "x-api-key", string.Empty }
            })
            .Throw(new HttpRequestException("Forbidden", null, HttpStatusCode.Forbidden));

        mockOptions.Setup(x => x.Value).Returns(new LeadsApiConfig
        {
            BaseUri = "https://warmur-interview-api.azurewebsites.net/api/",
            Key = string.Empty
        });
        

        var sut = new LeadService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetLeadIndicators(0);
        
        result.IsFailed.ShouldBeTrue();
        result.Errors.ShouldContain(x => x.Message == "Forbidden");
    }
    
    [Fact]
    public async Task Indicators_Endpoint_Returns_Success_With_Valid_Api_Key()
    {
        var validGuid = Guid.NewGuid().ToString();
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<LeadsApiConfig>>();

        mockHttp.When("https://warmur-interview-api.azurewebsites.net/api/Leads/0/Indicators")
            .WithHeaders(new Dictionary<string, string>
            {
                { "x-api-key", validGuid}
            })
            .Respond("application/json", "{\"Items\": [{\"Id\":\"0\"}]}");

        mockOptions.Setup(x => x.Value).Returns(new LeadsApiConfig
        {
            BaseUri = "https://warmur-interview-api.azurewebsites.net/api/",
            Key = validGuid
        });
        

        var sut = new LeadService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetLeadIndicators(0);
        
        result.IsSuccess.ShouldBeTrue();
        result.Value.Items.ShouldNotBeEmpty();
    }
}