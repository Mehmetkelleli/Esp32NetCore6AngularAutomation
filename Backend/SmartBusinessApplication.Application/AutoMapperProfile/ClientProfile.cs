using AutoMapper;
using SmartBusinessApplication.Application.Dtos;
using SmartBusinessApplication.Domain.Entity;

namespace SmartBusinessApplication.Application.AutoMapperProfile
{
    public class ClientProfile:Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientDto, Client>();
            CreateMap<Client,ClientDto>();
            CreateMap<CreateClientDto, Client>();
            CreateMap<Client, CreateClientDto>();
        }
    }
}
