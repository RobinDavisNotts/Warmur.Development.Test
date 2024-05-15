namespace Warmur.Development.Test.Models.Leads;

public class Indicator
{
    public int Id { get; set; }
    public int LeadId { get; set; }
    public string? Name { get; set; }
    public Int32Assumable Value { get; set; }
}