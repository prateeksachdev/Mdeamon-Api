using System;

namespace Altn.Service.Plugin.Base.Interface
{
    public interface IPlugin
    {
        string PluginName { get; }

        void Start(ILog log, ISettings settings);
    }
}