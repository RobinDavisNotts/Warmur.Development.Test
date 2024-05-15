using FluentResults;
using Warmur.Development.Test.Models.Leads;
using Warmur.Development.Test.Models.Leads.Responses;

namespace Warmur.Development.Test.Services;

public interface ILeadsService
{
    Task<Result<Lead>> GetLead(int id);
    Task<Result<IndicatorResponse>> GetLeadIndicators(int id);
}