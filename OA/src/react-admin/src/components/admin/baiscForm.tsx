import React from 'react';
import { Input, Select, Form, Icon, Cascader, DatePicker } from 'antd';
import {
    CollectionCreateFormProps, UserCollectionCreateFormProps, TimeCardCollectionCreateFormProps, ReckoningListCollectionCreateFormProps,
    ReckoningCollectionCreateFormProps, ReawrdsAndPunishmentCollectionCreateFormProps, ModuleCollectionCreateFormProps, BringUpPersonCollectionCreateFormProps, FormItem
} from "./basicFormProps";
const readonly = true;
//模型表单 角色  名族
export const formName = (props: CollectionCreateFormProps) => {
    const { form, idshow } = props;
    const { getFieldDecorator } = form!;
    //const iddisplay = { display: (idshow ? "block" : "none")};
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>):null}
            <FormItem label="名称">
                {
                    getFieldDecorator('Name', {
                        rules: [{ required: true, message: '请输入名称!' }],
                    })(<Input />)
                }
            </FormItem>
        </Form>);
};
//考勤/账套项目
export const formAccountItem = (props: CollectionCreateFormProps) => { 
    const { form, idshow } = props;
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
                    rules: [
                        { required: true, message: '请输入名称!' },
                        { min: 2, max: 10, message: '名称长度只能为2-10位' },
                    ],
                })(<Input placeholder="名称" />)}
            </FormItem>
            <Form.Item label="类型">
                {getFieldDecorator('Type', {
                    initialValue: '',
                    rules: [{ required: true, message: '请选择类型!' }],
                })(
                    <Select key="Id" placeholder="请选择类型">
                        {['考勤', '账套'].map(item => (
                            <Select.Option key={item} value={item}>
                                {item}
                            </Select.Option>
                        ))}
                    </Select>
                )}
            </Form.Item>
            <FormItem label="单位">
                {getFieldDecorator('Utit', {
                    rules: [
                        { required: true, message: '请输入单位!' },
                        { min: 2, max: 10, message: '单位长度只能为2-10位' },
                    ],
                })(<Input placeholder="单位" />)}
            </FormItem>
        </Form>
    );
};
//用户
export const formUser = (props: UserCollectionCreateFormProps) => {
    const { form, options, onChange, idshow } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <FormItem label="账户">
                {getFieldDecorator('Account', {
                    rules: [
                        { required: true, message: '请输入账户!' },
                        { min: 5, max: 20, message: '账户长度只能为5-20位' },
                    ],
                })(
                    <Input
                        prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />}
                        placeholder="账户"
                    />
                )}
            </FormItem>
            <FormItem label="密码">
                {getFieldDecorator('Password', {
                    rules: [
                        { required: true, message: '请输入密码!' },
                        { min: 5, max: 50, message: '密码长度只能为5-50位' },
                    ],
                })(
                    <Input
                        prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />}
                        type="password"
                        placeholder="密码"
                    />
                )}
            </FormItem>
            <Form.Item label="角色">
                {getFieldDecorator('Role.Id', {
                    initialValue: '',
                    // rules: [
                    //     { type: 'array', required: true, message: '请选择角色!' },
                    // ],
                })(
                    <Cascader
                        options={options}
                        key="Id"
                        onChange={onChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择角色"
                    />
                )}
            </Form.Item>
        </Form>
    );
};
//培训内容
export const formBringUpContent = (props: UserCollectionCreateFormProps) => {
    const { form, idshow } = props;
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
                    rules: [
                        { required: true, message: '请输入名称!' },
                        { min: 2, max: 25, message: '名称长度只能为2-25位' },
                    ],
                })(
                    <Input placeholder="名称" />
                )}
            </FormItem>
            <FormItem label="内容">
                {getFieldDecorator('Content', {
                    rules: [
                        { required: true, message: '请输入内容!' },
                        { min: 5, max: 50, message: '内容长度只能为5-50位' },
                    ],
                })(
                    <Input placeholder="内容" />
                )}
            </FormItem>
            <FormItem label="开始时间">
                {getFieldDecorator('StartDate', {
                    rules: [{ required: true, message: '请输入开始时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm" />)}
            </FormItem>
            <FormItem label="结束时间">
                {getFieldDecorator('EndDate', {
                    rules: [{ required: true, message: '请输入结束时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm" />)}
            </FormItem>
            <FormItem label="单位">
                {getFieldDecorator('Unit', {
                    rules: [
                        { required: true, message: '请输入单位!' },
                        { min: 2, max: 25, message: '单位长度只能为5-25位' },
                    ],
                })(
                    <Input placeholder="单位" />
                )}
            </FormItem>
            <FormItem label="讲课者">
                {getFieldDecorator('Lecturer', {
                    rules: [
                        { required: true, message: '请输入讲课者!' },
                        { min: 2, max: 25, message: '讲课者长度只能为5-25位' },
                    ],
                })(
                    <Input placeholder="讲课者" />
                )}
            </FormItem>
            <FormItem label="地点">
                {getFieldDecorator('Place', {
                    rules: [
                        { required: true, message: '请输入地点!' },
                        { min: 2, max: 50, message: '地点长度只能为5-50位' },
                    ],
                })(
                    <Input placeholder="地点" />
                )}
            </FormItem>
        </Form>
    );
};
//考勤 各种逻辑 判断 暂无实现 默认测试用的
export const formTimeCard = (props: TimeCardCollectionCreateFormProps) => { 
    const { form, ratifierrecordoptions, recordoptions, idshow } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <FormItem label="员工">
                {getFieldDecorator('Record.Id', {
                    rules: [
                        { required: true, message: '请选择员工!' },
                    ],
                })(
                    <Select key="Id" placeholder="员工">
                        {recordoptions.map((item: { Id: any; Name: any; } )=> (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        ))}
                    </Select>
                )}
            </FormItem>
            <FormItem label="迟到原因">
                {getFieldDecorator('Explain', {
                    rules: [
                        { required: true, message: '请输入迟到原因!' },
                        { min: 2, max: 100, message: '迟到原因长度只能为2-100位' },
                    ],
                })(
                    <Input placeholder="迟到原因" />
                )}
            </FormItem>
            <FormItem label="上班时间">
                {getFieldDecorator('StartDate', {
                    rules: [{ required: true, message: '请输入上班时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm" />)}
            </FormItem>
            <FormItem label="下班时间">
                {getFieldDecorator('EndDate', {
                    rules: [{ required: true, message: '请输入下班时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm" />)}
            </FormItem>
            <FormItem label="批准人">
                {getFieldDecorator('RatifierRecord.Id', {
                    rules: [
                        { required: true, message: '请选择批准人!' },
                    ],
                })(
                    <Select key="Id" placeholder="批准人">
                        {ratifierrecordoptions.map(item => (
                            <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                            </Select.Option>
                        ))}
                    </Select>
                )}
            </FormItem>
            <FormItem label="批准时间">
                {getFieldDecorator('RatifierDate', {
                    rules: [{ required: true, message: '请输入批准时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm" />)}
            </FormItem>
        </Form>
    );
};
//账套人员设置
export const formReckoningList = (props: ReckoningListCollectionCreateFormProps) => {
    const {
        form,
        recordOptions,
        onRecordChange,
        reckoningNameOptions,
        onReckoningNameChange, idshow
    } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <Form.Item label="档案">
                {getFieldDecorator('Record.Id', {
                    initialValue: '',
                    rules: [{ type: 'array', required: true, message: '请选择档案!' }],
                })(
                    <Cascader
                        options={recordOptions}
                        key="Id"
                        onChange={onRecordChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择档案"
                    />
                )}
            </Form.Item>
            <Form.Item label="账套名称">
                {getFieldDecorator('ReckoningName.Id', {
                    initialValue: '',
                    rules: [{ type: 'array', required: true, message: '请选择账套名称!' }],
                })(
                    <Cascader
                        options={reckoningNameOptions}
                        key="Id"
                        onChange={onReckoningNameChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择账套名称"
                    />
                )}
            </Form.Item>
        </Form>
    );
};
//账套信息
export const formReckoning = (props: ReckoningCollectionCreateFormProps) => { 
    const {
        form,
        recordOptions,
        onRecordChange,
        accountItemOptions,
        onAccountItemChange, idshow
    } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <Form.Item label="档案">
                {getFieldDecorator('Record.Id', {
                    initialValue: '',
                    rules: [{ type: 'array', required: true, message: '请选择档案!' }],
                })(
                    <Cascader
                        options={recordOptions}
                        key="Id"
                        onChange={onRecordChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择档案"
                    />
                )}
            </Form.Item>
            <Form.Item label="考勤/账套项目">
                {getFieldDecorator('AccountItem.Id', {
                    initialValue: '',
                    rules: [{ type: 'array', required: true, message: '请选择考勤/账套项目!' }],
                })(
                    <Cascader
                        options={accountItemOptions}
                        key="Id"
                        onChange={onAccountItemChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择考勤/账套项目"
                    />
                )}
            </Form.Item>
            <FormItem label="账套金额">
                {getFieldDecorator('Money', {
                    rules: [{ required: true, message: '请输入账套金额!' }],
                })(<Input placeholder="请输入账套金额" />)}
            </FormItem>
        </Form>
    );
};
//奖惩信息
export const formReawrdsAndPunishment = (props: ReawrdsAndPunishmentCollectionCreateFormProps) => { 
    const {
        form,
        recordOptions,
        onRecordChange, idshow
    } = props;
    const { getFieldDecorator } = form!;
    const { Option } = Select;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <Form.Item label="档案">
                {getFieldDecorator('Record.Id', {
                    initialValue: '',
                    rules: [{ type: 'array', required: true, message: '请选择档案!' }],
                })(
                    <Cascader
                        options={recordOptions}
                        key="Id"
                        onChange={onRecordChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择档案"
                    />
                )}
            </Form.Item>
            <Form.Item label="奖惩">
                {getFieldDecorator('Type', {
                    initialValue: '',
                    rules: [{ required: true, message: '请选择奖惩!' }],
                })(
                    <Select style={{ width: 200 }} placeholder="请选择奖惩">
                        <Option value="1">奖</Option>
                        <Option value="2">惩</Option>
                    </Select>
                )}
            </Form.Item>
            <FormItem label="原因">
                {getFieldDecorator('Reason', {
                    rules: [
                        { required: true, message: '请输入原因!' },
                        { min: 2, max: 250, message: '原因长度只能为2-250位' },
                    ], initialValue: "暂无..."
                })(<Input placeholder="请输入原因" />)}
            </FormItem>
            <FormItem label="奖惩内容">
                {getFieldDecorator('Content', {
                    rules: [
                        { required: true, message: '请输入奖惩内容!' },
                        { min: 2, max: 100, message: '奖惩内容长度只能为2-100位' },
                    ],
                    initialValue: "暂无..."
                })(<Input placeholder="请输入奖惩内容" />)}
            </FormItem>
            <FormItem label="奖惩金额">
                {getFieldDecorator('Money', {
                    rules: [{ required: true, message: '请输入奖惩金额!' }],
                })(<Input placeholder="请输入奖惩金额" />)}
            </FormItem>
            <FormItem label="奖惩开始时间">
                {getFieldDecorator('StartDate', {
                    rules: [{ required: true, message: '请输入奖惩开始时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm:ss" />)}
            </FormItem>
            <FormItem label="奖惩结束时间">
                {getFieldDecorator('EndDate', {
                    rules: [{ required: true, message: '请输入奖惩开始时间!' }],
                })(<DatePicker format="YYYY-MM-DD HH:mm:ss" />)}
            </FormItem>
        </Form>
    );
};
//模块
export const formModule = (props: ModuleCollectionCreateFormProps) => { 
    const { form, parentOptions, onParentChange, idshow } = props;
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
                    rules: [
                        { required: true, message: '请输入名称!' },
                        { min: 2, max: 25, message: '名称长度只能为2-25位' },
                    ],
                })(<Input placeholder="名称" />)}
            </FormItem>
            <FormItem label="链接">
                {getFieldDecorator('Href', {
                    rules: [
                        // { required: true, message: '请输入链接!' },
                        // { min: 2, max: 50, message: '链接长度只能为2-50位' },
                    ],
                })(<Input placeholder="链接" />)}
            </FormItem>
            <Form.Item label="父模块">
                {getFieldDecorator('Parent.Id', {
                    initialValue: '',
                    // rules: [{ type: 'array', required: true, message: '请选择父模块!' }],
                })(
                    <Cascader
                        options={parentOptions}
                        key="Id"
                        onChange={onParentChange}
                        fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                        placeholder="请选择父模块"
                    />
                )}
            </Form.Item>
        </Form>
    );
};
//职位信息
export const formDuty = (props: CollectionCreateFormProps) => {
    const { form, idshow } = props;
    const { getFieldDecorator } = form!;
    return (
        <Form>
            {idshow ? (<FormItem label="编号" >
                {
                    getFieldDecorator('Id', {

                    })(<Input readOnly={readonly} />)
                }
            </FormItem>) : null}
            <FormItem label="加入时间">
                {getFieldDecorator('AccessionDate', {
                    rules: [
                        { required: true, message: '请输入加入时间!' },
                    ],
                })(<DatePicker format={'YYYY-MM-DD HH:mm:ss'} placeholder="加入时间" />)}
            </FormItem>
            <FormItem label="离开时间">
                {getFieldDecorator('DimissionDate', {
                    rules: [
                        { required: true, message: '请输入离开时间!' },
                    ],
                })(<DatePicker format={'YYYY-MM-DD HH:mm:ss'} placeholder="离开时间" />)}
            </FormItem>
      
            <FormItem label="离职原因">
                {getFieldDecorator('DimissionReason', {
                    rules: [
                        { required: true, message: '请输入离职原因!' },
                        { min: 2, max: 250, message: '离职原因长度只能为2-250位' },
                    ], initialValue: "暂无...."
                })(<Input placeholder="离职原因" />)}
            </FormItem>
            <FormItem label="第一次工作(签条约)时间">
                {getFieldDecorator('FirstPactDate', {
                    rules: [
                        { required: true, message: '请输入第一次工作(签条约)时间!' },
                    ],
                })(<DatePicker format={'YYYY-MM-DD HH:mm:ss'} placeholder="第一次工作(签条约)时间" />)}
            </FormItem>
            <FormItem label=" 第一次工作(签条约)开始时间">
                {getFieldDecorator('PactStartDate', {
                    rules: [
                        { required: true, message: '请输入第一次工作(签条约)开始时间!' },
                    ],
                })(<DatePicker format={'YYYY-MM-DD HH:mm:ss'} placeholder=" 第一次工作(签条约)开始时间" />)}
            </FormItem>
            <FormItem label=" 第一次工作(签条约)结束时间">
                {getFieldDecorator('PactEndDate', {
                    rules: [
                        { required: true, message: '请输入第一次工作(签条约)结束时间!' },
                    ],
                })(<DatePicker format={'YYYY-MM-DD HH:mm:ss'} placeholder=" 第一次工作(签条约)结束时间" />)}
            </FormItem>
            <FormItem label="银行名称">
                {getFieldDecorator('BankName', {
                    rules: [
                        { required: true, message: '请输入银行名称!' },
                        { min: 2, max: 20, message: '银行名称长度只能为2-20位' },
                    ], initialValue: "建设银行"
                })(<Input placeholder="银行名称" />)}
            </FormItem>
            <FormItem label="银行卡号">
                {getFieldDecorator('BandId', {
                    rules: [
                        { required: true, message: '请输入银行卡号!' },
                        { min: 18, max: 20, message: '银行卡号长度只能为18-20位' },
                    ]
                })(<Input placeholder="银行卡号" />)}
            </FormItem>
            <FormItem label="社会安全编号">
                {getFieldDecorator('SocietySafefyNo', {
                    rules: [
                        { required: true, message: '请输入社会安全编号!' },
                        { min: 18, max: 20, message: '社会安全编号长度只能为18-20位' },
                    ]
                })(<Input placeholder="社会安全编号" />)}
            </FormItem>
            <FormItem label="年保险金(养老金)编号">
                {getFieldDecorator('AnnuitySafefyNo', {
                    rules: [
                        { required: true, message: '请输入年保险金(养老金)编号!' },
                        { min: 18, max: 20, message: '年保险金(养老金)编号长度只能为18-20位' },
                    ]
                })(<Input placeholder="年保险金(养老金)编号" />)}
            </FormItem>
            <FormItem label="失业救助金编号">
                {getFieldDecorator('DoleSafefyNo', {
                    rules: [
                        { required: true, message: '请输入失业救助金编号!' },
                        { min: 18, max: 20, message: '失业救助金编号长度只能为18-20位' },
                    ]
                })(<Input placeholder="失业救助金编号" />)}
            </FormItem>
            <FormItem label="医疗保险金编号">
                {getFieldDecorator('MedicareSafefyNo', {
                    rules: [
                        { required: true, message: '请输入医疗保险金编号!' },
                        { min: 18, max: 20, message: '医疗保险金编号长度只能为18-20位' },
                    ]
                })(<Input placeholder="医疗保险金编号" />)}
            </FormItem>
            <FormItem label="工伤赔偿费编号">
                {getFieldDecorator('CompoSafefyNo', {
                    rules: [
                        { required: true, message: '请输入工伤赔偿费编号!' },
                        { min: 18, max: 20, message: '工伤赔偿费编号长度只能为18-20位' },
                    ]
                })(<Input placeholder="工伤赔偿费编号" />)}
            </FormItem>
            <FormItem label="公积金 编号">
                {getFieldDecorator('AcoumulationFundNo', {
                    rules: [
                        { required: true, message: '请输入公积金 编号!' },
                        { min: 18, max: 20, message: '公积金 编号长度只能为18-20位' },
                    ]
                })(<Input placeholder="公积金 编号" />)}
            </FormItem>
        </Form>
    );
};
//培训人员信息
export const formBringUpPerson = (props: BringUpPersonCollectionCreateFormProps) => {
    const { form, bringUpContentOptions, recordOptions, idshow } = props;
    const { getFieldDecorator } = form!;
    return (<Form>
        {idshow ? (<FormItem label="编号" >
            {
                getFieldDecorator('Id', {

                })(<Input readOnly={readonly} />)
            }
        </FormItem>) : null}
        <Form.Item label="培训内容">
            {getFieldDecorator('BringUpContent.Id', {
                //initialValue: '',
                // rules: [ { type: 'array', required: true, message: '请选择培训内容!' },],
            })(
                <Select key="Id" placeholder="员工">
                    {bringUpContentOptions.map((item: { Id: any; Name: any; }) => (
                        <Select.Option key={item.Id} value={item.Id}>
                            {item.Name}
                        </Select.Option>
                    ))}
                </Select>
                // <Cascader
                //     options={bringUpContentOptions}
                //     key="Id"
                //     onChange={onBringUpContentChange}
                //     fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                //     placeholder="请选择培训内容"
                // />
            )}
        </Form.Item>
        <Form.Item label="员工信息">
            {getFieldDecorator('Record.Id', {
                //initialValue: '',
                // rules: [ { type: 'array', required: true, message: '请选择员工信息!' },],
            })(
                <Select key="Id" placeholder="员工">
                    {recordOptions.map((item: { Id: any; Name: any; }) => (
                        <Select.Option key={item.Id} value={item.Id}>
                            {item.Name}
                        </Select.Option>
                    ))}
                </Select>
                // <Cascader
                //     options={recordOptions}
                //     key="Id"
                //     onChange={onRecordChange}
                //     fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                //     placeholder="请选择员工信息"
                // />
            )}
        </Form.Item>
        <FormItem label="培训成绩">
            {getFieldDecorator('Score', {
                rules: [
                    { required: true, message: '请输入培训成绩!' },
                ],
                initialValue: "60"
            })(
                <Input placeholder="培训成绩" />
            )}
        </FormItem>
        <FormItem label="培训等级">
            {getFieldDecorator('UpToGrate', {
                rules: [
                    { required: true, message: '请输入培训等级!' },
                    { min: 1, max: 2, message: '培训等级长度只能为1-2位' },
                ],
                initialValue:"1"
            })(
                <Input placeholder="培训等级" /> 
            )}
        </FormItem>
        <FormItem label="备注">
            {getFieldDecorator('Remark', {
                rules: [
                    { required: true, message: '请输入备注!' },
                    { min: 2, max: 100, message: '备注长度只能为2-100位' },
                ],
                initialValue: "暂无...."
            })(
                <Input placeholder="备注" />
            )}
        </FormItem>
    </Form>
    );
};
//角色权限
export const formAuthority = (props: UserCollectionCreateFormProps) => { 
    const { form, options, onChange, idshow } = props;
    const { getFieldDecorator } = form!;
    return (<Form>
        {idshow ? (<FormItem label="编号" >
            {
                getFieldDecorator('Id', {

                })(<Input readOnly={readonly} />)
            }
        </FormItem>) : null}
        <FormItem label="表">
            {getFieldDecorator('Table', {
                rules: [
                    { required: true, message: '请输入表!' },
                    { min: 5, max: 25, message: '表长度只能为5-25位' },
                ],
            })(
                <Input placeholder="表" />
            )}
        </FormItem>
        <FormItem label="类">
            {getFieldDecorator('EntityType', {
                rules: [
                    { required: true, message: '请输入类!' },
                    { min: 5, max: 25, message: '类长度只能为5-25位' },
                ],
            })(
                <Input placeholder="类" />
            )}
        </FormItem>
        <FormItem label="增">
            {getFieldDecorator('AddFalg', {
                rules: [{ required: true, message: '请选择!' }],
            })(
                <Select key="Id" placeholder="请选择">
                    {['0', '1'].map(item => (
                        <Select.Option key={item} value={item}>
                            {item}
                        </Select.Option>
                    ))}
                </Select>
            )}
        </FormItem>
        <FormItem label="改">
            {getFieldDecorator('EditFalg', {
                rules: [{ required: true, message: '请选择!' }],
            })(
                <Select key="Id" placeholder="请选择">
                    {['0', '1'].map(item => (
                        <Select.Option key={item} value={item}>
                            {item}
                        </Select.Option>
                    ))}
                </Select>
            )}
        </FormItem>
        <FormItem label="删">
            {getFieldDecorator('DeleteFalg', {
                rules: [{ required: true, message: '请选择!' }],
            })(
                <Select key="Id" placeholder="请选择">
                    {['0', '1'].map(item => (
                        <Select.Option key={item} value={item}>
                            {item}
                        </Select.Option>
                    ))}
                </Select>
            )}
        </FormItem>
        <FormItem label="查">
            {getFieldDecorator('SelectFalg', {
                rules: [{ required: true, message: '请选择!' }],
            })(
                <Select key="Id" placeholder="请选择">
                    {['0', '1'].map(item => (
                        <Select.Option key={item} value={item}>
                            {item}
                        </Select.Option>
                    ))}
                </Select>
            )}
        </FormItem>
        <Form.Item label="角色">
            {getFieldDecorator('Role.Id', {
                initialValue: '',
                // rules: [{ type: 'array', required: true, message: '请选择角色!' }, ],
            })(
                <Cascader
                    options={options}
                    key="Id"
                    onChange={onChange}
                    fieldNames={{ label: 'Name', value: 'Id', children: 'items' }}
                    placeholder="请选择角色"
                />
            )}
        </Form.Item>
    </Form>
    );
};

