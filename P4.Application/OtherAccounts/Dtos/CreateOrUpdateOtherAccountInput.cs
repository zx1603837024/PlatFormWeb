using Abp.Application.Services.Dto;

namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrUpdateOtherAccountInput : EntityRequestInput, IInputDto, IOperDto
    {
        //UserName, Password, TelePhone, IsPhoneConfirmed
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 车牌号  卡绑定车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPhoneConfirmed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoDeduction { get; set; }
    }
}
