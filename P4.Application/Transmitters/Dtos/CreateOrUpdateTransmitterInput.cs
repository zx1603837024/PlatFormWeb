using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Transmitters.Dtos
{
     [AutoMapTo(typeof(Transmitter))]
    public class CreateOrUpdateTransmitterInput : EntityRequestInput, IOperDto
    {
        public string oper { get; set; }
        /// <summary>
        /// 基站编号
        /// </summary>
   
        public string TransmitterNumber { get; set; }
        public  string TransmitterName { get; set; }
        /// <summary>
        /// 电池电量
        /// </summary>
        public  decimal? VoltageCaution { get; set; }

        public string Name { get; set; }
   
     
        public  string X { get; set; }

      
        public  string Y { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>
    
        public string Address { get; set; }

     

    }
}