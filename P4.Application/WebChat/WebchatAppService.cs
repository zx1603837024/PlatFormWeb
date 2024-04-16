using Abp.Domain.Repositories;
using P4.Webchat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.WebChat.Dtos;
using System.Data;

namespace P4.WebChat
{
    public class WebchatAppService : IWebchatAppService
    {

        private readonly IRepository<Ad> _abpAdRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpAdRepository"></param>
        public WebchatAppService(IRepository<Ad> abpAdRepository)
        {
            _abpAdRepository = abpAdRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dtos.GetAdsOutput GetAdsList()
        {

            Dtos.GetAdsOutput output = new GetAdsOutput();
            output.rows = _abpAdRepository.GetAll().MapTo<List<AdDto>>();
            return output;
        }


        public GetStopCarOutput GetStopCarList(int page, int size)
        {
            GetStopCarOutput output = new GetStopCarOutput();
            output.rows = Abp.DataProcessHelper.GetEntityFromTable<StopCarDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, " select * from (select ROW_NUMBER() OVER(Order by AbpBusinessDetail.Id DESC) AS RowNumber, AbpBusinessDetail.Id, PlateNumber, CONVERT(varchar(19), CarInTime, 21) as CarInTime, CONVERT(varchar(19), CarOutTime, 21) as CarOutTime, Money, OutOperaId, TrueName+'('+ UserName +')' as EmployeeName from AbpBusinessDetail with(nolock) left join AbpEmployees on AbpBusinessDetail.OutOperaId = AbpEmployees.Id where AbpBusinessDetail.IsDeleted = 0) as B where  RowNumber BETWEEN " + ((page - 1) * size) + " and  " + (page * size)).Tables[0]);
            return output;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public StopCarInfoDto GetStopCarInfo(long Id)
        {
            StopCarInfoDto dto = Abp.DataProcessHelper.GetEntityFromTable<StopCarInfoDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select AbpBusinessDetail.Id, StopTime, BerthsecName, guid, AbpRates.Remark, PlateNumber, CONVERT(varchar(19), CarInTime, 21) as CarInTime, CONVERT(varchar(19), CarOutTime, 21) as CarOutTime, Money, Prepaid, Receivable, FactReceive, Arrearage, Repayment, OutOperaId, TrueName+'('+ UserName +')' as EmployeeName from AbpBusinessDetail with(nolock) left join AbpEmployees with(nolock) on AbpBusinessDetail.OutOperaId = AbpEmployees.Id left join AbpBerthsecs with(nolock) on AbpBusinessDetail.BerthsecId = AbpBerthsecs.Id left join AbpRates on AbpBerthsecs.RateId = AbpRates.Id where AbpBusinessDetail.Id =" + Id).Tables[0])[0];

            dto.BusinessPicList = Abp.DataProcessHelper.GetEntityFromTable<BusinessPicDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select Id, BusinessDetailId from AbpBusinessDetailPicture where BusinessDetailGuid = '" + dto.guid + "'").Tables[0]);

            return dto;
        }
    }
}
