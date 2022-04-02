<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Báo cáo</a>
              </li>
              <li class="breadcrumb-item active">Báo cáo theo thời gian</li>
            </ol>
          </div>
          <h4 class="page-title">Danh sách yêu cầu</h4>
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
                       @click="handleSearch"
                       v-if="allowEdit" style="margin-left:10px">Tìm</el-button>
            <el-button type="success"
                       size="small"
                       icon="el-icon-download"
                       class="filter-item"
                       @click="handleExport">Xuất</el-button>

            <el-input clearable
                      v-model="search"
                      placeholder="Tìm kiếm"
                      style="width: 240px; float: left; "></el-input>
            <el-date-picker v-model="value1" 
                            type="datetime"
                            placeholder="Select date and time"
                            style="width: 240px; float: left;margin-left:5px">
            </el-date-picker>
            <el-date-picker v-model="value2"
                            type="datetime"
                            placeholder="Select date and time"
                            style="width: 240px; float: left;">
            </el-date-picker>
          </div>
          <el-table :data="renderData()"
                    border
                    stripe
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
            <el-table-column prop="Id" label="Mã" width="80" sortable>
              <template slot-scope="scope">
                <text-highlight :queries="search" style="word-break: normal;">
                  YC{{ scope.row.Id }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="TenYeuCau" label="Yêu cầu">
              <template slot-scope="scope">
                <!--<text-highlight >


      </text-highlight>-->
                <span :queries="search" style="word-break: normal;">
                  {{ scope.row.TenYeuCau }}
                </span>
              </template>
            </el-table-column>

            <el-table-column prop="NhanSu"
                             label="Người thực hiện"
                             width="180"
                             align="center"
                             style="word-break: normal;">

              <template slot-scope="scope" style="word-break: normal; max-width:180px">
                <span style="word-break: normal;"> {{ scope.row.NhanSuId ? scope.row.NhanSu.TenNhanSu : ""}}</span>

              </template>
            </el-table-column>
            <el-table-column prop="NgayTao"
                             label="Ngày Tạo"
                             width="130"
                             align="center"
                             sortable>
              <template slot-scope="scope">
                {{ formatDate(scope.row.NgayTao) }}
              </template>
            </el-table-column>
            <el-table-column prop="ThoiHan"
                             label="Deadline"
                             width="130"
                             align="center"
                             sortable>
              <template slot-scope="scope">
                {{ formatDate(scope.row.ThoiHan) }}
              </template>
            </el-table-column>
            <el-table-column prop="StateId"
                             label="Trạng thái"
                             width="120">
              <template slot-scope="scope">
                <text-highlight :queries="search" :style=changetextColor(scope.row.States.Id)>

                  {{ scope.row.StateId ? scope.row.States.StateName : ""}}

                </text-highlight>
              </template>
            </el-table-column>

            <el-table-column prop="Status" label="Tình trạng" width="100" v-if="!isXacNhan">

              <template slot-scope="scope">
                <span v-if="new Date(scope.row.ThoiHan) < Date.now() && scope.row.StateId != 6" style="color:darkorange">
                  Trễ hạn
                </span>
                <span v-if="new Date(scope.row.ThoiHan) >= Date.now() && scope.row.StateId != 6" style="color:green">
                  Trong hạn
                </span>
              </template>

            </el-table-column>
            <el-table-column prop="NgayXuLy" label="Ngày xử lý" width="130" v-if="isXacNhan">

              <template slot-scope="scope">
                {{ formatDate(scope.row.NgayXuLy) }}
              </template>

            </el-table-column>
            <el-table-column align="center" label="" fixed="right" width="190">
              <template slot="header" slot-scope="scope">

              </template>
              <template slot-scope="scope">
                <el-button v-if="" @click="handleView(scope.$index, scope.row)"
                           type="info"
                           title="Xem"
                           icon="el-icon-view"
                           size="mini"></el-button>

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




    <!--  ////////////////////////// -->

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

          {{formData1.TenYeuCau}}
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngày yêu cầu: " prop="NgayYeuCau">

              {{formatDate(formData1.NgayYeuCau)}}

            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Deadline: " prop="ThoiHan">

              {{formatDate(formData1.ThoiHan)}}
            </el-form-item>
          </el-col>

        </el-row>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Trạng thái: " prop="StateId">

              {{formData1.StateId ? formData1.States.StateName : ""}}
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân sự: " prop="NhanSuId">

              {{formData1.NhanSuId ? formData1.NhanSu.TenNhanSu : ""}}

            </el-form-item>

          </el-col>

        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Người giám sát: " prop="NguoiGiamSatId">

              {{formData1.NguoiGiamSatId ? formData1.NhanSu.TenNhanSu : ""}}
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Người tạo: " prop="NguoiTaoId">

              {{formData1.NguoiTaoId ? formData1.User.FullName : ""}}
            </el-form-item>
          </el-col>

        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item prop="ThoiHanMongMuon" label="Thời hạn KH: ">

              {{formatDate(formData1.ThoiHanMongMuon)}}
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Dịch vụ: " prop="DichVuId">

              {{formData1.DichVuId ? formData1.DichVu.TenDichVu : ""}}
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Đơn vị: " prop="DonViYeuCauId">

              {{formData1.DonViYeuCauId ? formData1.DonViYeuCau.TenDonViYeuCau : ""}}
            </el-form-item>

          </el-col>
        </el-row>
        <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
          <text-highlight style="word-break: normal;" v-html="formData1.NoiDung">
          </text-highlight>
        </el-form-item>

        <el-form-item label="Jira: " prop="JiraDaGui">

          {{formData1.JiraDaGui}}

        </el-form-item>


        <el-form-item label="Trạng thái jira: " prop="StatusJira">
          {{formData1.StatusJira}}
        </el-form-item>
        <el-form-item label="Số lần comment" prop="Comment">

          <el-tabs type="border-card">
            <el-tab-pane label="Jira comment">
              <div style="overflow-y:scroll; height:300px">
                <ul style="list-style-type:none; margin-left:-30px; margin-bottom:10px" v-for="item in Comment">
                  <li>
                    <b>+</b> {{item.body + ' - ' + item.author.displayName + ' - ' + formatDateTime(item.updated)}}
                  </li>
                </ul>
              </div>
            </el-tab-pane>
            <el-tab-pane label="Yêu cầu comment: ">
              <div style="overflow-y:scroll; max-height:400px">
                <el-form-item v-for="item in YeuCauComment">
                  <!--{{item.Comments + ' - ' + item.NgayComment}}-->
                  <label>{{item.User.FullName +' : '}}</label> <span v-html="item.Comments + ' ' + formatDateTime(item.NgayComment)"></span>
                </el-form-item>
              </div>

              <el-form-item prop="CommentYeuCau">
                <ckeditor :editor="editor"
                          v-model="formData1.CommentYeuCau"
                          :config="editorConfig" disabled></ckeditor>
                <!--<el-button @click="handleAddComment">Save Comment</el-button>-->
              </el-form-item>


            </el-tab-pane>
          </el-tabs>


        </el-form-item>
        <el-form-item label="Mức độ: " prop="Priority" :style=changetextColorPio(formData1.Priority)>

          {{formData1.Priority}}
        </el-form-item>
        <el-form-item label="Tập tin đính kèm: ">
          <!--<el-upload action="/api/TapTin/UploadDoc"
                   :limit="3"
                   :multiple="true"
                   :on-preview="handlePreview"
                   :file-list="fileList"
                   :on-success="handleSuccess"
                   :before-upload="beforeUpload"
                   accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm,image/jpeg,image/gif,image/png"
                   :auto-upload="true"
                   size="mini">

        </el-upload>-->
          <div v-for="item in fileList">
            <a :href=item.url download>{{item.name}}</a>
          </div>
        </el-form-item>
      </el-form>

      <span slot="footer" class="dialog-footer">
        <el-button @click="resetFormView" size="small">Bỏ qua</el-button>
        <el-button v-if="isAccept" @click="AcceptYeuCau" size="small" type="success">Tiếp nhận</el-button>
      </span>
    </el-dialog>



    <!--         Xem Yêu Cầu        -->
    <el-dialog title="Xem nội dung yêu cầu"
               :visible.sync="dialogFormBHView"
               top="55px"
               width="80%"
               center>
      <el-form :model="formData3"
               :rules="formRules"
               ref="formData3"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">
        <div class="card" style="box-shadow: 0 0 25px #DCDCDC	; border-radius:0.5rem">
          <h4 style="margin-left:20px">Nội dung yêu cầu: </h4>
          <el-form-item label="Dịch vụ:" prop="DichVuId">

            {{formData3.DichVuId ? formData3.DichVu.TenDichVu : ""}}
          </el-form-item>

          <el-form-item label="Tiêu đề yêu cầu: " prop="TenYeuCau">

            {{formData3.TenYeuCau}}
          </el-form-item>
          <el-row>
            <el-col :span="12">
              <el-form-item label="Nhân sự thực hiện: " prop="NhanSuId">

                {{formData3.NhanSuId ? formData3.NhanSu.TenNhanSu : ""}}
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Người tạo: " prop="NguoiTaoId">

                {{formData3.NguoiTaoId ? formData3.User.FullName : ""}}
              </el-form-item>
            </el-col>

          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="Ngày KH yêu cầu: " prop="NgayYeuCau">

                {{formatDate(formData3.NgayYeuCau)}}
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Hạn xử lý: " prop="ThoiHan">

                {{formatDate(formData3.ThoiHan)}}
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Trạng thái: " prop="StateId">

                {{formData3.StateId ? formData3.States.StateName : ""}}
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Mã số thuế: " prop="MaSoThue">

                {{formData3.MaSoThue}}
              </el-form-item>
            </el-col>
          </el-row>
          <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
            <span v-html="formData3.NoiDung">
            </span>
            <!--{{formData3.NoiDung}}-->
          </el-form-item>
          <el-form-item label="Jira: " prop="JiraDaGui">

            {{formData3.JiraDaGui}}
          </el-form-item>

          <el-form-item label="Số lần comment: " prop="Comment">

            <el-tabs type="border-card">
              <el-tab-pane label="Jira Comment">
                <div style="overflow-y:scroll; height:300px">
                  <ul style="list-style-type:none; margin-left:-30px; margin-bottom:10px" v-for="item in Comment">
                    <li>
                      <b>+</b> {{item.body + ' - ' + item.author.displayName + ' - ' + formatDateTime(item.updated)}}
                    </li>
                  </ul>
                </div>
              </el-tab-pane>
              <el-tab-pane label="Yêu Cầu Comment">
                <div style="overflow-y:scroll; height:400px">
                  <el-form-item v-for="item in YeuCauComment">
                    <!--{{item.Comments + ' - ' + item.NgayComment}}-->
                    <label>{{item.User.FullName +' : '}}</label> <span v-html="item.Comments + ' ' + formatDateTime(item.NgayComment)"></span>
                  </el-form-item>
                </div>
                <el-form-item prop="CommentYeuCau">
                  <!--<el-input type="text"
                size="small" v-model="formData3.CommentYeuCau"></el-input>-->
                  <ckeditor :editor="editor"
                            v-model="formData3.CommentYeuCau"
                            :config="editorConfig" v-if="isComment" disabled></ckeditor>
                  <!--<el-button @click="handleAddComment" v-if="isComment">Save Comment</el-button>-->
                </el-form-item>


              </el-tab-pane>
            </el-tabs>


          </el-form-item>
          <el-form-item label="Mức độ: " prop="Priority" :style=changetextColorPio(formData3.Priority)>

            {{formData3.Priority}}
          </el-form-item>
          <el-form-item label="Tập tin đính kèm:">
            <!--<el-upload action="/api/TapTin/UploadDoc"
                     :limit="4"
                     :multiple="true"
                     :on-preview="handlePreview"
                     :on-remove="handleRemove"
                     :file-list="fileList"
                     :on-success="handleSuccess"
                     :before-upload="beforeUpload"
                     accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm,image/jpeg,image/gif,image/png"
                     :auto-upload="true"
                     size="mini">
          </el-upload>-->
            <div v-for="item in fileList">
              <a :href=item.url download>{{item.name}}</a>
            </div>
          </el-form-item>
        </div>


        <div class="card" style="box-shadow: 0 0 25px #DCDCDC; border-radius:0.5rem">
          <h4 style="margin-left:20px">Nội dung xử lý: </h4>
          <el-form-item label="Kết quả xử lý" prop="NoiDungXuLy" style="margin-right:10px">

            <span v-html="formData3.NoiDungXuLy">
            </span>

          </el-form-item>
          <el-form-item label="Tập tin xử lý: ">

            <div v-for="item in fileList1">
              <a :href=item.url download>{{item.name}}</a>
            </div>
          </el-form-item>
        </div>

      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormBHView" size="small">Bỏ qua</el-button>

      </span>
    </el-dialog>




  </div>
</template>
<script>
  import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
  import moment from "moment";
  import { getRole, getUser, eGkxtz } from "../../store/common";
  import {
    getYeuCau,
    getListDanhMucYeuCau,
    selectYeuCau,
    updateYeuCau,
    addYeuCau,
    getTrangThai,
    selectYeuCauAll,
    selectComments,
    addComments,
    updateComments,
    deleteComments,
    sendTeleAsync,
    getUserUnitId,
    getYCUnitId,
    getCurrentNS,
    getycbytime,
    currenQL,
    NSQL,
  } from "../../store/api";
import { log } from "util";
  export default {
    data() {
      return {
        title: null,
        dialogVisibleDoc: false,
        dialogFileUrl: null,
        FileDowloadName: null,
        dialogFormDisplay: false,
        dialogFormBHDisplay: false,
        dialogFormBHEdit: false,
        dialogFormView: false,
        dialogFormBHHandle: false,
        dialogFormBHView: false,
        dialogXacNhan: false,
        loading: false,
        isEditor: false,
        isXacThuc: false,
        allowEdit: true,
        isPermiss: true,
        isView: false,
        isEdit: false,
        isChangeNhanSu: true,
        isComment: true,
        isChuyenYc: false,
        isButtonEdit: false,
        isButtonDelete: false,
        isButtonView: false,
        isViewBT: false,
        isTrangThai: true,
        isXacNhan: false,
        isLoaiYC: true,
        search: "",
        isAccept: false,
        isChuyenNS: false,
        StateIdFilter: 5,
        DichVuIdFilter: 1,
        JiraDaGuiLink: "",
        UserUnitId: 0,
        YCUnit: 0,
        UserDV: 0,
        NhanSuId: 0,
        UserId: 0,
        NhanSu: "",
        value1: '',
        value2: '',
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
          ThoiHanMongMuon: null,
          KeyJira: null,
          Priority: null,
          UnitId: null,
          domains: [{
            key: 1,
            value: ''
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
          CommentYeuCau: null,
        },
        formData2: {

          TenYeuCau: null,
          DichVuId: null,
          NhanSuId: null,
          ThoiHanMongMuon: null,
          ThoiHan: null,
          NoiDung: null,
          JiraDaGui: null,
          FileUpload: null,
          MaSoThue: null,
          NguoiTaoId: null,
          LoaiYeuCauId: null,

        },
        formData3: {
          Id: null,
          TenYeuCau: null,
          DichVuId: null,
          NhanSuId: null,
          NguoiTaoId: null,
          NgayYeuCau: null,
          ThoiHan: null,
          NoiDung: null,
          JiraDaGui: null,
          FileUpload: null,
          NoiDungXuLy: null,
          Priority: null,
          KeyJira: null,
          StatusJira: null,
          CommentYeuCau: null,
          StateId: null,
          MaSoThue: null,
          NgayXuLy: null,
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
        YeuCauComment: [],
        ListDMTrangThai: [],
        ListDMNhanSu: [],
        ListDMDichVu: [],
        ListDMJira: [],
        ListDMTinhTrang: [],
        ListDMDonVi: [],
        ListJira: [],
        ListQLDV: [],
        TrangThai: [],
        Comment: [],
        fileList: [],
        fileDoc: [],
        fileList1: [],
        fileDoc1: [],
        fileImage: [],
        pagination: 10,
        total: 10,
        activePage: 1,
        dataFilter: null,
        stateOld: [],
        stateNew: [],
        UserProFile: [],
        QLDVList: [],
        ListNhansuQLDV: [],
        NhansuQLDV: [],
        ListLoaiYC: [],
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
      formatMYC(val) {
        if (val) {
          var MYC = "YC" + val;
          return MYC;
        } else return null;
      },
      formatHtml(val) {
        if (val) {
          return val.replace(/<[^>]*>?/gm, '');

        }

      },
      formatJira(val) {
        if (val) {
          var jira = "";
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
              if (f2 == "Unit") {
                return result ? result.UnitName : "";
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
            if (f == "NoiDung") {
              return this.formatHtml(result)
            }
            if (f == "ThoiHanMongMuon") {
              return this.formatDate(result);
            }
            if (f == "Id") {
              return this.formatMYC(result);
            }
            return result;
          })
        );
      },
      setNguoiThucHien(val) {
        var dv = this.ListDMDichVu.find(obj => obj.Id == val);


        var qldv = this.ListQLDV.find(obj => obj.DichVuId == dv.Id && obj.UnitId == this.UserUnitId);
        if (qldv) {
          this.formData2.NhanSuId = qldv.NhanSuId;

        }
        if (val == 6) {
          this.isLoaiYC = false;
        }
        else if (val != 6) {
          this.isLoaiYC = true;
          this.formData2.LoaiYeuCauId = null;
        }
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

     
      handleView(index, row) {

        if (row.StateId == 6) {
          this.isComment = false;

        }

        if (row.DichVuId == this.UserDV) {
          if (this.$refs.formData3 !== undefined) {
            this.$refs.formData3.resetFields();
          }
          this.formData3 = Object.assign({}, row);

          if (row.JiraDaGui != null) {
            var jiralink = row.JiraDaGui;
            var split = jiralink.split('/');
            for (var i = 0; i < split.length; i++) {
              this.JiraDaGuiLink = split[split.length - 1];
            }
            var name = getUser();

            this.getDataTrangThai();

          }

          this.getListComment(this.formData3.Id);

          if (this.formData3.FileUpload) {
            this.fileList = [];
            this.fileDoc = [];
            this.fileImage = [];

            var _arr = JSON.parse(this.formData3.FileUpload);
            for (var i = 0; i < _arr.length; i++) {
              var urlFile = "/uploads/" + _arr[i];
              console.log(_arr[i]);
              this.fileList.push({ name: _arr[i], url: urlFile });
              this.fileDoc.push({ key: _arr[i], file: _arr[i] });
            }
          }

          if (this.formData3.FileXuLy) {
            this.fileList1 = [];
            this.fileDo1c = [];


            var _arr = JSON.parse(this.formData3.FileXuLy);
            for (var i = 0; i < _arr.length; i++) {
              var urlFile = "/uploads/" + _arr[i];
              console.log(_arr[i]);
              this.fileList1.push({ name: _arr[i], url: urlFile });
              this.fileDoc1.push({ key: _arr[i], file: _arr[i] });
            }
          }

          this.isEditor = false;
          this.dialogFormBHView = true;
        }
        else {

          if (this.UserUnitId == 1) {
            if (this.$refs.formData1 !== undefined) {
              this.$refs.formData1.resetFields();
            }
            this.formData1 = Object.assign({}, row);

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

          }
          else {
            if (this.$refs.formData3 !== undefined) {
              this.$refs.formData3.resetFields();
            }
            this.formData3 = Object.assign({}, row);

            if (row.JiraDaGui != null) {
              var jiralink = row.JiraDaGui;
              var split = jiralink.split('/');
              for (var i = 0; i < split.length; i++) {
                this.JiraDaGuiLink = split[split.length - 1];
              }
              var name = getUser();

              this.getDataTrangThai();

            }

            this.getListComment(this.formData3.Id);

            if (this.formData3.FileUpload) {
              this.fileList = [];
              this.fileDoc = [];

              var _arr = JSON.parse(this.formData3.FileUpload);
              for (var i = 0; i < _arr.length; i++) {
                var urlFile = "/uploads/" + _arr[i];
                this.fileList.push({ name: _arr[i], url: urlFile });
                this.fileDoc.push({ key: _arr[i], file: _arr[i] });
              }
            }
            if (this.formData3.FileXuLy) {
              this.fileList1 = [];
              this.fileDo1c = [];


              var _arr = JSON.parse(this.formData3.FileXuLy);
              for (var i = 0; i < _arr.length; i++) {
                var urlFile = "/uploads/" + _arr[i];
                console.log(_arr[i]);
                this.fileList1.push({ name: _arr[i], url: urlFile });
                this.fileDoc1.push({ key: _arr[i], file: _arr[i] });
              }
            }
            this.isEditor = false;
            this.dialogFormBHView = true;

          }



        }


      },
      handleSearch() {
        console.log(this.value1);
        console.log(this.value2);
        this.getListData();

      },
      handleSuccess(response, file, ListJira) {
        this.fileDoc.push({ key: file.name, file: response.file });
      },
      handleRemove(file, ListJira) {
        let i = this.fileDoc.map(item => item.key).indexOf(file.name);
        this.fileDoc.splice(i, 1);
      },
      handleSuccess1(response, file, ListJira) {
        this.fileDoc1.push({ key: file.name, file: response.file });
      },
      handleRemove1(file, ListJira) {
        let i = this.fileDoc1.map(item => item.key).indexOf(file.name);
        this.fileDoc1.splice(i, 1);
      },
      handlePreview(file) {
        this.dialogFileUrl = file.url;

        var fileurl = this.dialogFileUrl;
        var filename = fileurl.split('/');
        console.log(this.fileList);
        this.FileDowloadName = filename[2];
        this.dialogVisibleDoc = true;


      },

      beforeUpload(file) {
        const isPdf = file.type === "application/pdf";
        const isDoc = file.type === "application/msword";
        const isDocx =
          file.type ===
          "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        const isXls = file.type === "application/vnd.ms-excel";
        const isXlsx = file.type === "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        const isXlsm = file.type === "application/vnd.ms-excel.sheet.macroEnabled.12";
        const isjpg = file.type == "image/jpeg";
        const ispng = file.type == "image/png";
        const israr = file.type == "";

        const iszip = file.type == "application/x-zip-compressed";
        const isp7b = file.type == "application/x-pkcs7-certificates";
        const isLt10M = file.size / 1024 / 1024 < 10;
        if (!isPdf && !isDoc && !isDocx && !isXls && !isXlsx && !isXlsm && !isjpg && !ispng && !israr && !iszip && !isp7b) {
          this.$message.error("Không đúng định dạng quy định");
        }
        if (!isLt10M) {
          this.$message.error("Dung lượng vượt quá 10M");
        }
        return (isPdf || isDoc || isDocx || isXls || isXlsx || isXlsm || isjpg || ispng || israr || iszip || isp7b) && isLt10M;
      },
      handleExport() {
        import("../../vendor/Export2Excel").then(excel => {
          const tHeader = [
            "Mã yêu cầu",
            "Tên yêu cầu",
            "Nội dung",
            "Ngày tạo",
            "Thời hạn",
            "Task Jira",
            "Trạng thái",
            "Nhân sự",
            "Dịch vụ",
            "Đơn vị",
            "Mã số thuế",
            "Thời hạn KH mong muốn"
          ];
          const filterVal = [
            "Id",
            "TenYeuCau",
            "NoiDung",
            "",
            "ThoiHan",
            "JiraDaGui",
            "States.StateName",
            "NhanSu.TenNhanSu",
            "DichVu.TenDichVu",
            "Unit.UnitName",
            "MaSoThue",
            "ThoiHanMongMuon",

          ];
          const data = this.formatJson(filterVal, this.listData);

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
      resetFormBHView() {
        this.dialogFormBHView = false;
        return true;
      },
      resetFormBHHandle() {

        this.dialogFormBHHandle = false;
        return true;
      },
      resetFormBH() {

        this.dialogFormBHDisplay = false;
        return true;

      },
      resetFormXacNhan() {
        this.dialogXacNhan = false;
      },
      getListData() {
        this.loading = true;
        getCurrentNS().then(data => {
          if (data != null && data != "") {
            this.UserDV = data.DichVuId;
            this.UserId = data.UserId;
            this.NhanSuId = data.NhanSuId;
            this.NhanSu = data.TenNhanSu;

          }

        });
        currenQL().then(data => {
          if (data != null && data != "") {

            this.QLDVList = data

          }
        });
        NSQL().then(data => {
          if (data) {
            this.NhansuQLDV = data;

          }
        });
        //Quản lý
        this.loading = true;
        if (this.value1 != "" && this.value2 != "") {
          getycbytime(this.value1, this.value2).then(data => {
            this.listData = data;
            this.total = data.length;

            this.loading = false;
          });
        }
        else {
          if (this.StateIdFilter != 5 || this.DichVuIdFilter != 1) {
            selectYeuCau(this.StateIdFilter, this.DichVuIdFilter).then(data => {
              this.listData = data;
              this.total = data.length;

              this.loading = false;
            });
          }
          else {
            selectYeuCauAll(this.StateIdFilter, this.DichVuIdFilter).then(data => {
              this.listData = data;
              this.total = data.length;

              this.loading = false;
            });
          }

        }
       
        this.getDataTrangThai();
        getUserUnitId().then(data => {
          this.UserUnitId = data.UnitId;

        });


      },
      // get trạng thái
      getDataTrangThai() {
        this.loading = true;

        getTrangThai(this.JiraDaGuiLink).then(data => {
          if (data != null && data.errorMessages == null && data.fields) {
            this.TrangThai = data;
            this.Comment = data.fields.comment.comments;
            if (this.UserUnitId == 1) {

              this.formData1.KeyJira = data.key;
              this.formData1.StatusJira = data.fields.status.name;

              this.formData1.Priority = data.fields.priority.name;
            }
            else {
              this.formData3.KeyJira = data.key;
              this.formData3.StatusJira = data.fields.status.name;

              this.formData3.Priority = data.fields.priority.name;
            }


          }


          this.loading = false;
        });


      },
     
      getListComment(val) {
        if (val != null) {
          selectComments(val).then(data => {
            this.YeuCauComment = data;

          });
        }
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
      changetextColorPio(val) {
        if (val == "Medium") {
          var text = "color:coral;font-weight: bold;";
          return text

        }
        else if (val == "Highest") {

          var text = "color:red;font-weight: bold;";
          return text
        }
        else if (val == "High") {

          var text = "color:red;font-weight: bold;";
          return text
        }
        else if (val == "Low") {

          var text = "color:blue;font-weight: bold;";
          return text
        }
        else if (val == "Lowest") {

          var text = "color:blue;font-weight: bold;";
          return text
        }
      },
      renderData() {
        var _data = this.listData.filter(post => {
          var id = "YC" + post.Id;
          return post.TenYeuCau.toLowerCase().includes(this.search.toLowerCase()) || id.toLowerCase().includes(this.search.toLowerCase());

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
      
      getListDanhMucYeuCau().then(data => {
        if (data) {
          this.ListDMNhanSu = data.DMNhanSu;
          this.ListDMTinhTrang = data.DMTinhTrang;
          this.ListDMTrangThai = data.DMTrangThai;
          this.ListDMDichVu = data.DMDichVu;
          this.ListDMJira = data.DMJira;
          this.ListDMDonVi = data.DMDonVi;
          this.ListQLDV = data.DMQLDV;
          this.ListLoaiYC = data.DMLYC;
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

  .el-input__inner {
    height: 34px !important;
  }

  .el-form-item--small .el-form-item__content .image img {
    width: -webkit-fill-available;
    height: -webkit-fill-available;
  }

  .el-table {
    color: black !important;
  }

    .el-table thead {
      color: black !important;
    }
</style>
