<template>
  <el-container>
    <el-header>
      <el-row type="flex">
        <el-col :span="4" :offset="1">
          <div class="sub-title">日志类型</div>
          <el-select v-model="mailType" multiple placeholder="默认全选" size="mini">
            <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-col>
        <el-col :span="4" :offset="1">
          <div>
            <el-input size="mini" v-model="reciver" placeholder="收件人"></el-input>
          </div>
          <div>
            <el-input size="mini" v-model="sender" placeholder="发件人"></el-input>
          </div>
          <div>
            <el-input size="mini" v-model="ip" placeholder="IP"></el-input>
          </div>
        </el-col>
        <el-col :span="14" :offset="1">
          <div class="sub-title">发起时间范围</div>
          <input v-model="startTime" type="datetime-local">
          <span>至</span>
          <input v-model="endTime" type="datetime-local">
          <div>
            <el-button type="primary" @click="query" :loading="loading">查询</el-button>
          </div>
        </el-col>
      </el-row>
    </el-header>
    <el-main>
      <div class="html5buttons" v-if="total>0">
        <div class="dt-buttons btn-group">
          <el-button type="primary" @click="outputExcel">导出</el-button>
          <a ref="dlink" style="display: none;"></a>
        </div>
      </div>
      <el-table
        :data="logInfoEntities.slice((currentPage-1)*pagesize,currentPage*pagesize)"
        border
        size="mini"
        style="width: 100%"
        stripe
        max-height="600"
        @row-dblclick="handleRowDblClick"
      >
        <el-table-column type="index" width="50" align="center"></el-table-column>
        <el-table-column property="startTime" label="发起时间" align="center" sortable></el-table-column>
        <el-table-column property="endTime" label="结束时间" align="center"></el-table-column>
        <el-table-column property="mask" label="标识编号" align="center" sortable></el-table-column>
        <el-table-column property="ip" label="IP" align="center" sortable></el-table-column>
        <el-table-column property="reciver" label="收件人" align="center" sortable></el-table-column>
        <el-table-column property="sender" label="发件人" align="center" sortable></el-table-column>
        <el-table-column property="size" label="收/发件字节" align="center" sortable></el-table-column>
        <el-table-column property="logType" label="日志类型" align="center" sortable></el-table-column>
      </el-table>
      <div class="pagination" v-if="total>0">
        <el-pagination
          class="fy"
          layout="total, sizes, prev, pager, next"
          @current-change="current_change"
          @size-change="size_change"
          :total="total"
          :current-page.sync="currentPage"
          :page-size="pagesize"
        ></el-pagination>
      </div>
      <el-dialog title="详细信息" :visible.sync="dialogUpdateVisible" >
        <div style="min-height:300px" v-html="dialogContent"></div>
      </el-dialog>
    </el-main>
  </el-container>
</template>

<style>
.el-header {
  background-color: rgba(215, 252, 214, 0.986);
  color: #333;
  line-height: 50px;
  min-height: 180px;
  padding-top: 15px;
}

.el-main {
  background-color: #e9eef3;
  color: #333;
  min-height: 600px;
  height: 100%;
}

.el-select {
  width: 300px;
  height: 40px;
}
.el-select input {
  height: 40px;
}

.el-input {
  width: 200px;
  height: 30px;
}

.el-button {
  width: 100px;
}
.dt-buttons {
  width: 100%;
  background-color: white;
  padding: 10px;
}
.pagination {
  width: 100%;
  background-color: white;
  padding: 10px;
}
</style>

<script>
export default {
  data() {
    return {
      options: [
        {
          value: "smtpssl",
          label: "smtpssl"
        },
        {
          value: "smtpmsa",
          label: "smtpsma"
        },
        {
          value: "smtp",
          label: "smtp"
        },
        {
          value: "queue",
          label: "queue"
        },
        {
          value: "pop3ssl",
          label: "pop3ssl"
        },
        {
          value: "pop3",
          label: "pop3"
        },
        {
          value: "imap",
          label: "imap"
        },
        {
          value: "imapssl",
          label: "imapssl"
        }
      ],
      mailType: [],
      sender: "",
      reciver: "",
      ip: "",
      logInfoEntities: [],
      startTime: "",
      endTime: "",
      LogSchema: null,
      DetailSchema: null,
      mongoose: null,
      whereStr: {},
      loading: false,
      total: 0,
      pagesize: 20,
      currentPage: 1,
      dialogUpdateVisible: false,
      dialogContent: "",
      clickedRow: null
    };
  },
  methods: {
    outputExcel() {
      let uri = 'data:application/vnd.ms-excel;base64,';
      let template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel"' +
    'xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>'
    + '<x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets>'
    + '</x:ExcelWorkbook></xml><![endif]-->' +
    ' <style type="text/css">' +
    'table th td{' +
    'border-right: 1px solid #F00;' +
    'border - bottom: 1px solid #F00;' +
    'font - size: smaller;' +
    '}' +
    '</style>' +
    '</head><body ><table class="excelTable">{table}</table></body></html>';
    let data = [];
    let rawData = this.logInfoEntities;
      for (var i = 0; i < rawData.length; i++) {
                data.push({
                    No: i + 1,
                    StartTime:rawData[i].startTime,
                    EndTime:rawData[i].endTime,
                    Mask:rawData[i].mask,
                    IP:rawData[i].ip,
                    Reciver:rawData[i].reciver,
                    Sender:rawData[i].sender,
                    Size:rawData[i].size,
                    LogType:rawData[i].logType 
                });
            }
      let tableHtml = this.FormExcelContext(data,'');
       let ctx = { worksheet: "sheet1", table: tableHtml };
            let dlink = this.$refs.dlink;
            dlink.href = uri + window.btoa(unescape(encodeURIComponent(this.format(template, ctx))));
            dlink.download = 'MailLogInfo.xls';
            dlink.click();
    },
    query() {
      this.loading = true;
      this.logInfoEntities = [];
      this.currentPage = 1;
      this.total = 0;
      if (!this.startTime || !this.endTime)
        this.$message.warning("请选择正确的时间范围");
      let str = {};
      let start = new Date(
        this.startTime.split("-")[0],
        this.startTime.split("-")[1] - 1,
        this.startTime.split("-")[2].split("T")[0],
        this.startTime.split("T")[1].split(":")[0],
        this.startTime.split("T")[1].split(":")[1]
      );
      let end = new Date(
        this.endTime.split("-")[0],
        this.endTime.split("-")[1] - 1,
        this.endTime.split("-")[2].split("T")[0],
        this.endTime.split("T")[1].split(":")[0],
        this.endTime.split("T")[1].split(":")[1]
      );
      str.StartTime = {
        $gte: start,
        $lte: end
      };
      if (this.sender) str.SendAddress = new RegExp(this.sender);
      if (this.reciver) str.ReserveAddress = new RegExp(this.reciver);
      if (this.ip) str.IPAddress = new RegExp(this.ip);
      this.whereStr = str;
      if (this.mailType.length > 0) {
        this.mailType.forEach(element => {
          this.mongoQuery(element, this);
        });
      } else {
        this.options
          .map(m => m.value)
          .forEach(element => {
            this.mongoQuery(element, this);
          });
      }
    },
    FormExcelContext(tableData,caption) {
    if (tableData.length < 1 || (!Array.isArray(tableData))) return false;
    let tableHtml = "<tr><td>"+caption+"</td></tr><thead><tr>";
    let thead = Object.keys(tableData[0]);
    for (var i = 0; i < thead.length; i++) {
        tableHtml = tableHtml + '<th>' + thead[i] + '</th>';
    }
    tableHtml = tableHtml + '</tr></thead><tbody>';
    for (var i = 0; i < tableData.length; i++) {
        tableHtml = tableHtml + '<tr>';
        for (var j = 0; j < thead.length; j++) {
            tableHtml = tableHtml + '<td>' + tableData[i][thead[j]] + '</td>';
        }
        tableHtml = tableHtml + '</tr>';
    }
    tableHtml = tableHtml + '</tbody>';
    return tableHtml;
  },
    mongoQuery(col, vue) {
      var mongoose = this.mongoose;
      var LogSchema = this.LogSchema;
      mongoose.connect("mongodb://localhost/amslog", { useNewUrlParser: true });
      var db = mongoose.connection;
      db.on("error", console.error.bind(console, "connection error:"));
      db.once("open", function() {
        // we're connected!
        let Log = mongoose.model(col, LogSchema, col);
        Log.find(vue.whereStr, function(err, logs) {
          if (err) {
            vue.$message.error(err);
            vue.loading = false;
            vue.total = vue.logInfoEntities.length;
            return;
          }
          logs.forEach(element => {
            vue.logInfoEntities.push({
              startTime: element.StartTime.toMyString(),
              endTime: element.EndTime.toMyString(),
              mask: element.Mask,
              ip: element.IPAddress,
              reciver: element.ReserveAddress,
              sender: element.SendAddress,
              size: element.ReserveSize,
              logType: col,
              _id: element._id
            });
          });
          let check =
            vue.mailType.length == 0
              ? vue.options[7].value
              : vue.mailType[vue.mailType.length - 1];
          if (col == check) {
            vue.loading = false;
            vue.total = vue.logInfoEntities.length;
          }
        });
      });
    },
    current_change: function(currentPage) {
      this.currentPage = currentPage;
    },
    size_change: function(size) {
      this.pagesize = size;
    },
    handleRowDblClick(row) {
      this.queryDetail(row);
    },
    queryDetail(row) {
      let mongoose = this.mongoose;
      let DetailSchema = this.DetailSchema;
      let vue = this;
      mongoose.connect("mongodb://localhost/amslog", { useNewUrlParser: true });
      let db = mongoose.connection;
      db.on("error", console.error.bind(console, "connection error:"));
      db.once("open", () => {
        let Log = mongoose.model(row.logType+'_t', DetailSchema, row.logType);
        Log.find({ Mask: row.mask }, (err, logs) => {
          if (err) {
            vue.$message.error(err);
            return;
          }
          if (logs.length >= 1) {
            vue.dialogContent=logs[0].Details
            vue.dialogUpdateVisible=true
          } else {
            vue.$message.error("没有查找到对象！");
          }
        });
      });
    },
    format (s, c) {
    return s.replace(/{(\w+)}/g,
        function (m, p) {
            return c[p];
        })}
  },
  mounted() {
    let date = new Date();
    let y = date.getFullYear();
    let m = date.getMonth() + 1;
    let d = date.getDate();
    let h = date.getHours();
    let min = date.getMinutes();
    m = m < 10 ? "0" + m : m;
    d = d < 10 ? "0" + d : d;
    h = h < 10 ? "0" + h : h;
    min = min < 10 ? "0" + min : min;
    this.startTime = y + "-" + m + "-" + "01" + "T00:00";
    this.endTime = y + "-" + m + "-" + d + "T" + h + ":" + min;
    var mongoose = require("mongoose");
    this.mongoose = this.mongoose ? this.mongoose : mongoose;
    this.LogSchema = this.LogSchema
      ? this.LogSchema
      : new mongoose.Schema({
          StartTime: Date,
          EndTime: Date,
          Mask: Number,
          IPAddress: String,
          SendAddress: String,
          ReserveAddress: String,
          ReserveSize: Number
        });
    if (!this.DetailSchema) {
      this.DetailSchema = new mongoose.Schema({ Details: String });
    }
    if (!Date.prototype.toMyString) {
      Date.prototype.toMyString = function() {
        let y = this.getFullYear();
        let m = this.getMonth() - 1;
        m = m < 10 ? "0" + m : m;
        let d = this.getDate();
        d = d < 10 ? "0" + d : d;
        let h = this.getHours();
        h = h < 10 ? "0" + h : h;
        let mi = this.getMinutes();
        mi = mi < 10 ? "0" + mi : mi;
        let s = this.getSeconds();
        s = s < 10 ? "0" + s : s;
        return y + "/" + m + "/" + d + " " + h + ":" + mi + ":" + s;
      };
    }
  }
};
</script>

