using System.ServiceProcess;

namespace Altn.Service.Core
{
    public class StartService : ServiceBase
    {
        public StartService()
        {
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }

        protected override void OnStop()
        {
        }

        private void InitializeComponent()
        {
            ServiceName = GlobalSettings.GetSetting("ServiceName");
        }
    }
}