using SmartBusinessApplication.Domain.Entity.BaseEntity;

namespace SmartBusinessApplication.Domain.Entity
{
    public class Client:BaseClass
    {
        public string Name { get; set; }
        public double OutSideCurrentTemperature { get; set; } = -1;
        public double InSideCurrentTemperature { get; set; } = -1;
        public bool State { get; set; } = true;
        public double TemperatureLimit { get; set; } = -1;
        public bool Role1 { get; set; } = false;
        public bool Role2 { get; set; } = false;
        public string ClientUserName { get; set; }
        public string ClientPasswordEncrypt { get; set; }
        public bool AutoSystemEnabled { get; set; } = true;
    }
}
