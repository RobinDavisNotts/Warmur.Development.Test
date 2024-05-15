namespace Warmur.Development.Test.Models.Leads;

public class LeadViewModel
{
    public string Name { get; set; }
    public string JobTitle { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    
    public List<IndicatorsViewModel> Indicators { get; set; }
}