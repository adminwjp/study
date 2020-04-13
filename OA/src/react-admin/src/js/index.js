 export const apiUrl = "http://localhost:5000/";
//export const apiUrl = "http://localhost:11348/";
export const urls = {
    famous1: {
        get: 'api/v1/famous/get',
        add: 'api/v1/famous/add',
        edit: 'api/v1/famous/edit',
         delete: 'api/v1/famous/delete'
    },
    category: {
        role: apiUrl + 'api/v1/role/category',
        user: apiUrl + 'api/v1/user/category',
        record: apiUrl + 'api/v1/record/category',
        ratifier: apiUrl + 'api/v1/record/ratifier',//批准人
        reckoning_name: apiUrl + 'api/v1/reckoning_name/category',
        account_item: apiUrl + 'api/v1/account_item/category',
        module: apiUrl + 'api/v1/module/category',
        bring_up_content: apiUrl + 'api/v1/bring_up_content/category',
    },
    user: new UrlEntity("user"),
    time_card: new UrlEntity('time_card'),
    role: new UrlEntity('role', {category:"api/v1/role/category"}),
    record: new UrlEntity('record', {category:'api/v1/user/category'}),
    reckoning_name: new UrlEntity('reckoning_name'),
    reckoning_list: new UrlEntity('reckoning_list'),
    reckoning: new UrlEntity('reckoning'),
    reawrds_and_punishment: new UrlEntity('reawrds_and_punishment'),
    person: new UrlEntity('person'),
    module: new UrlEntity('module'),
    famous:new UrlEntity('famous'),
    duty: new UrlEntity('duty'),
    bring_up_person: new UrlEntity('bring_up_person'),
    bring_up_content: new UrlEntity('bring_up_content'),
    authority: new UrlEntity('authority'),
    account_item:new UrlEntity('account_item')
}; 
export function UrlEntity(name,options) {
    this.get = apiUrl+'api/v1/'+name+'/get';
    this.add = apiUrl+'api/v1/'+name+'/add';
    this.edit = apiUrl+'api/v1/'+name+'/edit';
    this.delete = apiUrl+'api/v1/'+name+'/delete';
    for (const key in options) {
        this[key] = apiUrl+options[key];
    }
 }