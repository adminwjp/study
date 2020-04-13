/**
 * 账套信息
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formReckoning } from './baiscForm';
import { ReckoningCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';

//模型表单
const CollectionCreateForm: any = Form.create()((props: ReckoningCollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formReckoning );
});
type ReckoningEditableTableState = EditableTableState & {
    recordOptions: any[];
    accountItemOptions: any[];
};
export default class Reckoning extends BasicTable<EditableTableProps, ReckoningEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            recordOptions: [],
            accountItemOptions: [],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.reckoning;//设置属性
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(0,1,
            {
                title: '档案信息',
                width: '10%',
                align: 'center',
                dataIndex: 'Record.Id', editable: true,
                render: (
                    text: any,
                    record: { Record: { Name: React.ReactNode } | null }
                ) => <span>{record.Record == null ? '' : record.Record.Name}</span>,
            },
            {
                title: '考勤/账套项目信息',
                width: '10%',
                align: 'center',
                dataIndex: 'AccountItem.Id', editable: true,
                render: (
                    text: any,
                    record: {
                        AccountItem: { Name: React.ReactNode } | null;
                    }
                ) => (
                        <span>{record.AccountItem == null ? '' : record.AccountItem.Name}</span>
                    ),
            },
            {
                title: '账套金额',
                width: '10%',
                align: 'center',
                dataIndex: 'Money', editable: true,
            }
        );
    }
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["Record","AccountItem"]);
        if (data.Record != null) {
            obj["Record.Id"] = [data.Record.Id];
        }
        if (data.AccountItem != null) {
            obj["AccountItem.Id"] = [data.AccountItem.Id];
        }
        this.form.setFieldsValue(obj);
    };
    parse = (row: any) => {
        if (row.Record.Id instanceof Array)
        {
            row.Record.Id = row.Record.Id[row.Record.Id.length - 1];
        }
        if (row.AccountItem.Id instanceof Array) {
            row.AccountItem.Id = row.AccountItem.Id[row.AccountItem.Id.length - 1];
        }
    };
    onRecordChange: any = (val: string) => { };
    onAccountItemChange: any = (val: string) => { };
    componentDidMount() {
        this.get(this);
        axios.get(urls.category.record).then(res => {
            if (res.data.Success) {
                this.setState({ recordOptions: res.data.Data });
            }
        });
        axios.get(urls.category.account_item).then(res => {
            if (res.data.Success) {
                this.setState({ accountItemOptions: res.data.Data });
            }
        });
    }
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "Record.Id") {
            return record.Record != null ? [record.Record.Id] : record[col.dataIndex];
        }
        if (col.dataIndex === "AccountItem.Id") {
            return record.AccountItem != null ? [record.AccountItem.Id] : record[col.dataIndex];
        }
        return record[col.dataIndex];
    };
    getColumns: any = () => {
        const result = (col: any, record: any) => {
            if (col.dataIndex === 'Record.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'AccountItem.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: null,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    recordoptions: this.state.recordOptions
                };
            }
            if (col.dataIndex === 'AccountItem.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'AccountItem.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: null,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    accountitemoptions : this.state.accountItemOptions
                };
            }
            return {
                record,
                inputType:'text',
                dataIndex: col.dataIndex,
                title: col.title,
                options: null,
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
                title="添加账套信息"
                recordOptions={this.state.recordOptions}
                accountItemOptions={this.state.accountItemOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onRecordChange={this.onRecordChange}
                onAccountItemChange={this.onAccountItemChange}
                idshow={this.state.idshow}
            />
        );
    };
}
