﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Core.Service.DataResources.Dto
{
    public class DataResourceInput : BaseIdInput
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual long Pid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 值 
        ///</summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
    }

    public class AddDataResourceInput : DataResourceInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "资源名称不能为空")]
        public override string Name { get; set; }
    }

    public class UpdateDataResourceInput : AddDataResourceInput
    {
    }

    public class DeleteDataResourceInput : BaseIdInput
    {
    }
}