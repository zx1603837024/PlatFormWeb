using Abp.Localization;
using P4.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemSettingModel
    {
        /// <summary>
        /// 
        /// </summary>
        public bool OperationLog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LoginPush { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Multiuser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool OperationPush { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemPromptBox { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetAllValueDataDto> ComboxValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetAllValueDataDto> Languages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MessageString { get; set; }

    }
}