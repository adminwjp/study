import Mock from 'mockjs';
import { urls } from '../js/index'
var fnames = ["汉族", "回族", "藏族"];
var fdata = [];
for (let i = 0; i < fnames.length; i++){
    fdata.push({
        Id: i + 1,
        Name: fnames[i],
        CreateDate: getDateStr(),
        UpdateDate: getDateStr()
    });
}
function operFamous(id,name, add) {
    if (add === 'add') {
        fdata.push({
            Id: fdata.length + 1,
            Name: name,
            CreateDate: getDateStr(),
            UpdateDate: getDateStr()
        });
    } else if (add === 'edit') {
        let index = fdata.findIndex(it => it.Id === Number(id));
        if (index > -1) {
            fdata[index].Name = name;
            fdata[index].UpdateDate = getDateStr();
            //fdata.splice(index, 1, fdata[index]);
        }
    }
    else {
        if (id instanceof Array) {
            for (const key in id) {
                let index = fdata.findIndex(it => it.Id === Number(id[key]));
                if (index > -1) {
                    fdata.splice(index, 1);
                }
            }
        }
        else {
            let index = fdata.findIndex(it => it.Id === Number(id));
            if (index > -1) {
                fdata.splice(index, 1);
            }
        }
    }
}
const test = [{
    url: urls.famous1.get,
    type: 'post',
    response: config => {
        return {
            success: true,
            code: 20000,
            message: 'success',
            data: fdata
        }
    }
},
    {
    url: urls.famous1.add,
    type: 'post',
        response: config => {
            console.log(config.body);
            const { Name } = JSON.parse(config.body);
            operFamous("", Name, 'add');
        return {
            success: true,
            code: 20000,
            message: 'success',
            data: fdata
        }
    }
} , {
    url: urls.famous1.edit,
    type: 'post',
        response: config => {
            console.log(config.body);
            const { Id,Name } = JSON.parse(config.body);
            operFamous(Id, Name, 'edit');
        return {
            success: true,
            code: 20000,
            message: 'success',
            data: fdata
        }
    }
}, {
    url: urls.famous1.delete,
    type: 'post',
        response: config => {
            console.log(config.body);
            const { Id } = JSON.parse(config.body);
            operFamous(Id, "", 'delete');
        return {
            success: true,
            code: 20000,
            message: 'success',
            data: fdata
        }
    }
}
];
function getDateStr() {
      let date = new Date();
    let dateStr = date.getFullYear() + "-" + date.getMonth() + "-" + date.getDay() + " " + date.getHours()
        + ":" + date.getMinutes() + ":" + date.getSeconds();
    return dateStr;
}
export default test.map(it => { return Mock.mock(it.url, it.type, it.response);});
// export default Mock.mock(urls.famous.get, 'post', {
//     success: true,
//     code: 20000,
//     message: 'success',
//     data: fdata
// });