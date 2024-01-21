using AutoMapper;
using ToDoList.ViewModel;

namespace Taskmaster.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ToDoList.DataAccess.Model.Task, TaskViewModel>().ReverseMap();
    }
}

