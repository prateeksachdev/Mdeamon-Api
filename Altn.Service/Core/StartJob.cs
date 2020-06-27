using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Timers;
using Altn.Service.Domain;
using Altn.Service.Plugin.Base.Interface;

namespace Altn.Service.Core
{
    public static class StartJob
    {
        private static readonly ILog _log = new Manager.Log();
        private static ISettings _settings;
        private static ICollection<Type> pluginTypes;
        private static Timer timer;

        public static void Run()
        {
            try
            {
                _settings = new Settings();
                LoadPlugins();

                timer = new Timer();
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Interval = 1000 * (_settings.SyncInterval * 86400);//Number of days multiply by 86400(seconds in a day)
                timer.Enabled = true;
            }
            catch (Exception exp)
            {
                _log.Error(exp.Message, exp);
                throw;
            }
        }

        private static void LoadPlugins()
        {
            Directory.CreateDirectory("Plugins");

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory +"\\Plugins", "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(files.Length);
            foreach (var file in files)
            {
                if (!file.Contains("Altn.Service.Plugin"))
                    continue;

                var an = AssemblyName.GetAssemblyName(file);
                var assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            var pluginType = typeof(IPlugin);

            pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    var types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                            continue;

                        if (type.GetInterface(pluginType.FullName) != null)
                        {
                            pluginTypes.Add(type);
                            _log.Info("Service.StartJob.LoadPlugins :: " + type.FullName.Replace("Start", ""));
                        }
                    }
                }
            }
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            timer.Stop();

            try
            {
                foreach (Type type in pluginTypes)
                {
                    var plugin = (IPlugin)FormatterServices.GetUninitializedObject(type);
                    plugin.Start(_log, _settings);
                }
            }
            catch (Exception exp)
            {
                _log.Error("Service.OnTimedEvent :: " + exp.Message, exp);
            }

            timer.Start();
        }
    }
}