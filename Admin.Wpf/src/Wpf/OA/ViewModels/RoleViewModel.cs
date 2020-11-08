using OA.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utility.Wpf.Attributes;
using Utility.Wpf.ViewModels;

namespace OA.Wpf.ViewModels
{
    [MappTypeAttribute(typeof(RoleInfo))]
    public class RoleViewModel : RoleInfo, INotifyPropertyChanged, IIsSelectedViewModel
    {
        public void CreateByNullInstance()
        {
            User ??= new UserViewModel();//防止 双向绑定时改对象为null 参数 传递不过来
            Parent ??= new RoleViewModel();
        }
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
        private RoleViewModel _parent;
        private ICollection<RoleViewModel> _roles;
        private UserViewModel _user;
        public new RoleViewModel Parent
        {
            get { return this._parent; }
            set { base.Set(ref this._parent, value, "Parent"); }
        }
        public new ICollection<RoleViewModel> Roles
        {
            get { return this._roles; }
            set { base.Set(ref this._roles, value, "Roles"); }
        }
        public new UserViewModel User
        {
            get { return this._user; }
            set { base.Set(ref this._user, value, "User"); }
        }
    }
}
