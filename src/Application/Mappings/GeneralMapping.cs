using AutoMapper;
using Checklist.Application.DTOs.Account;
using Checklist.Application.UseCases.Todos.Commands;
using Checklist.Application.UseCases.Todos.Queries;
using Checklist.Domain.Entities;

namespace Checklist.Application.Mappings;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateTodoCommand, Todo>();
        CreateMap<UpdateTodoCommand, Todo>();
            
        CreateMap<RegisterRequest, User>();
        
        CreateMap<GetAllTodoQuery, GetAllTodoParameter>();
        CreateMap<Todo, GetAllTodoVm>().ReverseMap();
    }
}