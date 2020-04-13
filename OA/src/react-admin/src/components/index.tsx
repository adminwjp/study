/**
 * 路由组件出口文件
 * yezi 2018年6月24日
 */
import Loadable from 'react-loadable';
import Loading from './widget/Loading';
import BasicForm from './forms/BasicForm';
import BasicTable from './tables/BasicTables';
import AdvancedTable from './tables/AdvancedTables';
import AsynchronousTable from './tables/AsynchronousTable';
import Echarts from './charts/Echarts';
import Recharts from './charts/Recharts';
import Icons from './ui/Icons';
import Buttons from './ui/Buttons';
import Spins from './ui/Spins';
import Modals from './ui/Modals';
import Notifications from './ui/Notifications';
import Tabs from './ui/Tabs';
import Banners from './ui/banners';
import Drags from './ui/Draggable';
import Dashboard from './dashboard/Dashboard';
import Gallery from './ui/Gallery';
import BasicAnimations from './animation/BasicAnimations';
import ExampleAnimations from './animation/ExampleAnimations';
import AuthBasic from './auth/Basic';
import RouterEnter from './auth/RouterEnter';
import Cssmodule from './cssmodule';
import MapUi from './ui/map';
import QueryParams from './extension/QueryParams';
import Visitor from './extension/Visitor';
import MultipleMenu from './extension/MultipleMenu';
import Famous from './admin/famous';
import User from './admin/user';
import Role from './admin/role';
import AccountItem from './admin/accountItem';
import BringUpContent from './admin/bringUpContent';
import ReckoningName from './admin/reckoningName';
import Person from './admin/person';
import TimeCard from './admin/timeCard';
import ReckoningList from './admin/reckoningList';
import Reckoning from './admin/reckoning';
import ReawrdsAndPunishment from './admin/reawrdsAndPunishment';
import Module from './admin/module';
import Duty from './admin/duty';
import BringUpPerson from './admin/bringUpPerson';
import Authority from './admin/authority';
import Record from './admin/record';
const WysiwygBundle = Loadable({
    // 按需加载富文本配置
    loader: () => import('./ui/Wysiwyg'),
    loading: Loading,
});

export default {
    BasicForm,
    BasicTable,
    AdvancedTable,
    AsynchronousTable,
    Echarts,
    Recharts,
    Icons,
    Buttons,
    Spins,
    Modals,
    Notifications,
    Tabs,
    Banners,
    Drags,
    Dashboard,
    Gallery,
    BasicAnimations,
    ExampleAnimations,
    AuthBasic,
    RouterEnter,
    WysiwygBundle,
    Cssmodule,
    MapUi,
    QueryParams,
    Visitor,
    MultipleMenu,
    Famous,
    User,
    Role,
    AccountItem,
    BringUpContent,
    ReckoningName,
    Person,
    TimeCard,
    ReckoningList,
    Reckoning,
    ReawrdsAndPunishment,
    Module,
    Duty,
    BringUpPerson,
    Authority,
    Record
} as any;
