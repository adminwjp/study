<template>
         <!-- 开始查询信息 -->
        <el-form :inline="true" :model="formQuery" ref="formQuery" class="demo-form-inline query">
            <el-form-item label="编号" prop="id">
                <el-select v-model="formQuery.id" placeholder="请选择" @change="handleCategoryQueryChangeEvent" clearable @visible-change="handleCategoryQueryVisableChangeEvent">
                    <el-option v-for="it in options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="工作公司" prop="company_name">
                <el-input v-model="formQuery.company_name" placeholder="工作公司"></el-input>
            </el-form-item>
            <el-form-item label="工作名称" prop="job">
                <el-input v-model="formQuery.job" placeholder="工作名称"></el-input>
            </el-form-item>
            <el-form-item label="工作分类" prop="category_id">
                <el-select v-model="formQuery.category_id" placeholder="请选择" @change="handleWorkQueryChangeEvent" clearable @visible-change="handleWorkQueryVisableChangeEvent">
                    <el-option v-for="it in job_options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="工作经历时间范围" prop="work_date">
                <el-date-picker v-model="formQuery.work_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions"
                                range-separator="至" start-placeholder="开始日期" align="right">
                </el-date-picker>
            </el-form-item>
            <el-form-item label="创建时间范围" prop="create_date">
                <el-date-picker v-model="formQuery.create_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions"
                                range-separator="至" start-placeholder="开始日期" align="right">
                </el-date-picker>
            </el-form-item>
            <el-form-item label="修改时间范围" prop="update_date">
                <el-date-picker v-model="formQuery.update_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions" range-separator="至" start-placeholder="开始日期"
                                end-placeholder="结束日期" align="right">
                </el-date-picker>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="loadTableDataOrQueryDataEvent">查询</el-button>
                <el-button @click="resetFormEvent('formQuery')">重置</el-button>
            </el-form-item>
        </el-form>
        <!-- 结束查询信息 -->
</template>
<script>
import {createDatePickerOptions} from "../../../js/base";
import {methods} from "../../../js/category";
methods.handleWorkQueryChangeEvent = function(val) {
  this.$parent.handleWorkQueryChangeEvent(val);
};
methods.handleWorkQueryVisableChangeEvent = function(val) {
  this.$parent.handleWorkQueryVisableChangeEvent(val);
};
export default {
  name: "query",
  props: ["formQuery", "createDatePickerOptions", "options", "job_options"],
  data: function() {
    return {
    };
  },
  mounted: function() {
  },
  methods: methods
};
</script>