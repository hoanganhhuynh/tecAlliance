using System;
using Application.Employee.Commands;
using Application.Employee.Queries.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile
{
	public class EmployeeProfile : Profile
	{
        public EmployeeProfile()
        {
            CreateMap<EmployeeEntity, GetEmployeeResponse>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, EmployeeEntity>().ReverseMap();
        }
    }
}

