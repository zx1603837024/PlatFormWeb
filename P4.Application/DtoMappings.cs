using AutoMapper;
using P4.MultiTenancy;
using P4.OtherAccounts;
using P4.Tenants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4
{
    /// <summary>
    /// 
    /// </summary>
    public class DtoMappings
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Map()
        {
            //自定义map映射
            Mapper.CreateMap<Menus.Menu, Menus.Dto.MenuTreeDto>()
                .ForMember(m => m.name, opts => opts.MapFrom(d => d.LocalizableString));

            Mapper.CreateMap<Regions.Region, Regions.Dtos.RegionDto>()
                .ForMember(m => m.CreatorUserName, opts => opts.MapFrom(d => d.CreatorUser.UserName))
                .ForMember(m => m.LastModifierUserName, opts => opts.MapFrom(d => d.LastModifierUser.UserName));

            Mapper.CreateMap<Tenant, TenantDto>()
                .ForMember(m => m.EditionName, opts => opts.MapFrom(d => d.Edition.DisplayName));

            Mapper.CreateMap<Parks.Park, Parks.Dtos.ParkDto>()
                .ForMember(m => m.LastModifierUserName, opts => opts.MapFrom(d => d.LastModifierUser.UserName))
                .ForMember(m => m.CreatorUserName, opts => opts.MapFrom(d => d.CreatorUser.UserName))
                .ForMember(m => m.RegionName, opts => opts.MapFrom(d => d.RegionModel.RegionName));

            Mapper.CreateMap<MonthlyCars.MonthlyCarHistory, MonthlyCars.Dtos.MonthlyCarHistoryDto>()
                .ForMember(m => m.CreationUserName, opts => opts.MapFrom(d => d.CreatorUser.Name));

            Mapper.CreateMap<MonthlyCars.MonthlyCarAbnormal, MonthlyCars.Dtos.MonthlyCarAbnormalDto>()
                .ForMember(m => m.BeginTime, opts => opts.MapFrom(d => d.MonthlyCar.BeginTime))
                .ForMember(m => m.EndTime, opts => opts.MapFrom(d => d.MonthlyCar.EndTime))
                .ForMember(m => m.VehicleOwner, opts => opts.MapFrom(d => d.MonthlyCar.VehicleOwner))
                .ForMember(m => m.Telphone, opts => opts.MapFrom(d => d.MonthlyCar.Telphone));

            Mapper.CreateMap<Inducibles.InducibleToPark, Inducibles.Dtos.InducibleToParkDto>()
               .ForMember(m => m.ParkName, opts => opts.MapFrom(d => d.Park.ParkName));

            Mapper.CreateMap<Sensors.Sensor, Sensors.Dtos.SensorDto>()
               .ForMember(m => m.BerthsecName, opts => opts.MapFrom(d => d.BerthSec.BerthsecName));
           

            Mapper.CreateMap<Parks.Park, Parks.Dtos.ParkAppDto>()
                .ForMember(m => m.name, opts => opts.MapFrom(d => d.ParkName))
                .ForMember(m => m.lat, opts => opts.MapFrom(d => d.X))
                .ForMember(m => m.lng, opts => opts.MapFrom(d => d.Y))
                .ForMember(m => m.id, opts => opts.MapFrom(d => d.Id))
                .ForMember(m => m.total, opts => opts.MapFrom(d => d.BerthCount))
                .ForMember(m => m.type, opts => opts.MapFrom(d => d.ParkType));

            Mapper.CreateMap<OtherAccount, OtherAccounts.Dtos.OtherAccountDto>()
                .ForMember(m => m.EnabledUserName, opts => opts.MapFrom(d => d.Employee.TrueName));

            Mapper.CreateMap<Sensors.SensorFault, Sensors.Dtos.SensorFaultDto>()
                .ForMember(m => m.RegionName, opts => opts.MapFrom(d => d.Region.RegionName))
                .ForMember(m => m.ParkName, opts => opts.MapFrom(d => d.Park.ParkName))
                .ForMember(m => m.BerthsecName, opts => opts.MapFrom(d => d.Berthsec.BerthsecName));

            Mapper.CreateMap<ParkingLot.ParkChannel, ParkingLot.Dtos.ParkChannelDto>()
             .ForMember(m => m.LastModifierUserName, opts => opts.MapFrom(d => d.LastModifierUser.UserName))
             .ForMember(m => m.CreatorUserName, opts => opts.MapFrom(d => d.CreatorUser.UserName))
             .ForMember(m => m.ParkName, opts => opts.MapFrom(d => d.Park.ParkName));

            Mapper.CreateMap<ParkingLot.ParkingMonthlyRent, ParkingLot.Dto.AbpParkingMonthlyRentDto>()
           .ForMember(m => m.CompanyName, opts => opts.MapFrom(d => d.Company.CompanyName))
           .ForMember(m => m.ParkName, opts => opts.MapFrom(d => d.Park.ParkName));
        }
    }
}
