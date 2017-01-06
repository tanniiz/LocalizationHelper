using Caliburn.Micro;
using LocalizationFileHelper.ApplicationShell;
using LocalizationFileHelper.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizationFileHelper
{
    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            Dictionary<string, object> windowSettingDictionary = new Dictionary<string, object>();

            windowSettingDictionary.Add("SizeToContent", SizeToContent.Manual);
            windowSettingDictionary.Add("Width", 800);
            windowSettingDictionary.Add("Height", 600);

            DisplayRootViewFor<ApplicationShellViewModel>(windowSettingDictionary);
        }

        protected override void Configure()
        {
            base.Configure();

            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<ApplicationShellViewModel>();
            _container.Singleton<MainViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
