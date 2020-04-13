/**
 * 职位信息
 */
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formDuty } from './baiscForm';
import { CollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
import moment from 'moment';

//模型表单
const CollectionCreateForm: any = Form.create()((props: CollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formDuty);
});
export default class Duty extends BasicTable<EditableTableProps, EditableTableState> {
    constructor(props: any) {
        super(props);
        this.tableOperator.urlEntity = urls.duty;
        this.columns[0].width = "5%";
        this.columns[1].width = "5%";
        this.columns[2].width = "5%";
        this.columns[3].width = "5%";
        this.columns.splice(1, 0, {
            title: '加入时间',
            dataIndex: 'AccessionDate',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '离开时间',
                dataIndex: 'DimissionDate',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '离职原因',
                dataIndex: 'DimissionReason',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '第一次工作(签条约)时间',
                dataIndex: 'FirstPactDate',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '第一次工作(签条约)开始时间',
                dataIndex: 'PactStartDate',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '第一次工作(签条约)结束时间',
                dataIndex: 'PactEndDate',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '银行名称',
                dataIndex: 'BankName',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '银行卡号',
                dataIndex: 'BandId',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '社会安全编号',
                dataIndex: 'SocietySafefyNo',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '年保险金(养老金)编号',
                dataIndex: 'AnnuitySafefyNo',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '失业救助金编号',
                dataIndex: 'DoleSafefyNo',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '医疗保险金编号',
                dataIndex: 'MedicareSafefyNo',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '工伤赔偿费编号',
                dataIndex: 'CompoSafefyNo',
            width: '5%',
            align: 'center', editable: true,
        }, {
                title: '公积金 编号',
                dataIndex: 'AcoumulationFundNo',
            width: '5%',
            align: 'center', editable: true,
        });
    }
    componentDidMount() {
        this.get(this);
    }
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data);
        obj["AccessionDate"] = moment(obj["AccessionDate"]);
        obj["DimissionDate"] = moment(obj["DimissionDate"]);
        obj["FirstPactDate"] = moment(obj["FirstPactDate"]);
        obj["PactStartDate"] = moment(obj["PactStartDate"]);
        obj["PactEndDate"] = moment(obj["PactEndDate"]);
        this.form.setFieldsValue(obj);
    };
    modelShow: any = () => {
        return (<CollectionCreateForm
            title="添加职位信息"
            ref={this.saveFormRef}
            visible={this.state.visible}
            onCancel={this.handleCancel}
            onCreate={this.handleCreate}
            idshow={this.state.idshow}
                />);
    };
    initialValue = (col: any, record: any) => {
        return col.dataIndex === 'AccessionDate' || col.dataIndex === 'DimissionDate'
            || col.dataIndex === 'FirstPactDate' || col.dataIndex === 'PactStartDate'
            || col.dataIndex === 'PactEndDate' ? moment(moment(record[col.dataIndex]).utcOffset(480).format('YYYY-MM-DD')) : record[col.dataIndex];
    };
    getColumns: any = () => {
        const columns = this.columns.map((col: { editable: any; dataIndex: any; title: any; }) => {
            if (!col.editable) {
                return col;
            }
            return {
                ...col,
                onCell: (record: any) => ({
                    record,
                    inputType: col.dataIndex === 'AccessionDate' || col.dataIndex === 'DimissionDate'
                        || col.dataIndex === 'FirstPactDate' || col.dataIndex === 'PactStartDate'
                        || col.dataIndex === 'PactEndDate'? 'datePicker' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col,record)
                }),
            };
        });
        return columns;
    };
}
