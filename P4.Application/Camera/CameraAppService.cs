using P4.Camera.Dtos;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using System;

namespace P4.Camera
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraAppService : P4AppServiceBase, ICameraAppService
    {
        #region Var
        private readonly IRepository<CameraHeartbeat> _abpCameraHeartbeatRepository;
        private readonly IRepository<Cameras> _abpCameraRepository;
        private readonly IRepository<CameraAlarmreport> _abpCameraAlarmreportRepository;
        private readonly IRepository<CameraBusinessDetail, long> _abpCameraBusinessDetailRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpCameraHeartbeatRepository"></param>
        /// <param name="abpCameraRepository"></param>
        /// <param name="abpCameraAlarmreportRepository"></param>
        /// <param name="abpCameraBusinessDetailRepository"></param>
        public CameraAppService(IRepository<CameraHeartbeat> abpCameraHeartbeatRepository, IRepository<Cameras> abpCameraRepository, IRepository<CameraAlarmreport> abpCameraAlarmreportRepository, IRepository<CameraBusinessDetail, long> abpCameraBusinessDetailRepository)
        {
            _abpCameraHeartbeatRepository = abpCameraHeartbeatRepository;
            _abpCameraRepository = abpCameraRepository;
            _abpCameraAlarmreportRepository = abpCameraAlarmreportRepository;
            _abpCameraBusinessDetailRepository = abpCameraBusinessDetailRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public AlarmReportResponseDto Alarmreport(AlarmReportRequestDto dto)
        {
            var returndto = new AlarmReportResponseDto();
            var dt = ConvertTimestamp(dto.timeStamp.sec);
            var model = new CameraAlarmreport();
            model.alarmtype = dto.alarmtype;
            model.BaseId = dto.id;
            model.devid = dto.devid;
            model.cmd = dto.cmd;
            model.devtype = dto.devtype;
            model.imageFile = dto.imageFile;
            model.imageFileLen = dto.imageFileLen;
            model.PhotoTime = dt;
            model.sn = dto.sn;
            returndto.cmd = dto.cmd;
            _abpCameraAlarmreportRepository.Insert(model);
            return returndto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        private DateTime ConvertTimestamp(long unixTimeStamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            return startTime.AddSeconds(unixTimeStamp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public HeartbeatResponseDto Heartbeat(HeartbeatRequestDto dto)
        {
            var model = new CameraHeartbeat();
            model.BaseId = dto.id;
            model.devid = dto.devid;
            model.gpslatitude = dto.gpslatitude;
            model.gpslongitude = dto.gpslongitude;
            model.gpsstatus = dto.gpsstatus;
            model.gyroX = dto.gyroX;
            model.gyroxstatus = dto.gyroxstatus;
            model.gyroY = dto.gyroY;
            model.gyroZ = dto.gyroZ;
            model.ledstatus = dto.ledstatus;
            model.lockstatus = dto.lockstatus;
            model.radarstatus = dto.radarstatus;
            model.second = dto.second;
            model.sn = dto.sn;
           
            var returndto = new HeartbeatResponseDto();
            returndto.cmd = dto.cmd;
            _abpCameraHeartbeatRepository.Insert(model);
            return returndto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public LoginRespnseDto Login(LoginRequestDto dto)
        {
            var model = new Cameras();
           
            var oldmodel = _abpCameraRepository.FirstOrDefault(entity => entity.sn == dto.sn);
            if (oldmodel == default(Cameras))
            {
                model.agrver = dto.agrver;
                model.BaseId = dto.id;
                model.devid = dto.devid;
                model.hardver = dto.hardver;
                model.idaddr = dto.idaddr;
                model.mac = dto.mac;
                model.sn = dto.sn;
                model.softver = dto.softver;
                _abpCameraRepository.Insert(model);
            }
            else
            {
                oldmodel.agrver = dto.agrver;
                oldmodel.BaseId = dto.id;
                oldmodel.devid = dto.devid;
                oldmodel.hardver = dto.hardver;
                oldmodel.idaddr = dto.idaddr;
                oldmodel.mac = dto.mac;
                oldmodel.sn = dto.sn;
                oldmodel.softver = dto.softver;
                oldmodel.CreationTime = DateTime.Now;
                _abpCameraRepository.Update(oldmodel);
            }
            var returndto = new LoginRespnseDto();
            returndto.cmd = dto.cmd;
           
            return returndto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseDto Parking(CameraRequestDto dto)
        {
            var returndto = new ResponseDto();
            var model = new CameraBusinessDetail();
            returndto.cmd = dto.cmd;
            model.cmd = dto.cmd;
            model.picnum = dto.picnum;
            model.BaseId = dto.id;
            model.colortype = dto.colortype;
            model.confidence = dto.confidence;
            model.devid = dto.devid;
            model.direction = dto.direction;
            model.license = dto.license;
            model.newcolortype = dto.newcolortype;
            model.newconfidence = dto.newconfidence;
            model.newlicense = dto.newlicense;
            model.newtype = dto.newtype;
            model.type = dto.type;
            //switch (dto.cmd)
            //{
            //    case "platereport"://纠正上报

            //        break;
            //    case "imgreport"://停车数据上报
                    
            //        break;
            //}
            _abpCameraBusinessDetailRepository.Insert(model);
            return returndto;
        }
    }
}
