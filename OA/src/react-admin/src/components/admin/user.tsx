/**
 * 用户
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formUser } from './baiscForm';
import { UserCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
//模型表单
const CollectionCreateForm: any = Form.create()((props: UserCollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formUser);
});
type UserEditableTableState = EditableTableState& {
    roleOptions:any
};
export default class User extends BasicTable<EditableTableProps, UserEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            roleOptions: [],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.user;//设置属性
        this.columns.splice(1, 0, {
            title: '账户',
            dataIndex: 'Account',
            width: '10%',
            align: 'center',
        },
            {
                title: '密码',
                dataIndex: 'Password',
                width: '10%',
                editable: true,
                align: 'center',
            },
            {
                title: '角色信息',
                width: '10%',
                align: 'center',
                editable: true,
                dataIndex: 'Role.Id',
                render: (text: any, record: { Role: { Name: React.ReactNode; } | null; }) => (<span>{record.Role == null ? "" : record.Role.Name}</span>)
            });
    }
    componentDidMount() {
        this.get(this);
        this.category();
    }
    parse = (row: any)=>{
        row.Role.Id = row.Role.Id[row.Role.Id.length - 1];
    };
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["Role", "RoleId"]);
        console.log(obj);
        if (data.Role != null) {
            obj["Role.Id"] = [data.Role.Id];
        }
        this.form.setFieldsValue(obj);
    };
    category = () => { 
        axios.get(urls.category.role).then(res => {
            if (res.data.Success) {
                this.setState({ roleOptions: res.data.Data });
            }
        });
    };
    handChange = (val:string) => { 
        console.log(val);
    };
   initialValue = (col: any, record: any) => {
        return col.dataIndex === "Role.Id" && record.Role != null ? [record.Role.Id] : record[col.dataIndex];
    };
    getColumns: any = () => {
        const columns = this.columns.map(col => {
            if (!col.editable) {
                return col;
            }
            return {
                ...col,
                onCell: (record: any) => ({
                    record,
                    inputType: col.dataIndex === 'Role.Id' ? 'cascader' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: col.dataIndex === 'Role.Id' ? this.state.roleOptions : null,
                    editing: this.isEditing(record, this),
                    onChange: col.dataIndex === 'Role.Id' ? this.handChange : null,
                    initialValue: this.initialValue(col, record)
                    //defaultValue: col.dataIndex === 'Role.Id' ? [record.Role|record.Role.Id] : null
                }),
            };
        });
        return columns;
    };
    modelShow: any = () => {
        return (
            <CollectionCreateForm
                title="添加用户信息"
                options={this.state.roleOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onChange={this.handChange}
                idshow={this.state.idshow}
            />
        );
    };
}
