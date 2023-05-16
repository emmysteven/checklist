using AutoMapper;
using Checklist.Application.DTOs.Account;
using Checklist.Application.UseCases.Items.Commands;
using Checklist.Application.UseCases.Items.Queries;
using Checklist.Application.UseCases.Summaries.Commands;
using Checklist.Application.UseCases.Summaries.Queries;
using Checklist.Application.UseCases.Todos.Commands;
using Checklist.Application.UseCases.Todos.Queries;
using Checklist.Domain.Entities;
using TodoVm = Checklist.Application.UseCases.Todos.Queries.TodoVm;

namespace Checklist.Application.Mappings;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateTodoCommand, Todo>();
        CreateMap<UpdateTodoCommand, Todo>();
        
        CreateMap<CreateItemCommand, Item>();
        CreateMap<UpdateItemCommand, Item>();
        
        CreateMap<CreateSummaryCommand, Summary>();
        CreateMap<UpdateSummaryCommand, Summary>();
            
        CreateMap<RegisterRequest, User>();
        
        CreateMap<GetTodoQuery, TodoParameter>();
        CreateMap<Todo, TodoVm>().ReverseMap();
        
        CreateMap<GetItemQuery, ItemParameter>();
        CreateMap<Item, ItemVm>().ReverseMap();
        
        CreateMap<GetSummaryQuery, SummaryParameter>();
        CreateMap<Summary, SummaryVm>().ReverseMap();
    }
}