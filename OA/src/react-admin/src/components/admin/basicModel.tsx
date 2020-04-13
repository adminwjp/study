import { CollectionCreateFormProps } from './basicFormProps';
import { Modal,Form } from 'antd';
import React from 'react';
//模型表单
export const commonModel: any =(props: CollectionCreateFormProps, form: any) => {
    return BasicModel.commonModel(props, form);
};
export default class BasicModel{
    static commonModel = (props: CollectionCreateFormProps & {}, form: any) => {
        const { visible, onCancel, onCreate, title, classs } = props;
        return (
            <Modal
                title={title}
                visible={visible}
                onCancel={onCancel}
                onOk={onCreate}
                okText="确定"
                cancelText="取消"
                className={classs}
            >
                {form(props)}
            </Modal>
        );
    };
    basicModel: any = (form: any) => {
        return Form.create()((props: CollectionCreateFormProps) => {
            return BasicModel.commonModel(props, form);
        });
    };
    basicModel1: any = (props: CollectionCreateFormProps,form: any) => {
        return BasicModel.commonModel(props, form);
    }
}