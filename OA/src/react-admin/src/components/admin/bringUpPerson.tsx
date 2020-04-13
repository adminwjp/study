/**
 * 培训人员信息
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formBringUpPerson } from './baiscForm';
import { BringUpPersonCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';

//模型表单
const CollectionCreateForm: any = Form.create()((props: BringUpPersonCollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formBringUpPerson);
});
type BringUpPersonEditableTableState = EditableTableState & {
    bringUpContentOptions: any[];
    onBringUpContentChange: () => void;
    recordOptions: any[];
    onRecordChange: () => void;
};
export default class BringUpPerson extends BasicTable<EditableTableProps, BringUpPersonEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            bringUpContentOptions: [],
            recordOptions:[],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.bring_up_person;//设置属性
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(1, 0, {
            title: '内容信息',
            width: '10%',
            align: 'center',
            dataIndex: 'BringUpContent.Id', editable: true,
            render: (
                text: any,
                record: { BringUpContent: { Name: React.ReactNode } | null }
            ) => <span>{record.BringUpContent == null ? '' : record.BringUpContent.Name}</span>,
        }, {
            title: '员工信息',
            width: '10%',
            align: 'center',
                dataIndex: 'Record.Id', editable: true,
            render: (
                text: any,
                record: { Record: { Name: React.ReactNode } | null }
            ) => <span>{record.Record == null ? '' : record.Record.Name}</span>,
        }, {
                title: '培训成绩',
            width: '10%',
                align: 'center', editable: true,
                dataIndex: 'Score'
        }, {
                title: '培训等级',
            width: '10%',
                align: 'center', editable: true,
                dataIndex: 'UpToGrate'
        }, {
                title: '备注',
            width: '10%',
                align: 'center', editable: true,
                dataIndex: 'Remark'
        },);
    }
    componentDidMount() {
        this.get(this);
        axios.get(urls.category.record).then(res => {
            if (res.data.Success) {
                this.setState({ recordOptions: res.data.Data });
            }
        });
        axios.get(urls.category.bring_up_content).then(res => {
            if (res.data.Success) {
                this.setState({ bringUpContentOptions: res.data.Data });
            }
        });
    }
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "BringUpContent.Id") {
            return record.BringUpContent != null ? [record.BringUpContent.Id] : record[col.dataIndex];
        }
        if (col.dataIndex === "Record.Id") {
            return record.Record != null ? [record.Record.Id] : record[col.dataIndex];
        }
        return record[col.dataIndex];
    };
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["BringUpContent", "Record"]);
        if (data.Record !== null) {
            obj["Record.Id"] = data.Record.Id;
        }
        if (data.BringUpContent !== null) {
            obj["BringUpContent.Id"] = data.BringUpContent.Id;
        }
        this.form.setFieldsValue(obj);
    };
    onBringUpContentChange: any = (val: string) => { };
    onRecordChange: any = (val: string) => { };
    getColumns: any = () => {
        const result = (col: any, record: any) => {
            if (col.dataIndex === 'Record.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'BringUpContent.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    recordoptions: this.state.recordOptions
                };
            }
            if (col.dataIndex === 'BringUpContent.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'BringUpContent.Id' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    bringupcontentoptions: this.state.bringUpContentOptions
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
                title="添加培训人员信息"
                bringUpContentOptions={this.state.bringUpContentOptions}
                recordOptions={this.state.recordOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onRecordChange={this.onRecordChange}
                onBringUpContentChange={this.onBringUpContentChange}
                idshow={this.state.idshow}
            />
        );
    };
}
