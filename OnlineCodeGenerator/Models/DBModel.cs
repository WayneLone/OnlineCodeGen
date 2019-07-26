using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineCodeGenerator.Models
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    public class DBModel
    {
        /// <summary>
        /// IP
        /// </summary>
        [Required(ErrorMessage = "请填写IP")]
        public string IP { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请填写登录用户")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "请填写登录密码")]
        public string Password { get; set; }

        /// <summary>
        /// 代码命名空间
        /// </summary>
        [Required(ErrorMessage = "请填写命名空间")]
        public string CodeNameSpace { get; set; }

        /// <summary>
        /// 数据库集合
        /// </summary>
        public List<SelectListItem> DbList { get; set; }

        /// <summary>
        /// 表集合
        /// </summary>
        public List<SelectListItem> TableList { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 数据表
        /// </summary>
        public string Table { get; set; }
    }
}
