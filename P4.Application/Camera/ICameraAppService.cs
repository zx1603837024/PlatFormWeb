using Abp.Application.Services;
using P4.Camera.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.Camera
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICameraAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        ResponseDto Parking(CameraRequestDto dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        HeartbeatResponseDto Heartbeat(HeartbeatRequestDto dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        LoginRespnseDto Login(LoginRequestDto dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        AlarmReportResponseDto Alarmreport(AlarmReportRequestDto dto);
    }
}
