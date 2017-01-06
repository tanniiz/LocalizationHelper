using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.Framework
{
    public class ScreenBase : Caliburn.Micro.Screen
    {
        #region Properties
        private Guid _screenId;
        public Guid ScreenId
        {
            get { return _screenId; }
            set
            {
                if (_screenId != value)
                    _screenId = value;

                NotifyOfPropertyChange(() => ScreenId);
            }
        }
        #endregion

        public ScreenBase()
        {
            this.ScreenId = new Guid();

            this.OnFirstLoad();
        }

        protected virtual void OnFirstLoad() { }
    }
}
