/**
 * 培训人员信息
 */
import { Form } from 'antd';
import { FormProps } from 'antd/lib/form';
export type CollectionCreateFormProps = {
    visible: boolean;
    onCancel: () => void;
    onCreate: () => void;
    ref: any;
    title: string;
    classs: "",
    idshow: false
} & FormProps;
//CollectionCreateFormProps&  可以运行 &CollectionCreateFormProps 编译失败 
export type UserCollectionCreateFormProps = CollectionCreateFormProps& {
    options: any;
    onChange: () => void;
}; 
export type TimeCardCollectionCreateFormProps = CollectionCreateFormProps & {
    recordoptions: any[];//员工
    ratifierrecordoptions: any[];//批准人
}; 
export type ReckoningListCollectionCreateFormProps = CollectionCreateFormProps & {
    recordOptions: any;
    onRecordChange: () => void;
    reckoningNameOptions: any;
    onReckoningNameChange: () => void;
}; 
export type ReckoningCollectionCreateFormProps = CollectionCreateFormProps & {
    recordOptions: any;
    onRecordChange: () => void;
    accountItemOptions: any;
    onAccountItemChange: () => void;
}; 
export type ReawrdsAndPunishmentCollectionCreateFormProps = CollectionCreateFormProps & {
    recordOptions: any;
    onRecordChange: () => void;
};
export type ModuleCollectionCreateFormProps = CollectionCreateFormProps & {
    parentOptions: any;
    onParentChange: () => void;
}; 
export type BringUpPersonCollectionCreateFormProps = CollectionCreateFormProps & {
    bringUpContentOptions: any;
    onBringUpContentChange: () => void;
    recordOptions: any;
    onRecordChange: () => void;
}; 
export const FormItem = Form.Item;