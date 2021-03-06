﻿using Caliburn.Micro;
using LocalizationFileHelper.Framework;
using LocalizationFileHelper.Localization;
using LocalizationFileHelper.Logic.Info.JsonInfo;
using LocalizationFileHelper.Logic.Interfaces.IJsonService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizationFileHelper.Screen
{
    public class MainViewModel : ScreenBase
    {
        private ObservableCollection<TabItemInfo> _tabs;
        public ObservableCollection<TabItemInfo> Tabs
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

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    NotifyOfPropertyChange(() => SelectedTabIndex);
                }
            }
        }

        protected override void OnFirstLoad()
        {
            Tabs = new ObservableCollection<TabItemInfo>();
            Tabs.Add(new TabItemInfo { TabHeader = "+", IsContentDisplayed = Visibility.Collapsed, OnTabClicked = AddNewTab });
            SelectedTabIndex = 0;

            base.OnFirstLoad();
        }

        public void OnSelectedTabChanged(object dataContext) 
        {
            var tabItemModel = dataContext as TabItemInfo;
            var tabItem = FindTabItemById(tabItemModel.TabItemId);

            if (tabItem != null)
            {
                var itemIndex = Tabs.IndexOf(tabItem);
                SelectedTabIndex = itemIndex;
            }
        }

        public void AddNewTab(object dataContext) 
        {
            if (Tabs != null)
            {
                var idx = Tabs.Count - 1;
                Tabs.Insert(idx, new TabItemInfo
                {
                    TabHeader = LocalizationResource.NewTab,
                    IsContentDisplayed = Visibility.Visible,
                    OnTabClicked = OnSelectedTabChanged
                });

                SelectedTabIndex = idx;
            }
        }

        public void RearrageJson()
        {
            var selectedTab = Tabs[SelectedTabIndex];
            var jsonService = IoC.Get<IJsonService>();

            var files = Directory.GetFiles(selectedTab.LocalizationDirectory).Where(x => Path.GetExtension(x).Equals(".json")).ToList();
            jsonService.Rearrange(new JsonInfo { OriginalFilePath = selectedTab.OriginalFilePath, LocalizationFilePaths = files });
        }

        public void Proceed()
        {
            var selectedTab = Tabs[SelectedTabIndex];

            if (selectedTab != null)
            {
                var jsonService = IoC.Get<IJsonService>();

                var files = Directory.GetFiles(selectedTab.LocalizationDirectory).Where(x => Path.GetExtension(x).Equals(".json")).ToList();
                jsonService.GetFileInfoes(new JsonInfo { OriginalFilePath = selectedTab.OriginalFilePath, LocalizationFilePaths = files });
            }
        }

        private TabItemInfo FindTabItemById(Guid id)
        {
            if (Tabs != null)
            {
                return Tabs.Where(x => x.TabItemId == id).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public class TabItemInfo : PropertyChangedBase
        {
            public TabItemInfo()
            {
                TabItemId = Guid.NewGuid();
            }

            private Guid _tabItemId;
            public Guid TabItemId
            {
                get { return _tabItemId; }
                set
                {
                    if (_tabItemId != value)
                    {
                        _tabItemId = value;
                        NotifyOfPropertyChange(() => TabItemId);
                    }
                }
            }

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

                            UpdateTabHeader();
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

            private Visibility _isContentDisplayed;
            public Visibility IsContentDisplayed 
            {
                get { return _isContentDisplayed; }
                set
                {
                    if (_isContentDisplayed != value) 
                    {
                        _isContentDisplayed = value;
                        NotifyOfPropertyChange(() =>  IsContentDisplayed);
                    }
                }
            }

            public delegate void OnTabSelected(object dataContext);

            private OnTabSelected _onTabClicked;
            public OnTabSelected OnTabClicked
            {
                get { return _onTabClicked; }
                set 
                {
                    _onTabClicked = value;
                    NotifyOfPropertyChange(() => _onTabClicked);
                }
            }

            public void OnTabItemClicked(object dataContext)
            {
                OnTabClicked(dataContext);
            }

            private void UpdateTabHeader()
            {
                if (!string.IsNullOrEmpty(OriginalFilePath)) 
                {
                    var rootDir = FindProjectRootDirectory(Path.GetDirectoryName(OriginalFilePath));

                    var dir = new DirectoryInfo(rootDir);
                    TabHeader = dir.Name;
                }
            }

            private string FindProjectRootDirectory(string dir)
            {
                var dInfo = new DirectoryInfo(dir);
                var isRootDir = dInfo.GetDirectories().Any(x => x.Name.Equals(".git"));

                if (isRootDir)
                {
                    return dir;
                }
                else
                {
                    return FindProjectRootDirectory(Directory.GetParent(dir).FullName);
                }
            }
        }

        public class LocalizationFileInfo : PropertyChangedBase
        {
            private bool _isOriginal;
            public bool IsOriginal
            {
                get { return _isOriginal; }
                set
                {
                    if (_isOriginal != value)
                    {
                        _isOriginal = value;
                        NotifyOfPropertyChange(() => IsOriginal);
                    }
                }
            }

            private int _totalKeyValue;
            public int TotalKeyValue
            {
                get { return TotalKeyValue; }
                set
                {
                    if (_totalKeyValue != value)
                    {
                        _totalKeyValue = value;
                        NotifyOfPropertyChange(() => TotalKeyValue);
                    }
                }
            }

            private int _totalMissingKeyValue;
            public int TotalMissingKeyValue
            {
                get { return _totalMissingKeyValue; }
                set
                {
                    if (_totalMissingKeyValue != value)
                    {
                        _totalMissingKeyValue = value;
                        NotifyOfPropertyChange(() => TotalMissingKeyValue);
                    }
                }
            }

            private string _filePath;
            public string FilePath
            {
                get { return _filePath; }
                set
                {
                    if (_filePath != value)
                    {
                        _filePath = value;
                        NotifyOfPropertyChange(() => FilePath);
                    }
                }
            }

            private Dictionary<string, string> _keyValues;
            public Dictionary<string, string> KeyValues
            {
                get { return _keyValues; }
                set
                {
                    if (_keyValues != value)
                    {
                        _keyValues = value;
                        NotifyOfPropertyChange(() => KeyValues);
                    }
                }
            }
        }
    }
}
