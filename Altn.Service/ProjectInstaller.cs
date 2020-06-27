using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;

namespace Altn.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceInstaller1.ServiceName = GetServiceNameAppConfig("ServiceName");
            serviceInstaller1.DisplayName = GetServiceNameAppConfig("ServiceName");
            serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
        }

        public string GetServiceNameAppConfig(string serviceName)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof(ProjectInstaller)).Location);
            return config.AppSettings.Settings[serviceName].Value;
        }
    }
}
