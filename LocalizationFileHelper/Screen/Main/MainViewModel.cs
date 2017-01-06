using LocalizationFileHelper.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.Screen
{
    public class MainViewModel : ScreenBase
    {
        private ObservableCollection<TabModel> _tabs;
        public ObservableCollection<TabModel> Tabs
        {
            get { return _tabs; }
            set
            {
                if (_tabs != value)
                {
                    _tabs = value;
                    NotifyOfPropertyChange(() => Tabs);
                }
            }
        }

        protected override void OnFirstLoad()
        {
            Tabs = new ObservableCollection<TabModel>();
            Tabs.Add(new TabModel { TabHeader = "Test" });
            Tabs.Add(new TabModel { TabHeader = "Test2" });

            base.OnFirstLoad();
        }

        public class TabModel : Caliburn.Micro.PropertyChangedBase
        {
            private string _tabHeader;
            public string TabHeader
            {
                get { return _tabHeader; }
                set
                {
                    if (_tabHeader != value)
                    {
                        _tabHeader = value;
                        NotifyOfPropertyChange(() => TabHeader);
                    }
                }
            }

            private string _originalFilePath;
            public string OriginalFilePath
            {
                get { return _originalFilePath; }
                set
                {
                    if (_originalFilePath != value)
                    {
                        _originalFilePath = value;

                        if (File.Exists(value))
                        {
                            var dir = System.IO.Path.GetDirectoryName(value);
                            LocalizationDirectory = dir;
                        }

                        NotifyOfPropertyChange(() => OriginalFilePath);
                    }
                }
            }

            private string _localizationDirectory;
            public string LocalizationDirectory
            {
                get { return _localizationDirectory; }
                set
                {
                    if (_localizationDirectory != value)
                    {
                        _localizationDirectory = value;
                        NotifyOfPropertyChange(() => LocalizationDirectory);
                    }
                }
            }
        }
    }
}
