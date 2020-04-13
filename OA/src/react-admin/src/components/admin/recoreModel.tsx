/**
 * 档案
 */
import React from 'react';
import {
    Input,
    Form,
    Modal,
    Icon,
    Cascader,
    Select,
    DatePicker,
    Upload
} from 'antd';
import {CollectionCreateFormProps } from './basicFormProps';
import { FormProps } from 'antd/lib/form';
const { Option } = Select;
const educationOptions = ["小学", "中学", "职中", "大专", "大学", "研究生", "博士生", "硕士生"];
const FormItem = Form.Item;

type RecordCollectionCreateFormProps = CollectionCreateFormProps& {
    userOptions: any;
    onBirthdayChange: () => void;
    beforeUpload: () => boolean;
    imageUrl: string;
    loading: boolean;
    onUserChange: () => void;
} & FormProps;
const readonly = true;
//模型表单
export const CollectionCreateForm: any = Form.create()((props: RecordCollectionCreateFormProps) => {
    const {
        visible,
        onCancel,
        onCreate,
        form,
        userOptions,
        beforeUpload,
        imageUrl,
        loading,
        onUserChange, idshow
    } = props;
    
    const { getFieldDecorator } = form!;
    return (
        <Modal
            title="添加档案信息"
            visible={visible}
            onCancel={onCancel}
            onOk={onCreate}
            okText="确定"
            cancelText="取消"
            className="login-form"
        >
            <Form>
                {idshow ? (<FormItem label="编号" >
                    {
                        getFieldDecorator('Id', {

                        })(<Input readOnly={readonly} />)
                    }
                </FormItem>) : null}
                <FormItem label="档案编号">
                    {getFieldDecorator('RecordNumber', {
                        rules: [
                            { required: true, message: '请输入档案编号!' },
                            { min: 5, max: 5, message: '档案编号长度只能为5位' },
                        ],
                    })(<Input />)}
                </FormItem>
                <FormItem label="姓名">
                    {getFieldDecorator('Name', {
                        rules: [
                            { required: true, message: '请输入姓名!' },
                            { min: 2, max: 5, message: '姓名长度只能为2-10位' },
                        ],
                    })(<Input />)}
                </FormItem>
                <Form.Item label="性别">
                    {getFieldDecorator('Sex', {
                        initialValue: '',
                        rules: [{ required: true, message: '请选择性别!' }],
                    })(
                        <Select style={{ width: 200 }} placeholder="请选择性别">
                            <Option value="男">男</Option>
                            <Option value="女">女</Option>
                        </Select>
                    )}
                </Form.Item>
                <FormItem label="出生日期">
                    {getFieldDecorator('Birthday', {
                        rules: [{ required: true, message: '请输入出生日期!' }],
                    })(<DatePicker format={'YYYY-MM-DD'} />)}
                </FormItem>
                <FormItem label="身份证号">
                    {getFieldDecorator('CardNo', {
                        rules: [
                            { required: true, message: '请输入身份证号!' },
                            { min: 18, max: 20, message: '身份证号长度只能为18-20位' },
                        ],
                    })(<Input />)}
                </FormItem>
                <FormItem label="头像">
                    {getFieldDecorator('Photo', {
                        // initialValue: (details.attachments || []).map(f => ({
                        //     // 为了提供给上传组件回显
                        //     uid: f.id, // 这是上传组件规定的文件唯一标识，内部会提供给Form以便正常渲染回显列表
                        //     name: f.filename,
                        //     status: 'done',
                        //     url: f.url,
                        //     // 为了迎合最终向后端提交附件时方便新老文件统一处理
                        //     saveParams: {
                        //         filename: f.filename,
                        //         url: f.url,
                        //         size: f.size
                        //     }
                        // })),
                        rules: [{ required: true, message: '请上传头像!' }],
                        valuePropName: 'fileList',
                        getValueFromEvent: (e) => {
                            if (Array.isArray(e)) {
                                return e;
                            }
                            return e && e.fileList;
                        }
                    })(
                        <Upload
                            name="Photo"
                            listType="picture-card"
                            className="avatar-uploader"
                            showUploadList={ false }
                            // action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
                            beforeUpload={beforeUpload}
                        >
                            {imageUrl ? (
                                <img src={imageUrl} alt="avatar" style={{ width: '100%' }} />
                            ) : (
                                    <div>
                                        <Icon type={loading ? 'loading' : 'plus'} />
                                        <div className="ant-upload-text">上传</div>
                                    </div>
                                )}
                        </Upload>
                    )}
                </FormItem>
                <Form.Item label="婚姻状况">
                    {getFieldDecorator('MaritalStatus', {
                        initialValue: '',
                        rules: [{ required: true, message: '请选择婚姻状况!' }],
                    })(
                        <Select style={{ width: 200 }} placeholder="请选择婚姻状况">
                            <Option value="已婚">已婚</Option>
                            <Option value="未婚">未婚</Option>
                            <Option value="丧偶">丧偶</Option>
                            <Option value="未知">未知</Option>
                        </Select>
                    )}
                </Form.Item>
                <FormItem label="地址">
                    {getFieldDecorator('Addrees', {
                        rules: [
                            { required: true, message: '请输入地址!' },
                            { min: 5, max: 50, message: '地址长度只能为5-50位' },
                        ],
                    })(<Input />)}
                </FormItem>
                <FormItem label="邮政编码">
                    {getFieldDecorator('Postlacode', {
                        rules: [
                            { required: true, message: '请输入邮政编码!' },
                            { min: 5, max: 5, message: '邮政编码长度只能为5-位' },
                        ],
                    })(<Input />)}
                </FormItem>
                <Form.Item label="党员">
                    {getFieldDecorator('PartyMember', {
                        initialValue: '',
                        rules: [{ required: true, message: '请选择党员!' }],
                    })(
                        <Select style={{ width: 200 }} placeholder="请选择党员">
                            <Option value="是">是</Option>
                            <Option value="否">否</Option>
                        </Select>
                    )}
                </Form.Item>
                <FormItem label="校龄">
                    {getFieldDecorator('SchoolAge', {
                        rules: [
                            { required: true, message: '请输入校龄!' },
                            { min: 1, max: 3, message: '校龄长度只能为1-3位' },
                        ],
                    })(<Input />)}
                </FormItem>
                <FormItem label="特长">
                    {getFieldDecorator('Speciaity', {
                        rules: [
                            { required: true, message: '请输入特长!' },
                            { min: 1, max: 5, message: '特长长度只能为1-5位' },
                        ], initialValue:"无"
                    })(<Input />)}
                </FormItem>
                <FormItem label="外语">
                    {getFieldDecorator('ForeignLanguage', {
                        rules: [
                            { required: true, message: '请输入外语!' },
                            { min: 2, max: 5, message: '外语长度只能为2-5位' },
                        ], initialValue:"英语"
                    })(<Input />)}
                </FormItem>{' '}
                <FormItem label="外语等级">
                    {getFieldDecorator('Grate', {
                        rules: [
                            { required: true, message: '请输入外语等级!' },
                            { min: 2, max: 5, message: '外语等级长度只能为2-5位' },
                        ], initialValue:"3级"
                    })(<Input />)}
                </FormItem>
                <FormItem label="名族">
                    {getFieldDecorator('FamousRace', {
                        rules: [
                            { required: true, message: '请输入名族!' },
                            { min: 2, max: 5, message: '名族长度只能为2-5位' },
                        ], initialValue:"汉族"
                    })(<Input />)}
                </FormItem>{' '}
                <FormItem label="籍贯">
                    {getFieldDecorator('NativePlace', {
                        rules: [
                            { required: true, message: '请输入籍贯!' },
                            { min: 2, max: 5, message: '籍贯长度只能为2-5位' },
                        ], initialValue:"湖北"
                    })(<Input />)}
                </FormItem>
                <Form.Item label="政治面貌">
                    {getFieldDecorator('PoliticalOutlook', {
                        initialValue: '',
                        rules: [{ required: true, message: '请选择政治面貌!' }],
                    })(
                        <Select style={{ width: 200 }} placeholder="请选择政治面貌">
                            <Option value="团员">团员</Option>
                            <Option value="党员">党员</Option>
                            <Option value="无">无</Option>
                        </Select>
                    )}
                </Form.Item>
                <Form.Item label="学历">
                    {getFieldDecorator('Education', {
                        initialValue: '',
                        rules: [{ required: true, message: '请选择学历!' }],
                    })(
                        <Select style={{ width: 200 }} placeholder="请选择学历">
                            {educationOptions.map(item => (
                                <Select.Option key={item} value={item}>
                                    {item}
                                </Select.Option>
                            ))}
                        </Select>
                    )}
                </Form.Item>
                <FormItem label="专业">
                    {getFieldDecorator('Major', {
                        rules: [
                            { required: true, message: '请输入专业!' },
                            { min: 2, max: 50, message: '专业长度只能为2-50位' },
                        ], initialValue:"计算机"
                    })(<Input />)}
                </FormItem>
                <FormItem label="用工形式">
                    {getFieldDecorator('EmploymentForm', {
                        rules: [
                            { required: true, message: '请输入用工形式!' },
                            { min: 2, max: 50, message: '用工形式长度只能为2-50位' },
                        ], initialValue:"合同工"
                    })(<Input />)}
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
        </Modal>
    );
});