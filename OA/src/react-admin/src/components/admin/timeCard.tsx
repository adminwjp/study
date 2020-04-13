/**
 * 考勤
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formTimeCard } from './baiscForm';
import { TimeCardCollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
import moment from 'moment';
//模型表单
const CollectionCreateForm: any = Form.create()((props: TimeCardCollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formTimeCard);
});
type TimeCardEditableTableState = EditableTableState & {
    recordoptions: any[];//员工
    ratifierrecordoptions: any[];//批准人
};
export default class TimeCard extends BasicTable<EditableTableProps, TimeCardEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            recordoptions: [],//员工
            ratifierrecordoptions: [],//批准人
            idshow:false
        };
        this.tableOperator.urlEntity = urls.time_card;//设置属性
        this.columns[0].width = "10%";
        this.columns[1].width = "10%";
        this.columns[2].width = "10%";
        this.columns[3].width = "10%";
        this.columns.splice(1, 0, {
            title: '员工',
            width: '10%',
            align: 'center', dataIndex: 'Record.Id', editable: true,
            render: (
                text: any,
                record: { Record: { Name: React.ReactNode } | null }
            ) => <span>{record.Record == null ? '' : record.Record.Name}</span>,
        }, {
            title: '迟到原因',
                dataIndex: 'Explain', editable: true,
            width: '5%',
            align: 'center',
        },
            {
                title: '上班时间',
                dataIndex: 'StartDate', editable: true,
                width: '5%',
                align: 'center',
            },
            {
                title: '下班时间',
                dataIndex: 'EndDate', editable: true,
                width: '5%',
                align: 'center',
            }, {
                title: '批准人', editable: true,
            width: '10%',
                align: 'center', dataIndex: 'RatifierRecord.Id',
            render: (
                text: any,
                record: { RatifierRecord: { Name: React.ReactNode } | null }
            ) => <span>{record.RatifierRecord == null ? '' : record.RatifierRecord.Name}</span>,
        }, {
            title: '批准时间',
                dataIndex: 'RatifierDate', editable: true,
            width: '5%',
            align: 'center',
        });
    }
    componentDidMount() {
        this.get(this);
        this.record();
        //this.user();
        this.ratifierRecord();
    };
    //员工
    record: any = () => { 
        axios.get(urls.category.record).then(res => {
            if (res.data.Success) {
                this.setState({ recordoptions: res.data.Data });
            }
        });
    };
    //批准人
    ratifierRecord: any = () => {
        axios.get(urls.category.ratifier).then(res => {
            if (res.data.Success) {
                this.setState({ ratifierrecordoptions: res.data.Data });
            }
        });
    };
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["Record", "RatifierRecord","User"]);
        if (data.Record != null) {
            obj["Record.Id"] = data.Record.Id;//[data.Record.Id];
        }
        if (data.RatifierRecord != null) {
            obj["RatifierRecord.Id"] = data.RatifierRecord.Id;// [data.RatifierRecord.Id];
        }
        obj["StartDate"] = moment(data["StartDate"]);
        obj["EndDate"] = moment(data["EndDate"]);
        obj["RatifierDate"] = moment(data["RatifierDate"]);
        this.form.setFieldsValue(obj);
    };
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "Record.Id") {
            return record.Record != null ? [record.Record.Id] : record[col.dataIndex];
        }
        if (col.dataIndex === "RatifierRecord.Id") {
            return record.RatifierRecord != null ? [record.RatifierRecord.Id] : record[col.dataIndex];
        }
        if (col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' || col.dataIndex === 'RatifierDate') {
            return moment(moment(record[col.dataIndex]).utcOffset(480).format('YYYY-MM-DD HH:mm:ss')) ;
        }
        return record[col.dataIndex];
    };
    getColumns: any = () => {
        const result = (col: any, record: any) => {
            //该方式 编辑 没反应
            // var temp = Object.create(
            //     {
            //     record,
            //     inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'RatifierRecord.Id' ? 'select' :
            //         (col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' || col.dataIndex === 'RatifierDate' ? 'datePicker' : 'text'),
            //     dataIndex: col.dataIndex,
            //     title: col.title,
            //     editing: this.isEditing(record, this),
            //     initialValue: this.initialValue(col, record)
            // });
            if (col.dataIndex === 'Record.Id') {
                //temp.recordoptions = this.state.recordoptions;
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'RatifierRecord.Id' ? 'select' :
                        (col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' || col.dataIndex === 'RatifierDate' ? 'datePicker' : 'text'),
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    recordoptions : this.state.recordoptions
                };
            }
            if (col.dataIndex === 'RatifierRecord.Id') {
               // temp.ratifierrecordoptions = this.state.ratifierrecordoptions;
                return {
                    record,
                    inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'RatifierRecord.Id' ? 'select' :
                        (col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' || col.dataIndex === 'RatifierDate' ? 'datePicker' : 'text'),
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record),
                    ratifierrecordoptions: this.state.ratifierrecordoptions
                };
            }
           // return temp;
            return {
                record,
                inputType: col.dataIndex === 'Record.Id' || col.dataIndex === 'RatifierRecord.Id' ? 'select' :
                    (col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' || col.dataIndex === 'RatifierDate' ? 'datePicker' : 'text'),
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
                title="添加考勤信息"
                recordoptions={this.state.recordoptions}
                ratifierrecordoptions={this.state.ratifierrecordoptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                idshow={this.state.idshow}
            />
        );
    };

}
