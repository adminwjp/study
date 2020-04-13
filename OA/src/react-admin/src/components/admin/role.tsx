/**
 * 角色
 */
import { EmptyBasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formName } from './baiscForm';
import { CollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
export const CollectionCreateForm: any = Form.create()((props: CollectionCreateFormProps) => {
    return BasicModel.commonModel(props, formName);
});
export default class Role extends EmptyBasicTable {
    constructor(props: any) {
        super(props);
        this.tableOperator.urlEntity = urls.role;//设置属性
        this.columns.splice(1, 0, {
            title: '名称',
            dataIndex: 'Name',
            width: '15%',
            editable: true,
            align: 'center',
        });
      
    }
    componentDidMount() {
        this.get(this);
    }
    setValue: any = (data: any) => {
        this.form.setFieldsValue({ Name: data.Name });
    };
    /**
      * 表单 展示
      */
    modelShow: any = () => {
        return (<CollectionCreateForm
            title="添加角色信息"
            ref={this.saveFormRef}
            visible={this.state.visible}
            onCancel={this.handleCancel}
            onCreate={this.handleCreate}
            idshow={this.state.idshow}
                />);
    };
}
