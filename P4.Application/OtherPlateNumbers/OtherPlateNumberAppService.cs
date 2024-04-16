using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.OtherPlateNumbers
{
    public class OtherPlateNumberAppService : ApplicationService, IOtherPlateNumberAppService
    {
        #region Var
        private readonly IRepository<OtherPlateNumbers.OtherPlateNumber, long> _abpOtherPlateNumberRepository;
        #endregion

        public OtherPlateNumberAppService(IRepository<OtherPlateNumbers.OtherPlateNumber, long> abpOtherPlateNumberRepository)
        {
            _abpOtherPlateNumberRepository = abpOtherPlateNumberRepository;
        }

        public Dtos.GetAllOtherPlateNumberOutput GetAll(int Id)
        {
            return new Dtos.GetAllOtherPlateNumberOutput() {
                Items = _abpOtherPlateNumberRepository.GetAll().Where(model => model.AssignedOtherAccountId == Id).ToList().MapTo<List<Dtos.OtherPlateNumberDto>>()
            };
        }
    }
}
