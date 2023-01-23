using Microsoft.AspNetCore.SignalR;
using SmartBusinessApplication.Api.Hubs.HubModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartBusinessApplication.Api.Hubs
{
    public class DataHub : Hub
    {
        private static Dictionary<string,UserHub> _User= new Dictionary<string,UserHub>();
        public async Task SendData(UserHub model)
        {
            if (!_User.ContainsKey(model.Key))
            {
                _User.Add(model.Key, model);
            }
        }
        public async Task ReceiveData(ClientHub model)
        {
            foreach (var item in _User)
            {
                if(item.Value.UserName == model.UserName && item.Value.Password == model.Password)
                {
                    await Clients.Client(item.Key).SendAsync("receiveData", model);
                }
            }
        }

    }
}
