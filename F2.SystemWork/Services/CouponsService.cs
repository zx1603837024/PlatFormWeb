
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
    public interface CouponsService
    {
        /// <summary>
        /// 查询优惠券计划列表
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Hashtable getCouponsPlanList(CouponsQueryList param);

        /// <summary>
        /// 添加优惠劵计划
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Hashtable insertCouponsPlan(InsertPlanQuery param);
        /// <summary>
        /// 编辑优惠券计划
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Hashtable couponsEdit(InsertPlanQuery param);

        /// <summary>
        /// 删除优惠券计划
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Hashtable couponsDelete(InsertPlanQuery param);
        
        /// <summary>
        /// 获取优惠券发放列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Hashtable getCouponsDetail(CouponsDetailQuery param);
        
        
        /// <summary>
        /// 优惠券发放
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Hashtable grantCoupons(GrantCouponsQuery param);
        
    }
}
