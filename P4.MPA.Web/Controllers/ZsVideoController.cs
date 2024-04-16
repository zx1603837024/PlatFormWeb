using System;
using F2.OptionSystem.Service;
using F2.OptionSystem.Service.impl;

namespace P4.Web.Controllers
{
    public class ZsVideoController : P4ControllerBase
    {
        private readonly ZSVideoService _zSVideoService;

        public ZsVideoController()
        {
            _zSVideoService = new ZSVideoServiceImpl();
        }
        public Object ProcessAuditStatus(String input)
        {
            return _zSVideoService.zspdns(input);
        }
    }
}