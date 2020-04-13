<template>
          <div>
            <template v-for="item in menu_list">
                <el-menu-item v-if="!item.children||item.children.length==0" :key="item.index" :index="item.label+','+item.style+','+item.href">
                    <i v-if="item.style" :class="item.style"></i>
                    <span slot="title">{{ item.label}}</span>
                </el-menu-item>
                <el-submenu v-else :key="item.index" :index="item.label+''">
                    <template slot="title">
                        <i v-if="item.style" :class="item.style"></i>
                        <span slot="title">{{ item.label}}</span>
                    </template>
                    <el-menu-item-group v-if="item.group">
                        <span slot="title">{{ item.group}}</span>
                    </el-menu-item-group>
                    <slidermenu :menu_list="item.children" />
                </el-submenu>
            </template>
        </div>
</template>
<script>
export default {
  name: "slidermenu",
  props: {
    menu_list: {
      type: Array
    }
  },
  data: function() {
    return {};
  }

}
</script>
<style scoped>
    /*  组件 用div 包裹  样式需要重写 */
    .el-menu--collapse > div > .el-menu-item span, .el-menu--collapse > div > .el-submenu > .el-submenu__title span {
        height: 0;
        width: 0;
        overflow: hidden;
        visibility: hidden;
        display: inline-block;
    }
    .el-menu--collapse > div> .el-menu-item .el-submenu__icon-arrow, .el-menu--collapse> div > .el-submenu > .el-submenu__title .el-submenu__icon-arrow {
        display: none;
    }
</style>