<template>
  <div>
    <!-- start page title -->
    <div class="row">
      <div class="col-12">
        <div class="page-title-box">
          <div class="page-title-right">
            <ol class="breadcrumb m-0">
              <li class="breadcrumb-item">
                <a href="javascript: void(0);">Thống kê</a>
              </li>
              <li class="breadcrumb-item active">Thống kê theo địa bàn</li>
            </ol>
          </div>
          <h4 class="page-title">Thống kê theo địa bàn</h4>
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
                       @click="searchDSYC"
                       v-if="allowEdit" style="margin-left:5px">Tìm</el-button>
           
            <el-button type="success"
                       size="small"
                       icon="el-icon-download"
                       class="filter-item"
                       @click="handleExport">Xuất</el-button>
            <treeselect v-model="UnitIdS"
                        :multiple="false"
                        placeholder="Chọn địa bàn"
                        :searchable="true"
                        :options="ListDiaBan"
                        style="width: 160px; float: left;text-transform:none;margin-left:5px; " />
            <el-select style="width: 160px; float: left;margin-right:3px"
                       v-model="DichVuIdS"
                       placeholder="Chọn Dịch Vụ">
              <el-option v-for="item in ListDMDichVu"
                         :key="item.Id"
                         :label="item.TenDichVu"
                         :value="item.Id">
              </el-option>
            </el-select>
            <el-date-picker v-model="TuNgayS"
                            type="datetime"
                            placeholder="Chọn ngày bắt đầu"
                            style="width: 200px; float: left;margin-left:5px">
            </el-date-picker>
            <el-date-picker v-model="DenNgayS"
                            type="datetime"
                            placeholder="Chọn ngày kết thúc"
                            style="width: 200px; float: left;">
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
            <el-table-column width="70" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>


            <el-table-column prop="UnitName"
                             label="Tên địa bàn"
                             width="245"
                             align="center"
                             style="word-break: normal;">

              <template slot-scope="scope" style="word-break: normal; max-width:180px">
                <span style="word-break: normal;">{{scope.row.UnitName}}</span>

              </template>
            </el-table-column>
            <el-table-column prop="TongYeuCau"
                             label="Tổng yêu cầu"
                             width="200"
                             align="center"
                             style="word-break: normal;">

              <template slot-scope="scope" style="word-break: normal; max-width:180px">
                <span style="word-break: normal;">{{scope.row.SoLuongTong}}</span>

              </template>
            </el-table-column>
            <el-table-column prop="SoLuongCTH"
                             label="Chưa tiếp nhận"
                             width="180"
                             align="center"
                             style="word-break: normal;">

              <template slot-scope="scope" style="word-break: normal; max-width:180px">
                <span style="word-break: normal;">{{scope.row.SoLuongCTH}}</span>

              </template>
            </el-table-column>
            <el-table-column prop="SoLuongDTH"
                             label="Đang xử lý"
                             width="180"
                             align="center"
                             style="word-break: normal;">

              <template slot-scope="scope" style="word-break: normal; max-width:180px">
                <span style="word-break: normal;">{{scope.row.SoLuongDTH}}</span>

              </template>
            </el-table-column>
            <el-table-column prop="SoLuongHT"
                             label="Đã hoàn thành"
                             width="180"
                             align="center"
                             style="word-break: normal;">

              <template slot-scope="scope" style="word-break: normal; max-width:180px">
                <span style="word-break: normal;">{{scope.row.SoLuongHT}}</span>

              </template>
            </el-table-column>
            <!--<el-table-column align="center" label="" fixed="right" width="190">
              <template slot="header" slot-scope="scope">

              </template>
              <template slot-scope="scope">
                <el-button v-if="" @click="handleView(scope.$index, scope.row)"
                           type="info"
                           title="Xem"
                           icon="el-icon-view"
                           size="mini"></el-button>

              </template>
            </el-table-column>-->
          </el-table>
          <el-pagination class="pt-2 pl-0"
                         :page-size="pagination"
                         background
                         style="width: 100%"
                         @size-change="handleSizeChange"
                         :current-page.sync="activePage"
                         :page-sizes="[20, 50, 100, 500]"
                         layout="total,sizes,prev, pager, next"
                         :total="total">
          </el-pagination>
        </div>
      </div>
    </div>




 


   
    <el-dialog top="50px"
               title="Tìm kiếm"
               :visible.sync="dialogFormSearch"
               width="55%">
      <el-form :model="formData2"
               :rules="formRules"
               ref="formData2"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">
        <!--<el-form-item label="Loại ngày cần tìm: " prop="index">
          <el-select v-model="formData2.index"
                     placeholder="Loại ngày"
                     class="w-100">
            <el-option v-for="item in ListIndex"
                       :key="item.value"
                       :label="item.label"
                       :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>-->
        <el-row>
          <el-col :span="12">
            <el-form-item label="Chọn địa bàn: " prop="UnitId">
              <treeselect v-model="formData2.UnitId"
                          :multiple="false"
                          placeholder="Chọn địa bàn"
                          :searchable="true"
                          :options="ListDiaBan"
                          :select="changeDiaban(formData2.UnitId)"
                          style="width: 240px; float: left;text-transform:none;margin-left:5px; " />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân sự: " prop="NhanSuId">
              <el-select v-model="formData2.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100">
                <el-option v-for="item in ListNhanSuDiaBan"
                           :key="item.Id"
                           :label="item.TenNhanSu"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>

        </el-row>

      </el-form>
      <div style="text-align: center; margin-top:5px">
        <el-button type="warning" size="small" @click="resetFormSearch">Bỏ</el-button>
        <el-button type="success" size="small" @click="getValueSearch">Tìm</el-button>
      </div>


    </el-dialog>


  </div>
</template>
<script>
  import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
  import moment from "moment";
  import { getRole, getUser, eGkxtz } from "../../store/common";
  import Treeselect from "@riophae/vue-treeselect";
  // import the styles
  import "@riophae/vue-treeselect/dist/vue-treeselect.css";
  import {
    getListDanhMucYeuCau,
    getUserUnitId,
    getCurrentNS,
    getListUnitAll,
    getDSYCDonVi,
  } from "../../store/api";
  import { log } from "util";
  import { compileFunction } from "vm";
  export default {
    components: { Treeselect },
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
        dialogFormSearch: false,
        loading: false,
        isEditor: false,
        isXacThuc: false,
        allowEdit: true,
        isPermiss: true,
        isAdvantage: false,
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
        StateIdFilter: 5,
        DichVuIdFilter: 1,
        JiraDaGuiLink: "",
        UserUnitId: 0,
        YCUnit: 0,
        UserDV: 0,
        NhanSuId: 0,
        UserId: 0,
        NhanSu: "",
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

          UnitId: null,
          NhanSuId: null,
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
        pagination: 20,
        total: 20,
        activePage: 1,
        dataFilter: null,
        UserProFile: [],
        QLDVList: [],
        ListNhansuQLDV: [],
        NhansuQLDV: [],
        ListLoaiYC: [],
        ListDiaBan: [],
        ListNhanSuDiaBan: [],
        UnitIdS: null,
        DichVuIdS: 6,
        TuNgayS:  Date.now(),
        DenNgayS: Date.now(),
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
      searchDSYC() {
        this.getListData();
      },
      changeDiaban(val) {
        var diaban;
        var count = 0;
        //this.ListNhanSuDiaBan = [];
        


        for (var i = 0; i < this.ListDMNhanSu.length; i++) {
          if (this.ListDMNhanSu[i].UnitId == this.formData2.UnitId) {
            this.ListNhanSuDiaBan.push(this.ListDMNhanSu[i]);
          }
        }

        console.log(this.ListNhanSuDiaBan);

      },
      changeStateIdFilter() {

        this.getListData();
      },
      changeDichVuIdFilter() {

        this.getListData();
      },


     
      handleSearch() {


        this.isAdvantage = true;
        this.getListData();
        //console.log(this.DiaBanId);

      },
      handleSearchAdvantage() {
        this.dialogFormSearch = true;
        this.isAdvantage = true;
        if (this.$refs.formData2 !== undefined) {
          this.$refs.formData2.resetFields();

        }
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
            "Tên địa bàn",
            "Số lượng tổng",
            "Chưa tiếp nhận",
            "Đang xử lý",
            "Đã hoàn thành",
           
          ];
          const filterVal = [
            "UnitName",
            "SoLuongTong",
            "SoLuongCTH",
            "SoLuongDTH",
            "SoLuongHT",
          

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
      resetFormSearch() {
        this.dialogFormSearch = false;
        this.isAdvantage = false;
      },
      getValueSearch() {
        this.getListData();
        this.isAdvantage = false;
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
        
        //Quản lý
        this.loading = true;
        var tungay = this.formatDate(this.TuNgayS);
        var denngay = this.formatDate(this.DenNgayS);
        getDSYCDonVi(this.UnitIdS, this.DichVuIdS, tungay, denngay).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });


       
        getUserUnitId().then(data => {
          this.UserUnitId = data.UnitId;

        });
       

      },
      
      listUnit() {
        this.loading = true;
        getListUnitAll().then(data => {
          if (data != null) {
            this.ListDiaBan = data;
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
          return post.UnitName.toLowerCase().includes(this.search.toLowerCase()) || id.toLowerCase().includes(this.search.toLowerCase());

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
      this.listUnit();


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

  .vue-treeselect__control {
    height: 34px !important;
  }
</style>
