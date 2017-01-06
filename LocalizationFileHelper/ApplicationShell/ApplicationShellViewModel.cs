using Caliburn.Micro;
using LocalizationFileHelper.Screen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.ApplicationShell
{
    public class ApplicationShellViewModel : Caliburn.Micro.Screen
    {
        private IWindowManager _windowManager;

        private MainViewModel mainScreen_;
        public MainViewModel MainScreen
        {
            get { return mainScreen_; }
            set
            {
                if (value != mainScreen_)
                {
                    mainScreen_ = value;
                    NotifyOfPropertyChange(() => MainScreen);
                }
            }
        }

        public ApplicationShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.RegisterScreenInstances();
        }

        private void RegisterScreenInstances()
        {
            MainScreen = IoC.Get<MainViewModel>();
        }
    }
}
