using Abp.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.Equipments;
using P4.Equipments.Dtos;
using P4.Companys;
using P4.Employees;
using P4.Dictionarys;
using P4.Users;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;


namespace P4.EntityFramework.Repositories
{
    public class EquipmentRepository : P4RepositoryBase<Equipment, long>, IEquipmentRepository
    {

        public EquipmentRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 

        }

        public Equipments.Dtos.GetAllEquipmentOutput GetAllEquipmentOutputList(Equipments.Dtos.EquipmentInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new EquipmentDto
            {
                Id = c.Id,
                CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(dictype => dictype.Id == c.CompanyId).CompanyName,
                PDA = c.PDA,
                // Type = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode.Equals(c.Type) == true && dictype.TypeCode == P4Consts.Type).ValueData,
                Type = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.Type.ToString() && dictype.TypeCode == P4Consts.PDAType).ValueData, 
                Sim = c.Sim,
                SD = c.SD,
                Pasm = c.Pasm,
                Printers = c.Printers,
                IsActive=c.IsActive,
                IsUpgrade = c.IsUpgrade,
                Version = c.Version,
                Standard = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.Standard.ToString() && dictype.TypeCode == P4Consts.PDAStandard).ValueData,
                GPS = c.GPS,
                Scan = c.Scan,
                DeviceType = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.DeviceType.ToString() && dictype.TypeCode == P4Consts.PDADeviceType).ValueData,
                UseStatusInt = c.UseStatus,
                UseStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.UseStatus.ToString() && dictype.TypeCode == P4Consts.PDAUseStatus).ValueData,
                EmployeeName = Context.Set<Employee>().FirstOrDefault(dictype=>dictype.Id==c.EmployeeId).TrueName,
                GetTime = c.GetTime,
                Remark=c.Remark
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllEquipmentOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }




        public GetAllEquipmentMaintenanceOutput GetAllEquipmentMaintenanceOutputList(EquipmentMaintenanceInput input)
        {
            int records = Table.Filters(input).Count();
            var EquipmentMaintenance= Context.Set<EquipmentMaintenance>();
            var query =EquipmentMaintenance.Select(c => new EquipmentMaintenanceDto
            {
                Id = c.Id,
                PDA = c.PDA,
                UseStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.UseStatus.ToString() && dictype.TypeCode == P4Consts.PDAUseStatus).ValueData,
                CreationTime = c.CreationTime,
                CreatorUserName = Context.Set<User>().FirstOrDefault(dictype => dictype.Id == c.CreatorUserId).UserName,
                Ramark = c.Ramark
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllEquipmentMaintenanceOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }



        /// <summary>
        /// 获取设备使用状态饼状图显示数据
        /// </summary>
        /// <returns></returns>
        public List<EquipmentDto> GetEquipmentNumGroupByUseStatus(int? TenantID)
        {
            string sqlStr = "";
            if (!TenantID.HasValue)
            {
                //tenantID ="null";
                sqlStr = @"select UseStatus as UseStatusInt,count(Id) as UseNum from AbpEquipments where IsDeleted=0 group by UseStatus";
            }
            else
            {
                sqlStr = @"select UseStatus as UseStatusInt,count(Id) as UseNum from AbpEquipments where IsDeleted=0 and TenantId=" + TenantID.Value + " group by UseStatus";
            }
            List<EquipmentDto> Listdto= Context.Database.SqlQuery<EquipmentDto>(sqlStr, new SqlParameter[] { }).ToList();
            List<EquipmentDto> ListRTdto = new List<EquipmentDto>();//因为使用状态数据类型问题需要重新转换，再定义一个List返回值
            if (Listdto.Count > 0)
            {
                for (int i = 0; i < Listdto.Count; i++)
                {
                    EquipmentDto emdto = new EquipmentDto();
                    string usestring = Listdto[i].UseStatusInt.ToString();
                    emdto.UseStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == usestring && dictype.TypeCode == P4Consts.PDAUseStatus).ValueData;
                    emdto.UseNum = Listdto[i].UseNum;
                    ListRTdto.Add(emdto);
                }
            }
            return ListRTdto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<EquipmentExcelDto> GetEquipmentExcelDtoGroupByUseStatus(int? TenantID)
        {
           
            string sqlStr = "";
            if (!TenantID.HasValue)
            {
                sqlStr = @"select UseStatus as UseStatusInt,count(Id) as UseNum from AbpEquipments where IsDeleted=0 group by UseStatus";
            }
            else
            {
                sqlStr = @"select UseStatus as UseStatusInt,count(Id) as UseNum from AbpEquipments where IsDeleted=0 and TenantId=" + TenantID.Value + " group by UseStatus";
            }
            List<EquipmentDto> Listdto = Context.Database.SqlQuery<EquipmentDto>(sqlStr, new SqlParameter[] { }).ToList();
            List<EquipmentExcelDto> ListRTdto = new List<EquipmentExcelDto>();//因为使用状态数据类型问题需要重新转换，再定义一个List返回值
            for (int i = 0; i < Listdto.Count; i++)
            {
                EquipmentExcelDto emdto = new EquipmentExcelDto();
                string usestring = Listdto[i].UseStatusInt.ToString();
                emdto.UseStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == usestring && dictype.TypeCode == P4Consts.PDAUseStatus).ValueData;
                emdto.UseNum = Listdto[i].UseNum;
                ListRTdto.Add(emdto);
            }
            return ListRTdto;
        }


        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        public void CargoInfoExport(int? TenantID)
        {
            DataTable dt = ListToDataTable<EquipmentExcelDto>(GetEquipmentExcelDtoGroupByUseStatus(TenantID));
            dt.TableName = "PDA设备统计";
            Export1(dt);
        }

        public static MemoryStream RenderToExcel(DataTable table)
        {
            MemoryStream ms = new MemoryStream();

            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);

                // handling header.
                foreach (DataColumn column in table.Columns)
                {
                    string caption = column.Caption.ToString();
                    if (column.Caption.ToString() == "UseStatus")
                    {
                        caption = "使用状态";
                    }
                    if (column.Caption.ToString() == "UseNum")
                    {
                        caption = "数量";
                    }
                    headerRow.CreateCell(column.Ordinal).SetCellValue(caption);//If Caption not set, returns the ColumnName value

                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.Center;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    headStyle.SetFont(font);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                }
                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

            }
            return ms;
        }

        static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }


        public void Export1(DataTable dt)
        {

            string filename = dt.TableName + "报表" + DateTime.Now.ToFileTimeUtc().ToString();
            MemoryStream ms = RenderToExcel(dt);
            //string path = Path.Combine("C:/Users/208/Desktop/", Path.GetFileName(filename + ".xls"));
            //string path = Path.Combine(System.Web.HttpContext.Current.Request.MapPath("~/ExportExcel/"), Path.GetFileName(filename + ".xls"));
            //SaveToFile(ms, path);
            //return "/UploadFiles/ExportExcel/" + filename + ".xls";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/Report/",
                Path.GetFileName(filename + ".xls"));
            SaveToFile(ms, path);

            System.Web.HttpContext.Current.Response.ContentType = "application/ms-download";
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.End();

        }

    }
}
