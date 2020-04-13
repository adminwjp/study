/**
 * 基本操作封装 类
 */
import axios from 'axios';
import React from 'react';
import {
    Table,
    Button,
    Modal,
} from 'antd';
import { TableRowSelection } from 'antd/lib/table';
import { EditableFormRow, EditableCell } from './editTable';
import { TableOperator } from './table';
//import BasicModel from './basicModel';
export type EditableTableProps = {};
export type EditableTableState= {
    selectedRowIds: any;
    data: any;
    editingId: string;
    visible: boolean;
    sortedInfo: any;
    idshow:boolean;
};
export class BasicTable<P1 extends {}, S1 extends EditableTableState> extends React.Component<P1, S1> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedRowIds: [],
            data: [],
            editingId: '',
            visible: false,
            sortedInfo: null,
            idshow: false
        };
        this.tableOperator = new TableOperator();
        this.tableOperator.get = () => { this.get(this); };
        this.tableOperator.isEditing = (record: any) => { return this.isEditing(record, this); };
        this.tableOperator.edit = (key: string) => { this.edit(key, this); };
        this.tableOperator.cancelEdit = () => { this.cancel(this); };
        this.tableOperator.save = (form: any, key: string) => { this.save(form, key, this); };
        let cols: any[] = [];
        this.tableOperator.columns.forEach((it: any) => { cols.push(it) });
        this.columns = cols;
    }
    state: any;
    //分页、排序、筛选变化时触发
    onChange: any = (pagination: any, filters: any, sorter: any, extra: { currentDataSource: [] })=>{ 
    
    };
    //默认实现 重新设置属性
    tableOperator: any;
    //默认未实现 重写 覆盖
    collectionCreateForm: any = ()=> { };
    get:any= (self:any) => {
        axios.post(self.tableOperator.urlEntity.get).then(res => {
            if (res.data.Success) {
                self.setState({ data: res.data.Data });
            }
        });
    };
    columns: any[]=[];
    isEditing: any = (record: any, self: any) => {
        return record.Id === self.state.editingId;
    };
    edit: any = (key: string, self: any) => {
        self.setState({ editingId: key });
    };
    save: any = (form: any, key: string, self: any)=> {
        this.tableOperator.saveData(form, key, self);
    }
    cancel:any = (self: any) => {
        self.setState({ editingId: '' });
    };
    onSelectChange:any = (selectedRowIds: string[] | number[]) => {
        console.log('selectedRowIds changed: ', selectedRowIds);
        this.setState({ selectedRowIds });
    };
    form: any;
    //basicModel:any|BasicModel = new BasicModel();
    showModal: any = () => {
        this.form.resetFields();
        this.setState({ visible: true,idshow:false });
    };
    handleCancel: any = () => {
        this.setState({ visible: false });
    };
    handleCreate: any = () => {
        const form = this.form;
        this.tableOperator.handleCreate(form, this);
    };
    saveFormRef:any = (form: any) => {
        this.form = form;
    };
    deleteMany:any = () => {
        const { selectedRowIds } = this.state;
        this.tableOperator.deleteManyData(selectedRowIds);
    };
    /**
      * 表单 展示
      */
    modelShow: any = () => { };
    getColumns: any = () => { 
        const columns = this.columns.map((col: { editable: any; dataIndex: any; title: any; }) => {
            if (!col.editable) {
                return col;
            }
            return {
                ...col,
                onCell: (record: any) => ({
                    record,
                    //inputType: col.dataIndex === 'age' ? 'number' : 'text',
                    dataIndex: col.dataIndex,
                    title: col.title,
                    editing: this.isEditing(record, this),
                }),
            };
        });
        return columns;
    };
    componentDidMount() {
        this.get(this);
    }
    width: any = () => {
        return '100%';
    };
    setValue: any = (data: any) => { 
       
    };
    setEditValue: any = (data: any, skipOptions: string[] | null) => {
        let obj = Object.create({});
        for (const key in data) {
            const element = data[key];
            if (key === "Id" || key === "CreateDate" || key === "UpdateDate") {
                continue;
            }
            if (skipOptions && skipOptions.indexOf(key)>-1) {
                continue;
            }
            obj[key] = element;
        } 
        console.log(obj);
        return obj;
    };
    editModel: any = () => {
        //https://blog.csdn.net/weixin_42424269/article/details/88374895
        const { selectedRowIds } = this.state;
        if (selectedRowIds.length === 1) {
            const { data } = this.state;
            const obj = [...data].find(it => it.Id === selectedRowIds[0]);
            console.log(obj)
            this.setValue(obj);
            setTimeout(() => {
                this.form.setFieldsValue({ Id: obj.Id });
            }, 0);
            this.setState({ visible: true, idshow: true });
        }
        else {
            Modal.warn({
                title: '提示',
                content: '编辑只能选中一行数据...',
            });
        }
     };
    render() {
        const components = {
            body: {
                row: EditableFormRow,
                cell: EditableCell,
            },
        };
        const dialog = this.modelShow();
        const columns = this.getColumns();
        const { selectedRowIds } = this.state;
        const rowSelection: TableRowSelection<any> = {
            selectedRowKeys: selectedRowIds,
            onChange: this.onSelectChange,
        };
        const w = this.width();
        return (
            <div style={{ margin: 10,width:'100%' }}>
                <Button type="primary" onClick={this.showModal}>
                    添加
                               </Button>
                <Button type="primary" onClick={this.editModel}>
                    编辑
                </Button>
                <Button type="primary" onClick={this.deleteMany}>
                    删除
                 </Button>
                {dialog}
                <Table
                    style={{ margin: 10, width: w }}
                    scroll={{ x:'100%'}}
                    key="Id"
                    rowKey="Id"
                    rowSelection={rowSelection}
                    components={components}
                    bordered
                    dataSource={this.state.data}
                    columns={columns}
                    rowClassName={(record: any, index: number) => 'editable-row'}
                />
            </div>
        );
    }
}

export class EmptyBasicTable extends BasicTable<EditableTableProps, EditableTableState>{

}
