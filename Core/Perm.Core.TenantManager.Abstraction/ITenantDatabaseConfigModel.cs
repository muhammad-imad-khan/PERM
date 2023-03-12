namespace Perm.Core.TenantManager.Abstraction
{
    public interface ITenantDataBaseConfigModel
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}