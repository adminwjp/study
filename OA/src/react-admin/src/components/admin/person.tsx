/**
 * 员工信息
 */
import axios from 'axios';
import React from 'react';
import { urls } from '../../js/index';
import { PersonCollectionCreateForm } from './personModel';
import { EditableTableProps, EditableTableState, BasicTable } from './basicTable';
import moment from 'moment';
import { Modal } from 'antd';
type PersonEditableTableState = EditableTableState & { userOptions: any;};
export default class Person extends BasicTable<EditableTableProps, PersonEditableTableState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            userOptions: [],
            idshow:false
        };
        this.tableOperator.urlEntity = urls.person;//设置属性
        this.columns[0].width = "4%";
        this.columns[1].width = "4%";
        this.columns[2].width = "4%";
        this.columns[3].width = "4%";
        this.columns.splice(1, 0,
            {
                title: 'QQ',
                dataIndex: 'QQ',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '邮箱',
                dataIndex: 'Email',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '电话',
                dataIndex: 'Handset',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '手机号',
                dataIndex: 'Telphone',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '地址',
                dataIndex: 'Address',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '邮政编码',
                dataIndex: 'Postlacode',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '第一次学龄',
                dataIndex: 'SecondSchoolAge',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '二次专业',
                dataIndex: 'SecondSpeciaity',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '大学学校',
                dataIndex: 'GraduateSchool',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '大学就读时间',
                dataIndex: 'GraduateDate',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '入党时间',
                dataIndex: 'PartyMemberDate',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '计算机等级',
                dataIndex: 'ComputerGrate',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '爱好',
                dataIndex: 'Likes',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '特长',
                dataIndex: 'OnesStrongSuit',
                width: '4%',
                align: 'center', editable: true,
            },
            {
                title: '用户信息',
                width: '4%',
                align: 'center',
                dataIndex: 'User.Id', editable: true,
                render: (
                    text: any,
                    record: { User: { Account: React.ReactNode } | null }
                ) => (
                        <span>
                            {record.User == null ? '' : record.User.Account}
                        </span>
                    ),
            });
    }
    componentDidMount() {
        this.get(this);
        this.category();
    };
    width: any = () => {
        return '120%';
    };
    category = () => {
        axios.get(urls.category.user).then(res => {
            if (res.data.Success) {
                this.setState({ userOptions: res.data.Data });
            }
        });
    };
    edit: any = (key: string, self: any) => {
        //文件编辑暂无实现
        Modal.warning({
            title: '提示',
            content: '编辑暂无,敬请公告...',
        });
        //self.setState({ editingId: key });
    };
    setValue: any = (data: any) => {
        let obj = this.setEditValue(data, ["User","UserId"]);
        if (data.User != null) {
            obj["User.Id"] = [data.User.Id];
        }
        obj["GraduateDate"] = moment(data["GraduateDate"]);
        obj["PartyMemberDate"] = moment(data["PartyMemberDate"]);
        this.form.setFieldsValue(obj);
    };
    parse = (row: any) => {
        row.User.Id = row.User.Id[row.User.Id.length - 1];
    };
    initialValue = (col: any, record: any) => {
        if (col.dataIndex === "User.Id") {
            return record.User != null ? [record.User.Id] : record[col.dataIndex];
        }
        return col.dataIndex === 'GraduateDate' || col.dataIndex === 'PartyMemberDate' ? moment(moment(record[col.dataIndex]).utcOffset(480).format('YYYY-MM-DD HH:mm')) : record[col.dataIndex];
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
                    inputType: col.dataIndex === 'User.Id' ? 'cascader' : (col.dataIndex === 'GraduateDate' || col.dataIndex === 'PartyMemberDate' ? 'datePicker': 'text'),
                    dataIndex: col.dataIndex,
                    title: col.title,
                    options: col.dataIndex === 'User.Id' ? this.state.userOptions : null,
                    editing: this.isEditing(record, this),
                    onChange: col.dataIndex === 'User.Id' ? this.handChange : null,
                    initialValue: this.initialValue(col, record)
                }),
            };
        });
        return columns;
    };
    handChange = (val: string) => {
        console.log(val);
    };
    modelShow: any = () => {
        return (
            <PersonCollectionCreateForm
                title="添加员工信息"
                userOptions={this.state.userOptions}
                ref={this.saveFormRef}
                visible={this.state.visible}
                onCancel={this.handleCancel}
                onCreate={this.handleCreate}
                onUserChange={this.handChange}
                idshow={this.state.idshow}
            />
        );
    };
                   
}
