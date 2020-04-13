using Utility;

namespace Utility
{
    public enum ObjectState { 
        Active,
        Idea
    }
    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        [Desc(ChineseDesc = "中文", EnglishDesc = "Chinese")]
        Chinese,
        [Desc(ChineseDesc = "英文", EnglishDesc = "English")]
        English
    }
    #region 返回码 及描述
    /// <summary>
    /// 返回码 及描述
    /// </summary>
    public enum Code
    {
        None,
        [Desc(ChineseDesc = "成功", EnglishDesc = "sucess")]
        Success =20000,
        [Desc(ChineseDesc = "添加成功", EnglishDesc = "add sucess")]
        AddSuccess =20001,
        [Desc(ChineseDesc = "编辑成功", EnglishDesc = "modify sucess")]
        ModifySuccess =20002,
        [Desc(ChineseDesc = "删除成功", EnglishDesc = "delete sucess")]
        DeleteSuccess = 20003,
        [Desc(ChineseDesc = "查询成功", EnglishDesc = "query sucess")]
        QuerySuccess = 20004,
        [Desc(ChineseDesc = "登录成功", EnglishDesc = "login sucess")]
        LoginSuccess =20005,
        [Desc(ChineseDesc = "退出成功", EnglishDesc = "logout sucess")]
        LogoutSuccess = 20005,
        [Desc(ChineseDesc = "上传文件成功", EnglishDesc = "upload file sucess")]
        UploadFileSuccess = 20006,
        [Desc(ChineseDesc = "操作成功", EnglishDesc = "operator sucess")]
        OperatorSuccess = 20007,
        [Desc(ChineseDesc = "失败", EnglishDesc = "fail")]
        Fail = 40000,
        [Desc(ChineseDesc = "系统繁忙", EnglishDesc = "system error")]
        Error = 50000,
        [Desc(ChineseDesc = "参数错误", EnglishDesc = "param  error")]
        ParamError =40001,
        [Desc(ChineseDesc = "参数不能为空", EnglishDesc = "param is not null")]
        ParamNotNull =40002,
        [Desc(ChineseDesc = "认证失败", EnglishDesc = "auth fail")]
        AuthFail =40003,
        [Desc(ChineseDesc = "登录失败", EnglishDesc = "login fail")]
        LoginFail =40004,
        [Desc(ChineseDesc = "id 找不到", EnglishDesc = "id not found")]
        IdNotFound =40005,
        [Desc(ChineseDesc = "级联删除失败", EnglishDesc = "cascde delete fail")]
        CascadeDeleteFail =40006, 
        [Desc(ChineseDesc = "上传文件失败", EnglishDesc = "upload file fail")]
        UploadFileFail = 40007,
        [Desc(ChineseDesc = "不支持的操作", EnglishDesc = "not support operator")]
        NotSupport =40008,
        [Desc(ChineseDesc = "操作失败", EnglishDesc = "operator fail")]
        OperatorFail = 40009, 
        [Desc(ChineseDesc = "token不能为空", EnglishDesc = "token not null")]
        TokenNotNull = 40010, 
        [Desc(ChineseDesc = "token失效", EnglishDesc = "token expires")]
        TokenExpires = 40011,
        [Desc(ChineseDesc = "删除失败", EnglishDesc = "delete fail")]
        DeleteFail = 40012,
        [Desc(ChineseDesc = "不支持的操作", EnglishDesc = "not support operator")]
        NotSupportOperator = 40013,
        [Desc(ChineseDesc = "不存在", EnglishDesc = "not exists")]
        NotExists = 40014,
        /// <summary>
        /// 参数格式错误
        /// </summary>
        [Desc(ChineseDesc = "参数错误", EnglishDesc = "param  error")]
        参数错误 = 40000,
        /// <summary>
        /// 参数不能为空
        /// </summary>
        [Desc(ChineseDesc = "参数不能为空", EnglishDesc = "param is not null")]
        参数不能为空 = 401,
        /// <summary>
        /// 参数格式错误
        /// </summary>
        [Desc(ChineseDesc = "参数格式错误", EnglishDesc = "param format error")]
        参数格式错误 = 402,
        /// <summary>
        /// 不支持的错误
        /// </summary>
        [Desc(ChineseDesc = "不支持的错误", EnglishDesc = "not support error")]
        不支持的错误 = 403,
        /// <summary>
        /// 操作成功
        /// </summary>
        [Desc(ChineseDesc = "成功", EnglishDesc = "success")]
        成功 = 200,
        /// <summary>
        /// 操作失败
        /// </summary>
        [Desc(ChineseDesc = "失败", EnglishDesc = "fail")]
        失败 = 404,
        /// <summary>
        /// 系统内部错误
        /// </summary>
        [Desc(ChineseDesc = "系统内部错误", EnglishDesc = "system inner error")]
        系统内部错误 = 500,
        [Desc(ChineseDesc = "存在", EnglishDesc = "exists")]
        存在 = 201,
        [Desc(ChineseDesc = "不存在", EnglishDesc = "not exists")]
        不存在 = 202,
        [Desc(ChineseDesc = "手机号已存在", EnglishDesc = "phone exists")]
        手机号已存在 = 203,
        [Desc(ChineseDesc = "邮箱已存在", EnglishDesc = "email exists")]
        邮箱已存在 = 204,
        [Desc(ChineseDesc = "账号错误", EnglishDesc = "account error")]
        账号错误 = 205,
        [Desc(ChineseDesc = "注册成功", EnglishDesc = "register success")]
        注册成功 = 400,
        [Desc(ChineseDesc = "密码不一致", EnglishDesc = "password not eq")]
        密码不一致 = 206,
        [Desc(ChineseDesc = "登录成功", EnglishDesc = "login success")]
        登录成功 = 300,
        [Desc(ChineseDesc = "密码错误", EnglishDesc = "password error")]
        密码错误 = 207,
        [Desc(ChineseDesc = "账号不存在", EnglishDesc = "account not exists")]
        账号不存在 = 208,
        [Desc(ChineseDesc = "账号存在", EnglishDesc = "account exists")]
        账号存在 = 209,
        [Desc(ChineseDesc = "发送成功", EnglishDesc = "send success")]
        发送成功 = 210,
        [Desc(ChineseDesc = "发送失败", EnglishDesc = "send fail")]
        发送失败 = 211,
    }
    #endregion 返回码 及描述
}
