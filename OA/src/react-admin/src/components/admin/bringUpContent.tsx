/**
 * 培训内容
 */
import { Form } from 'antd';
import { EmptyBasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formBringUpContent } from './baiscForm';
import { CollectionCreateFormProps } from './basicFormProps';
import React from 'react';
import moment from 'moment';
//模型表单
const CollectionCreateForm: any = Form.create()((props: CollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formBringUpContent);
});
export default class BringUpContent extends EmptyBasicTable {
    constructor(props: any) {
        super(props);
        this.tableOperator.urlEntity = urls.bring_up_content;//设置属性
        this.columns[0].width = "8%";
        this.columns[1].width = "8%";
        this.columns[2].width = "8%";
        this.columns.splice(1, 0,
            {
                title: '名称',
                dataIndex: 'Name',
                width: '8%',
                align: 'center',
                editable: true,
            },
            {
                title: '内容',
                dataIndex: 'Content',
                width: '8%',
                align: 'center',
                editable: true,
            },
            {
                title: '开始时间',
                dataIndex: 'StartDate',
                width: '8%',
                align: 'center',
                editable: true,
            },
            {
                title: '结束时间',
                dataIndex: 'EndDate',
                width: '8%',
                align: 'center',
                editable: true,
            },
            {
                title: '单位',
                dataIndex: 'Unit',
                width: '8%',
                align: 'center',
                editable: true,
            },
            {
                title: '讲课者',
                dataIndex: 'Lecturer',
                width: '8%',
                align: 'center',
                editable: true,
            },
            {
                title: '地点',
                dataIndex: 'Place',
                width: '8%',
                align: 'center',
                editable: true,
            });
    }
    componentDidMount() {
        this.get(this);
    }
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data);
        obj["StartDate"] = moment(obj["StartDate"]);
        obj["EndDate"] = moment(obj["EndDate"]);
        this.form.setFieldsValue(obj);
    };
    handChange = (val: string) => {
        console.log(val);
    };
    initialValue = (col:any,record:any) => { 
       // return col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' ? record[col.dataIndex].format('YYYY-MM-DD HH:mm') : record[col.dataIndex];
        return col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' ? moment(moment(record[col.dataIndex]).utcOffset(480).format('YYYY-MM-DD HH:mm')) : record[col.dataIndex];
    };
    modelShow: any = () => { 
        return (
            <CollectionCreateForm
                title="添加培训内容信息"
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onChange={this.handChange}
                idshow={this.state.idshow}
            />
        );
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
                    inputType: col.dataIndex === 'StartDate' || col.dataIndex === 'EndDate' ? 'datePicker' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                    initialValue: this.initialValue(col, record)
                }),
            };
        });
        return columns;
    };
}
