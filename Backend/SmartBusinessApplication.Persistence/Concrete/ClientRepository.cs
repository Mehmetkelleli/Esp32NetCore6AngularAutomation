using Microsoft.EntityFrameworkCore;
using SmartBusinessApplication.Application.Abstract;
using SmartBusinessApplication.Application.Dtos;
using SmartBusinessApplication.Domain.Entity;
using SmartBusinessApplication.Persistence.Data.Context;
using SQLitePCL;

namespace SmartBusinessApplication.Persistence.Concrete
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private DataContext _Context;
        public ClientRepository(DataContext Context) : base(Context)
        {
            _Context = Context;
        }

        public async Task<bool> CreateClientAsync(Client model)
        {
            var state = await CreateAsycn(model);
            return state;
        }

        public async Task<bool> MachineUpdateAsync(MachineUpdateDto model)
        {
            if(await _Context.Clients.FirstOrDefaultAsync(i => i.ClientPasswordEncrypt == model.Password && i.ClientUserName == model.UserName) == null)
            {
                return false;
            }
            var client = await _Context.Clients.FirstOrDefaultAsync(i => i.ClientPasswordEncrypt == model.Password && i.ClientUserName == model.UserName);
            client.InSideCurrentTemperature = model.InsideTemperature;
            client.OutSideCurrentTemperature = model.OutTemperature;
            if (Update(client))
            {
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
