using Microsoft.AspNetCore.Mvc;
using OnlineCodeGenerator.Models;

namespace OnlineCodeGenerator.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : Controller
    {
        #region 首页 +IActionResult Index()
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            DBModel db = new DBModel();
            return View(db);
        }
        #endregion

        #region 获得数据库 +IActionResult GetDb(DBModel db)
        /// <summary>
        /// 获得数据库
        /// </summary>
        /// <param name="db">数据对象</param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult GetDb(DBModel db)
        {
            if (ModelState.IsValid)
            {
                DBUtil.Util.GetDatabases(db);
            }
            return View("Index", db);
        }
        #endregion

        #region 获得数据表 +IActionResult GetDbTable(DBModel db)
        /// <summary>
        /// 获得数据表
        /// </summary>
        /// <param name="db">数据对象</param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult GetDbTable(DBModel db)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(db.Database))
            {
                DBUtil.Util.GetTables(db);
            }
            return View("Index", db);
        }
        #endregion

        #region 生成代码 +IActionResult Build(DBModel db)
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="db">数据对象</param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Build(DBModel db)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(db.Database) && !string.IsNullOrWhiteSpace(db.Table))
            {
                Poco poco = DBUtil.Util.BuildPoco(db);
                return View(poco);
            }
            return View("Index", db);
        }
        #endregion
    }
}
