using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EntityFramework.Configurations
{
    public class IconMap : EntityTypeConfiguration<Icons.Icon>
    {
        public IconMap()
        {
            Ignore(p => p.Test);
        }
    }
    public class EmployeeMap : EntityTypeConfiguration<Employees.Employee>
    {
        public EmployeeMap()
        {
            Ignore(p => p.CheckOuttype);
        }
    }
    //排除巡查员的某些字段不嵌入到数据库中的巡查员表
   
   
}
