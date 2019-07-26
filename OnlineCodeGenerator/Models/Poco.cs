using System.Collections.Generic;

namespace OnlineCodeGenerator.Models
{
    /// <summary>
    /// C Sharp Poco
    /// </summary>
    public class Poco
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 类注释
        /// </summary>
        public string ClassComment { get; set; }

        /// <summary>
        /// 类中属性
        /// </summary>
        public List<PocoProperty> Properties { get; set; } = new List<PocoProperty>();
    }
}
