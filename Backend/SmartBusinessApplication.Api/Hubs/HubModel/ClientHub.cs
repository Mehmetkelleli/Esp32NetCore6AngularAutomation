namespace SmartBusinessApplication.Api.Hubs.HubModel
{
    public class ClientHub
    {
        public double InsıdeTemperature { get; set; }
        public double OutSideTemperature { get; set; }
        public string MachineMessage { get; set; } = "";
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
