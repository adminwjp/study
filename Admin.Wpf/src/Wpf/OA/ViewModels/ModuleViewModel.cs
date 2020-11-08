using OA.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utility.Wpf.Attributes;
using Utility.Wpf.ViewModels;

namespace OA.Wpf.ViewModels
{
    [MappTypeAttribute(typeof(ModuleInfo))]
    public class ModuleViewModel : ModuleInfo, INotifyPropertyChanged, IIsSelectedViewModel
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    Set(ref _isSelected, value, "IsSelected");
                    if (AllSelectEvent != null)
                    {
                        AllSelectEvent(this);
                    }
                }
         
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IIsSelectedViewModel> AllSelectEvent;

        protected override void Set<T>(ref T oldVal, T newVal, string propertyName = null)
        {
            //值 类型 比较 无效
            if (typeof(T).IsValueType)
            {
                if (oldVal.ToString().Equals(newVal.ToString()))
                {
                    return;
                }
            }
            else
            {
                if (EqualityComparer<T>.Default.Equals(oldVal, newVal))
                {
                    return;
                }
            }
            oldVal = newVal;
            this.OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ModuleViewModel _parent;
        private IList<ModuleViewModel> _modules;
        private UserViewModel _user;
        public new ModuleViewModel Parent
        {
            get { return this._parent; }
            set {base.Set(ref this._parent, value, "Parent"); }
        }
        public new IList<ModuleViewModel> Modules
        {
            get { return this._modules; }
            set{  base.Set(ref this._modules, value, "Modules");}
        }
        public new UserViewModel User
        {
            get { return this._user; }
            set{ base.Set(ref this._user, value, "User");}
        }
        public void CreateByNullInstance()
        {
            //没有更新父模块 
            Parent ??= new ModuleViewModel();//防止 双向绑定时改对象为null 参数 传递不过来
            User ??= new UserViewModel();
        }
    }
}
