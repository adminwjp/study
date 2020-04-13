import { EditableContext } from './editTable';
import React from 'react';
import axios from 'axios';
import { Popconfirm,Button, Modal } from 'antd';

export class TableOperator {
    isEditing: (record: any) => boolean;
    urlEntity: any;
    /**
     * 默认为实现 重写覆盖
     */
    get: () => void;
    edit: (key: string) => void;
    cancelEdit: () => void;
    save: (form: any, key: string) => void;
    columns: any;
    constructor() {
        this.get = () => { };
        /**
        * 列表 是否正在编辑事件 默认未 实现 重写覆盖
        * @param record
        */
        this.isEditing = (record: any) => {
            return false;
        };
        /**
        * 列表 编辑按钮事件 默认未 实现 重写覆盖
        * @param key
        */
        this.edit = (key: string) => { };
        /**
         * 列表 取消按钮事件 默认未实现  重写覆盖
         */
        this.cancelEdit = () => { };
        /**
         * 列表 编辑 保存按钮事件 默认事件 未实现 重写
         * @param form
         * @param key
         */
        this.save = (form: any, key: string) => { };
        this.columns = this.tableColumns;
    }
   
     tableColumns = [
        {
            title: '编号',
            key: 'Id',
            dataIndex: 'Id',
            width: '25%',
            align: 'center',
            sorter: (a: { Id: number; }, b: { Id: number; }) => a.Id - b.Id
        },
        {
            title: '创建时间',
            dataIndex: 'CreateDate',
            width: '15%',
            align: 'center',
        },
        {
            title: '修改时间',
            dataIndex: 'UpdateDate',
            width: '15%',
            align: 'center',
        },
        {
            title: '操作',
            align: 'center',
            dataIndex: 'operation',
            render: (text: any, record: any) => {
                const editable = this.isEditing(record);
                return (
                    <div>
                        {editable ? (
                            <span>
                                <EditableContext.Consumer>
                                    {(form: any) => (
                                        <Button
                                            onClick={() => this.save(form, record.Id)}
                                            style={{ marginRight: 8 }}
                                        >
                                            保存
                                            </Button>
                                    )}
                                </EditableContext.Consumer>
                                <Popconfirm
                                    title="确认取消?"
                                    okText="确定"
                                    cancelText="取消"
                                    onConfirm={() => this.cancelEdit()}
                                >
                                    <Button>取消</Button>
                                </Popconfirm>
                            </span>
                        ) : (
                                <div>
                                    <Button onClick={() => this.edit(record.Id)}>编辑</Button>
                                    <Button onClick={() => this.deleteData(record.Id)}>删除</Button>
                                </div>
                            )}
                    </div>
                );
            },
        }
    ];
   
   //$self = this;
    /**
     * 列表 删除按钮事件
     * @param key 
     */
     deleteData(key: string) {
        if (key === "") {
            Modal.error({
                title: '提示',
                content: '没有数据删除...',
            });
        }
        else {
            axios.post(this.urlEntity.delete, { Id: key }).then(res => {
                if (res.data.Success) {
                    Modal.success({
                        title: '提示',
                        content: '删除成功...',
                    });
                    this.get();
                } else {
                    Modal.warning({
                        title: '提示',
                        content: '删除失败...',
                    });
                }
            });
        }
    };
    /**
    * 列表 编辑 保存按钮事件
    * @param form 
    * @param key 
    * @param self
    */
    saveData(form: any, key: string, self: any) {
        form.validateFields((error: any, row: any) => {
            if (error) {
                return;
            }
            const newData = [...self.state.data];
            const index = newData.findIndex(item => key === item.Id);
            if (index > -1) {
                row.Id = key;
                if (self.parse) {
                    self.parse(row);
                }
                console.log(row);
                axios
                    .post(this.urlEntity.edit, row)
                    .then(res => {
                        if (res.data.Success) {
                            Modal.success({
                                title: '提示',
                                content: '编辑成功...',
                            });
                            this.get();
                            //newData.splice(index, 0, row);
                            // self.setState({data:newData});
                        } else {
                            Modal.warning({
                                title: '提示',
                                content: '编辑失败...',
                            });
                        }
                        this.cancelEdit();
                    })
                    .then(err => {
                        this.cancelEdit();
                    });
            } else {
                this.cancelEdit();
            }
            console.log(self.state.data);
        });
    }
    /**
    * 复选框选中 事件
    */
     onSelectChange(selectedRowIds: string[] | number[]) {
        console.log('selectedRowIds changed: ', selectedRowIds);
        return selectedRowIds;
    };
    /**
     * 表单添加 确定按钮 事件
     * @param form 
     */
     handleCreate(form: any, self: any) {
        form.validateFields((err: any, values: any) => {
            if (err) {
                Modal.warning({
                    title: '提示',
                    content: '验证失败...',
                });
                return;
            }
            if (self.parse) {
                self.parse(values);
            }
            const { idshow } = self.state;
            axios.post(idshow ? this.urlEntity.edit : this.urlEntity.add, values).then(res => {
                if (res.data.Success) {
                    self.handleCancel();
                    this.get();
                    Modal.success({
                        title: '提示',
                        content: idshow ?'编辑成功...':'添加成功...',
                    });

                } else {
                    self.handleCancel();
                    Modal.warning({
                        title: '提示',
                        content: idshow ? '编辑失败...' :'添加失败...',
                    });

                }
            }).then(err => { self.handleCancel(); });
            console.log('Received values of form: ', values);
            form.resetFields();
        });
    };
    /**
     * 
     * @param selectedRowIds 复选框 删除按钮事件
     */
     deleteManyData(selectedRowIds: any) {
        const ids = [...selectedRowIds];
        if (ids.length === 0) {
            Modal.error({
                title: '提示',
                content: '没有数据删除操作...',
            });
        }
        else {
            axios.post(this.urlEntity.delete, { Ids: ids }).then(res => {
                if (res.data.Success) {
                    Modal.success({
                        title: '提示',
                        content: '删除成功...',
                    });
                    this.get();
                } else {
                    Modal.warning({
                        title: '提示',
                        content: '删除失败...',
                    });
                }
            });
        }
    };
};

   
   
   
