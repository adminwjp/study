/**
 * 账套人员设置
 */
import axios from 'axios';

import React from 'react';
import {Form } from 'antd';
import { urls } from '../../js/index';
import { ReckoningListCollectionCreateFormProps } from './basicFormProps';
import { formReckoningList } from './baiscForm';
import { commonModel } from './basicModel';
import { EditableTableProps, EditableTableState } from './basicTable';
import { BasicTable } from './basicTable';
//模型表单
const CollectionCreateForm: any = Form.create()((props: ReckoningListCollectionCreateFormProps) => {
    return commonModel(props, formReckoningList);
});
type ReckoningListEditableTableState = EditableTableState &{
    recordOptions: any[];
    onRecordChange: () => void;
    reckoningNameOptions: any[];
    onReckoningNameChange: () => void;
};
export default class ReckoningList extends BasicTable<EditableTableProps, ReckoningListEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            recordOptions: [],
            reckoningNameOptions: [],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.reckoning_list;
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(0,1,
           
            {
                title: '档案信息',
                width: '10%',
                align: 'center',
                dataIndex: 'Record.Id',
                editable: true,
                render: (
                    text: any,
                    record: { Record: { Name: React.ReactNode } | null }
                ) => (
                        <span>
                            {record.Record == null ? '' : record.Record.Name}
                        </span>
                    ),
            },
            {
                title: '账套名称信息',
                width: '20%',
                align: 'center',
               // dataIndex: 'ReckoningName.Id',
                children: [
                    {
                        title: '账套名称',
                        dataIndex: 'ReckoningName.Id',
                        width: '10%',
                        align: 'center',
                        editable: true,
                        render: (
                            text: any,
                            record: {
                                ReckoningName: { Name: React.ReactNode } | null;
                            }
                        ) => (
                                <span>
                                    {record.ReckoningName == null
                                        ? ''
                                        : record.ReckoningName.Name}
                                </span>
                            ),
                    },
                    {
                        title: '账套名称说明',
                        dataIndex: 'Record.Explain',
                        width: '10%',
                        align: 'center',
                        render: (
                            text: any,
                            record: {
                                ReckoningName: { Explain: React.ReactNode } | null;
                            }
                        ) => (
                                <span>
                                    {record.ReckoningName == null
                                        ? ''
                                        : record.ReckoningName.Explain}
                                </span>
                            ),
                    },
                ],
            }
        );
    }
    onRecordChange: any = (val:string) => {

     };
    onReckoningNameChange: any = (val: string) => {

    };
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["Record", "ReckoningName"]);
        if (data.Record != null) {
            obj["Record.Id"] = [data.Record.Id];
        }
        if (data.ReckoningName != null) {
            obj["ReckoningName.Id"] = [data.ReckoningName.Id];
        }
        this.form.setFieldsValue(obj);
    };
    parse = (row: any) => {
        if (row.Record.Id instanceof Array) {
            row.Record.Id = row.Record.Id[row.Record.Id.length - 1];
        }
        if (row.ReckoningName.Id instanceof Array) {
            row.ReckoningName.Id = row.ReckoningName.Id[row.ReckoningName.Id.length - 1];
        }
    };
    componentDidMount() {
        this.get(this);
        axios.get(urls.category.record).then(res => {
            if (res.data.Success) {
                this.setState({ recordOptions: res.data.Data });
            }
        });
        axios.get(urls.category.reckoning_name).then(res => {
            if (res.data.Success) {
                this.setState({ reckoningNameOptions: res.data.Data });
            }
        });
    }
    modelShow: any = () => { 
        return (
            <CollectionCreateForm
                title="添加账套人员设置信息"
                recordOptions={this.state.recordOptions}
                reckoningNameOptions={this.state.reckoningNameOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onRecordChange={this.onRecordChange}
                onReckoningNameChange={this.onReckoningNameChange}
                idshow={this.state.idshow}
            />
        );
    };
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "Record.Id") {
            return record.Record != null ? [record.Record.Id] : record[col.dataIndex];
        }
        if (col.dataIndex === "ReckoningName.Id") {
            return record.ReckoningName != null ? [record.ReckoningName.Id] : record[col.dataIndex];
        }
        return record[col.dataIndex];
    };
    getColumns: any = () => {
        const result = (col: any, record: any) => {
            if (col.dataIndex === 'Record.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'ReckoningName.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    recordoptions: this.state.recordOptions
                };
            }
            if (col.dataIndex === 'ReckoningName.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'ReckoningName.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    reckoningnameoptions: this.state.reckoningNameOptions
                };
            }
            return {
                record,
                inputType:'text',
                dataIndex: col.dataIndex,
                title: col.title,
                editing: this.isEditing(record, this),
                initialValue: this.initialValue(col, record)
            };
        }
        const columns = this.columns.map(col => {
            if (!col.editable) {
                if (col.children && col.children.length > 0) {
                    const columns1 = col.children.map((col1: { editable: any; dataIndex: string; }) => {
                        if (!col1.editable) {
                            return col1;
                        }
                        return {
                            ...col1,
                            onCell: (record: any) => (result(col1, record)),
                        };
                    });
                    col.children = columns1;
                }
                return col;
            }
            return {
                ...col,
                onCell: (record: any) => (result(col, record)),
            };
        });
        return columns;
    };
}
