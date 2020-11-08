using OA.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Domain.Uow;
using Utility.Ioc;
using Utility.ObjectMapping;
using Utility.Wpf;
using Utility.Wpf.ViewModels;
using OA.Wpf.ViewModels;

namespace Wpf.OA
{
    public sealed class OAService
    {
        public static void RegisterDataSource()
        {
            CacheListModelManager.CacheDataSource["OA.Role"] = GetRole;
            CacheListModelManager.CacheDataSource["OA.Content"] = GetContent;
            CacheListModelManager.CacheDataSource["OA.Record"] = GetRecord; 
            CacheListModelManager.CacheDataSource["OA.ParentModule"] = GetParentModules;
            CacheListModelManager.CacheDataSource["OA.User"] = GetUsers;
            CacheListModelManager.CacheDataSource["OA.ReckoningName"] = GetReckoningNames;
            CacheListModelManager.CacheDataSource["OA.AccountItem"] = GetAccountItems;
            CacheListModelManager.CacheDataSource["OA.ParentRole"] = GetAccountItems;
            //父模块 自关联  无法 更新 bug
        }
        
        public static object FindList(OAFlag flag,int page, int size)
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    return AutoMapperObjectMapper.Empty.Map<List<AccountItemViewModel>>(OANHibernateManager.FindList<AccountItemInfo>(page, size));
                case OAFlag.AuthorityOperator:
                    return AutoMapperObjectMapper.Empty.Map<List<AuthorityOperatorViewModel>>(OANHibernateManager.FindList<AuthorityOperatorInfo>(page, size));
                case OAFlag.BringUpContent:
                    return AutoMapperObjectMapper.Empty.Map<List<BringUpContentViewModel>>(OANHibernateManager.FindList<BringUpContentInfo>(page, size));
                case OAFlag.BringUpPerson:
                    return AutoMapperObjectMapper.Empty.Map<List<BringUpPersonViewModel>>(OANHibernateManager.FindList<BringUpPersonInfo>(page, size));
                case OAFlag.Duty:
                    return AutoMapperObjectMapper.Empty.Map<List<DutyViewModel>>(OANHibernateManager.FindList<DutyInfo>(page, size));
                case OAFlag.FamousRace:
                    return AutoMapperObjectMapper.Empty.Map<List<FamousRaceViewModel>>(OANHibernateManager.FindList<FamousRaceInfo>(page, size));
                case OAFlag.Module:
                    return AutoMapperObjectMapper.Empty.Map<List<ModuleViewModel>>(OANHibernateManager.FindList<ModuleInfo>(page, size));
                case OAFlag.Person: 
                    return AutoMapperObjectMapper.Empty.Map<List<PersonViewModel>>(OANHibernateManager.FindList<PersonInfo>(page, size));
                case OAFlag.ReawrdsAndPunishment:
                    return AutoMapperObjectMapper.Empty.Map<List<ReawrdsAndPunishmentViewModel>>(OANHibernateManager.FindList<ReawrdsAndPunishmentInfo>(page, size));
                case OAFlag.ReckoningList:
                    return AutoMapperObjectMapper.Empty.Map<List<ReckoningListViewModel>>(OANHibernateManager.FindList<ReckoningListInfo>(page, size));
                case OAFlag.ReckoningName:
                    return AutoMapperObjectMapper.Empty.Map<List<ReckoningNameViewModel>>(OANHibernateManager.FindList<ReckoningNameInfo>(page, size));
                case OAFlag.Reckoning:
                    return AutoMapperObjectMapper.Empty.Map<List<ReckoningViewModel>>(OANHibernateManager.FindList<ReckoningInfo>(page, size));
                case OAFlag.Role:
                    return AutoMapperObjectMapper.Empty.Map<List<RoleViewModel>>(OANHibernateManager.FindList<RoleInfo>(page, size));
                case OAFlag.TimeCard:
                    return AutoMapperObjectMapper.Empty.Map<List<TimeCardViewModel>>(OANHibernateManager.FindList<TimeCardInfo>(page, size));
                case OAFlag.User:
                    return AutoMapperObjectMapper.Empty.Map<List<UserViewModel>>(OANHibernateManager.FindList<UserInfo>(page, size));
                default:
                    throw new NotSupportedException();
            }
        }
        public static int Count(OAFlag flag)
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    return OANHibernateManager.Count<AccountItemInfo>();
                case OAFlag.AuthorityOperator:
                    return OANHibernateManager.Count<AuthorityOperatorInfo>();
                case OAFlag.BringUpContent:
                    return OANHibernateManager.Count<BringUpContentInfo>();
                case OAFlag.BringUpPerson:
                    return OANHibernateManager.Count<BringUpPersonInfo>();
                case OAFlag.Duty:
                    return OANHibernateManager.Count<DutyInfo>();
                case OAFlag.FamousRace:
                    return OANHibernateManager.Count<FamousRaceInfo>();
                case OAFlag.Module:
                    return OANHibernateManager.Count<ModuleInfo>();
                case OAFlag.Person:
                    return OANHibernateManager.Count<PersonInfo>();
                case OAFlag.ReawrdsAndPunishment:
                    return OANHibernateManager.Count<ReawrdsAndPunishmentInfo>();
                case OAFlag.ReckoningList:
                    return OANHibernateManager.Count<ReckoningListInfo>();
                case OAFlag.ReckoningName:
                    return OANHibernateManager.Count<ReckoningNameInfo>();
                case OAFlag.Reckoning:
                    return OANHibernateManager.Count<ReckoningInfo>();
                case OAFlag.Role:
                    return OANHibernateManager.Count<RoleInfo>();
                case OAFlag.TimeCard:
                    return OANHibernateManager.Count<TimeCardInfo>();
                case OAFlag.User:
                    return OANHibernateManager.Count<UserInfo>();
                default:
                    throw new NotSupportedException();
            }
        }
        public static int Add(OAFlag flag,object  obj) 
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<AccountItemInfo>(obj));
                case OAFlag.AuthorityOperator:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<AuthorityOperatorInfo>(obj));
                case OAFlag.BringUpContent:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<BringUpContentInfo>(obj));
                case OAFlag.BringUpPerson:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<BringUpPersonInfo>(obj));
                case OAFlag.Duty:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<DutyInfo>(obj));
                case OAFlag.FamousRace:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<FamousRaceInfo>(obj));
                case OAFlag.Module:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<ModuleInfo>(obj));
                case OAFlag.Person:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<PersonInfo>(obj));
                case OAFlag.ReawrdsAndPunishment:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<ReawrdsAndPunishmentInfo>(obj));
                case OAFlag.ReckoningList:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<ReckoningListInfo>(obj));
                case OAFlag.ReckoningName:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<ReckoningNameInfo>(obj));
                case OAFlag.Reckoning:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<ReckoningInfo>(obj));
                case OAFlag.Role:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<RoleInfo>(obj));
                case OAFlag.TimeCard:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<TimeCardInfo>(obj));
                case OAFlag.User:
                    return OANHibernateManager.Add(AutoMapperObjectMapper.Empty.Map<UserInfo>(obj));
                default:
                    throw new NotSupportedException();
            }
        }

        public static void Save(OAFlag flag,Object  obj) 
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<AccountItemInfo>(obj));
                    break;
                case OAFlag.AuthorityOperator:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<AuthorityOperatorInfo>(obj));
                    break;
                case OAFlag.BringUpContent:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<BringUpContentInfo>(obj));
                    break;
                case OAFlag.BringUpPerson:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<BringUpPersonInfo>(obj));
                    break;
                case OAFlag.Duty:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<DutyInfo>(obj));
                    break;
                case OAFlag.FamousRace:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<FamousRaceInfo>(obj));
                    break;
                case OAFlag.Module:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<ModuleInfo>(obj));
                    break;
                case OAFlag.Person:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<PersonInfo>(obj));
                    break;
                case OAFlag.ReawrdsAndPunishment:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<ReawrdsAndPunishmentInfo>(obj));
                    break;
                case OAFlag.ReckoningList:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<ReckoningListInfo>(obj));
                    break;
                case OAFlag.ReckoningName:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<ReckoningNameInfo>(obj));
                    break;
                case OAFlag.Reckoning:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<ReckoningInfo>(obj));
                    break;
                case OAFlag.Role:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<RoleInfo>(obj));
                    break;
                case OAFlag.TimeCard:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<TimeCardInfo>(obj));
                    break;
                case OAFlag.User:
                    OANHibernateManager.Save(AutoMapperObjectMapper.Empty.Map<UserInfo>(obj));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public static void Delete(OAFlag flag,object obj) 
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<AccountItemInfo>(obj));
                    break;
                case OAFlag.AuthorityOperator:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<AuthorityOperatorInfo>(obj));
                    break;
                case OAFlag.BringUpContent:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<BringUpContentInfo>(obj));
                    break;
                case OAFlag.BringUpPerson:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<BringUpPersonInfo>(obj));
                    break;
                case OAFlag.Duty:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<DutyInfo>(obj));
                    break;
                case OAFlag.FamousRace:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<FamousRaceInfo>(obj));
                    break;
                case OAFlag.Module:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ModuleInfo>(obj));
                    break;
                case OAFlag.Person:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<PersonInfo>(obj));
                    break;
                case OAFlag.ReawrdsAndPunishment:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReawrdsAndPunishmentInfo>(obj));
                    break;
                case OAFlag.ReckoningList:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReckoningListInfo>(obj));
                    break;
                case OAFlag.ReckoningName:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReckoningNameInfo>(obj));
                    break;
                case OAFlag.Reckoning:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReckoningInfo>(obj));
                    break;
                case OAFlag.Role:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<RoleInfo>(obj));
                    break;
                case OAFlag.TimeCard:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<TimeCardInfo>(obj));
                    break;
                case OAFlag.User:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<UserInfo>(obj));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public static void Delete(OAFlag flag, object[] objs)
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<AccountItemInfo[]>(objs));
                    break;
                case OAFlag.AuthorityOperator:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<AuthorityOperatorInfo[]>(objs));
                    break;
                case OAFlag.BringUpContent:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<BringUpContentInfo[]>(objs));
                    break;
                case OAFlag.BringUpPerson:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<BringUpPersonInfo[]>(objs));
                    break;
                case OAFlag.Duty:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<DutyInfo[]>(objs));
                    break;
                case OAFlag.FamousRace:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<FamousRaceInfo[]>(objs));
                    break;
                case OAFlag.Module:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ModuleInfo[]>(objs));
                    break;
                case OAFlag.Person:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<PersonInfo[]>(objs));
                    break;
                case OAFlag.ReawrdsAndPunishment:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReawrdsAndPunishmentInfo[]>(objs));
                    break;
                case OAFlag.ReckoningList:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReckoningListInfo[]>(objs));
                    break;
                case OAFlag.ReckoningName:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReckoningNameInfo[]>(objs));
                    break;
                case OAFlag.Reckoning:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<ReckoningInfo[]>(objs));
                    break;
                case OAFlag.Role:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<RoleInfo[]>(objs));
                    break;
                case OAFlag.TimeCard:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<TimeCardInfo[]>(objs));
                    break;
                case OAFlag.User:
                    OANHibernateManager.Delete(AutoMapperObjectMapper.Empty.Map<UserInfo[]>(objs));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static List<ItemViewModel> Roles;
        private static List<ItemViewModel> Contents;
        private static List<ItemViewModel> Records;
        private static List<ItemViewModel> ParentModules;
        private static List<ItemViewModel> Users;
        private static List<ItemViewModel> ReckoningNames;
        private static List<ItemViewModel> AccountItems;
        private static List<ItemViewModel> ParentRoles;
        public static List<ItemViewModel> GetRole(bool load = false)
        {
            if (load || Roles == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<RoleInfo>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                Roles = data;
            }
            return Roles;
        }
        public static List<ItemViewModel> GetContent(bool load = false)
        {
            if (load || Contents == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<BringUpContentInfo>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                Contents = data;
            }
            return Contents;
        }
        public static List<ItemViewModel> GetRecord(bool load = false)
        {
            if (load || Records == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<RecordInfo>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                Records = data;
            }
            return Records;
        }
        public static List<ItemViewModel> GetParentModules(bool load = false)
        {
            if (load || ParentModules == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<ModuleInfo>(it=>it.Parent==null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                ParentModules = data;
            }
            return ParentModules;
        }
        public static List<ItemViewModel> GetUsers(bool load = false)
        {
            if (load || Users == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<UserInfo>(null).Select(it => new ItemViewModel() { Label = it.Account, Value = it.Id }).ToList();
                Users = data;
            }
            return Users;
        }
        public static List<ItemViewModel> GetReckoningNames(bool load = false)
        {
            if (load || ReckoningNames == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<ReckoningNameInfo>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                ReckoningNames = data;
            }
            return ReckoningNames;
        }
        public static List<ItemViewModel> GetAccountItems(bool load = false)
        {
            if (load || AccountItems == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<AccountItemInfo>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                AccountItems = data;
            }
            return AccountItems;
        }
        public static List<ItemViewModel> GetParentRoles(bool load = false)
        {
            if (load || ParentRoles == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Find<RoleInfo>(it=>it.Roles==null||it.Roles.Count==0).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                ParentRoles = data;
            }
            return ParentRoles;
        }
    }
}
