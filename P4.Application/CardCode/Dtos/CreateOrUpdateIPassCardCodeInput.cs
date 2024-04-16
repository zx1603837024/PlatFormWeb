using Abp.Application.Services.Dto;

namespace P4.CardCode.Dtos
{
    public class CreateOrUpdateIPassCardCodeInput : EntityRequestInput, IOperDto
    {
        public string oper { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
    }
}
