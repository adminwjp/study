/**
 * 奖惩信息
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formReawrdsAndPunishment } from './baiscForm';
import { ReawrdsAndPunishmentCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
import moment from 'moment';
//模型表单
const CollectionCreateForm: any = Form.create()((props: ReawrdsAndPunishmentCollectionCreateFormProps) => {
    return BasicModel.commonModel(props,formReawrdsAndPunishment);
});
type ReawrdsAndPunishmentEditableTableState = EditableTableState & {
    recordOptions: any[];
};
export default class ReawrdsAndPunishment extends BasicTable<EditableTableProps, ReawrdsAndPunishmentEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            recordOptions: [],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.reawrds_and_punishment;//设置属性
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(1,0,
            {
                title: '档案信息',
                width: '10%',
                align: 'center',
                dataIndex: 'Record.Id', editable:true,
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
                title: '奖惩',
                dataIndex: 'Type',
                width: '5%',
                align: 'center', editable: true,
                render: (text: any, record: any) => {
                    return <span>{record.Type === 1 ? '奖' : '惩'}</span>;
                },
            },
            {
                title: '原因',
                dataIndex: 'Reason',
                width: '10%', editable: true,
                align: 'center',
            },
            {
                title: '内容',
                dataIndex: 'Content', editable: true,
                width: '10%',
                align: 'center',
            },
            {
                title: '奖惩金额',
                dataIndex: 'Money', editable: true,
                width: '5%',
                align: 'center',
            },
            {
                title: '奖惩结束时间',
                dataIndex: 'StartDate', editable: true,
                width: '10%',
                align: 'center',
            },
            {
                title: '奖惩结束时间', editable: true,
                dataIndex: 'EndDate',
                width: '10%',
                align: 'center',
            }
        );
    }
    componentDidMount() {
        this.get(this);
        axios.get(urls.category.record).then(res => {
            if (res.data.Success) {
                this.setState({ recordOptions: res.data.Data });
            }
        });
    }
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["Record"]);
        if (data.Record != null) {
            obj["Record.Id"] = [data.Record.Id];
        }
        obj.Type = obj.Type === 1 ? "奖" : "惩";
        obj["StartDate"] = moment(data["StartDate"]);
        obj["EndDate"] = moment(data["EndDate"]);
        this.form.setFieldsValue(obj);
    };
    parse = (row: any) => {
        if (row.Record.Id instanceof Array) {
            row.Record.Id = row.Record.Id[row.Record.Id.length - 1];
        }
    };
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "Record.Id") {
            return record.Record != null ? [record.Record.Id] : record[col.dataIndex];
        }
        return col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' ? moment(moment(record[col.dataIndex]).utcOffset(480).format('YYYY-MM-DD HH:mm:ss')) : record[col.dataIndex];
    };
    onRecordChange: any = (val: string) => { };
    getColumns: any = () => {
        const result = (col: any, record: any) => {
            if (col.dataIndex === 'Record.Id') {
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' ? 'select' : (col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' ? 'datePicker' : 'text'),
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    recordoptions : this.state.recordOptions
                };
            }
            return {
                record,
                inputType:(col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' ? 'datePicker' : 'text'),
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
                title="添加奖惩信息"
                recordOptions={this.state.recordOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onRecordChange={this.onRecordChange}
                idshow={this.state.idshow}
            />
        );
    };           
}
