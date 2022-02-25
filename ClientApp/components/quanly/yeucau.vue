<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Quản lý</a>
              </li>
              <li class="breadcrumb-item active">Yêu cầu</li>
            </ol>
          </div>
          <h4 class="page-title">Quản lý yêu cầu</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class="header-title pb-3" style="margin-bottom: 10px; float: right">

            <el-button type="primary"
                       size="small"
                       icon="el-icon-plus"
                       class="filter-item"
                       @click="handleAdd"
                       v-if="allowEdit">Thêm</el-button>
            <el-button type="success"
                       size="small"
                       icon="el-icon-download"
                       class="filter-item"
                       @click="handleExport">Xuất</el-button>
            <el-input clearable
                      v-model="search"
                      placeholder="Tìm kiếm"
                      style="width: 240px; float: left; "></el-input>
            <el-select style="width: 240px; float: left;"
                       v-model="StateIdFilter"
                       @change="changeStateIdFilter"
                       placeholder="Chọn trạng thái">
              <el-option v-for="item in ListDMTrangThai"
                         :key="item.Id"
                         :label="item.StateName"
                         :value="item.Id">
              </el-option>
            </el-select>
            <el-select style="width: 240px; float: left;margin-right:3px"
                       v-model="DichVuIdFilter"
                       @change="changeDichVuIdFilter"
                       placeholder="Chọn Dịch Vụ">
              <el-option v-for="item in ListDMDichVu"
                         :key="item.Id"
                         :label="item.TenDichVu"
                         :value="item.Id">
              </el-option>
            </el-select>
          </div>
          <el-table :data="renderData()"
                    border
                    v-loading="loading"
                    default-expand-all
                    row-key="Id"
                    style="width: 100%">

            <!-- <el-table-column width="50" label="" align="center">
    <template></template>
  </el-table-column> -->
            <el-table-column width="50" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column prop="Id" label="Mã Yêu Cầu" width="150">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  YC{{ scope.row.Id }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="TenYeuCau" label="Yêu Cầu" width="250">
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  {{ scope.row.TenYeuCau }}
                </text-highlight>
              </template>
            </el-table-column>
            <!--<el-table-column prop="NoiDung"
                   label="Nội Dung"
                   min-width="250">
    <template slot-scope="scope">
      <text-highlight :queries="search" style="word-break: normal;" v-html="scope.row.NoiDung">
      </text-highlight>
    </template>
  </el-table-column>-->
            <el-table-column prop="JiraDaGui"
                             label="Task Jira"
                             width="150">
              <template slot-scope="scope">
                <!--<text-highlight :queries="search" style="word-break: normal;" >
        {{ scope.row.JiraDaGui}}
      </text-highlight>-->
                <el-link :href="scope.row.JiraDaGui" target="_blank">{{ formatJira(scope.row.JiraDaGui)}}</el-link>
              </template>


            </el-table-column>


            <el-table-column prop="NhanSu"
                             label="Người Thực Hiện"
                             width="130"
                             align="center">
              <template slot-scope="scope">
                {{ scope.row.NhanSuId ? scope.row.NhanSu.TenNhanSu : ""}}
              </template>
            </el-table-column>
            <el-table-column prop="ThoiHan"
                             label="Deadline"
                             width="120"
                             align="center">
              <template slot-scope="scope">
                {{ formatDate(scope.row.ThoiHan) }}
              </template>
            </el-table-column>
            <el-table-column prop="StateId"
                             label="Trạng Thái"
                             width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" :style=changetextColor(scope.row.States.Id)>
                  {{ scope.row.States ? scope.row.States.StateName : ""}}
                </text-highlight>
              </template>
            </el-table-column>

            <el-table-column prop="Status" label="Tình Trạng" width="125">

              <template slot-scope="scope">
                <span v-if="new Date(scope.row.ThoiHan) < Date.now() && scope.row.StateId != 6" style="color:darkorange">
                  Trễ hạn
                </span>
                <span v-if="new Date(scope.row.ThoiHan) >= Date.now() && scope.row.StateId != 6" style="color:green">
                  Trong hạn
                </span>
              </template>

            </el-table-column>
            <el-table-column align="center" label="" width="185" fixed="right">
              <template slot="header" slot-scope="scope">

              </template>
              <template slot-scope="scope">
                <el-button @click="handleView(scope.$index, scope.row)"
                           type="info"
                           title="Xem"
                           icon="el-icon-view"
                           size="mini"></el-button>
                <el-button @click="handleEdit(scope.$index, scope.row)"
                           type="primary"
                           :title="allowEdit ? 'Cập nhật' : 'Xem chi tiết'"
                           :icon="allowEdit ? 'el-icon-edit' : 'el-icon-view'"
                           size="mini"></el-button>
                <el-button @click="handleDelete(scope.row)"
                           type="danger"
                           icon="el-icon-delete"
                           title="Xóa"
                           size="mini"
                           v-if="allowEdit"></el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="pt-2 pl-0"
                         :page-size="pagination"
                         background
                         style="width: 100%"
                         @size-change="handleSizeChange"
                         :current-page.sync="activePage"
                         :page-sizes="[10, 20, 50, 100, 500]"
                         layout="total,sizes,prev, pager, next"
                         :total="total">
          </el-pagination>
        </div>
      </div>
    </div>
    <el-dialog title="Quản lý yêu cầu"
               :visible.sync="dialogFormDisplay"
               top="55px"
               width="80%"
               center>
      <el-form :model="formData"
               :rules="formRules"
               ref="formData"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">
        <el-form-item label="Tiêu đề yêu cầu" prop="TenYeuCau">
          <el-input v-model="formData.TenYeuCau"
                    type="text"
                    size="small"></el-input>
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngày yêu cầu" prop="NgayYeuCau">
              <el-date-picker v-model="formData.NgayYeuCau"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd">
              </el-date-picker>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Deadline" prop="ThoiHan">
              <el-date-picker v-model="formData.ThoiHan"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd">
              </el-date-picker>
            </el-form-item>
          </el-col>

        </el-row>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Trạng thái" prop="StateId">
              <el-select v-model="formData.StateId"
                         placeholder="Chọn trạng thái"
                         class="w-100" filterable>
                <el-option v-for="item in ListDMTrangThai"
                           :key="item.Id"
                           :label="item.StateName"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân Sự" prop="NhanSuId">
              <el-select v-model="formData.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100" filterable>
                <el-option v-for="item in ListDMNhanSu"
                           :key="item.Id"
                           :label="item.TenNhanSu"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Người giám sát" prop="NguoiGiamSatId">
              <el-select v-model="formData.NguoiGiamSatId"
                         placeholder="Chọn người giám sát"
                         class="w-100"
                         filterable>
                <el-option v-for="item in ListDMNhanSu"
                           :key="item.Id"
                           :label="item.TenNhanSu"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item prop="ThoiHanMongMuon" label="Thời hạn KH">
              <el-date-picker v-model="formData.ThoiHanMongMuon"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd">
              </el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Dịch vụ" prop="DichVuId">
              <el-select v-model="formData.DichVuId"
                         placeholder="Chọn dịch vụ"
                         @change="changeDichVu(formData.DichVuId)"
                         class="w-100" filterable>
                <el-option v-for="item in ListDMDichVu"
                           :key="item.Id"
                           :label="item.TenDichVu"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Đơn vị" prop="DonViYeuCauId">
              <el-select v-model="formData.DonViYeuCauId"
                         placeholder="Chọn đơn vị"
                         class="w-100"
                         clearable
                         filterable>
                <el-option v-for="item in ListDMDonVi"
                           :key="item.Id"
                           :label="item.TenDonViYeuCau"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>

          </el-col>
        </el-row>
        <el-form-item label="Nội dung yêu cầu" prop="NoiDung">
          <ckeditor :editor="editor"
                    v-model="formData.NoiDung"
                    :config="editorConfig"
                    :disabled="!allowEdit"></ckeditor>
        </el-form-item>

        <el-form-item label="Jira" prop="JiraDaGui">
          <el-input v-model="formData.JiraDaGui"
                    type="text"
                    size="small"></el-input>

          <!--<el-button type="primary" size="small" @click="handleAddJira">Thêm</el-button>-->
          <!--<el-button size="small" @click="updateData">Xóa</el-button>-->
        </el-form-item>

        <el-form-item label="Tập tin đính kèm">
          <el-upload action="/api/TapTin/UploadDoc"
                     :limit="3"
                     :multiple="true"
                     :on-preview="handlePreview"
                     :on-remove="handleRemove"
                     :file-list="fileList"
                     :on-success="handleSuccess"
                     :before-upload="beforeUpload"
                     accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm"
                     :auto-upload="true"
                     size="mini">
            <el-button size="small" type="primary">Tải lên</el-button>
          </el-upload>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetForm" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateData">Cập nhật</el-button>
      </span>
    </el-dialog>
    <el-dialog title="Xem nội dung yêu cầu"
               :visible.sync="dialogFormView"
               top="55px"
               width="80%"
               center>
      <el-form :model="formData1"
               :rules="formRules"
               ref="formData1"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">
        <el-form-item label="Tiêu đề yêu cầu:" prop="TenYeuCau">

          <!--<el-input v-model="formData1.TenYeuCau"
           type="text"
           size="small" disabled></el-input>-->
          {{formData1.TenYeuCau}}
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngày yêu cầu: " prop="NgayYeuCau">
              <!--<el-date-picker v-model="formData1.NgayYeuCau"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd" disabled>
              </el-date-picker>-->
              {{formData1.NgayYeuCau}}

            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Deadline: " prop="ThoiHan">

              <!--<el-date-picker v-model="formData1.ThoiHan"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd" disabled>
              </el-date-picker>-->
              {{formData1.ThoiHan}}
            </el-form-item>
          </el-col>

        </el-row>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Trạng thái: " prop="StateId">
              <!--<el-select v-model="formData1.StateId"
                         placeholder="Chọn trạng thái"
                         class="w-100" disable>
                <el-option v-for="item in ListDMTrangThai"
                           :key="item.Id"
                           :label="item.StateName"
                           :value="item.Id">
                </el-option>
              </el-select>-->
              {{formData1.StateId ? formData1.States.StateName : ""}}
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân Sự: " prop="NhanSuId">
              <!--<el-select v-model="formData1.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100" disable>
                <el-option v-for="item in ListDMNhanSu"
                           :key="item.Id"
                           :label="item.TenNhanSu"
                           :value="item.Id">
                </el-option>
              </el-select>-->
              {{formData1.NhanSuId ? formData1.NhanSu.TenNhanSu : ""}}

            </el-form-item>

          </el-col>

        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Người giám sát: " prop="NguoiGiamSatId">
              <!--<el-select v-model="formData1.NguoiGiamSatId"
                         placeholder="Chọn người giám sát"
                         class="w-100"
                         disable>
                <el-option v-for="item in ListDMNhanSu"
                           :key="item.Id"
                           :label="item.TenNhanSu"
                           :value="item.Id">
                </el-option>
              </el-select>-->
              <!--{{formData1.NguoiGiamSatId ? formData1.NhanSu.TenNhanSu : ""}}-->
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item prop="ThoiHanMongMuon" label="Thời hạn KH: ">
              <!--<el-date-picker v-model="formData1.ThoiHanMongMuon"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd" disable>
              </el-date-picker>-->
              {{formData1.ThoiHanMongMuon}}
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Dịch vụ: " prop="DichVuId">
              <!--<el-select v-model="formData1.DichVuId"
                         placeholder="Chọn dịch vụ"
                         @change="changeDichVu(formData.DichVuId)"
                         class="w-100" disable>
                <el-option v-for="item in ListDMDichVu"
                           :key="item.Id"
                           :label="item.TenDichVu"
                           :value="item.Id">
                </el-option>
              </el-select>-->
              {{formData1.DichVuId ? formData1.DichVu.TenDichVu : ""}}
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Đơn vị: " prop="DonViYeuCauId">
              <!--<el-select v-model="formData1.DonViYeuCauId"
                         placeholder="Chọn đơn vị"
                         class="w-100"
                         clearable
                         filterable
                         disabled>
                <el-option v-for="item in ListDMDonVi"
                           :key="item.Id"
                           :label="item.TenDonViYeuCau"
                           :value="item.Id">
                </el-option>
              </el-select>-->
              {{formData1.DonViYeuCauId ? formData1.DonViYeuCau.TenDonViYeuCau : ""}}
            </el-form-item>

          </el-col>
        </el-row>
        <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
          <!--<span v-html="formData1.NoiDung"></span>-->
          <!--<ckeditor :editor="editor"
            v-model="formData1.NoiDung"
            :config="editorConfig"
            disabled></ckeditor>-->
          <!--{{formData1.NoiDung}}-->
          <text-highlight :queries="search" style="word-break: normal;" v-html="formData1.NoiDung">
          </text-highlight>
        </el-form-item>

        <el-form-item label="Jira: " prop="JiraDaGui">

          {{formData1.JiraDaGui}}
          <!--<el-input v-model="formData1.JiraDaGui"
                    type="text"
                    size="small" disabled></el-input>-->
          <!--<el-button type="primary" size="small" @click="handleAddJira">Thêm</el-button>-->
          <!--<el-button size="small" @click="updateData">Xóa</el-button>-->
        </el-form-item>


        <el-form-item label="Trạng thái jira: " prop="StatusJira">
          {{formData1.StatusJira}}
        </el-form-item>
        <el-form-item label="Số lần comment" prop="Comment">
          <!--<el-input type="text"
                    rows="5"
                    size="small"
                    v-for="item in Comment"
                    :value="item.body + ' - ' + item.author.displayName + ' - ' + formatDateTime(item.updated)"></el-input>-->
          <el-tabs type="border-card">
            <el-tab-pane label="Jira Comment">
              <div>
                <ul style="list-style-type:none; margin-left:-30px; margin-bottom:10px" v-for="item in Comment">
                  <li>
                    <b>+</b> {{item.body + ' - ' + item.author.displayName + ' - ' + formatDateTime(item.updated)}}
                  </li>
                </ul>
              </div>
            </el-tab-pane>
            <el-tab-pane label="Yêu Cầu Comment">
              <el-form-item v-for="item in YeuCauComment" :label="item.User.FullName +' : '" >
                {{item.Comments}}
                <!--<el-button @click="handleEditComment(item.Id, item.Comments)" v-if="isPermiss" icon="el-icon-edit" style="margin-left:10px"></el-button>
                <el-button @click="DeleteComment(item.Id)" v-if="isPermiss" icon="el-icon-delete"></el-button>-->
              </el-form-item>
              <el-form-item prop="CommentYeuCau">
                <el-input type="text"
                          size="small" v-model="formData1.CommentYeuCau"></el-input>
                <el-button @click="handleAddComment">Save Comment</el-button>
              </el-form-item>


            </el-tab-pane>
          </el-tabs>


        </el-form-item>
        <el-form-item label="Mức độ: " prop="Priority" :style=changetextColorPio(formData1.Priority)>
          <!--<el-input v-model="formData1.Priority"

                    size="small" disable>

          </el-input>-->
          {{formData1.Priority}}
        </el-form-item>
        <el-form-item label="Tập tin đính kèm: ">
          <el-upload action="/api/TapTin/UploadDoc"
                     :limit="3"
                     :multiple="true"
                     :on-preview="handlePreview"
                     :file-list="fileList"
                     :on-success="handleSuccess"
                     :before-upload="beforeUpload"
                     accept=".pdf,.doc,.docx,.xls,  .xlsx, .xlsm"
                     :auto-upload="true"
                     size="mini">

          </el-upload>
        </el-form-item>
      </el-form>

      <span slot="footer" class="dialog-footer">
        <el-button @click="resetFormView" size="small">Bỏ qua</el-button>
      </span>
    </el-dialog>

    <!--<el-dialog title="Xem nội dung yêu cầu"
               :visible.sync="dialogFormJiraState"
               top="165px"
               center>
      <el-form :model="formData1"
               :rules="formRules"
               ref="formData1"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">

        <el-form-item label="Mã Jira :" prop="KeyJira">
          {{formData1.KeyJira}}
        </el-form-item>
        <el-form-item label="Trạng thái :" prop="StatusJira">
          {{formData1.StatusJira}}
        </el-form-item>

      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormJira" size="small">Bỏ qua</el-button>
      </span>
    </el-dialog>-->
    <!--<el-dialog title="Cập nhật comment"
               :visible.sync="dialogFormDisplayComment"
               top="165px"
               center>
      <el-form :model="formData2"
               :rules="formRules"
               ref="formData2"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">

        <el-form-item label="Comment mới" prop="CommentYeuCau">
         <el-input type="text"
                          size="small" v-model="formData2.CommentYeuCau">

         </el-input>
        </el-form-item>
        

      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormComment" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="UpdateComment">Cập nhật</el-button>
      </span>
    </el-dialog>-->
  </div>
</template>
<script>
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import moment from "moment";
import { getRole, getUser } from "../../store/common";
import {
  getYeuCau,
  getListDanhMucYeuCau,
  selectYeuCau,
  updateYeuCau,
  deleteYeuCau,
  addYeuCau,
  addJira,
  selectJira,
  updateJira,
  deleteJira,
  getTrangThai,
  selectYeuCauAll,
  getListUser,
  requestGetProfile,
  selectComments,
  addComments,
  updateComments,
  deleteComments,
  sendTeleAsync,
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogFormDisplay: false,
      dialogFormDisplayComment: false,
      dialogFormView: false,
      loading: false,
      isEditor: false,
      isXacThuc: false,
      allowEdit: true,
      isPermiss: true,
      search: "",
      StateIdFilter: 5,
      DichVuIdFilter: 1,
      JiraDaGuiLink:"",
      formData: {
        Id: null,
        TenYeuCau: null,
        NoiDung: null,
        ThoiHan: null,
        JiraDaGui: null,
        StatusId: null,
        StateId: null,
        NhanSuId: null,
        DonViYeuCauId: null,
        DichVuId: null,
        NgayYeuCau: null,
        StatusJira: null,
        FileUpload: null,
        NguoiGiamSatId: null,
        ThoiHanMongMuon:null,
        KeyJira: null,
        Priority: null,
        UnitId: null,
        domains: [{
          key: 1,
          value:''
        }]
      },
      formData1: {
        Id: null,
        TenJira: null,
        LinkJira: null,
        YeuCauId: null,
        KeyJira: null,
        StatusJira: null,
        TenYeuCau: null,
        NoiDung: null,
        ThoiHan: null,
        JiraDaGui: null,
        StatusId: null,
        StateId: null,
        NhanSuId: null,
        DonViYeuCauId: null,
        DichVuId: null,
        NgayYeuCau: null,
        FileUpload: null,
        NguoiGiamSatId: null,
        ThoiHanMongMuon: null,
        Priority: null,
        CommentYeuCau:null,
      },
      formData2: {
        CommentYeuCau: null,
        Id: null,
      },

      formRules: {
        TenYeuCau: [
          {
            required: true,
            message: "Nhập tiêu đề yêu cầu",
            trigger: "blur"
          }
        ],
        NoiDung: [
          {
            required: true,
            message: "Vui lòng nhập nội dung",
            trigger: "blur"
          }
        ],
        ThoiHan: [
          {
            required: true,
            message: "Vui lòng chọn thời hạn",
            trigger: "blur"
          }
        ],
        NgayYeuCau: [
          {
            required: true,
            message: "Vui lòng chọn ngày yêu cầu",
            trigger: "blur"
          }
        ],
        StateId: [
          {
            required: true,
            message: "Vui lòng chọn trạng thái",
            trigger: "blur"
          }
        ],

       DichVuId: [
          {
            required: true,
            message: "Vui lòng chọn dịch vụ",
            trigger: "blur"
          }
        ],
        TenJira: [
          {
            required: true,
            message: "Vui lòng nhập dữ liệu",
            trigger: "blur"
          }
        ],
        LinkJira: [
          {
            required: true,
            message: "Vui lòng nhập dữ liệu",
            trigger: "blur"
          }
        ],
      },
      editor: ClassicEditor,
      editorConfig: {
        // The configuration of the editor.
        ckfinder: {
          // Upload the images to the server using the CKFinder QuickUpload command.
          uploadUrl:
            "/api/TapTin/CKUpload?command=QuickUpload&type=Images&responseType=json"
        }
      },
      RoleId: getRole(),
      listData: [],
      YeuCauComment:[],
      ListDMTrangThai: [],
      ListDMNhanSu: [],
      ListDMDichVu: [],
      ListDMJira: [],
      ListDMTinhTrang: [],
      ListDMDonVi :[],
      ListJira: [],
      TrangThai: [],
      Comment: [],
      fileList: [],
      fileDoc: [],
      pagination: 10,
      total: 10,
      activePage: 1,
      dataFilter: null,
      stateOld : [],
      stateNew: [],
    };
  },

  computed: {
    //...mapState({
    //  currentCount: state => state.counter
    //})
  },

  methods: {
    formatDate(date) {
      if (date) {
        return moment(date).format("DD/MM/YYYY");
      } else return null;
    },
  formatDateTime(date) {
      if (date) {
        return moment(date).format("MM/DD/YYYY hh:mm");
      } else return null;
    },
    formatTenTapTin(val) {
      if (val) {
        return val.substring(val.length - 18, val.length);
      } else return null;
    },
    formatJira(val) {
      if (val) {
        var jira="";
        var jiralink = val;
        var split = jiralink.split('/');
        for (var i = 0; i < split.length; i++) {
          jira = split[split.length - 1];
        }
        return jira;
      } else return null;
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(data =>
        filterVal.map(f => {
          let result;
          if (f.includes(".")) {
            let f1 = f.split(".")[0];
            let f2 = f.split(".")[1];
            result = data[f1][f2];

            if (f2 == "NhanSu") {
              return result ? result.TenNhanSu : "";
            }

            if (f2 == "States") {
              return result ? result.StateName : "";
            }
            if (f2 == "DichVu") {
              return result ? result.TenDichVu : "";
            }
           if (f2 == "DonViYeuCau") {
            return result ? result.TenDonViYeuCau : "";
          }
            if (f2 == "NhanSu") {
              return result ? result.TenNhanSu : "";
            }
          } else {
            if (f.startsWith("Ngay")) {
              return this.formatDate(data[f]);
            }
            result = data[f];
          }

          if (f == "ThoiHan") {
            return this.formatDate(result);
          }

          if (f == "ThoiHanMongMuon") {
            return this.formatDate(result);
          }
          return result;
        })
      );
    },
    changeDichVu(val) {
      //console.log(val)
      var dv = this.ListDMDichVu.find(obj => obj.Id == val);
      if (dv) {
        this.ListDMDonVi = dv.DonVi;

      } else {
        this.DonVi = [];
      }
      delete this.formData.DonViYeuCauId;
    },
    changeStateIdFilter() {

      this.getListData();
    },
    changeDichVuIdFilter() {

      this.getListData();
    },

    //xemtrangthaiJira(index, row) {
    //  var jiralink = row.JiraDaGui;
    //  var split = jiralink.split('/');
    //  for (var i = 0; i < split.length; i++) {
    //    this.JiraDaGuiLink = split[split.length-1];
    //  }

    //  console.log(split);
    //  this.getDataTrangThai();
    //  this.dialogFormJiraState = true;


    //},
    handleAdd() {
      if (this.StateIdFilter || this.DichVuIdFilter) {
        if (this.$refs.formData !== undefined) {
          this.$refs.formData.resetFields();
        }
        this.formData = {
          //IsActive = true
          StateId: this.StateIdFilter,
          DichVuId: this.DichVuIdFilter,
        };
        this.fileList = [];
        this.fileDoc = [];
        this.ListJira = [];
        this.isEditor = false;
        this.dialogFormDisplay = true;
      } else {
        alert("Chọn trạng thái trước khi thêm mới!");
      }

    },

    handleAddJira() {
      if (this.$refs.formData1 !== undefined) {
        this.$refs.formData1.resetFields();
      }

      this.formData1 = {
        YeuCauId: this.formData.Id
      };

      this.isEditor = false;
      this.dialogFormDisplayJira = true;
    },
    handleView(index, row) {
      if (this.$refs.formData1 !== undefined) {
        this.$refs.formData1.resetFields();
      }
      this.formData1 = Object.assign({}, row);

      console.log(split);
      if (row.JiraDaGui != null) {
        var jiralink = row.JiraDaGui;
        var split = jiralink.split('/');
        for (var i = 0; i < split.length; i++) {
          this.JiraDaGuiLink = split[split.length - 1];
        }
        var name = getUser();

        this.getDataTrangThai();

      }

      this.getListComment(this.formData1.Id);

      if (this.formData1.FileUpload) {
        this.fileList = [];
        this.fileDoc = [];
        var _arr = JSON.parse(this.formData1.FileUpload);
        for (var i = 0; i < _arr.length; i++) {
          var urlFile = "/uploads/" + _arr[i];
          this.fileList.push({ name: _arr[i], url: urlFile });
          this.fileDoc.push({ key: _arr[i], file: _arr[i] });
        }
      }

      this.isEditor = false;
      this.dialogFormView = true;
    },
    handleEdit(index, row) {
      if (this.$refs.formData !== undefined) {
        this.$refs.formData.resetFields();

      }
      this.formData = Object.assign({}, row);
      this.stateOld = this.formData.StateId;

      console.log(this.stateOld);
      if(this.formData.DichVuId != null){
          var dv = this.ListDMDichVu.find(obj => obj.Id == this.formData.DichVuId);
          this.ListDMDonVi = dv.DonVi;
      }

      if (this.formData.FileUpload) {
        this.fileList = [];
        this.fileDoc = [];
        var _arr = JSON.parse(this.formData.FileUpload);
        for (var i = 0; i < _arr.length; i++) {
          var urlFile = "/uploads/" + _arr[i];
          this.fileList.push({ name: _arr[i], url: urlFile });
          this.fileDoc.push({ key: _arr[i], file: _arr[i] });
        }
      }
      this.isEditor = true;
      this.dialogFormDisplay = true;

    },

    handleDelete(row) {
     var role = this.RoleId;

        this.$confirm("Bạn có chắc chắn muốn xóa?", "Thông báo", {
          confirmButtonText: "OK",
          cancelButtonText: "Cancel",
          type: "warning"
        }).then(() => {
          deleteYeuCau(row.Id).then(data => {
            if(data == 1){
              this.$message({
               type: "success",
                message: "Xóa thành công!"
              });
              this.getListData();
            }
            else{
               this.$message({
               type: "warning",
                message: "Không thể xóa!"
              });
            }
          });
        });

    },
    handleSuccess(response, file, ListJira) {
      this.fileDoc.push({ key: file.name, file: response.file });
    },
    handleRemove(file, ListJira) {
      let i = this.fileDoc.map(item => item.key).indexOf(file.name);
      this.fileDoc.splice(i, 1);
    },
    handlePreview(file) {
      this.dialogFileUrl = file.url;
      this.dialogVisibleDoc = true;
    },

    beforeUpload(file) {
      const isPdf = file.type === "application/pdf";
      const isDoc = file.type === "application/msword";
      const isDocx =
        file.type ===
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
      const isLt10M = file.size / 1024 / 1024 < 10;
      if (!isPdf && !isDoc && !isDocx) {
        this.$message.error("Không đúng định dạng quy định");
      }
      if (!isLt10M) {
        this.$message.error("Dung lượng vượt quá 10M");
      }
      return (isPdf || isDoc || isDocx) && isLt10M;
    },
    handleExport() {
      import("../../vendor/Export2Excel").then(excel => {
        const tHeader = [
          "Tên yêu cầu",
          "Nội dung",
          "Thời hạn",
          "Task Jira",
          "Trạng thái",
          "Nhân sự",
          "Dịch vụ",
          "Đơn vị",
          //"Người giám sát",
          "Thời hạn KH mong muốn"
        ];
        const filterVal = [
          "TenYeuCau",
          "NoiDung",
          "ThoiHan",
          "JiraDaGui",
          "States.StateName",
          "NhanSu.TenNhanSu",
          "DichVu.TenDichVu",
          "DonViYeuCau.TenDonViYeuCau",
          //"NhanSu.TenNhanSu",
          "ThoiHanMongMuon",

        ];
        const data = this.formatJson(filterVal, this.listData);
        console.log(data);
        var filename = "DSYeuCau_" + moment().format("YYYYMMDD_hhmmss");
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: filename
        });
      });
    },
    resetForm() {
      this.dialogFormDisplay = false;
      return true;
    },
    resetFormView() {
      this.dialogFormView = false;
      return true;
    },
    resetFormComment(){
      this.dialogFormDisplayComment= false;
      return true;
    },
    getListData() {
      this.loading = true;

        //Quản lý
        this.loading = true;
      if (this.StateIdFilter != 5 || this.DichVuIdFilter != 1) {
        selectYeuCau(this.StateIdFilter, this.DichVuIdFilter).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
      else {
        selectYeuCauAll().then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
      this.getDataTrangThai();

    },
    // get trạng thái
    getDataTrangThai() {
      this.loading = true;

        getTrangThai(this.JiraDaGuiLink).then(data => {
          if (data != null && data.errorMessages == null) {
            this.TrangThai = data;
            this.formData1.KeyJira = data.key;
            this.formData1.StatusJira = data.fields.status.name;
            this.Comment = data.fields.comment.comments;
            this.formData1.Priority = data.fields.priority.name;

          }

          console.log(this.Comment);
          this.loading = false;
          //console.log(data);
        });


    },
    getDataTrangThai1() {
      this.loading = true;

      getTrangThai(this.JiraDaGuiLink).then(data => {
        if (data != null ) {

          this.TrangThai = data;
          this.formData.KeyJira = data.key;
          this.formData.StatusJira = data.fields.status.name;
          this.Comment = data.fields.comment.comments;
          this.formData.Priority = data.fields.priority;
        }

        this.loading = false;
        console.log(data);
      });


    }
    ,
    getListDataJira() {
      this.loading = true;

      //Quản lý
      this.loading = true;

      selectJira(this.formData.Id).then(data => {
        this.listData = data;
        this.total = data.length;

        //this.isXacThuc = true;
        this.loading = false;
      });

    }
    ,
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          this.formData.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file)
          );
          if (this.isEditor == 0) {
            addYeuCau(this.formData).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
                  if(this.formData.UnitId != 1){
                    // var text = "Yêu cầu " + this.formData.TenYeuCau + " đã đổi trạng thái sang " +  this.formData.States.StateName;

                    // sendTeleAsync(text);
                    sendTeleAsync(data);
                  }
              this.getListData();
            });
          } else {
            //delete this.formData.LinhVuc;
            updateYeuCau(this.formData).then(data => {
              console.log(data);
              if(data != ""){
                  this.$message({
                  type: "success",
                  message: "Cập nhật thành công!"
                  });
                  this.stateNew = this.formData.StateId;
                  console.log(this.stateNew);
                  if(this.stateNew != this.stateOld && this.formData.UnitId != 1){
                    // var text = "Yêu cầu " + this.formData.TenYeuCau + " đã đổi trạng thái sang " +  this.formData.States.StateName;

                    // sendTeleAsync(text);
                    sendTeleAsync(this.formData.Id);
                  }
                  this.getListData();
              }
              else{
                 this.$message({
                type: "warning",
                message: "Không thể cập nhật!"
              });
             }

              this.getListData();
            });
          }
          this.dialogFormDisplay = false;
        } else {
          return false;
        }
      });
    },
    updateDataJira() {
      this.$refs.formData1.validate(valid => {
        if (valid) {
          if (this.isEditor == 0) {
            addJira(this.formData1).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Thêm mới thành công!"
              });
            });
          } else {
            //delete this.formData.LinhVuc;
            updateJira(this.formData1).then(data => {
              //console.log(data);
              this.$message({
                type: "success",
                message: "Cập nhật thành công!"
              });
            });
          }
          this.dialogFormDisplayJira = false;
        } else {
          return false;
        }
      });
    },

    getListComment(val){
      if(val != null){
        selectComments(val).then(data => {
            this.YeuCauComment = data;

        });
      }
    },
    handleAddComment(){
      addComments(this.formData1.CommentYeuCau, this.formData1.Id).then(data => {
        if(data != "" && data != null){
          this.$message({
                  type: "success",
                  message: "Thêm thành công!"
                  });
          this.getListComment(this.formData1.Id);
          this.formData1.CommentYeuCau = "";
        }
        else{

          this.$message({
                  type: "warning",
                  message: "Không thể thêm comment!"
                  });
        }
      });
    },
    handleEditComment(id, value){
      if (this.$refs.formData2 !== undefined) {
        this.$refs.formData2.resetFields();

      }
      this.formData2.CommentYeuCau = value;
      this.formData2.Id = id;
      this.dialogFormDisplayComment = true;
      this.isEditor = true;
    },
    UpdateComment(){

      updateComments(this.formData2.CommentYeuCau, this.formData2.Id).then(data => {
        if(data != "" && data != null){
          this.$message({
                  type: "success",
                  message: "Cập nhật thành công!"
                  });
          this.dialogFormDisplayComment = false;
          this.getListComment(this.formData1.Id);

        }
        else{

          this.$message({
                  type: "warning",
                  message: "Không thể cập nhật comment!"
                  });
        }
      });

    },
    DeleteComment(val){
      deleteComments(val).then(data => {
            if(data == 1){
              this.$message({
               type: "success",
                message: "Xóa thành công!"
              });
              this.getListComment(this.formData1.Id);
            }
            else{
               this.$message({
               type: "warning",
                message: "Không thể xóa!"
              });
            }
          });

    },
    handleSizeChange(val) {
      this.pagination = val;
    },
    changetextColor(val) {
      if (val == 8) {
        var text = "color:red;font-weight: bold;";
        return text
      }
      else if (val == 6) {
        var text = "color:green;font-weight: bold;";
        return text
      }
      else if (val == 7) {
        var text = "color:dodgerblue;font-weight: bold;";
        return text
      }
    },
    checkTrangThai() {

    },
    changetextColorPio(val){
      if(val == "Medium"){
        var text = "color:coral;font-weight: bold;";
        return text

      }
      else if(val == "Highest"){

        var text = "color:red;font-weight: bold;";
        return text
      }
      else if(val == "High"){

        var text = "color:red;font-weight: bold;";
        return text
      }
      else if(val == "Low"){

        var text = "color:blue;font-weight: bold;";
        return text
      }
      else if(val == "Lowest"){

      var text = "color:blue;font-weight: bold;";
      return text
    }
    },
    renderData() {
      var _data = this.listData.filter(post => {
        return post.TenYeuCau.toLowerCase().includes(this.search.toLowerCase());
        return post.Id.includes(this.search);
      });
      this.total = _data.length;
      return _data.slice(
        (this.activePage - 1) * this.pagination,
        (this.activePage - 1) * this.pagination + this.pagination
      );
    },
    renderIndex(idx) {
      return idx + this.pagination * (this.activePage - 1) + 1;
    },
  },

  created() {
    //getCauHinh("ChoPhepCapNhatDN").then(data => {
    //  if (data) {
    //    this.allowEdit = data[0].GiaTri == "1";
    //    console.log(this.allowEdit);
    //  }
    //});
    getListDanhMucYeuCau().then(data => {
      if (data) {
        this.ListDMNhanSu = data.DMNhanSu;
        this.ListDMTinhTrang = data.DMTinhTrang;
        this.ListDMTrangThai = data.DMTrangThai;
        this.ListDMDichVu = data.DMDichVu;
        this.ListDMJira = data.DMJira;
        this.ListDMDonVi = data.DMDonVi;
      }
    });

    this.getListData();

  }
};
</script>
<style>
  .el-input.is-disabled .el-input__inner {
    background-color: #fff !important;
    color: #606266 !important;
  }

  .ck-editor__editable_inline {
    min-height: 200px;
  }
</style>
