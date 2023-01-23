namespace SmartBusinessApplication.Application.Dtos
{
    public class MachineUpdateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public double OutTemperature { get; set; }
        public double InsideTemperature { get; set; }
        public double InsideHumudity { get; set; }
        public double OutHumudity { get; set; }
        public string MachineMessage { get; set; }
    }
}
