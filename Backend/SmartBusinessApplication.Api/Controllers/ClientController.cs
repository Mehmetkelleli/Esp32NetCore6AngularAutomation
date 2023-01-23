 using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmartBusinessApplication.Api.Hubs;
using SmartBusinessApplication.Application.Abstract;
using SmartBusinessApplication.Application.Dtos;
using SmartBusinessApplication.Application.Enums;
using SmartBusinessApplication.Domain.Entity;


namespace SmartBusinessApplication.Api.Controllers
{
    public class ClientController : BaseController
    {
        private IHubContext<DataHub> _Hub;
        private IClientRepository _Client;
        public ClientController(IMapper Mapper, IClientRepository Client, IHubContext<DataHub> hub) : base(Mapper)
        {
            _Client = Client;
            _Hub = hub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_Client.GetAll());
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetClient(LoginClient model)
        {
            if (ModelState.IsValid)
            {
                if(await _Client.GetByFilterAsync(i=>i.ClientUserName == model.Name && i.ClientPasswordEncrypt == model.Password) != null)
                {
                    return Ok(_Mapper.Map<ClientDto>(await _Client.GetByFilterAsync(i => i.ClientUserName == model.Name && i.ClientPasswordEncrypt == model.Password)));
                }
                return BadRequest(ReturnType.FalseUserNameOrPassword);
            }
            return BadRequest(ReturnType.FillRequiredFields);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ClientDto Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ReturnType.FillRequiredFields);
            }
            if(await _Client.GetByFilterAsync(i=>i.ClientUserName == Model.ClientUserName && i.ClientPasswordEncrypt == Model.ClientPasswordEncrypt) != null)
            {
                if (_Client.Update(_Mapper.Map<Client>(Model)))
                {
                    await _Client.SaveAsync();
                    return Ok(_Mapper.Map<ClientDto>(await _Client.GetByFilterAsync(i => i.ClientUserName == Model.ClientUserName && i.ClientPasswordEncrypt == Model.ClientPasswordEncrypt)));
                }
                else
                {
                    return BadRequest(ReturnType.SystemError);
                }
            }
            return BadRequest(ReturnType.AccesDenied);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateClientDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ReturnType.FillRequiredFields);
            }
            if(await _Client.GetByFilterAsync(i=>i.ClientUserName == model.ClientUserName && i.ClientPasswordEncrypt == model.ClientPasswordEncrypt) == null)
            {
                if(await _Client.CreateClientAsync(_Mapper.Map<Client>(model)))
                {
                    await _Client.SaveAsync();
                    return Ok(await _Client.GetByFilterAsync(i => i.ClientUserName == model.ClientUserName && i.ClientPasswordEncrypt == model.ClientPasswordEncrypt));
                }
            }
            return Ok(await _Client.GetByFilterAsync(i => i.ClientUserName == model.ClientUserName && i.ClientPasswordEncrypt == model.ClientPasswordEncrypt));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> All()
        {
            var list = new List<Client>();
            for (int i = 0; i < 10; i++)
            {
                var client = new Client { Name = $"{i} Moruq" };
                list.Add(client);
            }
            return Ok(list);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> MachineUpdate(MachineUpdateDto Model)
        {
            if(await _Client.MachineUpdateAsync(Model))
            {
                await _Client.SaveAsync();
                await _Hub.Clients.All.SendAsync("receiveData", Model);
                return Ok();
            }
            return BadRequest();
        }
    }
    
}
