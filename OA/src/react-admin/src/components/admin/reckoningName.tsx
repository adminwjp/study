/**
 * 账套 名称 
 */
import { EmptyBasicTable } from './basicTable';
import BasicModel from './basicModel';
import { urls } from '../../js/index';
import { CollectionCreateFormProps, FormItem } from './basicFormProps';
import { Form, Input } from 'antd';
import React from 'react';
const readonly = true;
//模型表单
const customForm: any = (props: CollectionCreateFormProps) => {
    const { form , idshow } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <FormItem label="名称">
                {getFieldDecorator('Name', {
                    initialValue:"账套",
                    rules: [
                        { required: true, message: '请输入名称!' },
                        { min: 2, max: 10, message: '名称长度只能为2-10位' },
                    ],
                })(<Input />)}
            </FormItem>
            <FormItem label="说明">
                {getFieldDecorator('Explain', {
                    initialValue: "暂无",
                    rules: [
                        { required: true, message: '请输入名称!' },
                        { min: 2, max: 100, message: '说明长度只能为2-100位' },
                    ],
                })(<Input />)}
            </FormItem>
        </Form>);
};
const CollectionCreateForm: any = Form.create()((props: CollectionCreateFormProps) => {
    return BasicModel.commonModel(props, customForm);
});
export default class ReckoningName extends EmptyBasicTable {
    constructor(props: any) {
        super(props);
        this.tableOperator.urlEntity = urls.reckoning_name;
        this.columns[0].width = "15%";
        this.columns[1].width = "15%";
        this.columns[2].width = "15%";
        this.columns.splice(1, 0, {
            title: '名称',
            dataIndex: 'Name',
            width: '15%',
            editable: true,
            align: 'center',
        },
            {
                title: '说明',
                dataIndex: 'Explain',
                width: '15%',
                editable: true,
                align: 'center',
            });
    }
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data);
        this.form.setFieldsValue(obj);
    };
    modelShow: any = () => { 
        return (
            <CollectionCreateForm
                title="添加账套名称信息"
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                idshow={this.state.idshow}
            />
        );
    };
    componentDidMount() {
        this.get(this);
    }
}
