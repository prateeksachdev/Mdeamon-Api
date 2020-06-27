using System.ServiceProcess;
using Altn.Service.Core;

namespace Altn.Service
{
    class CoreService : ServiceBase
    {
        public CoreService(){}

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            StartJob.Run();
        }

        private void InitializeComponent()
        {
            ServiceName = "Altn.Service";          
        }
    }
}
