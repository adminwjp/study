import React from 'react';
import { Input, InputNumber, Form, Select, Cascader, DatePicker, Upload, Icon } from 'antd';

type EditableCellProps = {
    inputType: string;
    editing: boolean;
    dataIndex: any;
    title: string;
    record: any;
    index: number;
    options: any;
    onChange: any;
    //defaultValue:any
    initialValue: any,
    recordoptions: any[],
    ratifierrecordoptions: any[],
    reckoningnameoptions: any[],
    accountitemoptions: any[],
    parentoptions: any[],
    bringupcontentoptions: any[],
    imageurl: string,
    loading1:string
};
function imgUrl(file: any) {
    if (window.URL !== undefined) {
        return window.URL.createObjectURL(file);
    }
    else if (window.webkitURL !== undefined) {
        return window.webkitURL.createObjectURL(file);
    }
    return null;
}
const FormItem = Form.Item;
export const EditableContext = React.createContext({});
export class EditableCell extends React.Component<EditableCellProps> {
     beforeUpload: any = (file: any) => {
        this.setState({ photo: file });
        var img = imgUrl(file);
        this.setState({
            imageUrl: img,
            loading: false,
        });
        return false;//手动上传
    };
    getInput = () => {
        if (this.props.inputType === 'number') {
            return <InputNumber />;
        }
        if (this.props.inputType === 'select') {
            const { options, recordoptions, ratifierrecordoptions, reckoningnameoptions, accountitemoptions, parentoptions, bringupcontentoptions } = this.props;
            return (
                <Select
                    // mode="multiple"
                    style={{ width: 120 }}
                >
                    {
                        options?
                        options.map((item: any) => (
                            <Select.Option key={item} value={item}>
                                {item}
                            </Select.Option>
                        )) : recordoptions ? recordoptions.map((item: any) => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        )) : reckoningnameoptions ? reckoningnameoptions.map((item: any) => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        )) : accountitemoptions ? accountitemoptions.map((item: any) => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        )) : parentoptions ? parentoptions.map((item: any) => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        )) : bringupcontentoptions ? bringupcontentoptions.map((item: any) => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        )):ratifierrecordoptions.map((item: any) => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        )) 
                    }
                </Select>
            )
        }
        //upload
        if (this.props.inputType === 'upload') {
            return <Upload
                name="Photo"
                listType="picture-card"
                className="avatar-uploader"
                showUploadList={false}
                // action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
                beforeUpload={this.beforeUpload}
                   >
                {this.props.imageurl ? (
                    <img src={this.props.imageurl} alt="avatar" style={{ width: '100%' }} />
                ) : (
                        <div>
                            <Icon type={this.props.loading1 ? 'loading' : 'plus'} />
                            <div className="ant-upload-text">上传</div>
                        </div>
                    )}
            </Upload>;
        }
        //DatePicker
        if (this.props.inputType === 'datePicker') {
            return <DatePicker format={'YYYY-MM-DD'} />;
        }
        // if (this.props.inputType === 'select') {
        //     const {options } = this.props;
        //     return (
        //         <div>
        //             <Select
        //                 mode="multiple"
        //                 style={{ width: 120 }}
        //             >
        //                 {
        //                     options.map((item: { Id: number, Name: string }) => (
        //                         <Select.Option key={item.Id.toString()} value={item.Name}>
        //                             {item}
        //                         </Select.Option>
        //                     ))
        //                 }
        //             </Select>
        //         </div>
        //     )
        // }
        //Cascader
        if (this.props.inputType === 'cascader') {
            const { options, onChange } = this.props;

            return (
                <Cascader
                    options={options}
                    key="Id"
                    onChange={onChange}
                    fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                    placeholder="请选择"
                />
            )
        }
        return <Input />;
    };
    render() {
        const { editing, dataIndex, title, inputType, record, index, initialValue, parentoptions , ...restProps } = this.props;
        const rule = parentoptions ? [] : [ { required: true,message: `请输入${title}!`}];
        return (
            <EditableContext.Consumer>
                {(form: any) => {
                    const { getFieldDecorator} = form;
                    return (
                        <td {...restProps}>
                            {editing ? (
                                <FormItem style={{ margin: 0 }}>
                                    {
                                        dataIndex !== "Photo" ? getFieldDecorator(dataIndex, {
                                            rules: rule,
                                            initialValue: initialValue ? initialValue : record[dataIndex],
                                        })(this.getInput()):
                                            getFieldDecorator('Photo', {
                                                rules: [{ required: true, message: '请上传头像!' }],
                                                valuePropName: 'fileList',
                                                getValueFromEvent: (e: any) => {
                                                    if (Array.isArray(e)) {
                                                        return e;
                                                    }
                                                    return e && e.fileList;
                                                }
                                            })(this.getInput())
                                    }
                                     </FormItem>
                            ) : (
                                    restProps.children
                                )}
                        </td>
                    );
                }}
            </EditableContext.Consumer>
        );
    }
};
type EditableRowProps = {
    form: any;
    index: any;
};
const EditableRow = ({ form, index, ...props }: EditableRowProps) => (
    <EditableContext.Provider value={form}>
        <tr {...props} />
    </EditableContext.Provider>
);
export const EditableFormRow = Form.create()(EditableRow);