namespace Checklist.Application.UseCases.Items.Queries;

public class GetAllItemVm
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}