/**
 * 模块
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formModule } from './baiscForm';
import { ModuleCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';

//模型表单
const CollectionCreateForm: any = Form.create()((props: ModuleCollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formModule);
});
type ModuleEditableTableState = EditableTableState & {
    parentOptions: any[];
};
export default class Module extends BasicTable<EditableTableProps, ModuleEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            parentOptions: [],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.module;
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(1,0,
            {
                title: '名称',
                dataIndex: 'Name',
                width: '10%',
                editable: true,
                align: 'center',
            },
            {
                title: '链接',
                dataIndex: 'Href',
                width: '10%',
                editable: true,
                align: 'center',
            }, {
                title: '父模块信息',
                dataIndex: 'Parent.Id',
                width: '10%',
                editable: true,
                align: 'center',
                render: (
                    text: any,
                    record: { Parent: { Name: React.ReactNode } | null }
                ) => (
                        <span>
                            {record.Parent == null ? '' : record.Parent.Name}
                        </span>
                    ),
            },
        );
    }
    setValue: any = (data: any) => {
        let obj = { Name: data.Name, Href: data.Href, Utit: data.Utit };
        if (data.Parent != null) {
            obj["Parent.Id"] = data.Parent.Id;
        }
        this.form.setFieldsValue(obj);
    };
    componentDidMount() {
        this.get(this);
        axios.get(urls.category.module).then(res => {
            if (res.data.Success) {
                this.setState({ parentOptions: res.data.Data });
            }
        });
    }
    parse = (row: any) => {
        console.log(row);
        if (row.Parent.Id instanceof Array) {
            row.Parent.Id = row.Parent.Id[row.Parent.Id.length - 1];
        }
        else if (row.Parent.Id===""){
            row.Parent = null;
        } else if (Object.prototype.toString.call(row.Parent) === "[object Object]") {
            row.Parent = null;
        }
    };
    onParentChange: any = (val: string) => { };
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "Parent.Id") {
            return record.Parent != null ? [record.Parent.Id] : record[col.dataIndex];
        }
        return record[col.dataIndex];
    };
    getColumns: any = () => {
        const result = (col: any, record: any) => {
            if (col.dataIndex === 'Parent.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Parent.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    parentoptions: this.state.parentOptions
                };
            }
            return {
                record,
                inputType: col.dataIndex === 'Parent.Id' ? 'select' : 'text',
                dataIndex: col.dataIndex,
                title: col.title,
                editing: this.isEditing(record, this),
                initialValue: this.initialValue(col, record)
            };
        }
        const columns = this.columns.map(col => {
            if (!col.editable) {
                return col;
            }
            return {
                ...col,
                onCell: (record: any) => (result(col, record)),
            };
        });
        return columns;
    };
    modelShow: any = () => {
        return (
            <CollectionCreateForm
                title="添加模块信息"
                parentOptions={this.state.parentOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onParentChange={this.onParentChange}
                idshow={this.state.idshow}
            />
        );
    };  
}
