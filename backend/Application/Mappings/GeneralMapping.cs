using AutoMapper;
using Checklist.Application.DTOs.Account;
using Checklist.Application.DTOs.Entities;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Application.Mappings;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<TodoDto, Todo>();
        CreateMap<CheckDto, Check>();
        CreateMap<SummaryDto, Summary>();

        CreateMap<RegisterRequest, User>();
        
        CreateMap<Todo, TodoVm>().ReverseMap();
        CreateMap<Check, CheckVm>().ReverseMap();
        CreateMap<Summary, SummaryVm>().ReverseMap();
    }
}