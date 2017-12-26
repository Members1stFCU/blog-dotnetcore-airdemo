using AirDemo.Domain;
using AirDemo.Service.Models;
using AutoMapper;

namespace AirDemo.Service
{
    public class AirplaneServiceMappingProfile : Profile
    {
        public AirplaneServiceMappingProfile()
        {
            this.CreateMap<Airplane, AirplaneResponse>();
        }
    }
}