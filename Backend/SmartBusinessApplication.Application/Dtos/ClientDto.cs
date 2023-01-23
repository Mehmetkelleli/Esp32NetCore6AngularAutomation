using SmartBusinessApplication.Domain.Entity.BaseEntity;

namespace SmartBusinessApplication.Application.Dtos
{
    public class ClientDto:BaseClass
    {
        public string Name { get; set; }
        public double OutSideCurrentTemperature { get; set; } = -1;
        public double InSideCurrentTemperature { get; set; } = -1;
        public double TemperatureLimit { get; set; } = -1;
        public string ClientUserName { get; set; }
        public bool State { get; set; } = true;
        public string ClientPasswordEncrypt { get; set; }
        public bool Role1 { get; set; } = false;
        public bool Role2 { get; set; } = false;
        public bool AutoSystemEnabled { get; set; }
    }
}
