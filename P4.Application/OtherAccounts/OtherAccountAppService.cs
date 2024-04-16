using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.OtherAccounts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Linq.Extensions;
using P4.OtherPlateNumbers;
using System.Web.Script.Serialization;
using P4.SmsManagements;
using Abp.Configuration;
using Abp.Domain.Uow;
using P4.SmsManagements.Dtos;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 
    /// </summary>
    public class OtherAccountAppService : ApplicationService, IOtherAccountAppService
    {
        #region Var
        private readonly IRepository<OtherAccount, long> _abpOtherAccountRepository;
        private readonly IRepository<DeductionRecord, long> _abpDeductionRecordRepository;
        private readonly IRepository<OtherPlateNumber, long> _abpOtherPlateNumberRepository;
        private readonly IOtherAccountRepository _otherAccountRepository;
        private readonly ISmsManagementAppService _smsManagementAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IOtherAccountRepository _OtherAccountRepository;
        private readonly ISmsModelAppService _smsModelAppService;

        JavaScriptSerializer js = new JavaScriptSerializer();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpOtherAccountRepository"></param>
        /// <param name="abpDeductionRecordRepository"></param>
        /// <param name="abpOtherPlateNumberRepository"></param>
        /// <param name="otherAccountRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="OtherAccountRepository"></param>
        /// <param name="smsModelAppService"></param>
        public OtherAccountAppService(IRepository<OtherAccount, long> abpOtherAccountRepository,
            IRepository<DeductionRecord, long> abpDeductionRecordRepository,
            IRepository<OtherPlateNumber, long> abpOtherPlateNumberRepository,
            IOtherAccountRepository otherAccountRepository, IUnitOfWorkManager unitOfWorkManager,
            ISmsManagementAppService smsManagementAppService, IOtherAccountRepository OtherAccountRepository, ISmsModelAppService smsModelAppService)
        {
            _abpOtherAccountRepository = abpOtherAccountRepository;
            _abpDeductionRecordRepository = abpDeductionRecordRepository;
            _abpOtherPlateNumberRepository = abpOtherPlateNumberRepository;
            _otherAccountRepository = otherAccountRepository;
            _smsManagementAppService = smsManagementAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _OtherAccountRepository = OtherAccountRepository;
            _smsModelAppService = smsModelAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OtherAccountDto Insert(CreateOrUpdateOtherAccountInput input)
        {
            var entity = new OtherAccount();
            entity.UserName = input.UserName;
            entity.Name = input.Name;
            entity.Password = input.Password;
            entity.TelePhone = input.TelePhone;
            entity.IsPhoneConfirmed = input.IsPhoneConfirmed;
            entity.Client = input.Client;
            entity.AccountLoginDatetime = DateTime.Now;
            return _abpOtherAccountRepository.Insert(entity).MapTo<OtherAccountDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OtherAccountDto Update(CreateOrUpdateOtherAccountInput input)
        {

            if (!string.IsNullOrWhiteSpace(input.PlateNumber))
            {
                long id = input.Id;
                var otherAccountPlateN = _abpOtherPlateNumberRepository.FirstOrDefault(a => a.AssignedOtherAccountId == id && a.IsActive == true && a.IsDeleted == false);
                if (otherAccountPlateN != null)
                {
                    otherAccountPlateN.PlateNumber = input.PlateNumber;
                    otherAccountPlateN.LastModificationTime = DateTime.Now;
                    otherAccountPlateN.LastModifierUserId = AbpSession.UserId;
                    _abpOtherPlateNumberRepository.Update(otherAccountPlateN);//修改
                }
                else
                {
                    OtherPlateNumber otherplatenumber = new OtherPlateNumber();
                    otherplatenumber.AssignedOtherAccountId = input.Id;//账户id
                    otherplatenumber.PlateNumber = input.PlateNumber;//绑定车牌号
                    otherplatenumber.Order = 1;//
                    otherplatenumber.IsActive = true;//是否激活
                    otherplatenumber.CreationTime = DateTime.Now;//创建时间
                    otherplatenumber.CreatorUserId = AbpSession.UserId;//创建用户
                    otherplatenumber.IsDeleted = false;//是否删除
                    _abpOtherPlateNumberRepository.Insert(otherplatenumber);//添加
                }
            }
            var entity = _abpOtherAccountRepository.FirstOrDefault(input.Id);
            entity.UserName = input.UserName;
            entity.Name = input.Name;
            entity.Password = input.Password;
            entity.TelePhone = input.TelePhone;
            entity.IsPhoneConfirmed = input.IsPhoneConfirmed;
            entity.IsActive = input.IsActive;
            entity.AutoDeduction = input.AutoDeduction;
            entity.Client = input.Client;
            return _abpOtherAccountRepository.Update(entity).MapTo<OtherAccountDto>();
        }
        /// <summary>
        /// 根据主键返回账户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OtherAccountDto GetAccountByID(int Id)
        {
            var entity = _abpOtherAccountRepository.FirstOrDefault(a => a.Id == Id && a.IsActive == true).MapTo<OtherAccountDto>();
            return entity;
        }

        /// <summary>
        /// 账户充值，修改余额 后台
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="upMoney"></param>
        /// <returns></returns>
        public async Task<int> UpdateAccountWallet(int Id, decimal upMoney)
        {
            var entity = _abpOtherAccountRepository.FirstOrDefault(a => a.Id == Id && a.IsActive == true);
            decimal BeginMoney = entity.Wallet;

            var plateNumberModel = _abpOtherPlateNumberRepository.FirstOrDefault(entry => entry.AssignedOtherAccountId == entity.Id);

            if (entity != null)
            {
                entity.Wallet += upMoney;
                _abpOtherAccountRepository.Update(entity);
                DeductionRecord deductionR = new DeductionRecord();
                deductionR.OtherAccountId = entity.Id;
                deductionR.OperType = 1;
                deductionR.Money = upMoney;
                deductionR.PayStatus = true;
                deductionR.Remark = "平台续费";
                deductionR.CompanyId = entity.CompanyId.Value;
                deductionR.BeginMoney = BeginMoney;
                deductionR.EndMoney = entity.Wallet;
                deductionR.PlateNumber = (plateNumberModel == default(DeductionRecord)) ? "" : plateNumberModel.PlateNumber;
                deductionR.CardNo = entity.CardNo;//卡号
                                                  //少了一个车牌号
                deductionR.InTime = DateTime.Now;
                _abpDeductionRecordRepository.Insert(deductionR); //添加续费记录

                if (!string.IsNullOrEmpty(entity.TelePhone))
                {
                    //短信通知，账户充值 短信通知
                    _smsManagementAppService.SendSms(new SmsAccountDto()
                    {
                        Destnumbers = entity.TelePhone,
                        Msg =
                            string.Format(_smsModelAppService.GetSmsModelByType("RenewCardModel").SmsContext, DateTime.Now.ToString("yyyy年MM月dd日hh时mm分"), upMoney,
                                entity.Wallet)
                    });
                }
                return 1; //表示充值成功
            }
            return 2; //充值失败
        }


        /// <summary>
        /// 第三方账号登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public OtherAccountDto AccountLogin(string username, string password)
        {
            var entry = _abpOtherAccountRepository.FirstOrDefault(entity => entity.UserName == username && entity.Password == password);
            if (entry != default(OtherAccount))
            {
                AbpSession.Sumname = entry.Name;
            }
            return entry.MapTo<OtherAccountDto>();
        }

        /// <summary>
        /// 返回第三方账号记录列表
        /// </summary>
        /// <returns></returns>
        public GetAllOtherAccountOutput GetAll(SearchOtherAccountInput input)
        {
            int records = _abpOtherAccountRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllOtherAccountOutput()
            {
                rows = _abpOtherAccountRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<OtherAccountDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllOtherAccountOutput GetAllOtherAccountAndPlateNumber(SearchOtherAccountInput input)
        {
            return _OtherAccountRepository.GetAllOtherAccountAndPlateNumber(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _abpOtherAccountRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetTotal()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            temp.Add("total", _abpOtherAccountRepository.Count());
            temp.Add("Register", _abpOtherAccountRepository.Count(model => model.CreationTime > DateTime.Now));
            return temp;
        }

        /// <summary>
        /// 获取钱包明细记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllDeductionRecordOutput GetAllDeductionRecord(SearchDeductionRecordInput input)
        {
            int records = _abpDeductionRecordRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllDeductionRecordOutput()
            {
                rows = _abpDeductionRecordRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<DeductionRecordDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 动态报表 获取账户明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllDeductionRecordOutput GetDynamicReportDeductionRecord(SearchDeductionRecordInput input)
        {
            return _otherAccountRepository.GetDynamicReportDeductionRecord(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DeductionRecordDto> GetDynamicRDeductionEchar(SearchDeductionRecordInput input)
        {
            return _otherAccountRepository.GetDynamicRDeductionEchar(input);
        }
        /// <summary>
        /// 根据商户 进行统计商户时间段内总的消费详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DeductionRecordDto> GetDynamicRDeduStatisEchar(SearchDeductionRecordInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _otherAccountRepository.GetDynamicRDeduStatisEchar(input, tenantID);
        }
        /// <summary>
        /// 激活卡片
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns>1:激活成功,2:激活失败,3:已激活,4:未找到卡号,5:已退卡</returns>
        [AbpAuthorize]
        public int UpdateEnabled(string cardNo)
        {
            return UpdateEnabled2(cardNo, null, null, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        /// <summary>
        /// 激活卡片
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="enableTime"></param>
        /// <returns>1:激活成功,2:激活失败,3:已激活,4:未找到卡号,5:已退卡</returns>
        [AbpAuthorize]
        public int UpdateEnabled1(string cardNo, string enableTime)
        {
            return UpdateEnabled2(cardNo, null, null, enableTime);
            //var entry = _abpOtherAccountRepository.FirstOrDefault(entity => entity.CardNo == cardNo);
            //if (entry == default(OtherAccount))
            //     throw new AbpAuthorizationException("激活失败:未找到该卡号！", "501");//报文
            //   // return 4;
            //if (!entry.IsActive)
            //    return 5;
            //if (entry.IsEnabled)
            //    return 3;
            //entry.IsEnabled = true;
            //entry.EnabledTime = Convert.ToDateTime(enableTime);//卡激活时间;
            //entry.EnabledUserId = AbpSession.UserId.Value;

            //var entity1 = _abpDeductionRecordRepository.FirstOrDefault(e => e.OtherAccountId == entry.Id && e.OperType == 1 && e.Remark == "开卡");
            //if (entity1 != default(DeductionRecord))
            //{
            //    entity1.InTime = entry.EnabledTime.Value;
            //    _abpDeductionRecordRepository.Update(entity1);
            //}
            //return _abpOtherAccountRepository.Update(entry) == default(OtherAccount) ? 2 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckEnabled()
        {
            _otherAccountRepository.CheckEnabled();
        }

        /// <summary>
        /// 激活卡片
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="TelePhone"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="EnableTime"></param>
        /// <returns>1:激活成功,2:激活失败,3:已激活,4:未找到卡号,5:已退卡</returns>
        [AbpAuthorize]
        public int UpdateEnabled2(string CardNo, string TelePhone, string PlateNumber, string EnableTime)
        {

            var entry = _abpOtherAccountRepository.FirstOrDefault(entity => entity.CardNo == CardNo);
            if (entry == default(OtherAccount))
                throw new AbpAuthorizationException("激活失败:未找到该卡号！", "501");//报文
            // return 4;
            if (!entry.IsActive)
                return 5;
            if (entry.IsEnabled)
                return 3;
            entry.IsEnabled = true;
            if (TelePhone != null && TelePhone != "0")//TelePhone=="0" 空值
                entry.TelePhone = TelePhone;
            entry.EnabledTime = Convert.ToDateTime(EnableTime);//卡激活时间;
            entry.EnabledUserId = AbpSession.UserId.Value;
            // e.Remark == "开卡" 是为了兼容晋中1017之前的版本
            var entity1 = _abpDeductionRecordRepository.FirstOrDefault(e => e.OtherAccountId == entry.Id && e.OperType == 1 && (e.Remark == "财务开卡" || e.Remark == "开卡"));
            if (entity1 != default(DeductionRecord))
            {
                entity1.InTime = entry.EnabledTime.Value;
                entity1.Remark = "开卡";
                if (PlateNumber != "0")
                    entity1.PlateNumber = PlateNumber;
                _abpDeductionRecordRepository.Update(entity1);
            }

            if (!string.IsNullOrWhiteSpace(PlateNumber) && PlateNumber != "0")//PlateNumber=="0" 空值
            {
                OtherPlateNumber otherPlateNumber = new OtherPlateNumber();
                otherPlateNumber.AssignedOtherAccountId = entry.Id;
                otherPlateNumber.PlateNumber = PlateNumber;
                otherPlateNumber.Order = 1;
                otherPlateNumber.IsActive = true;
                otherPlateNumber.IsDeleted = false;
                _abpOtherPlateNumberRepository.Insert(otherPlateNumber);
            }

            return _abpOtherAccountRepository.Update(entry) == default(OtherAccount) ? 2 : 1;
        }


        /// <summary>
        /// 开卡
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="money">金额</param>
        /// <param name="phoneNum">电话</param>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="sourcetype">来源</param>
        /// <param name="clien">设备编号</param>
        /// <returns>卡信息jso字符串</returns>
        [AbpAuthorize]
        public string NewCard(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien)
        {
            //return NewCard(cardNo, money, phoneNum, plateNumber, sourcetype, clien, "", "");
            return NewCardAddTime(cardNo, money, phoneNum, plateNumber, sourcetype, clien, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public string NewCard(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien, string guid)
        {
            //return NewCard(cardNo, money, phoneNum, plateNumber, sourcetype, clien);
            return NewCardAddTime(cardNo, money, phoneNum, plateNumber, sourcetype, clien, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <param name="guid"></param>
        /// <param name="productNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public string NewCard(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien, string guid, string productNo)
        {
            return NewCardAddTime(cardNo, money, phoneNum, plateNumber, sourcetype, clien, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region  不中用 注释掉了
            //处理初始化数据
            //phoneNum = (phoneNum == "0" ? null : phoneNum);
            //plateNumber = (plateNumber == "0" ? null : plateNumber);

            //ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();
            //if (string.IsNullOrWhiteSpace(cardNo))
            //{
            //    returnOtherAccountModel.msg = "卡号不能为空";
            //    return js.Serialize(returnOtherAccountModel);
            //}
            //if (_abpOtherAccountRepository.FirstOrDefault(oa => oa.CardNo == cardNo && oa.IsActive == true) != default(OtherAccount))
            //{
            //    returnOtherAccountModel.msg = "卡号重复，不能重复开卡";
            //    return js.Serialize(returnOtherAccountModel);
            //}
            //OtherAccount otherAccount = new OtherAccount();
            //otherAccount.CardNo = cardNo;
            //otherAccount.AuthenticationSource = sourcetype;
            //otherAccount.Wallet = money;
            //otherAccount.Client = clien;
            //otherAccount.UserName = phoneNum;
            //otherAccount.Name = phoneNum;
            //otherAccount.Password = "123456";
            //otherAccount.TelePhone = phoneNum;
            //otherAccount.AccountLoginDatetime = DateTime.Now;
            //otherAccount.IsPhoneConfirmed = false;
            //otherAccount.ProductNo = productNo;
            //otherAccount.IsActive = true;
            //otherAccount.IsEnabled = false;//未激活
            //long OtherAccountId = _abpOtherAccountRepository.InsertAndGetId(otherAccount);

            //DeductionRecord deductionRecord = new DeductionRecord();

            //deductionRecord.OtherAccountId = OtherAccountId;
            //deductionRecord.OperType = 1;
            //deductionRecord.Money = money;
            //deductionRecord.PayStatus = true;
            //deductionRecord.InTime = DateTime.Now;
            //deductionRecord.Remark = "财务开卡";
            //_abpDeductionRecordRepository.Insert(deductionRecord);
            //if (!string.IsNullOrWhiteSpace(plateNumber))
            //{
            //    OtherPlateNumber otherPlateNumber = new OtherPlateNumber();
            //    otherPlateNumber.AssignedOtherAccountId = OtherAccountId;
            //    otherPlateNumber.PlateNumber = plateNumber;
            //    otherPlateNumber.Order = 1;
            //    otherPlateNumber.IsActive = true;
            //    otherPlateNumber.IsDeleted = false;
            //    _abpOtherPlateNumberRepository.Insert(otherPlateNumber);
            //}

            //returnOtherAccountModel.CardNo = cardNo;
            //returnOtherAccountModel.msg = "success";
            //returnOtherAccountModel.PlateNumber = plateNumber;
            //returnOtherAccountModel.TelePhone = phoneNum;
            //returnOtherAccountModel.Wallet = money;

            //if (!string.IsNullOrWhiteSpace(phoneNum))
            //    _smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
            //    {
            //        Destnumbers = phoneNum,
            //        Msg = plateNumber == null ? string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet) :
            //        string.Format(P4Consts.RegisterHavaPlateNumberModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), plateNumber, returnOtherAccountModel.Wallet)
            //        //Msg = string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet)
            //    });
            //return js.Serialize(returnOtherAccountModel);
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="phoneNum"></param>
        /// <param name="plateNumber"></param>
        /// <param name="sourcetype"></param>
        /// <param name="clien"></param>
        /// <param name="guid"></param>
        /// <param name="productNo"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public string NewCardAddTime(string cardNo, decimal money, string phoneNum, string plateNumber, string sourcetype, string clien, string guid, string productNo, string time)
        {
            //处理初始化数据
            phoneNum = (phoneNum == "0" ? null : phoneNum);
            plateNumber = (plateNumber == "0" ? null : plateNumber);

            ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();
            if (string.IsNullOrWhiteSpace(cardNo))
            {
                returnOtherAccountModel.msg = "卡号不能为空";
                return js.Serialize(returnOtherAccountModel);
            }
            if (_abpOtherAccountRepository.FirstOrDefault(oa => oa.CardNo == cardNo && oa.IsActive == true) != default(OtherAccount))
            {
                returnOtherAccountModel.msg = "卡号重复，不能重复开卡";
                return js.Serialize(returnOtherAccountModel);
            }
            OtherAccount otherAccount = new OtherAccount();
            otherAccount.CardNo = cardNo;
            otherAccount.AuthenticationSource = sourcetype;
            otherAccount.Wallet = money;
            otherAccount.Client = clien;
            otherAccount.UserName = phoneNum;
            otherAccount.Name = phoneNum;
            otherAccount.Password = "123456";
            otherAccount.TelePhone = phoneNum;
            otherAccount.AccountLoginDatetime = Convert.ToDateTime(time);
            otherAccount.IsPhoneConfirmed = false;
            otherAccount.ProductNo = productNo;
            otherAccount.IsActive = true;
            otherAccount.IsEnabled = false;//未激活
            long OtherAccountId = _abpOtherAccountRepository.InsertAndGetId(otherAccount);

            OtherPlateNumber otherPlateNumber = new OtherPlateNumber();
            if (!string.IsNullOrWhiteSpace(plateNumber))
            {
                otherPlateNumber.AssignedOtherAccountId = OtherAccountId;
                otherPlateNumber.PlateNumber = plateNumber;
                otherPlateNumber.Order = 1;
                otherPlateNumber.IsActive = true;
                otherPlateNumber.IsDeleted = false;
                _abpOtherPlateNumberRepository.Insert(otherPlateNumber);
            }
            DeductionRecord deductionRecord = new DeductionRecord();

            deductionRecord.OtherAccountId = OtherAccountId;
            deductionRecord.OperType = 1;
            deductionRecord.Money = money;
            deductionRecord.BeginMoney = 0;
            deductionRecord.EndMoney = money;
            deductionRecord.PayStatus = true;
            deductionRecord.InTime = Convert.ToDateTime(time);
            deductionRecord.Remark = "财务开卡";
            deductionRecord.CardNo = cardNo;
            deductionRecord.EmployeeId = AbpSession.UserId.Value;
            //deductionRecord.PlateNumber = otherAccount.PlateNumber;
            deductionRecord.PlateNumber = otherPlateNumber == null ? "" : otherPlateNumber.PlateNumber;
            _abpDeductionRecordRepository.Insert(deductionRecord);

            _abpDeductionRecordRepository.Insert(deductionRecord);
            returnOtherAccountModel.CardNo = cardNo;
            returnOtherAccountModel.msg = "success";
            returnOtherAccountModel.PlateNumber = plateNumber;
            returnOtherAccountModel.TelePhone = phoneNum;
            returnOtherAccountModel.Wallet = money;

            if (!string.IsNullOrWhiteSpace(phoneNum))
                 _smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
                {
                    Destnumbers = phoneNum,
                    Msg = plateNumber == null ? string.Format(_smsModelAppService.GetSmsModelByType("RegisterModel").SmsContext, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet) :
                    string.Format(_smsModelAppService.GetSmsModelByType("RegisterHavaPlateNumberModel").SmsContext, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), plateNumber, returnOtherAccountModel.Wallet)
                });
            return js.Serialize(returnOtherAccountModel);
        }
        /// <summary>
        /// 续费
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="money">金额</param>
        /// <returns>卡信息json字符串</returns>
        public string RenewCard(string cardNo, decimal money)
        {
            return RenewCardAddTime(cardNo, money, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region 2016-4-25 不要了
            //ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();
            //if (string.IsNullOrWhiteSpace(cardNo))
            //{
            //    returnOtherAccountModel.msg = "卡号不能为空";
            //    return js.Serialize(returnOtherAccountModel);
            //}
            //OtherAccount otherAccount = _abpOtherAccountRepository.FirstOrDefault(oa => oa.CardNo == cardNo && oa.IsActive == true);
            //if (otherAccount == default(OtherAccount))
            //{
            //    returnOtherAccountModel.msg = "不存在该卡号";
            //    return js.Serialize(returnOtherAccountModel);
            //}
            ////if (!otherAccount.IsEnabled)
            ////{
            ////    returnOtherAccountModel.msg = "卡片未激活";
            ////    return js.Serialize(returnOtherAccountModel);
            ////}
            //otherAccount.Wallet += money;
            //_abpOtherAccountRepository.Update(otherAccount);
            //DeductionRecord deductionRecord = new DeductionRecord();

            //deductionRecord.OtherAccountId = otherAccount.Id;
            //deductionRecord.OperType = 1;
            //deductionRecord.Money = money;
            //deductionRecord.PayStatus = true;
            //deductionRecord.InTime = Convert.ToDateTime(time);
            //deductionRecord.Remark = "续费";
            //_abpDeductionRecordRepository.Insert(deductionRecord);

            //returnOtherAccountModel.CardNo = cardNo;
            //returnOtherAccountModel.msg = "success";
            //returnOtherAccountModel.TelePhone = otherAccount.TelePhone;
            //returnOtherAccountModel.Wallet = otherAccount.Wallet;

            //if (!string.IsNullOrWhiteSpace(otherAccount.TelePhone))
            //    _smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
            //    {
            //        Destnumbers = otherAccount.TelePhone,

            //            Msg =
            //                string.Format(P4Consts.RenewCardModel, deductionRecord.InTime.ToString("yyyy年MM月dd日HH时mm分"), money,
            //                    returnOtherAccountModel.Wallet)
            //                    });
            //return js.Serialize(returnOtherAccountModel);
            #endregion

        }

        /// <summary>
        /// 续费
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="money">金额</param>
        /// <param name="guid">流水号</param>
        /// <returns></returns>
        public string RenewCardGuid(string cardNo, decimal money, string guid)
        {
            return RenewCardAddTime(cardNo, money, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 续费
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="money">金额</param>
        /// <param name="time">续费时间</param>
        /// <returns>卡信息json字符串</returns>
        public string RenewCardAddTime(string cardNo, decimal money, string time)
        {
            decimal BeginMoney = 0;
            ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();
            if (string.IsNullOrWhiteSpace(cardNo))
            {
                returnOtherAccountModel.msg = "卡号不能为空";
                return js.Serialize(returnOtherAccountModel);
            }
            OtherAccount otherAccount = _abpOtherAccountRepository.FirstOrDefault(oa => oa.CardNo == cardNo && oa.IsActive == true);
            if (otherAccount == default(OtherAccount))
            {
                returnOtherAccountModel.msg = "不存在该卡号";
                return js.Serialize(returnOtherAccountModel);
            }
            //if (!otherAccount.IsEnabled)
            //{
            //    returnOtherAccountModel.msg = "卡片未激活";
            //    return js.Serialize(returnOtherAccountModel);
            //}

            BeginMoney = otherAccount.Wallet;//充值前金额

            otherAccount.Wallet += money;
            _abpOtherAccountRepository.Update(otherAccount);


            OtherPlateNumber otherPlateNumber = _abpOtherPlateNumberRepository.FirstOrDefault(op => op.AssignedOtherAccountId == otherAccount.Id);
            //if (otherPlateNumber != default(OtherPlateNumber))
            //{
            //    otherPlateNumber.IsActive = false;
            //    otherPlateNumber.IsDeleted = true;
            //    _abpOtherPlateNumberRepository.Update(otherPlateNumber);
            //}
            DeductionRecord deductionRecord = new DeductionRecord();

            deductionRecord.OtherAccountId = otherAccount.Id;
            deductionRecord.OperType = 1;
            deductionRecord.Money = money;
            deductionRecord.PayStatus = true;
            deductionRecord.InTime = Convert.ToDateTime(time);
            deductionRecord.Remark = "续费";
            deductionRecord.CardNo = cardNo;
            deductionRecord.BeginMoney = BeginMoney;
            deductionRecord.EndMoney = otherAccount.Wallet;//充值后金额
            deductionRecord.EmployeeId = AbpSession.UserId.Value;
            //deductionRecord.PlateNumber = otherAccount.PlateNumber;
            deductionRecord.PlateNumber = otherPlateNumber == null ? "" : otherPlateNumber.PlateNumber;
            _abpDeductionRecordRepository.Insert(deductionRecord);

            returnOtherAccountModel.CardNo = cardNo;
            returnOtherAccountModel.msg = "success";
            returnOtherAccountModel.TelePhone = otherAccount.TelePhone;
            returnOtherAccountModel.Wallet = otherAccount.Wallet;

            if (!string.IsNullOrWhiteSpace(otherAccount.TelePhone))
                _smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
                {
                    Destnumbers = otherAccount.TelePhone,

                    Msg =
                        string.Format(_smsModelAppService.GetSmsModelByType("RenewCardModel").SmsContext, deductionRecord.InTime.ToString("yyyy年MM月dd日HH时mm分"), money,
                            returnOtherAccountModel.Wallet)
                });
            return js.Serialize(returnOtherAccountModel);

        }
        /// <summary>
        /// 查询卡信息
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="phone">电话</param>
        /// <param name="plateNumber">车牌号</param>
        /// <returns>卡信息json字符串</returns>
        [AbpAuthorize]
        public string QueryCard(string cardNo, string phone, string plateNumber)
        {
            ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();
            OtherAccountDto otherAccountDto = _otherAccountRepository.QueryCard(cardNo, phone, plateNumber, AbpSession.TenantId.Value);
            if (otherAccountDto == default(OtherAccountDto))
            {
                returnOtherAccountModel.msg = "该卡不存在";
                return js.Serialize(returnOtherAccountModel);
            }
            returnOtherAccountModel.CardNo = otherAccountDto.CardNo;
            returnOtherAccountModel.msg = "success";
            returnOtherAccountModel.PlateNumber = otherAccountDto.PlateNumber;
            returnOtherAccountModel.TelePhone = otherAccountDto.TelePhone;
            returnOtherAccountModel.Wallet = otherAccountDto.Wallet;
            returnOtherAccountModel.AutoDeduction = otherAccountDto.AutoDeduction;
            //if (!otherAccountDto.IsEnabled)
            //{
            //    returnOtherAccountModel.msg = "该卡未激活";
            //   // return js.Serialize(returnOtherAccountModel);
            //}
            return js.Serialize(returnOtherAccountModel);
        }
        /// <summary>
        /// 退卡
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public string ReturnCard(string cardNo, decimal money, string guid)
        {
            return ReturnCardAddTime(cardNo, money, guid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 退卡
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="money">金额>0</param>
        /// <returns></returns>
        [AbpAuthorize]
        public string ReturnCard(string cardNo, decimal money)
        {
            return ReturnCardAddTime(cardNo, money, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region 2016-4-25 不要了
            //ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();

            //if (string.IsNullOrWhiteSpace(cardNo))
            //{
            //    returnOtherAccountModel.msg = "卡号不能为空";
            //    return js.Serialize(returnOtherAccountModel);
            //}

            //OtherAccount otherAccount = _abpOtherAccountRepository.FirstOrDefault(oa => oa.CardNo == cardNo && oa.IsActive == true);
            //if (otherAccount == default(OtherAccount))
            //{
            //    returnOtherAccountModel.msg = "不存在该卡号";
            //    return js.Serialize(returnOtherAccountModel);
            //}

            ////if (!otherAccount.IsEnabled)
            ////{
            ////    returnOtherAccountModel.msg = "卡号未激活";
            ////    return js.Serialize(returnOtherAccountModel);
            ////}

            //if (money > otherAccount.Wallet)
            //{
            //    returnOtherAccountModel.msg = "退款金额不能大于余额";
            //    return js.Serialize(returnOtherAccountModel);
            //}
            //otherAccount.Wallet = otherAccount.Wallet-money;
            //otherAccount.IsActive = false;
            //otherAccount.IsDeleted = true;
            //_abpOtherAccountRepository.Update(otherAccount);

            //OtherPlateNumber otherPlateNumber = _abpOtherPlateNumberRepository.FirstOrDefault(op => op.AssignedOtherAccountId == otherAccount.Id);
            //if (otherPlateNumber != default(OtherPlateNumber))
            //{
            //    otherPlateNumber.IsActive = false;
            //    otherPlateNumber.IsDeleted = true;
            //    _abpOtherPlateNumberRepository.Update(otherPlateNumber);
            //}


            //DeductionRecord deductionRecord = new DeductionRecord();

            //deductionRecord.OtherAccountId = otherAccount.Id;
            //deductionRecord.OperType = 4;
            //deductionRecord.Money = money;
            //deductionRecord.PayStatus = true;
            //deductionRecord.InTime = Convert.ToDateTime(time);
            //deductionRecord.Remark = "退卡";
            //_abpDeductionRecordRepository.Insert(deductionRecord);

            //returnOtherAccountModel.CardNo = cardNo;
            //returnOtherAccountModel.msg = "success";
            ////returnOtherAccountModel.PlateNumber = otherAccount.PlateNumber;
            //returnOtherAccountModel.TelePhone = otherAccount.TelePhone;
            //returnOtherAccountModel.Wallet = otherAccount.Wallet;
            //return js.Serialize(returnOtherAccountModel);
            #endregion
        }

        /// <summary>
        /// 退卡
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="money">金额>0</param>
        /// <returns></returns>
        [AbpAuthorize]
        public string ReturnCardAddTime(string cardNo, decimal money, string guid, string time)
        {
            ReturnOtherAccountModel returnOtherAccountModel = new ReturnOtherAccountModel();

            if (string.IsNullOrWhiteSpace(cardNo))
            {
                returnOtherAccountModel.msg = "卡号不能为空";
                return js.Serialize(returnOtherAccountModel);
            }

            OtherAccount otherAccount = _abpOtherAccountRepository.FirstOrDefault(oa => oa.CardNo == cardNo && oa.IsActive == true);
            if (otherAccount == default(OtherAccount))
            {
                returnOtherAccountModel.msg = "不存在该卡号";
                return js.Serialize(returnOtherAccountModel);
            }

            //if (!otherAccount.IsEnabled)
            //{
            //    returnOtherAccountModel.msg = "卡号未激活";
            //    return js.Serialize(returnOtherAccountModel);
            //}

            if (money > otherAccount.Wallet)
            {
                returnOtherAccountModel.msg = "退款金额不能大于余额";
                return js.Serialize(returnOtherAccountModel);
            }
            otherAccount.Wallet = otherAccount.Wallet - money;
            otherAccount.IsActive = false;
            otherAccount.IsDeleted = true;
            _abpOtherAccountRepository.Update(otherAccount);

            OtherPlateNumber otherPlateNumber = _abpOtherPlateNumberRepository.FirstOrDefault(op => op.AssignedOtherAccountId == otherAccount.Id);
            if (otherPlateNumber != default(OtherPlateNumber))
            {
                otherPlateNumber.IsActive = false;
                otherPlateNumber.IsDeleted = true;
                _abpOtherPlateNumberRepository.Update(otherPlateNumber);
            }


            DeductionRecord deductionRecord = new DeductionRecord();

            deductionRecord.OtherAccountId = otherAccount.Id;
            deductionRecord.OperType = 4;
            deductionRecord.Money = money;
            deductionRecord.PayStatus = true;
            deductionRecord.InTime = Convert.ToDateTime(time);
            deductionRecord.Remark = "退卡";
            deductionRecord.CardNo = cardNo;
            deductionRecord.EmployeeId = AbpSession.UserId.Value;
            deductionRecord.PlateNumber = otherPlateNumber == null ? "" : otherPlateNumber.PlateNumber;
            _abpDeductionRecordRepository.Insert(deductionRecord);

            returnOtherAccountModel.CardNo = cardNo;
            returnOtherAccountModel.msg = "success";
            //returnOtherAccountModel.PlateNumber = otherAccount.PlateNumber;
            returnOtherAccountModel.TelePhone = otherAccount.TelePhone;
            returnOtherAccountModel.Wallet = otherAccount.Wallet;
            return js.Serialize(returnOtherAccountModel);
        }

        /// <summary>
        /// 车主注册
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public string CarOwnerRegister(string userPhone)
        {
            return NewCard("", 0, userPhone, "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public string GetIdentifyingCode(string userPhone)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant);
            if (string.IsNullOrEmpty(userPhone))
                return "";
            var num = "";
            Random rm = new Random();
            for (int i = 0; i < 4; i++)
            {
                num = num + rm.Next(0, 9);
            }

            OtherAccount otherAccount = _abpOtherAccountRepository.FirstOrDefault(entity => entity.UserName == userPhone && entity.IsActive == true);
            if (otherAccount != default(OtherAccount))
            {
                return "3";//已经注册过了
            }
            return num.ToString();
            //_smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
            //{
            //    Destnumbers = userPhone,
            //    Msg = string.Format(P4Consts.IdentifyingCodeModel, num)
            //    //Msg = string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet)
            //});

            otherAccount = new OtherAccount();
            otherAccount.CardNo = "";
            otherAccount.AuthenticationSource = "";
            otherAccount.Wallet = 0;
            otherAccount.Client = "";
            otherAccount.UserName = userPhone;
            otherAccount.Name = userPhone;
            otherAccount.Password = "";
            otherAccount.TelePhone = userPhone;
            otherAccount.CodeTime = DateTime.Now;
            otherAccount.IsPhoneConfirmed = false;
            otherAccount.IsActive = true;
            otherAccount.IsDeleted = false;
            otherAccount.PhoneCode = num;
            otherAccount.IsLock = false;

            _abpOtherAccountRepository.Insert(otherAccount);


            //_smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
            //{
            //    Destnumbers = userPhone,
            //    Msg = string.Format(P4Consts.IdentifyingCodeModel, num)
            //    //Msg = string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet)
            //});
            return userPhone + "," + num.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public string GetIdentifyingCode1(string userPhone)
        {

            if (string.IsNullOrEmpty(userPhone))
                return "";
            var num = "";
            Random rm = new Random();
            for (int i = 0; i < 4; i++)
            {
                num = num + rm.Next(0, 9);
            }

            OtherAccount otherAccount = _abpOtherAccountRepository.FirstOrDefault(entity => entity.UserName == userPhone && entity.IsActive == true);
            if (otherAccount != default(OtherAccount))
            {
                return "3";//已经注册过了
            }
            //return num.ToString();
            //_smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
            //{
            //    Destnumbers = userPhone,
            //    Msg = string.Format(P4Consts.IdentifyingCodeModel, num)
            //    //Msg = string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet)
            //});

            otherAccount = new OtherAccount();
            otherAccount.CardNo = "";
            otherAccount.AuthenticationSource = "";
            otherAccount.Wallet = 0;
            otherAccount.Client = "";
            otherAccount.UserName = userPhone;
            otherAccount.Name = userPhone;
            otherAccount.Password = "";
            otherAccount.TelePhone = userPhone;
            otherAccount.CodeTime = DateTime.Now;
            otherAccount.IsPhoneConfirmed = false;
            otherAccount.TenantId = 1014;
            otherAccount.IsActive = true;
            otherAccount.IsDeleted = false;
            otherAccount.PhoneCode = num;
            otherAccount.IsLock = false;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {

                _abpOtherAccountRepository.Insert(otherAccount);

            }
            //_smsManagementAppService.SendSms(new SmsManagements.Dtos.SmsAccountDto()
            //{
            //    Destnumbers = userPhone,
            //    Msg = string.Format(P4Consts.IdentifyingCodeModel, num)
            //    //Msg = string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet)
            //});
            return userPhone + "," + num.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecords(SearchDeductionRecordInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _otherAccountRepository.GetAccountAndDeductionRecords(input, tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecordsOnlyMonth(SearchDeductionRecordInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _otherAccountRepository.GetAccountAndDeductionRecordsOnlyMonth(input, tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetThisAccountAndDeductionRecords(SearchDeductionRecordInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _otherAccountRepository.GetThisAccountAndDeductionRecords(input, tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetLastMonthAccountAndDeductionRecords(SearchDeductionRecordInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _otherAccountRepository.GetLastMonthAccountAndDeductionRecords(input, tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetNextMonthAccountAndDeductionRecords(SearchDeductionRecordInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _otherAccountRepository.GetNextMonthAccountAndDeductionRecords(input, tenantID);
        }

        /// <summary>
        /// 解除车牌绑定
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int AbsolvePlatNumber(int Id)
        {
            _abpOtherPlateNumberRepository.Delete(p => p.AssignedOtherAccountId == Id);
            return 1;
        }

        /// <summary>
        /// 账户激活
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="IsEnableAccountName"></param>
        /// <param name="employeeIdInput"></param>
        /// <returns></returns>
        public int OtherAccountIsEnabnle(int Id, string IsEnableAccountName, int employeeIdInput)
        {
            //_abpOtherPlateNumberRepository.Delete(p => p.AssignedOtherAccountId == Id);
            var otherAccount = _abpOtherAccountRepository.FirstOrDefault(p => p.Id == Id);
            if (otherAccount != null)
            {
                otherAccount.IsEnabled = true;
                otherAccount.Name = IsEnableAccountName;//账户姓名
                otherAccount.EnabledUserId = employeeIdInput;
                otherAccount.AuthenticationSource = "后台";//数据来源
                otherAccount.LastModifierUserId = AbpSession.UserId;//最后修改人
                otherAccount.LastModificationTime = DateTime.Now;
                otherAccount.EnabledTime = DateTime.Now;
                _abpOtherAccountRepository.Update(otherAccount);
                return 1;
            }
            else
            {
                return 0;
            }

        }
    }
}
