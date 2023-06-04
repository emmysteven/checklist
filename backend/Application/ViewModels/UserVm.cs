using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.ViewModels;

public class UserVm : IMapFrom<User>
{
    public long Id { get; set; }
    public string? UserName { get; set; }
    public string? Role { get; set; }
}