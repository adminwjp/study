
using OA.Domain.Core;
using System;
using System.Linq.Expressions;
using Utility.Domain.Uow;

namespace Repository.Service
{
    /// <summary>
    /// 人事管理
    /// </summary>
    public class PersonnelMattersManager
    {
        readonly IUnitWork _unitWork;
        public PersonnelMattersManager(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        #region  档案管理
        public bool InsertPerson(PersonInfo personInfo)
        {
            _unitWork.Insert(personInfo);
            return true;
        }
        public bool InsertRecord(RecordInfo  recordInfo)
        {
            _unitWork.Insert(recordInfo);
            return true;
        }
        public bool ModifyPerson(Expression<Func<PersonInfo, bool>> where, Expression<Func<PersonInfo, PersonInfo>> update)
        {
            _unitWork.Update(where,update);
            return true;
        }
        public bool ModifyRecord(Expression<Func<RecordInfo, bool>> where, Expression<Func<RecordInfo, RecordInfo>> update)
        {
            _unitWork.Update(where, update);
            return true;
        }
        /// <summary>
        /// 新建个人档案信息
        /// </summary>
        /// <param name="personInfo"></param>
        /// <param name="recordInfo"></param>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public bool InerstArchives(PersonInfo personInfo, RecordInfo recordInfo)
        {
            return false;
        }
        /// <summary>
        /// 修改个人档案信息
        /// </summary>
        /// <param name="wherePerson"></param>
        /// <param name="updatePerson"></param>
        /// <param name="whereRecord"></param>
        /// <param name="updateRecord"></param>
        /// <param name="whereJob"></param>
        /// <param name="updateJob"></param>
        /// <returns></returns>
        public bool ModifyArchives(Expression<Func<PersonInfo, bool>> wherePerson, Expression<Func<PersonInfo, PersonInfo>> updatePerson, 
            Expression<Func<RecordInfo, bool>> whereRecord, Expression<Func<RecordInfo, RecordInfo>> updateRecord)
        {
            return ModifyPerson(wherePerson,updatePerson) && ModifyRecord(whereRecord,updateRecord) ;
        }
        #endregion 档案管理
        
        #region 考勤管理
        
        #endregion 考勤管理

    }
}
