/**
 * 员工信息
 */
import React from 'react';
import {
    Input,
    Form,
    Cascader,
    DatePicker
} from 'antd';
import {CollectionCreateFormProps, FormItem } from './basicFormProps';
import { commonModel } from './basicModel';
type PersonCollectionCreateFormProps = CollectionCreateFormProps & { classs: "login-form" } & {
    userOptions: any;
    onUserChange: () => void;
};
const readonly = true;
//模型表单
const customForm: any = (props: PersonCollectionCreateFormProps) => {
    const { form, userOptions, onUserChange, idshow } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <FormItem label="QQ">
                {getFieldDecorator('QQ', {
                    rules: [
                        { required: true, message: '请输入QQ!' },
                        { min: 5, max: 15, message: '账户长度只能为5-15位' },
                    ],
                })(<Input placeholder="账户" />)}
            </FormItem>
            <FormItem label="邮箱">
                {getFieldDecorator('Email', {
                    rules: [
                        { required: true, message: '请输入邮箱!' },
                        { min: 5, max: 20, message: '邮箱长度只能为5-20位' },
                    ],
                })(<Input placeholder="邮箱" />)}
            </FormItem>
            <FormItem label="电话">
                {getFieldDecorator('Handset', {
                    rules: [
                        { required: true, message: '请输入电话!' },
                        { min: 5, max: 15, message: '电话长度只能为5-15位' },
                    ],
                })(<Input placeholder="电话" />)}
            </FormItem>
            <FormItem label="手机号">
                {getFieldDecorator('Telphone', {
                    rules: [
                        { required: true, message: '请输入手机号!' },
                        { min: 11, max: 15, message: '手机号长度只能为11-15位' },
                    ],
                })(<Input placeholder="手机号" />)}
            </FormItem>{' '}
            <FormItem label="地址">
                {getFieldDecorator('Address', {
                    rules: [
                        { required: true, message: '请输入地址!' },
                        { min: 2, max: 50, message: '地址长度只能为2-50位' },
                    ],
                })(<Input placeholder="地址" />)}
            </FormItem>{' '}
            <FormItem label="邮政编码">
                {getFieldDecorator('Postlacode', {
                    rules: [
                        { required: true, message: '请输入邮政编码!' },
                        { min: 5, max: 5, message: '邮政编码长度只能为5位' },
                    ],
                })(<Input placeholder="邮政编码" />)}
            </FormItem>
            <FormItem label="第一次学龄">
                {getFieldDecorator('SecondSchoolAge', {
                    rules: [
                        { required: true, message: '请输入第一次学龄!' },
                        { min: 1, max: 5, message: '第一次学龄长度只能为1-5位' },
                    ],
                })(<Input placeholder="第一次学龄" />)}
            </FormItem>
            <FormItem label="二次专业">
                {getFieldDecorator('SecondSpeciaity', {
                    rules: [
                        { required: true, message: '请输入二次专业!' },
                        { min: 2, max: 20, message: '二次专业长度只能为2-20位' },
                    ],
                })(<Input placeholder="二次专业" />)}
            </FormItem>
            <FormItem label="大学学校">
                {getFieldDecorator('GraduateSchool', {
                    rules: [
                        { required: true, message: '请输入大学学校!' },
                        { min: 2, max: 20, message: '大学学校长度只能为2-20位' },
                    ],
                })(<Input placeholder="大学学校" />)}
            </FormItem>
            <FormItem label="大学就读时间">
                {getFieldDecorator('GraduateDate', {
                    rules: [{ required: true, message: '请输入大学就读时间!' }],
                })(<DatePicker format={'YYYY-MM-DD'} placeholder="大学就读时间" />)}
            </FormItem>
            <FormItem label="入党时间">
                {getFieldDecorator('PartyMemberDate', {
                    rules: [{ required: true, message: '请输入入党时间!' }],
                })(<DatePicker format={'YYYY-MM-DD'} placeholder="入党时间" />)}
            </FormItem>
            <FormItem label="计算机等级">
                {getFieldDecorator('ComputerGrate', {
                    rules: [
                        { required: true, message: '请输入计算机等级!' },
                        { min: 2, max: 5, message: '计算机等级长度只能为2-5位' },
                    ],
                })(<Input placeholder="计算机等级" />)}
            </FormItem>
            <FormItem label="爱好">
                {getFieldDecorator('Likes', {
                    rules: [
                        { required: true, message: '请输入爱好!' },
                        { min: 2, max: 25, message: '爱好长度只能为2-25位' },
                    ],
                })(<Input placeholder="爱好" />)}
            </FormItem>
            <FormItem label="特长">
                {getFieldDecorator('OnesStrongSuit', {
                    rules: [
                        { required: true, message: '请输入特长!' },
                        { min: 2, max: 25, message: '特长长度只能为2-25位' },
                    ],
                })(<Input placeholder="特长" />)}
            </FormItem>
            <Form.Item label="用户">
                {getFieldDecorator('User.Id', {
                    initialValue: '',
                    rules: [{ type: 'array', required: true, message: '请选择用户!' }],
                })(
                    <Cascader
                        options={userOptions}
                        key="Id"
                        onChange={onUserChange}
                        fieldNames={{ label: 'Account', value: 'Id', children: 'items' }}
                        placeholder="请选择用户"
                    />
                )}
            </Form.Item>
        </Form>
    )
};
export const PersonCollectionCreateForm: any = Form.create()((props: PersonCollectionCreateFormProps) => {
    return commonModel(props, customForm);
});

