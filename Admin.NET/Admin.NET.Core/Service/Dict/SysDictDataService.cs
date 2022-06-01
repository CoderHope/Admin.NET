﻿using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.NET.Core.Service
{
    /// <summary>
    /// 系统字典值服务
    /// </summary>
    [ApiDescriptionSettings(Name = "系统字典值", Order = 191)]
    [AllowAnonymous]
    public class SysDictDataService : IDynamicApiController, ITransient
    {
        private readonly SqlSugarRepository<SysDictData> _sysDictDataRep;

        public SysDictDataService(SqlSugarRepository<SysDictData> sysDictDataRep)
        {
            _sysDictDataRep = sysDictDataRep;
        }

        /// <summary>
        /// 获取字典值分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysDictData/pageList")]
        public async Task<SqlSugarPagedList<SysDictData>> GetDictDataPageList([FromQuery] PageDictDataInput input)
        {
            var code = !string.IsNullOrEmpty(input.Code?.Trim());
            var value = !string.IsNullOrEmpty(input.Value?.Trim());
            return await _sysDictDataRep.AsQueryable()
                .Where(u => u.DictTypeId == input.DictTypeId)
                .WhereIF(code, u => u.Code.Contains(input.Code))
                .WhereIF(value, u => u.Code.Contains(input.Value))
                .OrderBy(u => u.Order)
                .ToPagedListAsync(input.Page, input.PageSize);
        }

        /// <summary>
        /// 获取字典值列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/sysDictData/list")]
        public async Task<List<SysDictData>> GetDictDataList([FromQuery] GetDataDictDataInput input)
        {
            return await GetDictDataListByDictTypeId(input.DictTypeId);
        }

        /// <summary>
        /// 增加字典值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysDictData/add")]
        public async Task AddDictData(AddDictDataInput input)
        {
            var isExist = await _sysDictDataRep.IsAnyAsync(u => (u.Code == input.Code || u.Value == input.Value) && u.DictTypeId == input.DictTypeId);
            if (isExist)
                throw Oops.Oh(ErrorCodeEnum.D3003);

            var dictData = input.Adapt<SysDictData>();
            await _sysDictDataRep.InsertAsync(dictData);
        }

        /// <summary>
        /// 更新字典值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysDictData/update")]
        public async Task UpdateDictData(UpdateDictDataInput input)
        {
            var isExist = await _sysDictDataRep.IsAnyAsync(u => u.Id == input.Id);
            if (!isExist) throw Oops.Oh(ErrorCodeEnum.D3004);

            isExist = await _sysDictDataRep.IsAnyAsync(u => (u.Value == input.Value || u.Code == input.Code) && u.DictTypeId == input.DictTypeId && u.Id != input.Id);
            if (isExist) throw Oops.Oh(ErrorCodeEnum.D3003);

            var dictData = input.Adapt<SysDictData>();
            await _sysDictDataRep.UpdateAsync(dictData);
        }

        /// <summary>
        /// 获取字典值详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysDictData/detail")]
        public async Task<SysDictData> GetDictData([FromQuery] DictDataInput input)
        {
            return await _sysDictDataRep.GetFirstAsync(u => u.Id == input.Id);
        }

        /// <summary>
        /// 删除字典值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysDictData/delete")]
        public async Task DeleteDictData(DeleteDictDataInput input)
        {
            var dictData = await _sysDictDataRep.GetFirstAsync(u => u.Id == input.Id);
            if (dictData == null)
                throw Oops.Oh(ErrorCodeEnum.D3004);

            await _sysDictDataRep.DeleteAsync(dictData);
        }

        /// <summary>
        /// 修改字典值状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysDictData/changeStatus")]
        public async Task ChangeDictDataStatus(ChageStatusDictDataInput input)
        {
            var dictData = await _sysDictDataRep.GetFirstAsync(u => u.Id == input.Id);
            if (dictData == null)
                throw Oops.Oh(ErrorCodeEnum.D3004);

            if (!Enum.IsDefined(typeof(StatusEnum), input.Status))
                throw Oops.Oh(ErrorCodeEnum.D3005);

            dictData.Status = (StatusEnum)input.Status;
            await _sysDictDataRep.UpdateAsync(dictData);
        }

        /// <summary>
        /// 根据字典类型Id获取字典值集合
        /// </summary>
        /// <param name="dictTypeId"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<List<SysDictData>> GetDictDataListByDictTypeId(long dictTypeId)
        {
            return await _sysDictDataRep.AsQueryable()
                .Where(u => u.DictTypeId == dictTypeId)
                .OrderBy(u => u.Order).ToListAsync();
        }

        /// <summary>
        /// 根据字典唯一编码获取下拉框集合
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("/sysDictData/DictDataDropdown/{code}")]
        public async Task<dynamic> GetDictDataDropdown(string code)
        {
            return await _sysDictDataRep.Context.Queryable<SysDictType, SysDictData>((a, b) =>
                new JoinQueryInfos(JoinType.Left, a.Id == b.DictTypeId))
                .Where(a => a.Code == code)
                .Select((a, b) => new
                {
                    Label = b.Value,
                    Value = b.Code
                }).ToListAsync();
        }
    }
}