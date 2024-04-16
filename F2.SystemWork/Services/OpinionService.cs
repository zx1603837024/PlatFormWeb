
using F2.SystemWork.Models;
using F2.SystemWork.Query;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Services
{
    public interface OpinionService
    {
        /// <summary>
        /// 查询意见反馈列表
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Hashtable getOpinionList(OpinionListQuery param);

        /// <summary>
        /// 意见处理
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Hashtable opinionHandle(OpinionHandleQuery param);
        

    }
}
