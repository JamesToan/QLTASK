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
          <h4 class="page-title">{{title}}</h4>
        </div>
      </div>
    </div>
    <!-- end page title -->
    <div class="row">
      <div class="col-12">
        <div class="card-box table-responsive">
          <div class=" pb-3" style="margin-bottom: 10px; float: left; color: black;">
            <span>Dịch vụ:</span>
            <el-select style="width: 200px;margin-right:3px"
                       v-model="DichVuIdFilter"
                       @change="changeDichVuIdFilter(DichVuIdFilter)"
                       placeholder="Chọn Dịch Vụ">
              <el-option v-for="item in ListDMDichVu"
                         :key="item.Id"
                         :label="item.TenDichVu"
                         :value="item.Id">
              </el-option>

            </el-select>
            <!--<span v-if="isTrangThai">
    Trạng thái:
  </span>
  <el-select style="width: 200px;"
             v-model="StateIdFilter"
             @change="changeStateIdFilter"
             placeholder="Chọn trạng thái"
             v-if="isTrangThai">
    <el-option v-for="item in ListDMTrangThai"
               :key="item.Id"
               :label="item.StateName"
               :value="item.Id">
    </el-option>
  </el-select>-->
            <span v-if="">
              Loại yêu cầu:
            </span>
            <el-select v-model="LoaiYeuCauIdFilter"
                       placeholder="Chọn loại yêu cầu"
                       @change="changeLoaiYeuCauFilter"
                       
                       style="width: 200px;margin-right:3px">
              <el-option v-for="item in ListLoaiYC"
                         :key="item.Id"
                         :label="item.TenLoaiYeuCau"
                         :value="item.Id">
              </el-option>
            </el-select>
           
          </div>
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
                      style="width: 200px; float: left; margin-right: 5px"></el-input>
            <!--<el-select style="width: 200px; float: left;"
                       v-model="StateIdFilter"
                       @change="changeStateIdFilter"
                       placeholder="Chọn trạng thái"
                       v-if="isTrangThai">
              <el-option v-for="item in ListDMTrangThai"
                         :key="item.Id"
                         :label="item.StateName"
                         :value="item.Id">
              </el-option>
            </el-select>-->
            <!--<el-select style="width: 200px; float: left;margin-right:3px"
                       v-model="DichVuIdFilter"
                       @change="changeDichVuIdFilter(DichVuIdFilter)"
                       placeholder="Chọn Dịch Vụ">
              <el-option v-for="item in ListDMDichVu"
                         :key="item.Id"
                         :label="item.TenDichVu"
                         :value="item.Id">
              </el-option>
            </el-select>-->
          </div>
          <el-table :data="renderData()"
                    border
                    stripe
                    v-loading="loading"
                    default-expand-all
                    row-key="Id"
                    style="width: 100%">

            <el-table-column width="70" label="STT" align="center">
              <template slot-scope="scope">
                {{ renderIndex(scope.$index) }}
              </template>
            </el-table-column>
            <el-table-column prop="Id" label="Mã" width="80" sortable>
              <template slot-scope="scope">
                <text-highlight :queries="search"  :style=changecolor(scope.row.LoaiYeuCauId)>
                  YC{{ scope.row.Id }}
                </text-highlight>
              </template>
            </el-table-column>
            <el-table-column prop="TenYeuCau" label="Yêu cầu" min-width="250">
              <template slot-scope="scope">

                <span :queries="search"  :style=changecolor(scope.row.LoaiYeuCauId)>
                  {{ scope.row.TenYeuCau }}
                </span>
              </template>
            </el-table-column>

            <el-table-column prop="NhanSu"
                             label="Người thực hiện"
                             width="180"
                             align="center"
                             style="word-break: normal;"
                             column-key="NhanSu"
                             :filters=TenFilter
                             :filter-method="filterHandler"
                             >

              <template slot-scope="scope"  >
                <span :style=changecolor(scope.row.LoaiYeuCauId)> {{ scope.row.NhanSuId ? scope.row.NhanSu.TenNhanSu : ""}}</span>

              </template>
            </el-table-column>
            <!--<el-table-column prop="NhanSuId"
                             label="Người thực hiện"
                             width="180"
                             align="center"
                             style="word-break: normal;" v-if="UserUnitId ==1 || UserUnitId = 2">

              <template slot-scope="scope"  >
                <span :style=changecolor(scope.row.LoaiYeuCauId)> {{ scope.row.NhanSuId ? scope.row.NhanSu.TenNhanSu : ""}}</span>

              </template>
            </el-table-column>-->
            <el-table-column prop="NguoiTaoId"
                             label="Người tạo"
                             width="180"
                             align="center"
                             style="word-break: normal;" v-if="UserName != 'hqcuong'">

              <template slot-scope="scope" >
                <span :style=changecolor(scope.row.LoaiYeuCauId)> {{ scope.row.NguoiTaoId ? scope.row.User.FullName : ""}}</span>

              </template>
            </el-table-column>
            <el-table-column prop="NgayTao"
                             label="Ngày tạo"
                             width="140"
                             align="center"
                             sortable>
              <template slot-scope="scope"  >
                <span :style=changecolor(scope.row.LoaiYeuCauId)>
                  {{ formatDateTime(scope.row.NgayTao) }}
                </span>
                
              </template>
            </el-table-column>
            <el-table-column prop="ThoiHan"
                             label="Deadline"
                             width="120"
                             align="center"
                             sortable>
              <template slot-scope="scope"  >
                <span :style=changecolor(scope.row.LoaiYeuCauId)>{{ formatDate(scope.row.ThoiHan) }}</span>
                
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
            <el-table-column prop="NgayXuLy" label="Ngày xử lý" width="140" v-if="isXacNhan">

              <template slot-scope="scope">
                {{ formatDateTime(scope.row.NgayXuLy) }}
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
                <el-button @click="handleEdit(scope.$index, scope.row)"
                           type="primary"
                           :title="allowEdit ? 'Cập nhật' : 'Xem chi tiết'"
                           :icon="allowEdit ? 'el-icon-edit' : 'el-icon-view'"
                           size="mini"
                           v-if=" (RoleId==1 || (RoleId==2 && scope.row.StateId != 6 && UserUnitId == 1)|| (RoleId==3 && scope.row.NguoiTaoId == UserId && scope.row.StateId != 6) || (scope.row.DichVuId == AdminDVId && scope.row.StateId != 6)|| scope.row.StateId == 10 || scope.row.NhanSuId == NhanSuId && scope.row.StateId != 6 )&& UserUnitId != 2"></el-button>
                <el-button @click="handleDelete(scope.row)"
                           type="danger"
                           icon="el-icon-delete"
                           title="Xóa"
                           size="mini"
                           v-if="(scope.row.StateId==10 || RoleId==1 || scope.row.NguoiTaoId == UserId && scope.row.StateId != 6) && UserUnitId != 2"></el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="pt-2 pl-0"
                         :page-size="pagination"
                         background
                         style="width: 100%"
                         @size-change="handleSizeChange"
                         :current-page.sync="activePage"
                         :page-sizes="[10, 20, 50, 100, 500, 1000, 1500, 2000, 3000]"
                         layout="total,sizes,prev, pager, next"
                         :total="total">
          </el-pagination>
        </div>
      </div>
    </div>

    <!--    ///////////////////   -->
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
        <el-form-item label="Tiêu đề yêu cầu: " prop="TenYeuCau">
          <el-input v-model="formData.TenYeuCau"
                    type="text"
                    size="small"></el-input>
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item label="Ngày yêu cầu: " prop="NgayYeuCau">
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
            <el-form-item label="Deadline: " prop="ThoiHan">
              <el-date-picker v-model="formData.ThoiHan"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd"
                              :picker-options="pickerOptions">
              </el-date-picker>
            </el-form-item>
          </el-col>

        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Dịch vụ" prop="DichVuId">
              <el-select v-model="formData.DichVuId"
                         placeholder="Chọn dịch vụ: "
                         @change="changeDichVu(formData.DichVuId);"
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
                         placeholder="Chọn đơn vị: "
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
        <el-row>
          <el-col :span="12">
            <el-form-item label="Trạng thái: " prop="StateId">
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
            
            <el-form-item label="Nhân sự: " prop="NhanSuId">
              <el-select v-model="formData.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100">
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
            <el-form-item label="Người giám sát: " prop="NguoiGiamSatId">
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
            <el-form-item prop="ThoiHanMongMuon" label="Thời hạn KH: ">
              <el-date-picker v-model="formData.ThoiHanMongMuon"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd"
                              :picker-options="pickerOptions">
              </el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
          <ckeditor :editor="editor"
                    v-model="formData.NoiDung"
                    :config="editorConfig"
                    :disabled="!allowEdit"></ckeditor>
        </el-form-item>

        <el-form-item label="Jira: " prop="JiraDaGui">
          <el-input v-model="formData.JiraDaGui"
                    type="text"
                    size="small"></el-input>

          <!--<el-button type="primary" size="small" @click="handleAddJira">Thêm</el-button>-->
          <!--<el-button size="small" @click="updateData">Xóa</el-button>-->
        </el-form-item>
        <el-form-item label="Kết quả xử lý:" prop="NoiDungXuLy" style="margin-right:10px">
          <ckeditor :editor="editor"
                    v-model="formData.NoiDungXuLy"
                    :config="editorConfig"
                    :disabled="isAccept"></ckeditor>

        </el-form-item>
        <el-form-item label="Tập tin đính kèm: ">
          <el-upload action="/api/TapTin/UploadDoc"
                     :limit="3"
                     :multiple="true"
                     :on-preview="handlePreview"
                     :on-remove="handleRemove"
                     :file-list="fileList"
                     :on-success="handleSuccess"
                     :before-upload="beforeUpload"
                     accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm,image/jpeg,image/gif,image/png,.zip,.rar,.p7b"
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
            <el-tab-pane label="Yêu cầu comment: ">
              <div style="overflow-y:scroll; max-height:200px">
                <el-form-item v-for="item in YeuCauComment">
                  <!--{{item.Comments + ' - ' + item.NgayComment}}-->
                  <label>{{item.User.FullName +' : '}}</label> <span v-html="item.Comments + ' ' + formatDateTime(item.NgayComment)"></span>
                </el-form-item>
              </div>

              <el-form-item prop="CommentYeuCau">
                <ckeditor :editor="editor"
                          v-model="formData1.CommentYeuCau"
                          :config="editorConfig"></ckeditor>
                <el-button @click="handleAddComment">Save Comment</el-button>
              </el-form-item>


            </el-tab-pane>
            <el-tab-pane label="Jira comment">
              <div style="overflow-y:scroll; height:300px">
                <ul style="list-style-type:none; margin-left:-30px; margin-bottom:10px" v-for="item in Comment">
                  <li>
                    <b>+</b> {{item.body + ' - ' + item.author.displayName + ' - ' + formatDateTime(item.updated)}}
                  </li>
                </ul>
              </div>
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
    <!--     \\\\\\\\\\ Add PBH  \\\\\\\\\     -->

    <el-dialog title="Quản lý yêu cầu"
               :visible.sync="dialogFormBHDisplay"
               top="55px"
               width="80%"
               center>
      <el-form :model="formData2"
               :rules="formRules"
               ref="formData2"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">


        <el-form-item label="Dịch vụ: " prop="DichVuId">
          <el-select v-model="formData2.DichVuId"
                     placeholder="Chọn dịch vụ"
                     @change="setNguoiThucHien(formData2.DichVuId)"
                     class="w-100" filterable>
            <el-option v-for="item in ListDMDichVu"
                       :key="item.Id"
                       :label="item.TenDichVu"
                       :value="item.Id">
            </el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="Tiêu đề yêu cầu: " prop="TenYeuCau">
          <el-input v-model="formData2.TenYeuCau"
                    type="text"
                    size="small"></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Loại yêu cầu : " prop="NgayYeuCau">
              <el-select v-model="formData2.LoaiYeuCauId"
                         placeholder="Chọn loại yêu cầu"
                         class="w-100" :disabled="isLoaiYC">
                <el-option v-for="item in ListLoaiYC"
                           :key="item.Id"
                           :label="item.TenLoaiYeuCau"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân sự thực hiện: " prop="NhanSuId">
              <el-select v-model="formData2.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100" >
                <el-option v-for="item in ListNhansuQLDV"
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
            <el-form-item label="Ngày KH yêu cầu: " prop="NgayYeuCau">

              <el-date-picker v-model="formData2.NgayYeuCau"
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
            <el-form-item label="Hạn xử lý: " prop="ThoiHan">
              <el-date-picker v-model="formData2.ThoiHan"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd"
                              :picker-options="pickerOptions">
              </el-date-picker>
            </el-form-item>
          </el-col>

        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Mã số thuế: " prop="MaSoThue">
              <el-input v-model="formData2.MaSoThue" type="text"
                        size="small"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
          <ckeditor :editor="editor"
                    v-model="formData2.NoiDung"
                    :config="editorConfig"
                    :disabled="!allowEdit"></ckeditor>
        </el-form-item>
        <el-form-item label="Jira: " prop="JiraDaGui">
          <el-input v-model="formData2.JiraDaGui"
                    type="text"
                    size="small"></el-input>

        </el-form-item>

        <el-form-item label="Tập tin đính kèm: ">
          <el-upload action="/api/TapTin/UploadDoc"
                     :limit="3"
                     :multiple="true"
                     :on-preview="handlePreview"
                     :on-remove="handleRemove"
                     :file-list="fileList"
                     :on-success="handleSuccess"
                     :before-upload="beforeUpload"
                     accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm,image/jpeg,image/gif,image/png,.zip,.rar,.p7b"
                     :auto-upload="true"
                     size="mini">
            <el-button size="small" type="primary">Tải lên</el-button>
          </el-upload>
        </el-form-item>

      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormBH" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="addDataBH" >Cập nhật</el-button>
        <el-button v-if="isViewBT" @click="AcceptYeuCau1" size="small" type="success">Tiếp nhận</el-button>

        <!--<el-button type="success" size="small" @click="addForward" v-if="!isChuyenYc">Cập nhật và chuyển</el-button>
      <el-button type="success" size="small" @click="forwardYC" v-if="isChuyenYc">Chuyển yêu cầu</el-button>-->


      </span>
    </el-dialog>


    <!--     \\\\\\\\\\ Edit PBH  \\\\\\\\\     -->

    <el-dialog title="Quản lý yêu cầu"
               :visible.sync="dialogFormBHDisplayEdit"
               top="55px"
               width="80%"
               center>
      <el-form :model="formData5"
               :rules="formRules"
               ref="formData5"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">


        <el-form-item label="Dịch vụ: " prop="DichVuId">
          <el-select v-model="formData5.DichVuId"
                     placeholder="Chọn dịch vụ"
                     @change="setNguoiThucHien(formData5.DichVuId)"
                     class="w-100" filterable>
            <el-option v-for="item in ListDMDichVu"
                       :key="item.Id"
                       :label="item.TenDichVu"
                       :value="item.Id">
            </el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="Tiêu đề yêu cầu: " prop="TenYeuCau">
          <el-input v-model="formData5.TenYeuCau"
                    type="text"
                    size="small"></el-input>
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Loại yêu cầu : " prop="NgayYeuCau">
              <el-select v-model="formData5.LoaiYeuCauId"
                         placeholder="Chọn loại yêu cầu"
                         class="w-100" :disabled="isLoaiYC">
                <el-option v-for="item in ListLoaiYC"
                           :key="item.Id"
                           :label="item.TenLoaiYeuCau"
                           :value="item.Id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Nhân sự thực hiện: " prop="NhanSuId">
              <el-select v-model="formData5.NhanSuId"
                         placeholder="Chọn nhân sự"
                         class="w-100" :disabled="isChangeNhanSu">
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
            <el-form-item label="Ngày KH yêu cầu: " prop="NgayYeuCau">

              <el-date-picker v-model="formData5.NgayYeuCau"
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
            <el-form-item label="Hạn xử lý: " prop="ThoiHan">
              <el-date-picker v-model="formData5.ThoiHan"
                              type="date"
                              placeholder="Chọn ngày"
                              format="dd/MM/yyyy"
                              size="small"
                              style="width: 100%"
                              value-format="yyyy-MM-dd"
                              :picker-options="pickerOptions">
              </el-date-picker>
            </el-form-item>
          </el-col>

        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="Mã số thuế: " prop="MaSoThue">
              <el-input v-model="formData5.MaSoThue" type="text"
                        size="small"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
          <ckeditor :editor="editor"
                    v-model="formData5.NoiDung"
                    :config="editorConfig"
                    :disabled="!allowEdit"></ckeditor>
        </el-form-item>
        <el-form-item label="Jira: " prop="JiraDaGui">
          <el-input v-model="formData5.JiraDaGui"
                    type="text"
                    size="small"></el-input>

        </el-form-item>

        <el-form-item label="Tập tin đính kèm: ">
          <el-upload action="/api/TapTin/UploadDoc"
                     :limit="3"
                     :multiple="true"
                     :on-preview="handlePreview"
                     :on-remove="handleRemove"
                     :file-list="fileList"
                     :on-success="handleSuccess"
                     :before-upload="beforeUpload"
                     accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm,image/jpeg,image/gif,image/png,.zip,.rar,.p7b"
                     :auto-upload="true"
                     size="mini">
            <el-button size="small" type="primary">Tải lên</el-button>
          </el-upload>
        </el-form-item>

      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormBH" size="small">Bỏ qua</el-button>
        <el-button type="primary" size="small" @click="updateDataBHNT">Cập nhật</el-button>
        <el-button v-if="isViewBT" @click="AcceptYeuCau2" size="small" type="success">Tiếp nhận</el-button>

        <!--<el-button type="success" size="small" @click="addForward" v-if="!isChuyenYc">Cập nhật và chuyển</el-button>
      <el-button type="success" size="small" @click="forwardYC" v-if="isChuyenYc">Chuyển yêu cầu</el-button>-->


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
          <el-row>
            <el-col :span="5">
              <el-form-item label="Yêu cầu Id:" prop="Id">

                YC{{formData3.Id}}
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="Dịch vụ:" prop="DichVuId">

                {{formData3.DichVuId ? formData3.DichVu.TenDichVu : ""}}
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="Loại dịch vụ:" prop="LoaiYeuCauId">

                {{formData3.LoaiYeuCauId ? formData3.LoaiYeuCau.TenLoaiYeuCau : ""}}
              </el-form-item>
            </el-col>
            <el-col :span="7">
              <el-form-item label="Nhân sự thực hiện: " prop="NhanSuId">

                {{formData3.NhanSuId ? formData3.NhanSu.TenNhanSu : ""}}
              </el-form-item>
            </el-col>

          </el-row>


          <el-form-item label="Tiêu đề yêu cầu: " prop="TenYeuCau">

            {{formData3.TenYeuCau}}
          </el-form-item>
          <el-row>


          </el-row>
          <el-row>
            <el-col :span="7">
              <el-form-item label="Người tạo: " prop="NguoiTaoId">

                {{formData3.NguoiTaoId ? formData3.User.FullName : ""}}
              </el-form-item>

            </el-col>
            <el-col :span="6">
              <el-form-item label="Ngày KH yêu cầu: " prop="NgayYeuCau">

                {{formatDate(formData3.NgayYeuCau)}}
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <el-form-item label="Hạn xử lý: " prop="ThoiHan">

                {{formatDate(formData3.ThoiHan)}}
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <el-form-item label="Trạng thái: " prop="StateId">

                {{formData3.StateId ? formData3.States.StateName : ""}}
              </el-form-item>
            </el-col>

          </el-row>
          <el-row>
            <el-col :span="6">
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

            <el-tabs type="border-card" style="width:90%">
              <el-tab-pane label="Yêu Cầu Comment">
                <div style="overflow-y:scroll; max-height:200px;">
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
                            :config="editorConfig" v-if="isComment"></ckeditor>
                  <el-button @click="handleAddComment" v-if="isComment">Save Comment</el-button>
                </el-form-item>


              </el-tab-pane>
              <el-tab-pane label="Jira Comment">
                <div style="overflow-y:scroll; height:300px">
                  <ul style="list-style-type:none; margin-left:-30px; margin-bottom:10px" v-for="item in Comment">
                    <li>
                      <b>+</b> {{item.body + ' - ' + item.author.displayName + ' - ' + formatDateTime(item.updated)}}
                    </li>
                  </ul>
                </div>
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



    <!--         Xử Lý Yêu Cầu        -->
    <el-dialog title="Xử lý yêu cầu"
               :visible.sync="dialogFormBHHandle"
               top="55px"
               width="75%"
               center>
      <el-form :model="formData4"
               :rules="formRules"
               ref="formData4"
               label-width="140px"
               class="m-auto"
               size="small"
               :disabled="!allowEdit">
        <div class="card" style="box-shadow: 0 0 25px #DCDCDC	; border-radius:0.5rem">
          <h4 style="margin-left:20px">Nội dung yêu cầu: </h4>
          <el-row>
            <el-col :span="12">
              <el-form-item label="Dịch vụ:" prop="DichVuId">

                {{formData4.DichVuId ? formData4.DichVu.TenDichVu : ""}}
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Yêu cầu Id:" prop="Id">

                YC{{formData4.Id}}
              </el-form-item>
            </el-col>
          </el-row>

          <el-form-item label="Tiêu đề yêu cầu: " prop="TenYeuCau">

            {{formData4.TenYeuCau}}
          </el-form-item>
          <el-row>
            <el-col :span="12">

              <el-form-item label="Nhân sự thực hiện: " prop="NhanSuId">
                <span v-if="!isAccept && !isChuyenNS">
                  {{formData4.NhanSuId ? formData4.NhanSu.TenNhanSu : ""}}
                </span>
                <span v-if="isAccept || isChuyenNS">
                  <el-select v-model="formData4.NhanSuId"
                             placeholder="Chọn nhân sự"
                             class="w-100">
                    <el-option v-for="item in ListNhansuQLDV"
                               :key="item.Id"
                               :label="item.TenNhanSu"
                               :value="item.Id">
                    </el-option>
                  </el-select>
                </span>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Người tạo: " prop="NguoiTaoId">

                {{formData4.NguoiTaoId ? formData4.User.FullName : ""}}
              </el-form-item>
            </el-col>

          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="Ngày KH yêu cầu: " prop="NgayYeuCau">

                {{formatDate(formData4.NgayYeuCau)}}
              </el-form-item>
            </el-col>

            <el-col :span="12">
              <el-form-item label="Hạn xử lý: " prop="ThoiHan">

                {{formatDate(formData4.ThoiHan)}}
              </el-form-item>
            </el-col>

          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="Trạng thái: " prop="StateId">

                {{formData4.StateId ? formData4.States.StateName : ""}}
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Mã Số Thuế: " prop="MaSoThue">

                {{formData4.MaSoThue}}
              </el-form-item>
            </el-col>
          </el-row>
          <el-form-item label="Nội dung yêu cầu: " prop="NoiDung">
            <span v-html="formData4.NoiDung"></span>
            <!--{{formData4.NoiDung}}-->
          </el-form-item>
          <el-form-item label="Jira: " prop="JiraDaGui">

            {{formData4.JiraDaGui}}
          </el-form-item>

          <el-form-item label="Tập tin đính kèm: ">

            <div v-for="item in fileList">
              <a :href=item.url download>{{item.name}}</a>
            </div>
          </el-form-item>
        </div>
        <div class="card" style="box-shadow: 0 0 25px #DCDCDC	; border-radius:0.5rem">
          <h4 style="margin-left:20px">Nội dung xử lý: </h4>
          <el-form-item label="Kết quả xử lý:" prop="NoiDungXuLy" style="margin-right:10px">
            <ckeditor :editor="editor"
                      v-model="formData4.NoiDungXuLy"
                      :config="editorConfig"
                      :disabled="isAccept"></ckeditor>

          </el-form-item>
          <el-form-item label="Tập tin xử lý:">
            <el-upload action="/api/TapTin/UploadDoc"
                       :limit="4"
                       :multiple="true"
                       :on-preview="handlePreview"
                       :on-remove="handleRemove1"
                       :file-list="fileList1"
                       :on-success="handleSuccess1"
                       :before-upload="beforeUpload"
                       accept=".pdf,.doc,.docx,.xls,.xlsx,.xlsm,image/jpeg,image/gif,image/png,.zip,.rar,.p7b"
                       :auto-upload="true"
                       size="mini">
              <el-button size="small" type="primary">Tải lên</el-button>
            </el-upload>

          </el-form-item>
        </div>



      </el-form>
      <span slot="footer" class="dialog-footer" v-if="allowEdit">
        <el-button @click="resetFormBHHandle" size="small">Bỏ qua</el-button>
        <el-button v-if="isAccept || isChuyenNS" @click="capnhatYeuCau" size="small" type="primary">Chuyển yêu cầu</el-button>
        <el-button v-if="!isAccept" @click="capnhatYeuCau" size="small" type="primary">Cập nhật</el-button>


        <el-button v-if="isAccept" @click="AcceptYeuCau" size="small" type="success">Tiếp nhận</el-button>
        <el-button type="success" size="small" @click="handleXacNhan" v-else>Hoàn thành</el-button>

      </span>
    </el-dialog>
    <el-dialog top="50px"
               title="Hoàn thành"
               :visible.sync="dialogXacNhan"
               width="30%">
      <div style="text-align: center">
        <h3>Bạn xác nhận hoàn thành?</h3>
        <div style="margin-top:20px">
          <el-button style="margin-right:5px" type="warning" size="small" @click="resetFormXacNhan">No</el-button>
          <el-button type="success" size="small" @click="xuLyYeuCau">Yes</el-button>
        </div>

      </div>

    </el-dialog>
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
  getUserUnitId,
  getYCUnitId,
  getCurrentNS,
  completeYeuCau,
  acceptYeuCau,
  forwardYeuCau,
  deniesYeuCau,
  addYeuCauNew,
  currenQL,
  NSQL,
  getNhanSuXuLy,
  NSQLTen,
} from "../../store/api";
export default {
  data() {
    return {
      title: null,
      dialogVisibleDoc: false,
      dialogFileUrl: null,
      FileDowloadName:null,
      dialogFormDisplay: false,
      dialogFormBHDisplay: false,
      dialogFormBHDisplayEdit:false,
      dialogFormBHEdit: false,
      dialogFormView: false,
      dialogFormBHHandle: false,
      dialogFormBHView: false,
      dialogXacNhan: false,
      loading: false,
      isEditor: false,
      isXacThuc: true,
      allowEdit: true,
      isPermiss: true,
      isView: false,
      isEdit : false,
      isChangeNhanSu:true,
      isComment:true,
      isChuyenYc: false,
      isButtonEdit: false,
      isButtonDelete:false,
      isButtonView:false,
      isViewBT:false,
      isTrangThai: true,
      isXacNhan:false,
      isLoaiYC:true,
      isSort: false,
      search: "",
      isAccept: false,
      isChuyenNS :false,
      StateIdFilter: 5,
      DichVuIdFilter: 1,
      JiraDaGuiLink:"",
      NhanSuXuLyId: 0,
      UserUnitId:0,
      YCUnit: 0,
      UserDV: 0,
      NhanSuId: 0,
      UserId: 0,
      AdminDVId:0,
      NhanSu:"",
      isCheckDV :false,
      isCheckNS :false,
      LoaiYeuCauIdFilter:5,
      pickerOptions:{
          disabledDate(time) {
            return time.getTime() <= (Date.now()- 3600 * 1000 * 24);
          }
      },
      title:"Tất cả yêu cầu",
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
        NoiDungXuLy: null,
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
      formData2: {// Form add PBH

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
        NgayYeuCau:null,
        StateId: null,
        MaSoThue: null,

      },
      formData3: { // Form xem PBH
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
        CommentYeuCau:null,
        StateId: null,
        MaSoThue: null,
        NgayXuLy:null,
        LoaiYeuCauId: null,
      },
      formData4: {// Form xử lý PBH
        TenYeuCau: null,
        DichVuId: null,
        NguoiTaoId: null,
        NhanSuId: null,
        NgayYeuCau: null,
        ThoiHan: null,
        NoiDung: null,
        JiraDaGui: null,
        FileUpload: null,
        NoiDungXuLy: null,
        StateId: null,
        MaSoThue:null,
        FileXuLy:null,
        NgayXuLy:null,
        LoaiYeuCauId: null,
      },
      formData5: {// Form edit PBH

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
        NgayYeuCau:null,
        StateId: null,
        MaSoThue: null,
        NgayTao :null,
        NguoiTaoId:null,
        UnitId:null,
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
        NhanSuId:[
          {
            required: true,
            message: "Vui lòng chọn nhân sự",
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
      UserName : getUser(),
      listData: [],
      YeuCauComment:[],
      ListDMTrangThai: [],
      ListDMNhanSu: [],
      ListDMDichVu: [],
      ListDMJira: [],
      ListDMTinhTrang: [],
      ListDMDonVi :[],
      ListJira: [],
      ListQLDV: [],
      TrangThai: [],
      Comment: [],
      fileList: [],
      fileDoc: [],
      fileList1: [],
      fileDoc1: [],
      fileImage:[],
      pagination: 10,
      total: 10,
      activePage: 1,
      dataFilter: null,
      stateOld : [],
      stateNew: [],
      UserProFile:[],
      QLDVList:[],
      ListNhansuQLDV:[],
      NhansuQLDV:[],
      ListLoaiYC :[],
      ListNhanSuXuLy:[],
      TenFilter:[],
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
        return moment(date).format("DD/MM/YYYY-hh:mm");
      } else return null;
    },
    formatTenTapTin(val) {
      if (val) {
        return val.substring(val.length - 18, val.length);
      } else return null;
    },
    formatMYC(val){
      if(val){
        var MYC = "YC" + val;
        return MYC;
      }else return null;
    },
    formatHtml(val){
      if(val){
        return val.replace(/<[^>]*>?/gm, '');

      }

    },
    Datetimenow(){
      this.DateTime = new  Date().toLocaleDateString();

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
           if (f2 == "Unit") {
            return result ? result.UnitName : "";
            }
            if (f2 == "NhanSu") {
              return result ? result.TenNhanSu : "";
            }
             if (f2 == "User") {
              return result ? result.FullName : "";
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
          if(f == "NgayTao"){
            return this.formatDateTime(result);
          }
          if(f == "NgayXuLy"){
            return this.formatDateTime(result);
          }
          if(f == "NoiDung"){
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
    setNguoiThucHien(val){
        this.ListNhansuQLDV=[];
        this.formData2.NhanSuId='';
          this.formData.NhanSuId='';
        var dv = this.ListDMDichVu.find(obj => obj.Id == val);


        var qldv = this.ListQLDV.find(obj => obj.DichVuId == dv.Id && obj.UnitId == this.UserUnitId);
        if(qldv){
          this.formData2.NhanSuId = qldv.NhanSuId;
          this.formData.NhanSuId  = qldv.NhanSuId;
        }
        if(val == 6){
          this.isLoaiYC = false;
        }
        else if(val != 6){
          this.isLoaiYC = true;
          this.formData2.LoaiYeuCauId = null;
          this.formData.LoaiYeuCauId = null;
        }
        for(var i=0; i<this.NhansuQLDV.length ; i++){
                  if(this.NhansuQLDV[i].DichVuId == val){
                    //console.log(this.NhansuQLDV[i]);
                    for(var k=0; k<this.ListDMNhanSu.length ; k++){
                      if(this.NhansuQLDV[i].NhanSuId == this.ListDMNhanSu[k].Id ){

                          this.ListNhansuQLDV.push(this.ListDMNhanSu[k]);
                      }
                    }

                  }

                }
    },
    changeDichVu(val) {
      //console.log(val)
      //this.ListNhansuQLDV=[];


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
    changeDichVuIdFilter(val) {

      this.getListData();
    },
    changeLoaiYeuCauFilter(){
      this.getListData();

    },
    resetField(){

      this.formData2.TenYeuCau = null;
      this.formData2.DichVuId = null;
      this.formData2.NhanSuId = null;
      this.formData2.ThoiHanMongMuon = null;
      this.formData2.ThoiHan = null;
      this.formData2.NgayYeuCau = null;
      this.formData2.JiraDaGui = null;
      this.formData2.FileUpload = null;
      this.formData2.MaSoThue = null;
      this.formData2.NguoiTaoId = null;
      this.formData2.LoaiYeuCauId = null;
      this.formData2.NoiDung = "";

    },
    handleXacNhan(){
      this.dialogXacNhan = true;

    },
    handleAdd() {

      this.resetField();
      this.isEditor = false;

      if (this.StateIdFilter || this.DichVuIdFilter) {

        if(this.UserUnitId ==1 ){
          if(this.UserDV == 6 || this.UserDV == 11){


            this.isChuyenYc = false;
            this.dialogFormBHDisplay = true;
          }
          else{
            if (this.$refs.formData !== undefined) {
            this.$refs.formData.resetFields();
            }
            this.formData.NoiDung ="";
            this.isChuyenYc = false;
            this.dialogFormDisplay = true;
          }

        }
        else if (this.UserUnitId ==2){

          this.isChuyenYc = false;
          this.isChangeNhanSu = false;

          this.dialogFormBHDisplay = true;

        }
        else if (this.UserUnitId !=2 && this.UserUnitId !=1) {

            this.isChuyenYc = false;
            this.dialogFormBHDisplay = true;
        }

        this.formData = {
          //IsActive = true
          StateId: this.StateIdFilter,
          DichVuId: this.DichVuIdFilter,
        };


        this.fileList = [];
        this.fileDoc = [];
        this.fileImage = [];
        this.ListJira = [];


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

       if(row.StateId == 6){
          this.isComment = false;

      }
          var checkdv = false;

          for( var i = 0; i < this.QLDVList.length; i++){
            if(this.QLDVList[i].DichVuId == row.DichVuId && (this.QLDVList[i].DichVuId ==6 || this.QLDVList[i].DichVuId ==11 )){
                checkdv = true;
            }
          }


      if(checkdv){
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
            this.fileList.push({ name: _arr[i]  , url: urlFile });
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
            this.fileList1.push({ name: _arr[i]  , url: urlFile });
            this.fileDoc1.push({ key: _arr[i], file: _arr[i] });
          }
        }

        this.isEditor = false;
        this.dialogFormBHView = true;
      }
      else{

          if(this.UserUnitId == 1){
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
          else{
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
            this.fileList1.push({ name: _arr[i]  , url: urlFile });
            this.fileDoc1.push({ key: _arr[i], file: _arr[i] });
          }
        }
						this.isEditor = false;
						this.dialogFormBHView = true;

          }



      }


    },
    handleEdit(index, row) {

      this.ListNhansuQLDV= [];
      this.isChuyenYc = false;
      if(this.UserUnitId == 1 ){
          var bool = false;

          if(row.StateId ==10 && this.AdminDVId == row.DichVuId){
            this.isAccept = true;
          }
          if(row.StateId ==10 && row.NguoiTaoId == this.UserId){
            this.isViewBT = true;
          }
          else if(row.StateId !=10){
            this.isAccept = false;
            this.isViewBT = false;

          }

          if(row.StateId !=10 && (row.NhanSuId == this.NhanSuId || row.DichVuId == this.AdminDVId) && row.DichVuId == 6){
             this.isChuyenNS = true;
          }
          else if(row.DichVuId == 6){
            this.isChuyenNS = true;
          }
          else{
            this.isChuyenNS = false;
          }

          var checkdv = false;

          for( var i = 0; i < this.QLDVList.length; i++){
            if(this.QLDVList[i].DichVuId == row.DichVuId && (this.QLDVList[i].DichVuId ==6 || this.QLDVList[i].DichVuId ==11 )){
                checkdv = true;
            }
          }


          if(row.StateId == 6){
            this.$message({
               type: "warning",
                message: "Không thể cập nhật!"
              });
          }
           else{
             if (checkdv == true && ((row.NguoiTaoId == this.UserId && row.StateId !=10 )||row.NguoiTaoId != this.UserId ) || (this.RoleId ==2 && (row.DichVuId == 6))){
                if (this.$refs.formData4 !== undefined) {
                this.$refs.formData4.resetFields();

                }
                for(var i=0; i<this.NhansuQLDV.length ; i++){
                  if(this.NhansuQLDV[i].DichVuId == row.DichVuId){
                    //console.log(this.NhansuQLDV[i]);
                    for(var k=0; k<this.ListDMNhanSu.length ; k++){
                      if(this.NhansuQLDV[i].NhanSuId == this.ListDMNhanSu[k].Id ){

                          this.ListNhansuQLDV.push(this.ListDMNhanSu[k]);
                      }
                    }

                  }

                }


                this.formData4 = Object.assign({}, row);
                this.stateOld = this.formData4.StateId;


                if(this.formData4.DichVuId != null){
                    var dv = this.ListDMDichVu.find(obj => obj.Id == this.formData4.DichVuId);
                    this.ListDMDonVi = dv.DonVi;
                }

                if (this.formData4.FileUpload) {
                  this.fileList = [];
                  this.fileDoc = [];
                  var _arr = JSON.parse(this.formData4.FileUpload);
                  for (var i = 0; i < _arr.length; i++) {
                    var urlFile = "/uploads/" + _arr[i];
                    this.fileList.push({ name: _arr[i], url: urlFile });
                    this.fileDoc.push({ key: _arr[i], file: _arr[i] });
                  }
                }

                 if (this.formData4.FileXuLy) {
                  this.fileList1 = [];
                  this.fileDoc1 = [];
                  var _arr = JSON.parse(this.formData4.FileXuLy);
                  for (var i = 0; i < _arr.length; i++) {
                    var urlFile = "/uploads/" + _arr[i];
                    this.fileList1.push({ name: _arr[i], url: urlFile });
                    this.fileDoc1.push({ key: _arr[i], file: _arr[i] });
                  }
                }
                if(row.FileXuLy == [] || row.FileXuLy == null){
                  this.fileList1 = [];
                }
                if(row.FileUpload == [] || row.FileUpload == null){
                  this.fileList = [];
                }
                if(row.NoiDungXuLy == null){
                  this.formData4.NoiDungXuLy = "";
                }
                this.isEditor = true;
                this.dialogFormBHHandle = true;
            }
            else if(checkdv == true && row.NguoiTaoId == this.UserId && row.StateId == 10){
                if (this.$refs.formData5 !== undefined) {
                  this.$refs.formData5.resetFields();

                  }

                  this.formData5.TenYeuCau = row.TenYeuCau;
                  this.formData5.DichVuId = row.DichVuId;
                  this.formData5.NhanSuId =  row.NhanSuId;
                  this.formData5.ThoiHanMongMuon = row.ThoiHanMongMuon;
                  this.formData5.ThoiHan = row.ThoiHan;
                  this.formData5.JiraDaGui = row.JiraDaGui;
                  this.formData5.FileUpload = row.FileUpload;
                  this.formData5.MaSoThue = row.MaSoThue;
                  this.formData5.NguoiTaoId = row.NguoiTaoId;
                  this.formData5.LoaiYeuCauId = row.LoaiYeuCauId;
                  this.formData5.NoiDung = row.NoiDung;
                  this.formData5.NgayYeuCau = row.NgayYeuCau;
                  this.formData5.NgayTao = row.NgayTao;
                  this.formData5.NguoiTaoId = row.NguoiTaoId;
                  this.formData5.UnitId = row.UnitId;
                  this.formData5.Id = row.Id;
                  this.formData5.StateId = row.StateId;

                  this.stateOld = this.formData5.StateId;


                  if(this.formData5.DichVuId != null){
                      var dv = this.ListDMDichVu.find(obj => obj.Id == this.formData5.DichVuId);
                      this.ListDMDonVi = dv.DonVi;
                  }

                  if (this.formData5.FileUpload) {
                    this.fileList = [];
                    this.fileDoc = [];
                    var _arr = JSON.parse(this.formData5.FileUpload);
                    for (var i = 0; i < _arr.length; i++) {
                      var urlFile = "/uploads/" + _arr[i];
                      this.fileList.push({ name: _arr[i], url: urlFile });
                      this.fileDoc.push({ key: _arr[i], file: _arr[i] });
                    }
                  }

                if(row.FileUpload == [] || row.FileUpload == null){
                  this.fileList = [];
                }
                  this.isLoaiYC= false;
                  this.isEditor = false;
                  this.dialogFormBHDisplayEdit = true;
            }
            else{

              if(this.RoleId ==1 && row.UnitId !=1 ){
                    this.isEdit = true;
              }

              if (this.$refs.formData !== undefined) {
                this.$refs.formData.resetFields();

              }
              this.formData = Object.assign({}, row);
              this.stateOld = this.formData.StateId;


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
            }


            getYCUnitId(this.formData.Id).then(data => {
              this.YCUnit = data.UnitId;
            });


          }



      }
      else if(this.UserUnitId == 2){
            this.$message({
               type: "warning",
                message: "Không thể cập nhật!"
            });
      }
      else if(this.UserUnitId != 1){
        if(row.StateId != 6){

          //this.formData2 = Object.assign({}, row);

          this.formData5.TenYeuCau = row.TenYeuCau;
          this.formData5.DichVuId = row.DichVuId;
          this.formData5.NhanSuId =  row.NhanSuId;
          this.formData5.ThoiHanMongMuon = row.ThoiHanMongMuon;
          this.formData5.ThoiHan = row.ThoiHan;
          this.formData5.JiraDaGui = row.JiraDaGui;
          this.formData5.FileUpload = row.FileUpload;
          this.formData5.MaSoThue = row.MaSoThue;
          this.formData5.NguoiTaoId = row.NguoiTaoId;
          this.formData5.LoaiYeuCauId = row.LoaiYeuCauId;
          this.formData5.NoiDung = row.NoiDung;
          this.formData5.NgayYeuCau = row.NgayYeuCau;
          this.formData5.NgayTao = row.NgayTao;
          this.formData5.NguoiTaoId = row.NguoiTaoId;
          this.formData5.UnitId = row.UnitId;
          this.formData5.Id = row.Id;
           this.formData5.StateId = row.StateId;
          if(this.RoleId != 1 && row.StateId == 10){
            this.isChuyenYc = true;

          }


          if (this.formData5.FileUpload) {
            this.fileList = [];
            this.fileDoc = [];
            var _arr = JSON.parse(this.formData5.FileUpload);
            for (var i = 0; i < _arr.length; i++) {
              var urlFile = "/uploads/" + _arr[i];
              this.fileList.push({ name: _arr[i], url: urlFile });
              this.fileDoc.push({ key: _arr[i], file: _arr[i] });
            }
          }

                if(row.FileUpload == [] || row.FileUpload == null){
                  this.fileList = [];
                }
          this.isLoaiYC= false;
          this.isEditor = true;
          this.dialogFormBHDisplayEdit = true;
        }
        else{
              this.$message({
               type: "warning",
                message: "Yêu cầu đã hoàn thành, không thể cập nhật!"
              });
        }

      }

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
      const isXlsx = file.type ==="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      const isXlsm = file.type === "application/vnd.ms-excel.sheet.macroEnabled.12";
      const isjpg = file.type == "image/jpeg";
      const ispng = file.type == "image/png";
      const israr = file.type == "";

      const iszip = file.type == "application/x-zip-compressed";
      const isp7b = file.type =="application/x-pkcs7-certificates";
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
          "Ngày xư lý",
          "Task Jira",
          "Trạng thái",
          "Nhân sự",
          "Người tạo",
          "Dịch vụ",
          "Đơn vị",
          "Mã số thuế",
          "Thời hạn KH mong muốn"
        ];
        const filterVal = [
          "Id",
          "TenYeuCau",
          "NoiDung",
          "NgayTao",
          "ThoiHan",
          "NgayXuLy",
          "JiraDaGui",
          "States.StateName",
          "NhanSu.TenNhanSu",
          "User.FullName",
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
    resetFormBHView(){
      this.dialogFormBHView= false;
      return true;
    },
    resetFormBHHandle(){

      this.dialogFormBHHandle= false;
      return true;
    },
    resetFormBH(){

      this.dialogFormBHDisplay= false;
      this.dialogFormBHDisplayEdit= false;

      return true;

    },
    resetFormXacNhan(){
      this.dialogXacNhan = false;
    },

    getListData() {
      this.loading = true;



        if(this.AdminDVId != 0){
          this.isSort = true;
        }
        else{
          this.isSort = false;
        }

        currenQL().then(data =>{
          if(data != null && data != ""){

            this.QLDVList = data

          }
        });
        NSQL().then(data=> {
          if(data){
           this.NhansuQLDV = data;

          }
        });

        NSQLTen().then(data=>{
          if(data){
           this.TenFilter = data;

          }
        });

        //Quản lý
        this.loading = true;
      if (this.StateIdFilter != 5 || this.DichVuIdFilter != 1 || this.LoaiYeuCauIdFilter != 5) {
        selectYeuCau(this.StateIdFilter, this.DichVuIdFilter, this.LoaiYeuCauIdFilter).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
      else {
        selectYeuCauAll(this.StateIdFilter , this.DichVuIdFilter).then(data => {
          this.listData = data;
          this.total = data.length;

          this.loading = false;
        });
      }
        getCurrentNS().then(data => {
              if(data != null && data != ""){
                this.UserDV = data.DichVuId;
                this.UserId = data.UserId;
                this.NhanSuId = data.NhanSuId;
                this.NhanSu = data.TenNhanSu;
                this.AdminDVId = data.AdminDichVuId;
              }

           });

      this.getDataTrangThai();
       getUserUnitId().then(data => {
          this.UserUnitId = data.UnitId;

       });
      

    },
    getNhanSu(){
        getCurrentNS().then(data => {
              if(data != null && data != ""){
                this.UserDV = data.DichVuId;
                this.UserId = data.UserId;
                this.NhanSuId = data.NhanSuId;
                this.NhanSu = data.TenNhanSu;
                this.AdminDVId = data.AdminDichVuId;
              }

           });
    },

    // get trạng thái
    getDataTrangThai() {
      this.loading = true;

        getTrangThai(this.JiraDaGuiLink).then(data => {
          if (data != null && data.errorMessages == null && data.fields) {
            this.TrangThai = data;
            this.Comment = data.fields.comment.comments;
            if(this.UserUnitId ==1){

              this.formData1.KeyJira = data.key;
              this.formData1.StatusJira = data.fields.status.name;

              this.formData1.Priority = data.fields.priority.name;
            }
            else{
              this.formData3.KeyJira = data.key;
              this.formData3.StatusJira = data.fields.status.name;

              this.formData3.Priority = data.fields.priority.name;
            }


          }


          this.loading = false;
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

    },
    updateData() {
      this.$refs.formData.validate(valid => {
        if (valid) {
          this.formData.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file)
          );
          if (this.isEditor == 0) {
            addYeuCau(this.formData).then(data => {
              if(data != null && data != ""){
                  this.$message({
                  type: "success",
                  message: "Cập nhật thành công!"
                  });


                   sendTeleAsync(this.formData.Id);

                  this.getListData();
              }
              else{
                 this.$message({
                type: "warning",
                message: "Không thể cập nhật!"
              });
             }
            });
          } else {
            //delete this.formData.LinhVuc;
            updateYeuCau(this.formData).then(data => {

              if(data != null && data != ""){
                  this.$message({
                  type: "success",
                  message: "Cập nhật thành công!"
                  });
                  this.stateNew = this.formData.StateId;

                   sendTeleAsync(this.formData.Id);

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
    AcceptYeuCau(){

          this.$refs.formData4.validate(valid => {
            if (valid) {


                  acceptYeuCau(this.formData4.Id).then(data => {
                      if(data != null && data != ""){
                          this.$message({
                            type: "success",
                            message: "Cập nhật thành công!"
                          });
                         sendTeleAsync(this.formData4.Id);
                      }
                      else{
                          this.$message({
                            type: "warning",
                            message: "Không thể cập nhật!"
                           });

                      }
                  this.dialogFormBHHandle= false;
                  this.getListData();
                  });



            } else {
              return false;
            }
          });




    },
    AcceptYeuCau1(){
      this.$refs.formData2.validate(valid => {
          if (valid) {


                acceptYeuCau(this.formData2.Id).then(data => {
                    if(data != null && data != ""){
                        this.$message({
                          type: "success",
                          message: "Cập nhật thành công!"
                        });
                        sendTeleAsync(this.formData2.Id);
                    }
                    else{
                        this.$message({
                          type: "warning",
                          message: "Không thể cập nhật!"
                         });

                    }
                this.dialogFormBHDisplay= false;
                this.getListData();
                });



          } else {
            return false;
          }
        });
    },
  AcceptYeuCau2(){
      this.$refs.formData5.validate(valid => {
          if (valid) {


                acceptYeuCau(this.formData5.Id).then(data => {
                    if(data != null && data != ""){
                        this.$message({
                          type: "success",
                          message: "Cập nhật thành công!"
                        });
                        sendTeleAsync(this.formData5.Id);
                    }
                    else{
                        this.$message({
                          type: "warning",
                          message: "Không thể cập nhật!"
                         });

                    }
                this.dialogFormBHDisplayEdit= false;
                this.getListData();
                });



          } else {
            return false;
          }
        });
    },
    capnhatYeuCau(){

      this.$refs.formData4.validate(valid => {
        if (valid) {

            this.formData4.FileUpload = JSON.stringify(this.fileDoc.map(ite => ite.file));
            this.formData4.FileXuLy = JSON.stringify(this.fileDoc1.map(ite => ite.file));
              updateYeuCau(this.formData4).then(data => {
                  if(data != null && data != ""){
                      this.$message({
                        type: "success",
                        message: "Cập nhật thành công!"
                      });
                    sendTeleAsync(this.formData4.Id);
                  }
                  else{
                      this.$message({
                        type: "warning",
                        message: "Không thể cập nhật!"
                       });

                  }
              this.dialogFormBHHandle= false;
              this.getListData();
              });



        } else {
          return false;
        }
      });
    },
    xuLyYeuCau(){
      this.$refs.formData4.validate(valid => {
        if (valid) {


            this.formData4.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file));
            this.formData4.FileXuLy = JSON.stringify(this.fileDoc1.map(ite => ite.file));
              completeYeuCau(this.formData4).then(data => {
                  if(data != null && data != ""){
                      this.$message({
                        type: "success",
                        message: "Cập nhật thành công!"
                      });
                    sendTeleAsync(this.formData4.Id);
                  }
                  else{
                      this.$message({
                        type: "warning",
                        message: "Không thể cập nhật!"
                       });

                  }
              this.dialogXacNhan = false;
              this.dialogFormBHHandle= false;
              this.getListData();
              });



        } else {
          return false;
        }
      });

    },
    addDataBH() {
      this.$refs.formData2.validate(valid => {
        if (valid) {
        console.log(this.formData2);

            this.formData2.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file));


            if (this.isEditor == 0 ){
              addYeuCau(this.formData2).then(data => {
                  if(data != null && data != ""){
                      this.$message({
                        type: "success",
                        message: "Thêm mới thành công!"
                      });
                    sendTeleAsync(data);

                  }
                  else{
                      this.$message({
                        type: "warning",
                        message: "Không thể thêm mới!"
                       });

                  }
              this.dialogFormBHDisplay= false;
              this.isLoaiYC = true;
              this.formData2.LoaiYeuCauId = null;
              this.getListData();
              });

            }




        } else {
          return false;
        }
      });
    },
    updateDataBHNT(){
      this.$refs.formData5.validate(valid => {
        if (valid) {

            this.formData5.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file));
            if(this.isEditor != 0){
              updateYeuCau(this.formData5).then(data => {
                  if(data != null && data != ""){
                      this.$message({
                        type: "success",
                        message: "Cập nhật thành công!"
                      });

                  }
                  else{
                      this.$message({
                        type: "warning",
                        message: "Không thể cập nhật!"
                       });

                  }
              this.dialogFormBHDisplayEdit= false;
              this.getListData();
              });

            }

        } else {
          return false;
        }
      });
    },
    updateDataBH(){
      this.$refs.formData3.validate(valid => {
        if (valid) {

            this.formData3.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file));
            if(this.isEditor == 0){
              updateYeuCau(this.formData3).then(data => {
                  if(data != null && data != ""){
                      this.$message({
                        type: "success",
                        message: "Cập nhật thành công!"
                      });
                      sendTeleAsync(this.formData3.Id);
                  }
                  else{
                      this.$message({
                        type: "warning",
                        message: "Không thể cập nhật!"
                       });

                  }
              this.dialogFormBHDisplay= false;
              this.getListData();
              });

            }

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
      if(this.formData1.CommentYeuCau != null && this.formData1.Id != null){
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
      }
      else if(this.formData3.CommentYeuCau != null && this.formData3.Id != null){
          addComments(this.formData3.CommentYeuCau, this.formData3.Id).then(data => {
          if(data != "" && data != null){
            this.$message({
                    type: "success",
                    message: "Thêm thành công!"
                    });
            this.getListComment(this.formData3.Id);
            this.formData3.CommentYeuCau = "";
          }
          else{

            this.$message({
                    type: "warning",
                    message: "Không thể thêm comment!"
                    });
           }
        });

      }

    },
    forwardYC(){

      forwardYeuCau(this.formData2.Id).then(data =>{
        if(data != "" && data != null){
              this.$message({
               type: "success",
                message: "Chuyển thành công!"
              });
              //console.log(this.formData2.Id);
              sendTeleAsync(this.formData2.Id);
              this.getListComment(this.formData2.Id);

            }
            else{
               this.$message({
               type: "warning",
                message: "Chuyển thất bại!"
              });
            }
         this.dialogFormBHDisplay= false;
         this.getListData();
      });


    },
    addForward(){

        this.$refs.formData2.validate(valid => {
        if (valid) {

            this.formData2.FileUpload = JSON.stringify(
            this.fileDoc.map(ite => ite.file));


            if (this.isEditor == 0){
              addYeuCauNew(this.formData2).then(data => {
                  if(data != null && data != ""){
                      this.$message({
                        type: "success",
                        message: "Thêm mới thành công!"
                      });
                  }
                  else{
                      this.$message({
                        type: "warning",
                        message: "Không thể thêm mới!"
                       });

                  }
              this.dialogFormBHDisplay= false;
              this.getListData();
              });

            }


        } else {
          return false;
        }
      });


    },

    deniesYC(){

      deniesYeuCau(this.formData4.Id).then(data =>{
        if(data != "" && data != null){
              this.$message({
               type: "success",
                message: "Đã từ chối!"
              });
              this.getListComment(this.formData4.Id);
              sendTeleAsync(this.formData4.Id);
            }
            else{
               this.$message({
               type: "warning",
                message: "Từ chối thất bại!"
              });
            }
         this.dialogFormBHHandle= false;
         this.getListData();

      });


    },
    filterHandler(value, row) {

        return row.NhanSu.TenNhanSu === value;
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
    changecolor(val) {
      if(val == 4){
        var text = "color:red;word-break: normal;";
        return text
      }
      else if(val == 2){
        var text = "color:	#CC6600;word-break: normal;";
        return text
      }
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
        var id= "YC" + post.Id;
        var mst = ""+post.MaSoThue+"";
        return post.TenYeuCau.toLowerCase().includes(this.search.toLowerCase()) || id.toLowerCase().includes(this.search.toLowerCase())||
                       mst.toLowerCase().includes(this.search.toLowerCase());

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

   if(this.$route.params.StateId == 10){
      this.StateIdFilter = parseInt(this.$route.params.StateId);
      this.isTrangThai = false;
      this.isXacNhan= false;
      this.title="Chưa tiếp nhận";
   }
  else if(this.$route.params.StateId == 9){
     this.StateIdFilter = parseInt(this.$route.params.StateId);
      this.isTrangThai = false;
      this.isXacNhan= false;

  }
  else if(this.$route.params.StateId == 7){
     this.StateIdFilter = parseInt(this.$route.params.StateId);
      this.isTrangThai = false;
      this.isXacNhan= false;
      this.title="Đang xử lý";
  }
   else if(this.$route.params.StateId == 6){
     this.StateIdFilter = parseInt(this.$route.params.StateId);
      this.isTrangThai = false;
      this.isXacNhan= true;
      this.title="Đã hoàn thành";
  }
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
    this.getNhanSu();
    this.Datetimenow();
  console.log(this.TenFilter);
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
    height:34px !important;
  }

  .el-form-item--small .el-form-item__content .image img {
    width: -webkit-fill-available;
    height: -webkit-fill-available;
  }
  .el-table{
    color: black !important;
  }
    .el-table thead {
      color: black !important;
    }
</style>
