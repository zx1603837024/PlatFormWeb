﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WebChat.Dtos
{
    /// <summary>
    /// 微信获取停车记录
    /// </summary>
    public class GetStopCarOutput
    {
        public List<StopCarDto> rows { get; set; }
    }
}
