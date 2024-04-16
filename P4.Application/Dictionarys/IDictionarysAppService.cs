using Abp.Application.Services;
using P4.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDictionarysAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllDictionaryTypeOutput GetAllDictionaryTypeList(DictionaryTypeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllDictionaryValueOutput GetAllDictionaryValueList(DictionaryValueInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<DictionaryTypeDto> GetAllDictionaryType(DictionaryTypeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string DictionaryTypeInsert(CreateOrUpdateDictionaryTypeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string DictionaryTypeUpdate(CreateOrUpdateDictionaryTypeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string DictionaryTypeDelete(CreateOrUpdateDictionaryTypeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void DictionaryValueInsert(CreateOrUpdateDictionaryValueInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void DictionaryValueUpdate(CreateOrUpdateDictionaryValueInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void DictionaryValueDelete(CreateOrUpdateDictionaryValueInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        List<GetAllValueDataDto> GetAllValueData(string TypeCode);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<GetAllValueDataDto> GetDictionaryValue();

        /// <summary>
        /// 获取字典值
        /// 通过TypeCode和ValueCode
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <param name="ValueCode"></param>
        /// <returns></returns>
        string GetDictionaryValue(string TypeCode, string ValueCode);

    }
}
