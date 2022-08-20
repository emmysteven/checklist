using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.UseCases.Items.Queries;

public class ItemsVm : IMapFrom<Item>
{
    public int Id { get; set; }
    public int TodoId { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<Item, ItemDto>()
    //         .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
    // }
}