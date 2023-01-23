using SmartBusinessApplication.Application.Dtos;
using SmartBusinessApplication.Domain.Entity;

namespace SmartBusinessApplication.Application.Abstract
{
    public interface IClientRepository:IGenericRepository<Client>
    {
        Task<bool> CreateClientAsync(Client model);
        Task<bool> MachineUpdateAsync(MachineUpdateDto model);
    }
}
