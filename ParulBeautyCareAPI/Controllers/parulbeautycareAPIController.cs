using ParulBeautyCareAPI.Models.Logs;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParulBeautyCareAPI.Controllers
{
    public class parulbeautycareAPIController : ApiController
    {
        parulbeautycareEntities db = new parulbeautycareEntities();

        [HttpPost]
        [Route("api/parulbeautycareAPI/LoginCheckGet")]
        public IHttpActionResult LoginCheckGet([FromBody] LoginViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.result = db.PBLoginCheck(lvm.UserName, lvm.Password).SingleOrDefault().ToString();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "LoginCheckGet", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/LoginUserRtr")]
        public IHttpActionResult LoginUserRtr([FromBody] LoginViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.LoginUserList = db.PBLoginRtr(lvm.UserName, lvm.Password).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "LoginUserRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        #region UserManagement
        [HttpPost]
        [Route("api/parulbeautycareAPI/ModuleMasterRtr")]
        public IHttpActionResult ModuleMasterRtr([FromBody] ModuleViewModel lvm)
        {
            //Dictionary<string, object> objs = new Dictionary<string, object>();
            iNotifyLogger obj = new iNotifyLogger();
            //TestMasterViewModel re = new TestMasterViewModel();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.ModuleViewList = db.PBModuleMasterRtr(lvm.Action, lvm.ModuleId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "ModuleMasterRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/ModuleMasterInsUpd")]
        public IHttpActionResult ModuleMasterInsUpd([FromBody] ModuleViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBModuleMasterInsUpd(sm.ModuleId, sm.ModuleName.Trim(),sm.selfPage.ToString(), sm.ModulePriority,sm.FaIcon, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.UpdateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "ModuleMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/PageMasterRtr")]
        public IHttpActionResult PageMasterRtr([FromBody] PageViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.PageMasterList = db.PBPageMasterRtr(lvm.Action, lvm.PageId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "PageMasterRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/PageMasterInsUpd")]
        public IHttpActionResult PageMasterInsUpd([FromBody] PageViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBPageMasterInsUpd(sm.PageId, sm.PageName.Trim(), sm.PageUrl.ToString(), sm.CreateDate, sm.CreateUser, sm.IsActive,sm.PagePriority, sm.UpdateDate, sm.UpdateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "PageMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/IntePageModuleRtr")]
        public IHttpActionResult IntePageModuleRtr([FromBody] PageModuleViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.PageModuleInteList = db.PBIntePageModuleRtr(lvm.Action, lvm.ModuleId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "IntePageModuleRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/IntePageModuleInsUpd")]
        public IHttpActionResult IntePageModuleInsUpd([FromBody] PageModuleViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBIntePageModuleInsUpd(sm.InteId,sm.ModuleId,sm.PageId, sm.CreateDate, sm.CreateUser, sm.IsActive, sm.ModuleName, sm.PageName, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "IntePageModuleInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/InteRoleModuleRtr")]
        public IHttpActionResult InteRoleModuleRtr([FromBody] RoleModuleViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.RoleModuleInteList = db.PBInteRoleModuleRtr().ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "InteRoleModuleRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/InteRoleModuleInsUpd")]
        public IHttpActionResult InteRoleModuleInsUpd([FromBody] RoleModuleViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBInteRoleModuleInsUpd(sm.InteId,sm.RoleId, sm.ModuleId, sm.IsActive, sm.CreateDate, sm.CreateUser,  sm.Action,sm.MPInteId,sm.ViewRight==true?"1":"0",sm.InsertRight == true ? "1" : "0", sm.UpdateRight == true ? "1" : "0", sm.DeleteRight == true ? "1" : "0", sm.UpdateDate,sm.UpdateUser).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "InteRoleModuleInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/RoleMasterRetrieve")]
        public IHttpActionResult RoleMasterRetrieve([FromBody] RoleViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.RoleViewList = db.PBRoleMasterRetrieve(lvm.Action,lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "RoleMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion
    }

}