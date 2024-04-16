using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.InspectorCharges.Dtos;
using P4.Businesses;

namespace P4.InspectorCharges
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectorChargesAppService : IInspectorChargesAppService 
    {
        #region Var
        private readonly IBusinessRepository _businessRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessRepository"></param>
        public InspectorChargesAppService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorChargesOutput GetInspectorChargesDayReport(InspectorChargeInput input)
        {
            List<InspectorChargesDto> inspectorchargesDaylist = new List<InspectorChargesDto>();
            return _businessRepository.GetMoneyTotalGroupbyInspector(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<InspectorChargesDto> GetInspectorChargesDayReportToEchart(InspectorChargeInput input)
        {
            List<InspectorChargesDto> inspectorChargesDaylist = new List<InspectorChargesDto>();
            inspectorChargesDaylist = _businessRepository.GetMoneyTotalGroupbyInspectorEchars(input);
            return inspectorChargesDaylist;
        }
    }
}
