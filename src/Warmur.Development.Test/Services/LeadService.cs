using FluentResults;
using Microsoft.Extensions.Options;
using Warmur.Development.Test.Models.Configuration;
using Warmur.Development.Test.Models.Leads;
using Warmur.Development.Test.Models.Leads.Responses;

namespace Warmur.Development.Test.Services;

public class LeadService : ILeadsService
{
    private readonly HttpClient _client;
    private readonly IOptions<LeadsApiConfig> _leadApiConfig;
    public LeadService(HttpClient client, IOptions<LeadsApiConfig> leadApiConfig)
    {
        _client = client;
        _leadApiConfig = leadApiConfig;

        _client.BaseAddress = new Uri(leadApiConfig.Value.BaseUri);
        _client.DefaultRequestHeaders.Add("x-api-key", _leadApiConfig.Value.Key);
    }
    public async Task<Result<Lead>> GetLead(int id)
    {
        try
        {
            var result = await _client.GetFromJsonAsync<Lead>($"Leads/{id}");

            return Result.Ok(result);
        }
        catch (HttpRequestException e)
        {
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result<IndicatorResponse>> GetLeadIndicators(int id)
    {
        try
        {
            var result = await _client.GetFromJsonAsync<IndicatorResponse>($"Leads/{id}/Indicators");

            return Result.Ok(result);
        }
        catch (HttpRequestException e)
        {
            return Result.Fail(e.Message);
        }
    }
}