
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
    public interface PatrolCarService
    {

        /// <summary>
        /// 巡检车列表
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable getPatrolCarList(Hashtable parmas);
        /// <summary>
        /// 批量添加巡检车
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Hashtable InsertPatrolCar(string input);

        /// <summary>
        /// 绑定车牌号
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Hashtable bindingCar(BindingCarQuery param);

        /// <summary>
        /// 巡检车编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Hashtable PatrolEdit(PatrolCarModel input);

        /// <summary>
        /// 巡检车删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Hashtable PatrolDelete(PatrolCarModel input);

        /// <summary>
        /// 查询关联巡检车
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable getPatrolBerthsList(PatrolBerthsQuery parmas);
        /// <summary>
        /// 巡检车订单表查询
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable GetPatrolCarBusinessDetail(PatrolCarModel parmas);
        

        /// <summary>
        /// 解绑巡检车
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable deletePatrolBerths(DeletePatrolBerhsQuery parmas);

        /// <summary>
        /// 巡检车订单状态审核
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable UPatrolCarBusinessDetailAuditStatus(string parmas);
        /// <summary>
        /// 巡检车订单编辑
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable UPatrolCarBusinessDetail(PatrolCarModel parmas);

        /// <summary>
        /// 报警数据查询
        /// </summary>
        /// <param name="parmas"></param>
        /// <returns></returns>
        Hashtable getAlarmDataList(AlarmDataQuery parmas);

    }
}
