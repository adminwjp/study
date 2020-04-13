/**
 * 角色权限
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formAuthority } from './baiscForm';
import { UserCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';

//模型表单
const CollectionCreateForm: any = Form.create()((props: UserCollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formAuthority);
});

type AuthorityEditableTableState = EditableTableState & {
    roleOptions: any
};
export default class Authority extends BasicTable<EditableTableProps, AuthorityEditableTableState> {
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
        this.tableOperator.urlEntity = urls.authority;
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(1, 0, {
            title: '表',
            dataIndex: 'Table',
            width: '10%',
            editable: true,
            align: 'center',
        }, {
            title: '类',
                dataIndex: 'EntityType',
            width: '10%',
            editable: true,
            align: 'center',
        },
            {
                title: '增',
                dataIndex: 'AddFalg',
                width: '10%',
                editable: true,
                align: 'center',
            },
            {
                title: '改',
                dataIndex: 'EditFalg',
                width: '10%',
                editable: true,
                align: 'center',
            }, {
            title: '删',
            dataIndex: 'DeleteFalg',
            width: '10%',
            editable: true,
            align: 'center',
        },{
            title: '查',
                dataIndex: 'SelectFalg',
            width: '10%',
            editable: true,
            align: 'center',
        }, {
            title: '角色信息',
            width: '10%',
            align: 'center',
            editable: true,
            dataIndex: 'Role.Id',
            render: (text: any, record: { Role: { Name: React.ReactNode; } | null; }) => (<span>{record.Role == null ? "" : record.Role.Name}</span>)
        })
    }
    componentDidMount() {
        this.get(this);
        this.category();
    }
    parse = (row: any) => {
        row.Role.Id = row.Role.Id[row.Role.Id.length - 1];
    };
    setValue: any = (data: any) => {
        let skips = ["Role","RoleId"];
        let obj = this.setEditValue(data, skips);
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
                    inputType: col.dataIndex === 'Role.Id' ? 'cascader' : (
                        col.dataIndex === 'AddFalg' || col.dataIndex === 'EditFalg'
                            || col.dataIndex === 'DeleteFalg' || col.dataIndex === 'SelectFalg'? 'select' : 'text'
                    ),
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: col.dataIndex === 'Role.Id' ? this.state.roleOptions : (col.dataIndex === 'AddFalg' || col.dataIndex === 'EditFalg'
                        || col.dataIndex === 'DeleteFalg' || col.dataIndex === 'SelectFalg' ? [1,0]:null),
                    editing: this.isEditing(record, this),
                    onChange: col.dataIndex === 'Role.Id' ? this.handChange : null,
                    initialValue: this.initialValue(col, record)
                }),
            };
        });
        return columns;
    };
    handChange = (val: string) => {
        console.log(val);
    };
    modelShow: any = () => {
        return (
            <CollectionCreateForm
                title="添加角色权限信息"
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
