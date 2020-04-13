/// <reference path="js.cookie.js" />
//var newscript = document.createElement('script');
//newscript.setAttribute('type', 'text/javascript');
//newscript.setAttribute('src', '/js/js.cookie.js');
//var head = document.getElementsByTagName('head')[0];
//head.appendChild(newscript);
/*5.加载文件*/
/* 已加载文件缓存列表,用于判断文件是否已加载过，若已加载则不再次加载*/
var classcodes = [];
window.Import = {
    /*加载一批文件，_files:文件路径数组,可包括js,css,less文件,succes:加载成功回调函数*/
    LoadFileList: function (_files, succes) {
        var FileArray = [];
        if (typeof _files === "object") {
            FileArray = _files;
        } else {
            /*如果文件列表是字符串，则用,切分成数组*/
            if (typeof _files === "string") {
                FileArray = _files.split(",");
            }
        }
        if (FileArray != null && FileArray.length > 0) {
            var LoadedCount = 0;
            for (var i = 0; i < FileArray.length; i++) {
                loadFile(FileArray[i], function () {
                    LoadedCount++;
                    if (LoadedCount == FileArray.length) {
                        succes();
                    }
                })
            }
        }
        /*加载JS文件,url:文件路径,success:加载成功回调函数*/
        function loadFile(url, success) {
            if (!FileIsExt(classcodes, url)) {
                var ThisType = GetFileType(url);
                var fileObj = null;
                if (ThisType == ".js") {
                    fileObj = document.createElement('script');
                    fileObj.src = url;
                } else if (ThisType == ".css") {
                    fileObj = document.createElement('link');
                    fileObj.href = url;
                    fileObj.type = "text/css";
                    fileObj.rel = "stylesheet";
                } else if (ThisType == ".less") {
                    fileObj = document.createElement('link');
                    fileObj.href = url;
                    fileObj.type = "text/css";
                    fileObj.rel = "stylesheet/less";
                }
                success = success || function () { };
                fileObj.onload = fileObj.onreadystatechange = function () {
                    if (!this.readyState || 'loaded' === this.readyState || 'complete' === this.readyState) {
                        success();
                        classcodes.push(url)
                    }
                }
                document.getElementsByTagName('head')[0].appendChild(fileObj);
            } else {
                success();
            }
        }
        /*获取文件类型,后缀名，小写*/
        function GetFileType(url) {
            if (url != null && url.length > 0) {
                return url.substr(url.lastIndexOf(".")).toLowerCase();
            }
            return "";
        }
        /*文件是否已加载*/
        function FileIsExt(FileArray, _url) {
            if (FileArray != null && FileArray.length > 0) {
                var len = FileArray.length;
                for (var i = 0; i < len; i++) {
                    if (FileArray[i] == _url) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

var FilesArray = [];
Import.LoadFileList(FilesArray, function () {
    /*这里写加载完成后需要执行的代码或方法*/
});
const baseUrl = "https://localhost:44328/admin/api/v1/";
//const baseUrl = "https://localhost:5002/admin/api/v1/";
//通用vue 方法
const service = axios.create({
    baseURL: baseUrl, // url = base url + request url
    // withCredentials: true, // send cookies when cross-domain requests
    timeout: 5000 // request timeout
});
//request interceptor
axios.interceptors.request.use(
    config => {
        // do something before request is sent
        var to = getToken();
        if (to) config.headers['token'] = to;
        return config;
    },
    error => {
        // do something with request error
        console.log(error); // for debug
        return Promise.reject(error);
    }
);
/***
 * 
 * */
const ContentTypeJson = "application/json";
const ContentTypeForm = "application/x-www-form-urlencoded";
const ContentTypeMultipart = "multipart/form-data";
const ContentTypeTextXml = "text/xml";

function BasicUrl(controller, options) {
    this.add = getUrl(controller + "/add");
    this.edit = getUrl(controller + "/edit");
    this.delete = getUrl(controller + "/delete");
    this.query = getUrl(controller + "/query");
    this.operator = getUrl(controller + "/operator");
    this.category = getUrl(controller + "/category");
    if (options) {
        for (const key in options) {
            this[key] = options[key];
        }
    }
}
const urls = {
    role: new BasicUrl("role", {
        parent_category: getUrl("role/parent_category"),
        required: getUrl("role/required")
    }),
    admin: new BasicUrl("admin", {
        role_required: getUrl("admin/role_required"),
        login: getUrl("admin/login")
    }),
    user: new BasicUrl("user"),
    jobcategory: new BasicUrl("jobcategory"),
    likecategory: new BasicUrl("likecategory"),
    like: new BasicUrl("like"),

    skillcategory: new BasicUrl("skillcategory"), skill: new BasicUrl("skill"),
    filecategory: new BasicUrl("filecategory"),
    file: new BasicUrl("file"),
    edutioncategory: new BasicUrl("edutioncategory"),
    userstatus: new BasicUrl("userstatus"),
    userlevel: new BasicUrl("userlevel"),
    usertagcategory: new BasicUrl("usertagcategory"),
    usertag: new BasicUrl("usertag"),
    usermaritalstatus: new BasicUrl("maritalstatus"),
    menu: new BasicUrl("menu", {
        parent_category: getUrl("menu/parent_category")
    }),
    icon: new BasicUrl("icon"), 
    work: new BasicUrl("work"),
    usermenu: new BasicUrl("usermenu", {
        init: getUrl("usermenu/init")
    })
};
/**
 * 拼接请求地址  "https://localhost:44328/admin/api/v1/" +url
 * @param {any} url 地址
 * @returns 
 */
function getUrl(url) {
    return baseUrl + url + "";
}
//token 信息 存储cookie中 获取 localStorage中
const tokenKey = "token";
/**
 * 获取token缓存信息
 * @returns
 */
function getToken() {
    return !Cookies ? localStorage.getItem(tokenKey) : Cookies.get(tokenKey);
}
/**
 * 设置token缓存信息
 * @param token toekn信息
 * @returns
 */
function setToken(token) {
    if (!Cookies) localStorage.setItem(tokenKey, token);
    else Cookies.set(tokenKey, token);
}
/**
 * 移除token缓存信息
 * @returns
 */
function removeToken() {
    if (!Cookies) localStorage.removeItem(tokenKey);
    else Cookies.remove(tokenKey);
}

/**
 * 网络图像文件转Base64
 */
function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0, img.width, img.height);
    var ext = img.src.substring(img.src.lastIndexOf(".") + 1).toLowerCase();
    var dataURL = canvas.toDataURL("image/" + "jpg");
    return dataURL;
}
/**
*Base64字符串转二进制
*/
function dataURLtoBlob(dataurl) {
    var arr = dataurl.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], {
        type: mime
    });
}
 function parseParam(obj, skipOptions, timeOptions) {
     return parse("", obj, skipOptions, timeOptions);
}

 function parse(prefix, obj, skipOptions, timeOptions) {
    var str = "";
    for (var i in obj) {
        if (skipOptions && skipOptions.indexOf(i) > -1) continue;
        if (obj[i] instanceof Date) {
            str += encodeURI(prefix + i.toString()) + "=";
            str += encodeURI(parseDate(obj[i.toString()]))+"&";
        }
        else if (obj[i] instanceof Array) {
            if (obj[i] && obj[i].length > 0) {
                obj[i].map(it => {
                    str += encodeURI(prefix + i.toString()) + "=";
                    if (it) {
                        if (timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                            str += encodeURI(parseDate(it));
                        } else {
                            str += encodeURI(it);
                        }
                    }
                    str += "&";
                });
            }
            // else {
            //   str += encodeURI(prefix + i.toString()) + "=" + encodeURI("[]") + "&";
            //   str += encodeURI(prefix + i.toString()) + "=&";
            //   str += encodeURI(prefix + i.toString()) + "=&";
            // }
        } else if (obj[i] instanceof Object) {
            str += parse(prefix + i.toString() + ".", obj[i]) + "&";
        } else {
            str += encodeURI(prefix + i.toString()) + "=";
            if (obj[i.toString()] && timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                str += encodeURI(parseDate(obj[i.toString()]));
            } else if (obj[i.toString()]) {
                str += encodeURI(obj[i.toString()]);
            }
            str += "&";
        }
    }
    str = str.substring(0, str.length - 1);
    return str;
}

 function parseRequestParam(requestContentType, param, skipOptions, timeOptions) {
    var paramOptions = {};
    if (requestContentType == 1) {
        paramOptions.parem = param;
        paramOptions.contenType = ContentTypeJson;
    } else if (requestContentType == 2) {
        paramOptions.parem = parseParam(param, skipOptions, timeOptions);
        paramOptions.contenType = ContentTypeForm;
    } else {
        var formData = new FormData();
        parseFormData(formData, "", param, skipOptions, timeOptions);
        paramOptions.parem = formData;
        paramOptions.contenType = ContentTypeMultipart;
    }
    return paramOptions;
}

 function parseDate(date) {
    var dateObj = new Date(date);
     var time = dateObj.getFullYear() + "-" + (dateObj.getMonth() + 1) + "-" + dateObj.getDate() + " " + dateObj.getHours() + ":" +
         dateObj.getMinutes() + ":" + dateObj.getSeconds();
    return time;
}
 function parseFormData(formData, prefix, obj, skipOptions, timeOptions) {
    var str = "";
    for (var i in obj) {
        if (skipOptions && skipOptions.indexOf(i) > -1) continue;
        if (obj[i] instanceof Date) {
            formData.append(prefix + i.toString(), parseDate(obj[i.toString()]));
        }
        else if (obj[i] instanceof Array) {
            if (obj[i] && obj[i].length > 0) {
                obj[i].map(it => {
                    if (timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                        formData.append(prefix + i.toString(), parseDate(it));
                    } else {
                        formData.append(prefix + i.toString(), it.toString());
                    }
                });
            }
            // else {
            //   formData.append(prefix + i.toString() , encodeURI("[]"));
            //   formData.append(prefix + i.toString() , null);
            //   formData.append(prefix + i.toString(), null);
            // }
        } else if (obj[i] instanceof Object) {
            str += parseFormData(formData, prefix + i.toString() + ".", obj[i]);
        } else {
            if (obj[i.toString()] && timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                formData.append(prefix + i.toString(), parseDate(obj[i.toString()]));
            } else if (obj[i.toString()]) {
                formData.append(prefix + i.toString(), obj[i.toString()]);
            }
        }
    }
    str = str.substring(0, str.length - 1);
    return str;
}
function dataURLtoFile(dataurl, filename) {//将base64转换为文件
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
}
function Gibson(options) {
    this.el = '';
    this.data = {
        request_content_type: 2, // 1 json 方式请求 2 from 请求方式 3 from data 请求 方式
        createDatePickerOptions: {
            shortcuts: [{
                text: '最近一周',
                onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                    picker.$emit('pick', [start, end]);
                }
            }, {
                text: '最近一个月',
                onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                    picker.$emit('pick', [start, end]);
                }
            }, {
                text: '最近三个月',
                onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                    picker.$emit('pick', [start, end]);
                }
            }]
        },
        test: true,//测试用的
        //操作地址
        operatorUrl: {
            query: '', //查询数据地址
            add: '', //提交数据url
            modify: '', //编辑数据url
            delete: '' //删除数据url
        },
        isOperatorRequest: false, //允许操作请求访问
        controller: '', //控制名称
        //是否有增删改查权限
        operator: {
            add: true,
            modify: true,
            delete: true,
            query: true,
        },
        tableData: [], //查询列表数据
        loading: true, //查询列表数据是否成功 false 成功
        disabled: false, //表单是否禁用 预览时禁用
        multipleSelection: [], //复选框多条删除记录或列表删除记录
        //dialog 弹出框信息
        dialog: {
            visible: false, //dialog 是否可用 true 可用
            title: '', //dialog 弹出框信息标题
            submitText: '', //dialog 弹出框 表单提交按钮文本
            resetText: '重置' //dialog 弹出框 表单重置按钮文本
        },
        //查询表单信息
        formQuery: {},
        //表单信息提交
        formSubmit: {},
        //表单信息规则验证
        formRoleValidator: {},
        //分页记录
        page: {
            page: 1,
            size: 10,
            current_page: 1,
            total: 10,
            sizes: [10, 20, 30, 40]
        },
        //操作标识 1代表提交 2代表编辑 3 代表预览
        flag: 1,
        checkboxAllSelect: true, //是否选中列表复选框 true 选中
        checkboxAllSelectText: '全选', //是否选中列表复选框 提示文本 : 取消全选 或 全选
        labelPosition: 'right', //表单对齐方式 默认右对齐
        options :[],//分类数据
		baseUrl:baseUrl
    };
    this.created = function () {

    };
    this.mounted = function () {
        if (this.isOperatorRequest) {
            var $this = this;
            //增删改查权限
            get(getUrl(this.controller + "/operator"), function (response) { $this.operator = response.data.data; });
        }
        this.loadMounted();
        this.loadTableDataOrQueryDataEvent();
    };
    this.watched = function () {

    };
    this.methods = {
        handleCategoryQueryChangeEvent : function (val) {
            this.formQuery.id = val;
        },
        handleCategoryQueryVisableChangeEvent : function (val) {
            if (val) {
                this.queryCategoryEvent();
            }
        },
       queryCategoryEvent : function () {
           var $this = this;
           get(getUrl(this.controller + "/category"), response => { $this.options = response.data.data; });
        },
        //重新加载数据
        reload : function () {

        },
        //取消其他操作
        quitOtherOperatorEvent: function () {

        },
        //vue装载完成
        loadMounted: function () {

        },
        //提示
        tip: function (msg, title, confirmName, type, confirmTip, CancelTip) {
            this.$alert(msg, title, {
                confirmButtonText: confirmName,
                callback: action => {
                    //if (action == 'confirm') {
                    //    this.$message({
                    //        type: type,
                    //        message: confirmTip
                    //    });
                    //}
                    //else {
                    //    this.$message({
                    //        type:type,
                    //        message: CancelTip
                    //    });
                    //}
                }
            });
        },
        //查询列表数据 或查询按钮事件
        loadTableDataOrQueryDataEvent: function () {
            var $this = this;
            var paramOptions = parseRequestParam($this.request_content_type, $this.formQuery, undefined, this.queryTimeField());
            post($this.operatorUrl.query + "?page=" + $this.page.current_page + "&size=" + $this.page.size, paramOptions.parem, paramOptions.contenType, function (response) {
                $this.tableData = !response.data.data.data ? [] : response.data.data.data;
                $this.page.size = response.data.data.result.size;
                $this.page.page = response.data.data.result.page;
                $this.page.total = response.data.data.result.total;
                $this.loading = false;
            });
        },
        //表单查询时 时间需要转换
        queryTimeField: function () {
            if (this.request_content_type != 1) {
                //时间转换
                return ["create_date", "update_date"];
            }
            return [];
        },
        //提交表单事件 添加或编辑按钮提交事件
        submitFormEvent: function (formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    var result = false;
                    if (this.flag == 3) return result;
                    this.quitOtherOperatorEvent();
                    var $this = this;
                    var url = this.flag == 1 ? this.operatorUrl.add : this.operatorUrl.modify;
                    var paramOptions = parseRequestParam($this.request_content_type, $this.formSubmit);
                    post(url, paramOptions.parem, paramOptions.contenType, function (response) {
                        result = response.data.success;
                        if (result) {
                            $this.$nextTick(() => {
                                $this.loadTableDataOrQueryDataEvent(); //重新加载列表数据
                                $this.reload();
                                $this.dialog.visible = false;
                            });
                        }
                        //提示
                        $this.tip(response.data.message, '提示：', '确定',
                            'info', '确定!', '关闭!');
                    }, response => $this.tip(response.data.message, '提示：', '确定',
                        'info', '确定!', '关闭!'));
                    return result;
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        },
        //重置表单事件
        resetFormEvent: function (formName) {
            if (this.$refs[formName]) {
                this.$refs[formName].resetFields();
            }
        },
        //禁用表单
        disabledFormEvent: function (formName) {
            if (this.$refs[formName]) {
                this.$refs[formName].disabled = this.disabled;
            }
        },
        //切换表单复选框是否选中 事件 即取消全选或 全选 按钮事件
        toggleSelectionTableCheckBoxButtonEvent: function (rows) {
            var flag = rows ? true : false;
            this.checkboxAllSelectTextChanngeEvent(flag);
            if (flag) {
                rows.forEach(row => {
                    this.$refs.multipleTable.toggleRowSelection(row);
                });
            } else {
                this.$refs.multipleTable.clearSelection();
            }
        },
        //复选框多选或列表中编辑、删除、查询事件 即当选择项发生变化时会触发该事件
        handleSelectionChange: function (val) {
            this.checkboxAllSelectTextChanngeEvent(val.length > 0);
            this.multipleSelection = val;
        },
        //取消全选或 全选 按钮 文本改变事件
        checkboxAllSelectTextChanngeEvent(val) {
            if (val) {
                this.checkboxAllSelect = false;
                this.checkboxAllSelectText = "取消全选";
            } else {
                this.checkboxAllSelect = true;
                this.checkboxAllSelectText = "全选";
            }
        },
        //关闭dialog
        handleDialogClose: function (done) {
            this.$confirm('确认关闭？')
                .then(_ => {
                    done();
                })
                .catch(_ => { });
        },
        //预览事件(列表)  清空表单 重新赋值表单
        handleSelectClickEvent: function (row) {
            this.submitTextSelectChanngeEevent();
            this.flag = 3;
            this.cleanFormToSetValue(row);
        },
        //清空表单 重新赋值表单
        cleanFormToSetValue: function (row) {
            this.dialog.visible = true;
            var $this = this;
            this.$nextTick(() => {
                $this.resetFormEvent('formSubmit');
                //$this.disabledFormEvent('formSubmit');
                $this.setValue(row);
            });
        },
        //预览表单 dialog 提示改变 表单 按钮文本改变
        submitTextSelectChanngeEevent: function () {

        },
        // 表单编辑按钮事件(列表) 清空表单 重新赋值表单
        handleModifyClickEvent: function (row) {
            this.submitTextModifyChanngeEevent();
            this.flag = 2;
            this.cleanFormToSetValue(row);
        },
        //编辑表单 dialog 提示改变 表单 按钮文本改变
        submitTextModifyChanngeEevent: function () {

        },
        //删除按钮事件(列表)
        handleDeleteClickEvent: function (row) {
            if (row.id) {
                var $this = this;
                this.handleDialogClose(function () {
                    $this.delete(row.id);
                });
            } else {
                this.tip('删除失败,id未找到!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            }
        },
        //每页记录改变是触发事件
        handleSizeChangeEvent: function (val) {
            this.page.size = val;
            this.loadTableDataOrQueryDataEvent();
        },
        //当前页数改变事件
        handleCurrentPageChangeEvent: function (val) {
            this.page.current_page = val;
            this.loadTableDataOrQueryDataEvent();
        },
        //添加按钮事件 清空表单
        insertButtonClickEvent: function () {
            this.submitTextInsertChanngeEevent();
            this.flag = 1;
            this.dialog.visible = true;
            this.$nextTick(() => {
                this.resetFormEvent('formSubmit');
            });
        },
        //添加表单 dialog 提示改变 表单 按钮文本改变
        submitTextInsertChanngeEevent: function () {

        },
        //更新提交表单值
        setValue: function (row) {

        },
        //编辑按钮事件  dialog 提示改变 表单 按钮文本改变
        modifyButtonClickEvent: function () {
            if (this.multipleSelection.length != 1) {
                this.tip('请选中一行数据进行编辑!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            } else {
                var row = this.multipleSelection[0];
                this.handleModifyClickEvent(row);
            }
        },
        //查看按钮事件  dialog 提示改变 表单 按钮文本改变
        queryButtonClickEvent: function () {
            if (this.multipleSelection.length != 1) {
                this.tip('请选中一行数据进行预览!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            } else {
                var row = this.multipleSelection[0];
                this.handleSelectClickEvent(row);
            }
        },
        //删除按钮点击事件
        deleteButtonClickEvent: function () {
            if (this.multipleSelection.length == 0) {
                this.tip('请选中至少一行数据进行删除!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            } else {
                if (this.multipleSelection.length == 1) {
                    var row = this.multipleSelection[0];
                    this.handleDeleteClickEvent(row);
                } else {
                    var $this = this;
                    this.handleDialogClose( ()=> {
                        var ids = [];
                        var rows = $this.multipleSelection;
                        rows.forEach(row => {
                            ids.push(row.id);
                        });
                        //var str = "<DeleteEntry><Ids>";
                        //ids.forEach(it => { str += "<int>" + it + "</int>" });
                        //str +="</Ids></DeleteEntry>"
                        var paramOptions = parseRequestParam($this.request_content_type, { ids: ids});
                        post($this.operatorUrl.delete, paramOptions.parem, paramOptions.contenType,
                            (response) => {
                            $this.loadTableDataOrQueryDataEvent();
                            $this.reload();
                        }, (response) => { $this.tip(response.data.message, '提示：', '确定', 'info', '确定!', '关闭!'); });
                    });
                }
            }
        },
        //根据id单删除
        delete: function (id) {
            var $this = this;
            get(this.operatorUrl.delete + "/" + id, function(response)  {
                $this.loadTableDataOrQueryDataEvent();
                $this.reload();
            },
             function (response)  { $this.tip(response.data.message, '提示：', '确定', 'info', '确定!', '关闭!'); });
        }
    }
}
function setAdmin(obj) {
    if (obj.data.test) {
        obj.data.formSubmit.admin = { id: '' };
        obj.data.admin_options = [];
        obj.methods.adminCategoryQueryEvent = function () {
            var $this = this;
            get(urls.admin.category, function (response) {
                if (response.data.success) {
                    $this.admin_options = response.data.data;
                }
            });
        };
        obj.methods.handleAdminChangeEvent = function (val) {
            this.formSubmit.admin.id = val;
        };
        obj.methods.handleAdminVisableChangeEvent = function (val) {
            if (val) {
                this.adminCategoryQueryEvent();
            }
        };
    }
}
function setUser(obj) {
    if (obj.data.test) {
        obj.data.formSubmit.user = { id: '' };
        obj.data.user_options = [];
        obj.methods.userCategoryQueryEvent = function () {
            var $this = this;
            get(urls.user.category, function (response) {
                if (response.data.success) {
                    $this.user_options = response.data.data;
                }
            });
        };
        obj.methods.handleUserChangeEvent = function (val) {
            this.formSubmit.user.id = val;
        };
        obj.methods.handleUserVisableChangeEvent = function (val) {
            if (val) {
                this.userCategoryQueryEvent();
            }
        };
    }
}
function setRoleCategory(obj) {
    obj.data.role_options = [];
    //角色分类查询事件
    obj.methods.roleQueryEvent = function () {
        var $this = this;
        get(urls.role.parent_category, function (response) { $this.role_options = response.data.data; });
    };
    //角色分类改变 下拉框出现/隐藏时触发 事件
    obj.methods.handleRoleVisableChangeEvent = function (value) {
        if (value) {
            this.roleQueryEvent();
        }
    };
    //表单 角色分类改变 当选中节点变化时触发事件
    obj.methods.handleRoleFormChangeEvent = function (value) {
        if (value instanceof Array) {
            this.formSubmit.role.id = value[value.length - 1];
        }
        else {
            this.formSubmit.role.id = value;
        }
    };
    //查询表单 角色分类改变 当选中节点变化时触发事件
    obj.methods.handleRoleQueryChangeEvent = function (value) {
        if (value instanceof Array) {
            this.formQuery.role_id = value[value.length - 1];
        }
        else {
            this.formQuery.role_id = value;
        }
    };
    //表单 角色分类改变 当选中节点点击时触发事件
    obj.methods.handleRoleFormClickEvent = function (value) {
        this.handleRoleFormChangeEvent(value);
        if (value) {
            if (this.$refs.refHandleForm) {
                this.$refs.refHandleForm.dropDownVisible = false; //监听值发生变化就关闭它
            }
        }
    };
    //查询表单 角色分类改变 当选中节点点击时触发事件
    obj.methods.handleRoleQueryClickEvent = function (value) {
        this.handleRoleQueryChangeEvent(value);
        if (value) {
            if (this.$refs.refHandleQuery) {
                this.$refs.refHandleQuery.dropDownVisible = false; //监听值发生变化就关闭它
            }
        }
    };
}
function get(url, result) {
    getTip(url, result);
}

function getTip(url, result, tip) {
    axios.get(url).then(response => {
        if (response.data.success) result(response);
        else console.log(response.data);
        if (tip) tip(response);
    }).catch(function (error) { // 请求失败处理
        console.log(error);
    });
}
function post(url, param, contenType, result,tip) {
    axios.post(url, param, { headers: { 'Content-Type': contenType } }).then(response => {
        if (response.data.success)
        {
            result(response);
        }
        if (tip) tip(response);
    }).catch(function (error) { // 请求失败处理
        console.log(error);
    });
}
//获取页面元素位置 
//注意：IE、Firefox3+、Opera9.5、Chrome、Safari支持，
//在IE中，默认坐标从(2,2)开始计算，导致最终距离比其他浏览器多出两个像素，我们需要做个兼容
 function clientRect() {
    var rect = function () {
        if (document.documentElement.getBoundingClientRect) {
            return document.documentElement.getBoundingClientRect();
        }
        else if (document.body.getBoundingClientRect) {
            return document.body.getBoundingClientRect();
        }
        else {
            return undefined;
        }
    }
    var clientTop = document.documentElement.clientTop;  // 非IE为0，IE为2
    var clientleft = document.documentElement.clientLeft; // 非IE为0，IE为2
    if (rect)
        return {
            top: rect.top - clientTop,
            left: rect.left - clientleft,
            right: rect.right - clientleft,
            bottom: rect.bottom - clientTop
        };
    return undefined;
};
function readFile(filepath, result) {
    //ie10 chrome
    if (window.FileReader) {
        var reader = new FileReader();
        reader.onload = function () {
            result.content = this.result;
            console.log(this.result);
        };
        reader.onerror = function () {
            console.error(this.error);
        };
        reader.readAsText(filepath);
    }
    //>=ie7 <=ie10
    else if (typeof (window.ActiveXObject) != "undefined") {
        var xml = new window.ActiveXObject("Microsoft.XMLDOM");
        xml.async = false;
        xml.load(filepath);
        result.content = xml.xml;
        console.log(xml.xml);
    }
    //ff
    else if (document.implementation && document.implementation.createDocument) {
        var xml = document.implementation.createDocument("", "", null);
        xml.async = false;
        xml.load(filepath);
        result.content = xml.xml;
        console.log(xml.xml);
    } else {
        throw new Error("read file fail");
    }
}
