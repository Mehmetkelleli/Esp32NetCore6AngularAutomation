namespace SmartBusinessApplication.Application.Dtos
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string ClientUserName { get; set; }
        public string ClientPasswordEncrypt { get; set; }
    }
}
