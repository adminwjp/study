#pragma checksum "E:\work\program\SocialContact\src\SocialContact\Areas\Admin\Views\Icon\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd5706b19633413dd7b719e347573959734332bb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Icon_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Icon/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd5706b19633413dd7b719e347573959734332bb", @"/Areas/Admin/Views/Icon/Index.cshtml")]
    public class Areas_Admin_Views_Icon_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd5706b19633413dd7b719e347573959734332bb2763", async() => {
                WriteLiteral(@"
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"">
    <link rel=""icon"" href=""/favicon.ico"">
    <title>图标列表</title>
    <!-- 引入样式 -->
    <link rel=""stylesheet"" href=""https://unpkg.com/element-ui/lib/theme-chalk/index.css"">
    <style>
        .demo-form-inline {
            margin: 0px 70px;
            text-align: left;
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd5706b19633413dd7b719e347573959734332bb4258", async() => {
                WriteLiteral(@"
    <div class=""icon_list"">

        <!-- 开始查询信息 -->
        <el-form :inline=""true"" :model=""formQuery"" ref=""formQuery"" class=""demo-form-inline "">
            <el-form-item label=""图标分类"" prop=""id"">
                <el-select v-model=""formQuery.id"" filterable placeholder=""请选择"" ");
                WriteLiteral("@change=\"handleCategoryQueryChangeEvent\" ");
                WriteLiteral(@"@visible-change=""handleCategoryQueryVisableChangeEvent"">
                    <el-option v-for=""item in options"" :key=""item.label""
                               :label=""item.label"" :value=""item.id"">
                        <span style=""float: left"">{{ item.label }}</span>
                        <span style=""float: right; color: #8492a6; font-size: 13px""><i v-bind:class=""item.value""></i></span>
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label=""图标名称"" prop=""name"">
                <el-input v-model=""formQuery.name"" placeholder=""图标名称""></el-input>
            </el-form-item>
            <el-form-item label=""创建时间范围"" prop=""create_date"">
                <el-date-picker v-model=""formQuery.create_date"" type=""datetimerange"" value-format=""yyyy-MM-dd HH:mm:ss"" :picker-options=""createDatePickerOptions""
                                range-separator=""至"" start-placeholder=""开始日期"" align=""right"">
                </el-date-picker>
          ");
                WriteLiteral(@"  </el-form-item>
            <el-form-item label=""修改时间范围"" prop=""update_date"">
                <el-date-picker v-model=""formQuery.update_date"" type=""datetimerange"" value-format=""yyyy-MM-dd HH:mm:ss"" :picker-options=""createDatePickerOptions"" range-separator=""至"" start-placeholder=""开始日期""
                                end-placeholder=""结束日期"" align=""right"">
                </el-date-picker>
            </el-form-item>
            <el-form-item>
                <el-button type=""primary"" ");
                WriteLiteral("@click=\"loadTableDataOrQueryDataEvent\">查询</el-button>\r\n                <el-button ");
                WriteLiteral("@click=\"resetFormEvent(\'formQuery\')\">重置</el-button>\r\n            </el-form-item>\r\n        </el-form>\r\n        <!-- 结束查询信息 -->\r\n        <!-- 开始 操作按钮 -->\r\n        <div style=\"margin-top: 20px;text-align:left;margin-bottom:10px\">\r\n            <el-button ");
                WriteLiteral("@click=\"toggleSelectionTableCheckBoxButtonEvent(checkboxAllSelect?tableData:null)\">{{checkboxAllSelectText}}</el-button>\r\n            <el-button ");
                WriteLiteral("@click=\"insertButtonClickEvent\" v-if=\"operator.add\" icon=\"el-icon-plus\">添加</el-button>\r\n            <el-button ");
                WriteLiteral("@click=\"modifyButtonClickEvent\" v-if=\"operator.modify\" icon=\"el-icon-edit\">编辑</el-button>\r\n            <el-button ");
                WriteLiteral("@click=\"deleteButtonClickEvent\" v-if=\"operator.delete\" icon=\"el-icon-delete\">删除</el-button>\r\n            <el-button ");
                WriteLiteral("@click=\"queryButtonClickEvent\" icon=\"el-icon-view\">查看</el-button>\r\n            <el-button ");
                WriteLiteral(@"@click=""loadTableDataOrQueryDataEvent"" icon=""el-icon-refresh"">刷新</el-button>
        </div>
        <!-- 结束 操作按钮 -->
        <!-- 开始 列表信息 -->
        <el-table v-loading=""loading"" element-loading-spinner=""el-icon-loading"" element-loading-background=""rgba(0, 0, 0, 0.8)"" ref=""multipleTable"" :data=""tableData"" tooltip-effect=""dark"" style=""width: 100%""
                  highlight-current-row border ");
                WriteLiteral(@"@selection-change=""handleSelectionChange"" :default-sort=""{prop: 'create_date', order: 'descending'}"">
            <el-table-column type=""selection"" width=""55"" align=""center""> </el-table-column>
            <el-table-column type=""index"" width=""50"" align=""center"" sortable label=""序号""> </el-table-column>
            <el-table-column prop=""id"" label=""编号"" align=""center"" sortable width=""70"">  </el-table-column>
            <el-table-column prop=""name"" label=""图标名称"" align=""center"" width=""200""> </el-table-column>
            <el-table-column prop=""style"" label=""图标样式"" align=""center"" width=""200"">
                <template slot-scope=""scope"">
                    <el-popover trigger=""hover"" placement=""top"">
                        <p>样式: <i v-bind:class=""scope.row.style""></i> ");
                WriteLiteral(@" </p>
                        <div slot=""reference"" class=""name-wrapper"">
                            <el-tag size=""medium"">{{ scope.row.style }}</el-tag>
                        </div>
                    </el-popover>
                </template>
            </el-table-column>
            <el-table-column prop=""description"" label=""图标描述"" align=""center"" width=""300""> </el-table-column>
            <el-table-column prop=""admin.account"" align=""center"" label=""管理员账户"" sortable width=""200"">
                <template slot-scope=""scope"" v-if=""scope.row.admin"">
                    <el-popover trigger=""hover"" placement=""top"">
                        <p>管理员编号: {{ scope.row.admin.id }}</p>
                        <p>账户: {{ scope.row.admin.account }}</p>
                        <p>昵称: {{ scope.row.admin.nick_name }}</p>
                        <p>姓名: {{ scope.row.admin.real_name }}</p>
                        <p>
                            出生日期:  <i class=""el-icon-time""></i>
                            <sp");
                WriteLiteral(@"an style=""margin-left: 10px"">{{ scope.row.admin.birthday }}</span>
                        </p>
                        <p>
                            头像:<img v-if=""scope.row.admin.head_pic"" style=""margin-left:10px;margin-top:-5px;border-radius:50%;width:36px;height:36px;vertical-align:middle;"" :src=""baseUrl+'img/'+scope.row.admin.head_pic"" class=""avatar"">
");
                WriteLiteral(@"                            <img v-else style=""margin-left:10px;margin-top:-5px;border-radius:50%;width:36px;height:36px;vertical-align:middle;"" src=""https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png"" />
                        </p>
                        <p>手机: {{ scope.row.admin.phone }}</p>
                        <p>性别: {{ scope.row.admin.sex }}</p>
                        <p>描述: {{ scope.row.admin.description }}</p>
                        <p>邮箱: {{ scope.row.admin.email }}</p>
                        <p>角色: {{ scope.row.admin.role }}</p>
                        <div slot=""reference"" class=""name-wrapper"">
                            <el-tag size=""medium"">{{ scope.row.admin.account }}</el-tag>
                        </div>
                    </el-popover>
                </template>
            </el-table-column>
            <el-table-column prop=""create_date"" label=""创建日期"" align=""center"" sortable width=""200"">
                <template slot-scope=""scope"">
               ");
                WriteLiteral(@"     <i class=""el-icon-time""></i>
                    <span style=""margin-left: 10px"">{{ scope.row.create_date }}</span>
                </template>
            </el-table-column>
            <el-table-column prop=""update_date"" label=""修改日期"" align=""center"" sortable width=""200"">
                <template slot-scope=""scope"">
                    <i class=""el-icon-time""></i>
                    <span style=""margin-left: 10px"">{{ scope.row.update_date }}</span>
                </template>
            </el-table-column>
            <el-table-column label=""操作"" align=""center"" width=""200"">
                <template slot-scope=""scope"">
                    <el-button ");
                WriteLiteral("@click=\"handleSelectClickEvent(scope.row)\" type=\"text\" icon=\"el-icon-view\" size=\"small\">查看</el-button>\r\n                    <el-button type=\"text\" ");
                WriteLiteral("@click=\"handleModifyClickEvent(scope.row)\" v-if=\"operator.modify\" icon=\"el-icon-edit\" size=\"small\">编辑</el-button>\r\n                    <el-button type=\"text\" ");
                WriteLiteral(@"@click=""handleDeleteClickEvent(scope.row)"" v-if=""operator.delete"" icon=""el-icon-delete"" size=""small"">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <!-- 结束 列表信息 -->
        <!-- 开始 分页 -->
        <div class=""block"" style=""text-align:center;"">
            <el-pagination background ");
                WriteLiteral("@size-change=\"handleSizeChangeEvent\" ");
                WriteLiteral(@"@current-change=""handleCurrentPageChangeEvent"" :current-page=""page.current_page""
                           :page-sizes=""page.sizes"" :page-size=""page.size"" :page-count=""page.page"" layout=""total, sizes, prev, pager, next, jumper"" :total=""page.total"">
            </el-pagination>
        </div>
        <!-- 结束 分页 -->
        <!-- 开始 添加 修改 -->
        <el-dialog title=""提示"" :title=""dialog.title"" :visible.sync=""dialog.visible"" width=""30%"" :before-close=""handleDialogClose"">
            <el-radio-group v-model=""labelPosition"" size=""small"">
                <el-radio-button label=""left"">左对齐</el-radio-button>
                <el-radio-button label=""right"">右对齐</el-radio-button>
                <el-radio-button label=""top"">顶部对齐</el-radio-button>
            </el-radio-group>
            <div style=""margin: 20px;""></div>
            <el-form :model=""formSubmit"" status-icon :rules=""formRoleValidator"" ref=""formSubmit"" :disabled=""disabled"" class=""demo-ruleForm"" :label-position=""labelPosition"" label-width=""80px"">");
                WriteLiteral(@"
                <el-input type=""hidden"" v-model=""formSubmit.id""></el-input>
                <el-form-item label=""图标名称"" prop=""name"">
                    <el-input v-model=""formSubmit.name""></el-input>
                </el-form-item>
                <el-form-item label=""图标样式"" prop=""style"">
                    <el-input v-model=""formSubmit.style""></el-input>
                </el-form-item>
                <template v-if=""test"">
                    <el-form-item label=""管理员"" prop=""admin.id"">
                        <el-select v-model=""formSubmit.admin.id"" filterable placeholder=""请选择"" ");
                WriteLiteral("@change=\"handleAdminChangeEvent\" ");
                WriteLiteral(@"@visible-change=""handleAdminVisableChangeEvent"">
                            <el-option v-for=""item in admin_options"" :key=""item.id""
                                       :label=""item.category"" :value=""item.id"">
                            </el-option>
                        </el-select>
                    </el-form-item>
                </template>
                <el-form-item label=""样式展示"">
                    <i v-bind:class=""formSubmit.style""></i>
                </el-form-item>
                <el-form-item label=""图标描述"" prop=""description"">
                    <el-input type=""textarea"" v-model=""formSubmit.description"" value=""这个人很懒,什么也没留下!""></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type=""primary"" ");
                WriteLiteral("@click=\"submitFormEvent(\'formSubmit\')\">{{dialog.submitText}}</el-button>\r\n                    <el-button ");
                WriteLiteral(@"@click=""resetFormEvent('formSubmit')"">{{dialog.resetText}}</el-button>
                </el-form-item>
            </el-form>
        </el-dialog>
        <!-- 结束 添加 修改 -->

    </div>
    <!-- import Vue before Element -->
    <script src=""https://unpkg.com/vue/dist/vue.js""></script>
    <!-- import JavaScript -->
    <script src=""https://unpkg.com/element-ui/lib/index.js""></script>
    <script src=""https://unpkg.com/axios/dist/axios.min.js""></script>
    <script src=""/js/js.cookie.js""></script>
    <script src=""/js/index.js""></script>
    <script>
        var obj = new Gibson();
        obj.el = "".icon_list"";
        obj.data.operatorUrl = { add: urls.icon.add, modify: urls.icon.edit, delete: urls.icon.delete, query: urls.icon.query };
        obj.data.formQuery = { id: '', name: '', create_date: [], update_date: [] };
        obj.data.formSubmit = { id: '', name: '', style: '', description: '这个人很懒,什么也没留下!' };
        obj.data.controller = ""icon"";
        obj.data.isOperatorRequest = tr");
                WriteLiteral(@"ue;
        obj.data.formRoleValidator = {
            name: [
                { required: true, message: '请输入图标名称', trigger: 'blur' },
                { min: 2, max: 10, message: '长度在 2 到 10 个字符图标名称', trigger: 'blur' }
            ],
            style: { required: true, message: '请输入图标样式', trigger: 'blur' },
            description: [
                { required: true, message: '请输入图标描述', trigger: 'blur' },
                { min: 10, max: 500, message: '长度在 10 到 500 个字符图标描述', trigger: 'blur' }
            ]
        };
        obj.methods.reload = function () {
            this.queryCategoryEvent();
        };
        //覆盖此方法 重写
        obj.methods.loadMounted = function () {
            this.queryCategoryEvent();
            if (this.test) this.adminCategoryQueryEvent();
        };
        //测试用的
        setAdmin(obj);
        //覆盖此方法 重写
        obj.methods.submitTextSelectChanngeEevent = function () {
            this.dialog.title = ""查看图标信息"";
            this.dialog.submitText = ""预览图");
                WriteLiteral(@"标信息"";
            this.disabled = true;
        };
        //覆盖此方法 重写
        obj.methods.submitTextModifyChanngeEevent = function () {
            this.dialog.title = ""编辑图标信息"";
            this.dialog.submitText = ""立即编辑"";
            this.disabled = false;
        };
        //覆盖此方法 重写
        obj.methods.submitTextInsertChanngeEevent = function () {
            this.dialog.title = ""添加图标信息"";
            this.dialog.submitText = ""立即创建"";
            this.disabled = false;
        };
        //覆盖此方法 重写
        obj.methods.setValue = function (row) {
            for (var obj in this.formSubmit) {
                if (obj.toString() == 'admin') {
                    continue;
                }
                if (row[obj]) this.formSubmit[obj] = row[obj];
            }
            if (this.test && row.admin) this.formSubmit.admin.id = row.admin.id;
        };
        new Vue(obj);
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
