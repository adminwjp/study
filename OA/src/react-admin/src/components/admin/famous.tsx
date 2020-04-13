/**
 * 名族
 */
import { EmptyBasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { formName } from './baiscForm';
import { CollectionCreateFormProps } from './basicFormProps';
import { Form } from 'antd';
import React from 'react';
export const CollectionCreateForm: any = Form.create()((props: CollectionCreateFormProps) => {
    return BasicModel.commonModel(props,formName);
});

export default class Famous extends EmptyBasicTable {
    constructor(props: any) {
        super(props);
        this.tableOperator.urlEntity = urls.famous;//设置属性
        this.columns.splice(1, 0, {
            title: '名称',
            dataIndex: 'Name',
            width: '15%',
            editable: true,
            align: 'center',
        });
    }
    setValue: any = (data: any) => {
        this.form.setFieldsValue({ Name: data.Name });
    };
    /**
       * 表单 展示
       */
    modelShow: any = () => { 
        return (<CollectionCreateForm
            title="添加名族信息"
            ref={this.saveFormRef}
            visible={this.state.visible}
            onCancel={this.handleCancel}
            onCreate={this.handleCreate}
            idshow= {this.state.idshow}
                />);
    };
    //默认未实现 重写 覆盖
    collectionCreateForm: any = () => {
        // if (collectionCreateFormTemp===undefined) {
        //     //collectionCreateFormTemp = this.basicModel.basicModel1({ title: "添加名族信息", visible: this.state.visible, onCancel: this.handleCancel, onOk: this.handleCreate },formName);
        //     collectionCreateFormTemp = this.basicModel.basicModel(formName)({ title: "添加名族信息", visible: this.state.visible, onCancel: this.handleCancel, onOk: this.handleCreate });
        // }
        // this.collectionCreateForm1 = collectionCreateFormTemp;
        // console.log(collectionCreateFormTemp);//bug 没生成成功
        // return collectionCreateFormTemp;
    };
    componentDidMount() {
        this.tableOperator.get();
    }
}
