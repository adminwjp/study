/**
 * 考勤/账套项目
 */
import { EmptyBasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formAccountItem } from './baiscForm';
import { CollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
//模型表单
const CollectionCreateForm: any = Form.create()((props: CollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formAccountItem);
});
export default class AccountItem extends EmptyBasicTable {
    constructor(props: any) {
        super(props);
        this.tableOperator.urlEntity = urls.account_item;//设置属性
        this.columns.splice(1, 0, {
            title: '名称',
            dataIndex: 'Name',
            width: '10%',
            align: 'center',
            editable: true,
        },
            {
                title: '类型',
                dataIndex: 'Type',
                width: '10%',
                align: 'center',
                editable: true,
            },
            {
                title: '单位',
                dataIndex: 'Utit',
                width: '10%',
                align: 'center',
                editable: true,
            });
    }
    setValue: any = (data: any) => {
        this.form.setFieldsValue({ Name: data.Name, Type: data.Type, Utit: data.Utit});
    };
    /**
       * 表单 展示
       */
    modelShow: any = () => {
        return (
            <CollectionCreateForm
                title="添加考勤/账套项目信息"
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
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
                    inputType: col.dataIndex === 'Type' ? 'select' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: col.dataIndex === 'Type' ? ['考勤', '账套'] : null,
                    editing: this.isEditing(record, this),
                }),
            };
        });
        return columns;
    };
}
