/**
 * 档案
 */
import axios from 'axios';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import { urls, apiUrl } from '../../js/index';
import { CollectionCreateForm } from './recoreModel';
import React from 'react';
import moment from 'moment';
import {
    Modal
} from 'antd';
type RecordEditableTableState = EditableTableState & {
    userOptions: any;
    loading: boolean;
    imageUrl: string | ArrayBuffer | null;
    photo: any;
};
function imgUrl(file:any) {
   if (window.URL !== undefined) {
        return window.URL.createObjectURL(file);
    }
    else if (window.webkitURL !== undefined) {
        return window.webkitURL.createObjectURL(file);
    }
    return null;
}
export default class Record extends BasicTable<EditableTableProps, RecordEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            userOptions: [],
            loading: false,
            imageUrl: '',
            photo: null,
            idshow:false
        };
        this.tableOperator.urlEntity = urls.record;
        this.columns[0].width = "8%";
        this.columns[1].width = "8%";
        this.columns[2].width = "8%";
        this.columns[3].width = "8%";
        this.columns.splice(1,0,
            {
                title: '档案编号',
                dataIndex: 'RecordNumber',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '姓名',
                dataIndex: 'Name',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '性别',
                dataIndex: 'Sex',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '出生年月',
                dataIndex: 'Birthday',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '身份账号',
                dataIndex: 'CardNo',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '头像',
                dataIndex: 'Photo',
                width: '8%',
                editable: true,
                align: 'center',
                render: (text: any, record: { Photo: React.ReactNode | null }) => (
                    <span>
                        {record.Photo != null ? (
                            <img
                                src={apiUrl + record.Photo}
                                alt="avatar"
                                style={{ width: '100%' }}
                            />
                        ) : (
                            ''
                        )}
                    </span>
                ),
            },
            {
                title: '婚姻状况',
                dataIndex: 'MaritalStatus',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '地址',
                dataIndex: 'Addrees',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '邮政编码',
                dataIndex: 'Postlacode',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '是否是党员',
                dataIndex: 'PartyMember',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '校龄',
                dataIndex: 'SchoolAge',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '特长',
                dataIndex: 'Speciaity',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '外语',
                dataIndex: 'ForeignLanguage',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '外语等级',
                dataIndex: 'Grate',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '名族',
                dataIndex: 'FamousRace',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '籍贯',
                dataIndex: 'NativePlace',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '政治面貌',
                dataIndex: 'PoliticalOutlook',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '学历',
                dataIndex: 'Education',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '专业',
                dataIndex: 'Major',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '用工形式',
                dataIndex: 'EmploymentForm',
                width: '8%',
                editable: true,
                align: 'center',
            },
            {
                title: '用户信息',
                width: '8%',
                align: 'center',
                dataIndex: 'User.Id',
                render: (
                    text: any,
                    record: { User: { Account: React.ReactNode } | null }
                ) => <span>{record.User == null ? '' : record.User.Account}</span>,
              
            }
        );
    }
    componentDidMount() {
        this.get(this);
        this.category();
    }
    category = () => {
        axios.get(urls.category.user).then(res => {
            if (res.data.Success) {
                this.setState({ userOptions: res.data.Data });
            }
        });
    };
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["User", "UserId"]);
        if (data.User != null) {
            obj["User.Id"] = [data.User.Id];
        }
       // obj["Photo"] = apiUrl + obj["Photo"];
        this.setState({ imageUrl: apiUrl + obj["Photo"]});
        obj["Birthday"] = moment(data["Birthday"]);
        this.form.setFieldsValue(obj);
    };
    edit: any = (key: string, self: any) => {
        //文件编辑暂无实现
        Modal.warning({
            title: '提示',
            content: '编辑暂无,敬请公告...',
        });
        //self.setState({ editingId: key });
    };
    beforeUpload: any = (file: any) => {
        this.setState({ photo: file });
        var img = imgUrl(file);
        this.setState({
            imageUrl: img,
            loading: false,
        });
        return false;//手动上传
    };
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "User.Id") {
            return record.Role != null ? [record.User.Id] : record[col.dataIndex];
        }
        return col.dataIndex === 'Birthday' ? moment(moment(record[col.dataIndex]).utcOffset(480).format('YYYY-MM-DD')) : record[col.dataIndex];
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
                    inputType: col.dataIndex === 'User.Id' ? 'cascader' :
                        (col.dataIndex === 'Birthday' ? 'datePicker' : col.dataIndex === 'Photo' ? 'upload' : 'text'), 
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: col.dataIndex === 'User.Id' ? this.state.roleOptions : null,
                    editing: this.isEditing(record, this),
                    onChange: col.dataIndex === 'User.Id' ? this.handChange : null,
                    initialValue: this.initialValue(col, record),
                    imageurl: col.dataIndex === 'Photo' ? apiUrl + record.Photo : null,
                    loading1: col.dataIndex === 'Photo' ?"false":null
                }),
            };
        });
        return columns;
    };
    handleCreate: any = () => {
        const form = this.form;
        form.validateFields((err: any, values: any) => {
            if (err) {
                Modal.warning({
                    title: '提示',
                    content: '验证失败...',
                });
                return;
            }
            var data = new FormData();
            const { photo } = this.state;
            for (const key in values) {
                if (key === "User") {
                    data.append("User.Id", values[key].Id);
                    continue;
                }
                if (key === "Photo") {
                    data.append(key, photo);
                    continue;
                }
                if (values.hasOwnProperty(key)) {
                    const element = values[key];
                    data.append(key, element);
                }
            } 
            console.log(data);
            axios.post(this.tableOperator.urlEntity.add, data, { headers: { 'Content-Type': 'multipart/form-data' }}).then(res => {
                if (res.data.Success) {
                    this.handleCancel();
                    this.get();
                    Modal.success({
                        title: '提示',
                        content: '添加成功...',
                    });

                } else {
                    this.handleCancel();
                    Modal.warning({
                        title: '提示',
                        content: '添加失败...',
                    });

                }
            }).then(err => { this.handleCancel(); });
            console.log('Received values of form: ', values);
            form.resetFields();
        });
        //this.tableOperator.handleCreate(form, this);
    };
    handChange = (val: string) => {
        console.log(val);
    };
    modelShow: any = () => {
        return (
            <CollectionCreateForm
                userOptions={this.state.userOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onUserChange={this.handChange}
                imageUrl={this.state.imageUrl}
                loading={this.state.loading}
                beforeUpload={this.beforeUpload}
                idshow={this.state.idshow}
            />
        );
    };
}
