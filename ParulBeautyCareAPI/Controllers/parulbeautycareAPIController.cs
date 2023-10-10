using ParulBeautyCareAPI.Models.Logs;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareDbClasses.DataModels.ParulBeautyCareDatasetTableAdapters;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.InvoiceMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using ParulBeautyCareViewModel.ViewModel.StaffMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

        #region==> Dashboard

        #region==> Dashboard Count Rtr
        [HttpPost]
        [Route("api/parulbeautycareAPI/DashboardCountRtr")]
        public IHttpActionResult DashboardCountRtr([FromBody] DashboardCountViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.DashboardCountList = db.PBDashboardCountRtr(lvm.TodayDate, lvm.Action, lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "DashboardCountRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion

        #region==> Dashboard Weekly Appointment Chart Rtr
        [HttpPost]
        [Route("api/parulbeautycareAPI/DashboardWeeklyAppointmentChartRtr")]
        public IHttpActionResult DashboardWeeklyAppointmentChartRtr([FromBody] DashboardWeeklyChartViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.DashboardWeeklyAppointmentChartList = db.PBDashboardWeeklyAppointmentChart(lvm.Action, lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "DashboardWeeklyAppointmentChartRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion

        #region==> Today's Staff Services Retrieve
        [HttpPost]
        [Route("api/parulbeautycareAPI/DashboardstaffTodayServicesRtr")]
        public IHttpActionResult DashboardstaffTodayServicesRtr([FromBody] StaffServicesViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.StaffTodayServicesList = db.PBDashboardStaffTodayServicesRtr(lvm.TodayDate, lvm.Action, lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "DashboardstaffTodayServicesRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion

        #region==> Today's Appointments Retrieve
        [HttpPost]
        [Route("api/parulbeautycareAPI/DashboardAppointmentsRtr")]
        public IHttpActionResult DashboardAppointmentsRtr([FromBody] AppointmentsViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.AppointmentsList = db.PBDashboardAppointmentsRtr(lvm.TodayDate, lvm.Action,lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "DashboardAppointmentsRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion

        #endregion

        #region ==> UserManagement
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
                    sm.result = db.PBModuleMasterInsUpd(sm.ModuleId, sm.ModuleName == null ? "" : sm.ModuleName.Trim(), sm.selfPage.ToString() == null ? "" : sm.selfPage.ToString(), sm.ModulePriority, sm.FaIcon, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.UpdateUser, sm.Action).FirstOrDefault();
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
                    sm.result = db.PBPageMasterInsUpd(sm.PageId, sm.PageName.Trim(), sm.PageUrl.ToString(), sm.CreateDate, sm.CreateUser, sm.IsActive, sm.PagePriority, sm.UpdateDate, sm.UpdateUser, sm.Action).FirstOrDefault();
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
                    sm.result = db.PBIntePageModuleInsUpd(sm.InteId, sm.ModuleId, sm.PageId, sm.CreateDate, sm.CreateUser, sm.IsActive, sm.ModuleName, sm.PageName, sm.Action).FirstOrDefault();
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
                    sm.result = db.PBInteRoleModuleInsUpd(sm.InteId, sm.RoleId, sm.ModuleId, sm.IsActive, sm.CreateDate, sm.CreateUser, sm.Action, sm.MPInteId, sm.ViewRight == true ? "1" : "0", sm.InsertRight == true ? "1" : "0", sm.UpdateRight == true ? "1" : "0", sm.DeleteRight == true ? "1" : "0", sm.UpdateDate, sm.UpdateUser).FirstOrDefault();
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
        [Route("api/parulbeautycareAPI/MenuRtr")]
        public IHttpActionResult MenuRtr([FromBody] MenuViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.MenuList = db.PBMenuRtr(lvm.RoleId.ToString()).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "MenuRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }


        [HttpPost]
        [Route("api/parulbeautycareAPI/MenuRightsRtr")]
        public IHttpActionResult MenuRightsRtr([FromBody] MenuRightsViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.MenuRightsList = db.PBMenuRightsRtr(lvm.Usercode.ToString(), lvm.PageName).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "MenuRightsRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        #endregion

        #region==> Master

        #region==> RoleMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/RoleMasterRetrieve")]
        public IHttpActionResult RoleMasterRetrieve([FromBody] RoleMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.RoleMasterList = db.PBRoleMasterRetrieve(lvm.Action, lvm.CompanyCode, "").ToList();
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
        [HttpPost]
        [Route("api/parulbeautycareAPI/RoleMasterInsUpd")]
        public IHttpActionResult RoleMasterInsUpd([FromBody] RoleMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBRoleMasterInsUpd(sm.RoleId, sm.RoleName.Trim(), sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "RoleMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> StatusMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/StatusMasterRetrieve")]
        public IHttpActionResult StatusMasterRetrieve([FromBody] StatusMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.StatusMasterList = db.PBStatusMasterRetrieve(lvm.Action, lvm.CompanyCode, "").ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "StatusMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/StatusMasterInsUpd")]
        public IHttpActionResult StatusMasterInsUpd([FromBody] StatusMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBStatusMasterInsUpd(sm.StatusId, sm.StatusName, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "StatusMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==>TimeSlotMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/TimeSlotMasterRetrieve")]
        public IHttpActionResult TimeSlotMasterRetrieve([FromBody] TimeSlotMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.TimeSlotMasterList = db.PBTimeSlotMasterRetrieve(lvm.Action, lvm.CompanyCode, "").ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "TimeSlotMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/TimeSlotMasterInsUpd")]
        public IHttpActionResult TimeSlotMasterInsUpd([FromBody] TimeSlotMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBTimeSlotMasterInsUpd(sm.SlotId, sm.SlotName, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "TimeSlotMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==>CategoryMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/CategoryMasterRetrieve")]
        public IHttpActionResult CategoryMasterRetrieve([FromBody] CategoryMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.CategoryMasterList = db.PBCategoryMasterRetrieve(lvm.Action, lvm.CompanyCode, lvm.CategoryId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                    return ResponseMessage(response);
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "CategoryMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/CategoryMasterInsUpd")]
        public IHttpActionResult CategoryMasterInsUpd([FromBody] CategoryMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBCategoryMasterInsUpd(sm.CategoryId, sm.CategoryName, sm.Description, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "CategoryMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==>StaffMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/StaffMasterRetrieve")]
        public IHttpActionResult StaffMasterRetrieve([FromBody] StaffMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.StaffMasterList = db.PBStaffMasterRtr(lvm.CompanyCode, lvm.Action, lvm.StaffId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "StaffMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/StaffMasterInsUpd")]
        public IHttpActionResult StaffMasterInsUpd([FromBody] StaffMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBStaffMasterInsUpd(sm.StaffId, sm.StaffCode, sm.StaffName, sm.Contact, sm.Gender, sm.Password, sm.RoleId, sm.IsActive, sm.CompanyCode, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.UpdateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "StaffMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==>ProductTypeMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/ProductTypeMasterRetrieve")]
        public IHttpActionResult ProductTypeMasterRetrieve([FromBody] ProductTypeMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.ProductTypeMasterList = db.PBProductTypeMasterRetrieve(lvm.Action, lvm.CompanyCode, lvm.ProductTypeId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "ProductTypeMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/ProductTypeMasterInsUpd")]
        public IHttpActionResult ProductTypeMasterInsUpd([FromBody] ProductTypeMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBProductTypeMasterInsUpd(sm.ProductTypeId, sm.ProductTName, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "ProductTypeMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> ProductMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/ProductMasterRetrieve")]
        public IHttpActionResult ProductMasterRetrieve([FromBody] ProductMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.ProductMasterList = db.PBProductMasterRtr(lvm.CompanyCode, lvm.Action, lvm.ProductId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "ProductMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/ProductMasterInsUpd")]
        public IHttpActionResult ProductMasterInsUpd([FromBody] ProductMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBProductMasterInsUpd(sm.ProductId, sm.ProductCode, sm.ProductName, sm.ProductImage,sm.NumberOfPerson, sm.ProductTypeId, sm.IsActive, sm.CompanyCode, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.UpdateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "ProductMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> SubCategoryMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/SubCategoryMasterRetrieve")]
        public IHttpActionResult SubCategoryMasterRetrieve([FromBody] SubCategoryMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.SubCategoryMasterList = db.PBSubCategoryMasterRetrieve(lvm.Action, lvm.CompanyCode, lvm.CategoryId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "SubCategoryMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/SubCategoryMasterInsUpd")]
        public IHttpActionResult SubCategoryMasterInsUpd([FromBody] SubCategoryMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBSubCategoryMasterInsUpd(sm.SubCategoryId, sm.SubCategoryName, sm.CategoryId, sm.IsMultiPerson, sm.NumberOfPerson, sm.YearId, sm.NoOfSitting, sm.TimeDuraion, sm.Amount, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.DayInterval, sm.Incentive, sm.GSTPercentage, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "SubCategoryMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region ==> YearMaster
        [HttpPost]
        [Route("api/parulbeautycareAPI/YearMasterRetrieve")]
        public IHttpActionResult YearMasterRetrieve([FromBody] YearMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.YearMasterList = db.PBYearMasterRetrieve(lvm.Action, lvm.CompanyCode, lvm.YearId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "YearMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/YearMasterInsUpd")]
        public IHttpActionResult YearMasterInsUpd([FromBody] YearMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBYearMasterInsUpd(sm.YearId, sm.Yearperiod, sm.FromDate, sm.ToDate, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "YearMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> Department Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/DepartmentMasterRetrieve")]
        public IHttpActionResult DepartmentMasterRetrieve([FromBody] DepartmentMasterViewModel dpt)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    dpt.DepartmentList = db.PBDepartmentMasterRetrieve(dpt.Action, dpt.CompanyCode, dpt.DepartmentId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, dpt);
                    dpt.success = "true";

                }
            }
            catch (Exception e)
            {
                dpt.success = "false";
                dpt.message = e.Message;
                obj.LogMessage("APIController", "ProductMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(dpt);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/DepartmentMasterInsUpd")]
        public IHttpActionResult DepartmentMasterInsUpd([FromBody] DepartmentMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBDepartmentMasterInsUpd(sm.DepartmentId, sm.DepartmentName,sm.DeptAbrv,sm.GSTNo,sm.DeptAddress, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "DepartmentMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> Vendor Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/VendorMasterRetrieve")]
        public IHttpActionResult VendorMasterRetrieve([FromBody] VendorMasterViewModel vnd)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    vnd.VendorList = db.PBVendorMasterRetrieve(vnd.Action, vnd.CompanyCode, vnd.VendorId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, vnd);
                    vnd.success = "true";

                }
            }
            catch (Exception e)
            {
                vnd.success = "false";
                vnd.message = e.Message;
                obj.LogMessage("APIController", "VenderMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(vnd);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/VendorMasterInsUpd")]
        public IHttpActionResult VendorMasterInsUpd([FromBody] VendorMasterViewModel vnd)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    vnd.result = db.PBVendorMasterInsUpd(vnd.VendorId, vnd.VendorCode, vnd.VendorName, vnd.Email, vnd.Phone, vnd.Address, vnd.IsActive, vnd.CompanyCode, vnd.CreateDate, vnd.CreateUser, vnd.UpdateDate, vnd.UpdateUser,vnd.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, vnd);
                    vnd.success = "true";
                }
            }
            catch (Exception e)
            {
                vnd.success = "false";
                vnd.message = e.Message;
                obj.LogMessage("APIController", "VendorMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(vnd);
        }
        #endregion

        #region==> Package Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/PackageMasterRtr")]
        public IHttpActionResult PackageMasterRtr([FromBody] PackageMasterViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.PackageList = db.PBPackageMasterRtr(lvm.Action, lvm.CompanyCode, lvm.PackageId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "PackageMasterRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }


        [HttpPost]
        [Route("api/parulbeautycareAPI/PackageMasterInsUpd")]
        public IHttpActionResult PackageMasterInsUpd([FromBody] PackageMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBPackageMasterInsUpd(sm.PackageId, sm.PackageName, sm.PackageAmount, sm.IsActive, sm.CompanyCode, sm.CreateUser, sm.CreateDate, sm.UpdateUser, sm.UpdateDate, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "PackageMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> Integration Package Service Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/IntePackageServiceMasterRetrieve")]
        public IHttpActionResult IntePackageServiceMasterRetrieve([FromBody] IntePackageServiceMasterViewModel dpt)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    dpt.IntePackageServiceMasterList = db.PBIntePackageServiceMasterRetrieve(dpt.Action, dpt.CompanyCode, dpt.IntePackageServiceId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, dpt);
                    dpt.success = "true";

                }
            }
            catch (Exception e)
            {
                dpt.success = "false";
                dpt.message = e.Message;
                obj.LogMessage("APIController", "IntePackageServiceMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(dpt);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/IntePackageServiceMasterInsUpd")]
        public IHttpActionResult IntePackageServiceMasterInsUpd([FromBody] IntePackageServiceMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBIntePackageServiceMasterInsUpd(sm.IntePackageServiceId, sm.PackageId, sm.ProductId, sm.ServiceId, sm.IsActive, sm.CompanyCode, sm.CreateUser, sm.CreateDate, sm.UpdateUser, sm.UpdateDate, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "IntePackageServiceMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> Integration Service Product Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/InteServiceProductMasterRetrieve")]
        public IHttpActionResult InteServiceProductMasterRetrieve([FromBody] InteServiceProductMasterViewModel dpt)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    dpt.InteServiceProductMasterList = db.PBInteServiceProductMasterRetrieve(dpt.Action, dpt.CompanyCode, dpt.InteServiceProductId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, dpt);
                    dpt.success = "true";

                }
            }
            catch (Exception e)
            {
                dpt.success = "false";
                dpt.message = e.Message;
                obj.LogMessage("APIController", "InteServiceProductMasterRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(dpt);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/InteServiceProductMasterInsUpd")]
        public IHttpActionResult InteServiceProductMasterInsUpd([FromBody] InteServiceProductMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBInteServiceProductMasterInsUpd(sm.InteServiceProductId, sm.ServiceId, sm.ProductId, sm.IsActive, sm.CompanyCode, sm.CreateUser, sm.CreateDate, sm.UpdateUser, sm.UpdateDate, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "InteServiceProductMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> Item Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/ItemMasterRtr")]
        public IHttpActionResult ItemMasterRtr([FromBody] ItemMasterViewModel itm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    itm.ItemMasterList = db.PBItemMasterRetrieve(itm.Action, itm.CompanyCode,itm.ItemId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, itm);
                    itm.success = "true";
                }
            }
            catch (Exception e)
            {
                itm.success = "false";
                itm.message = e.Message;
                obj.LogMessage("APIController", "ItemMasterRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(itm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/ItemMasterInsUpd")]
        public IHttpActionResult ItemMasterInsUpd([FromBody] ItemMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBItemMasterInsUpd(sm.ItemId, sm.ItemName,  sm.CompanyCode, sm.IsActive,  sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "ItemMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> GST Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/GSTMasterRtr")]
        public IHttpActionResult GSTMasterRtr([FromBody] GSTMasterViewModel itm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    itm.GSTMasterList = db.PBGSTMasterRetrieve(itm.Action, itm.CompanyCode, itm.GSTId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, itm);
                    itm.success = "true";
                }
            }
            catch (Exception e)
            {
                itm.success = "false";
                itm.message = e.Message;
                obj.LogMessage("APIController", "GSTMasterRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(itm);
        }



        [HttpPost]
        [Route("api/parulbeautycareAPI/GSTMasterInsUpd")]
        public IHttpActionResult GSTMasterInsUpd([FromBody] GSTMasterViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {

                    sm.result = db.PBGSTMasterInsUpd(sm.GSTId, sm.GSTName, sm.GSTPerc, sm.FromDate, sm.ToDate, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.updatedate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "GSTMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #endregion

        #region =====> Stock Management API

        #region ==> Stock Purchase
        [HttpPost]
        [Route("api/parulbeautycareAPI/StockPurchaseRetrieve")]
        public IHttpActionResult StockPurchaseRetrieve([FromBody] StockPurchaseViewModel spvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    // spvm.Action = "all";

                    spvm.StockPurchaseList = db.PBStockPurchaseRetrieve(spvm.Action, spvm.CompanyCode, spvm.PurchaseId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, spvm);
                    spvm.success = "true";
                }
            }
            catch (Exception e)
            {
                spvm.success = "false";
                spvm.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(spvm);
        }
        [HttpPost]
        [Route("api/parulbeautycareAPI/StockPurchaseInsUpd")]
        public IHttpActionResult StockPurchaseInsUpd([FromBody] StockPurchaseViewModel spvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {

                    spvm.result = db.PBStockPurchaseMasterInsUpd(spvm.PurchaseId, spvm.CompanyCode, spvm.CreateDate, spvm.CreateUser, spvm.UpdateDate, spvm.UpdateUser, spvm.ProductId, spvm.ProductTypeId, spvm.Quantity, spvm.MfgDate, spvm.ExpDate, spvm.PurchaseDate, spvm.VendorId, spvm.DepartmentId,spvm.GSTId,spvm.Price, spvm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, spvm);
                    spvm.success = "true";
                }
            }
            catch (Exception e)
            {
                spvm.success = "false";
                spvm.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(spvm);
        }
        #endregion

        #region ==> Stock Allocation

        [HttpPost]
        [Route("api/parulbeautycareAPI/StockAllocationRetrieve")]
        public IHttpActionResult StockAllocationRetrieve([FromBody] StockAllocationMasterViewModel savm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    // spvm.Action = "all";

                    savm.StockAllocationList = db.PBStockAllocationRetrieve(savm.Action, savm.StockAllocationId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, savm);
                    savm.success = "true";
                }
            }
            catch (Exception e)
            {
                savm.success = "false";
                savm.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(savm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/StockAllocationInsUpd")]
        public IHttpActionResult StockAllocationInsUpd([FromBody] StockAllocationMasterViewModel spvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {

                    StockDetailMasterViewModel sdmv = new StockDetailMasterViewModel();
                    sdmv.CreateUser = User.Identity.Name;
                    sdmv.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sdmv.CreateUser = spvm.AllocateUser;

                    spvm.result = db.PBStockAllocationMasterInsUpd(spvm.StockAllocationId, spvm.PurchaseId, spvm.Qty.ToString(), spvm.AllocationDate, spvm.AllocateUser, spvm.StaffId, spvm.UpdateDate, spvm.UpdateUser, sdmv.StockUsed, sdmv.CreateDate, sdmv.CreateUser, sdmv.NoOfPerson, sdmv.PersonUsed,spvm.ExpDate, spvm.Price, spvm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, spvm);
                    spvm.success = "true";
                }
            }
            catch (Exception e)
            {
                spvm.success = "false";
                spvm.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(spvm);
        }
        #endregion

        #region ==> Stock Transfer
        [HttpPost]
        [Route("api/parulbeautycareAPI/AvailableStockRetrieve")]
        public IHttpActionResult AvailableStockRetrieve([FromBody] StockTransferHeaderViewModel savm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    savm.AvailableStockList = db.PBAvailableStockRtr(savm.FromStaffId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, savm);
                    savm.success = "true";
                }
            }
            catch (Exception e)
            {
                savm.success = "false";
                savm.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(savm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/StockTransferIns")]
        public IHttpActionResult StockTransferIns([FromBody] StockTransferHeaderViewModel sthvm)
        {
            iNotifyLogger obj = new iNotifyLogger();

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(
                       new DataColumn[10] {
                new DataColumn("StockDetailId", typeof(string)),
                new DataColumn("StockHeaderId", typeof(string)),
                new DataColumn("ProductId", typeof(string)),
                new DataColumn("Qty", typeof(string)),
                new DataColumn("TotalPerson", typeof(string)),
                new DataColumn("PersonAvailable", typeof(string)),
                new DataColumn("AutoSrNo", typeof(string)),
                new DataColumn("Barcode", typeof(string)),
                new DataColumn("ExpDate", typeof(string)),
                new DataColumn("Price", typeof(string))
                       });
                if (sthvm.StockTransferTypeTable != null)
                {
                    foreach (StockTransferTypeViewModel item in sthvm.StockTransferTypeTable)
                    {
                        string StockDetailId = item.StockDetailId;
                        string StockHeaderId = item.StockHeaderId;
                        string ProductId = item.ProductId;
                        string Qty = item.Qty;
                        string TotalPerson = item.TotalPerson;
                        string PersonAvailable = item.PersonAvailable;
                        string AutoSrNo = item.AutoSrNo;
                        string Barcode = item.Barcode;
                        string ExpDate = item.ExpDate;
                        string Price = item.Price;
                        dt.Rows.Add(StockDetailId, StockHeaderId, ProductId, Qty, TotalPerson, PersonAvailable, AutoSrNo, Barcode,ExpDate,Price);
                    }
                }

                QueriesTableAdapter re = new QueriesTableAdapter();
                sthvm.result = re.PBStockTransferInsert(sthvm.StockHeaderId, sthvm.TransferDate, sthvm.FromStaffId, sthvm.ToStaffId, sthvm.CreateDate, sthvm.CreateUser, sthvm.UpdateDate, sthvm.UpdateUser, sthvm.Action, dt).ToString();
                var response = Request.CreateResponse(HttpStatusCode.OK, sthvm);
                sthvm.success = "true";

            }/*{ "Error converting data type nvarchar to numeric.\r\nThe data for table-valued parameter \"@type\" doesn't conform to the table type of the parameter. SQL Server error is: 8114, state: 5\r\nThe statement has been terminated."}*/
            catch (Exception e)
            {
                sthvm.success = "false";
                sthvm.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sthvm);
        }
        #endregion

        #region==> Stock Allocated to Staff Report
        [HttpPost]
        [Route("api/parulbeautycareAPI/StockAllocatedToStaffRetrieve")]
        public IHttpActionResult StockAllocatedToStaffRetrieve([FromBody] StockTransferHeaderViewModel savm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    savm.StockAllocatedToStaffList = db.PBAllocatedStockToStaffRtr(savm.Action, savm.StaffCode, savm.ProductId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, savm);
                    savm.success = "true";
                }
            }
            catch (Exception e)
            {
                savm.success = "false";
                savm.message = e.Message;
                obj.LogMessage("APIController", "StockAllocatedToStaffRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(savm);
        }
        #endregion

        #endregion

        #region=====> Booking Management API

        #region ==> Booking Staff Allocation
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingHeaderRetrieve")]
        public IHttpActionResult BookingHeaderRetrieve([FromBody] BookingHeaderViewModel bhvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    bhvm.BookingHeaderList = db.PBBookingHeaderRtr(bhvm.BookingId,bhvm.FromDate,bhvm.ToDate, bhvm.Action, bhvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, bhvm);
                    bhvm.success = "true";
                }
            }
            catch (Exception e)
            {
                bhvm.success = "false";
                bhvm.message = e.Message;
                obj.LogMessage("APIController", "BookingHeaderRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(bhvm);
        }

        #region==> Booking Services Retrieve
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingServicesRetrieve")]
        public IHttpActionResult BookingServicesRetrieve([FromBody] BookingServicesViewModel bhvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    bhvm.BookingServicesList = db.PBBookingServicesRtr(bhvm.BookingId, bhvm.Action, bhvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, bhvm);
                    bhvm.success = "true";
                }
            }
            catch (Exception e)
            {
                bhvm.success = "false";
                bhvm.message = e.Message;
                obj.LogMessage("APIController", "BookingServicesRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(bhvm);
        }
        #endregion

        #region==> Booking Services Delete
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingServiceDelete")]
        public IHttpActionResult BookingServiceDelete([FromBody] BookAppointmentViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.Result = db.PBBookingServiceDelete(sm.BookingServiceId.ToString(), sm.SubCategoryId.ToString(), sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.Message = e.Message;
                obj.LogMessage("APIController", "BookingServiceDelete", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region ==> Booking Package Retrieve
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingPackageRetrieve")]
        public IHttpActionResult BookingPackageRetrieve([FromBody] BookingPackageViewModel bhvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    bhvm.PBIntePackageServiceList = db.PBIntePackageServiceRtr(bhvm.Action, bhvm.PackageId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, bhvm);
                    bhvm.success = "true";
                }
            }
            catch (Exception e)
            {
                bhvm.success = "false";
                bhvm.message = e.Message;
                obj.LogMessage("APIController", "BookingHeaderRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(bhvm);
        }
        #endregion

        #region ==> Book Appointment Insert
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingAppointmentInsUpd")]
        public IHttpActionResult BookingAppointmentInsUpd([FromBody] BookAppointmentViewModel mv)
        {
            iNotifyLogger obj = new iNotifyLogger();
            QueriesTableAdapter db1 = new QueriesTableAdapter();
            try
            {
                mv.Result = db1.PBBookingInsert_new(mv.BookingId.ToString(), mv.AdvanceBookingId.ToString(), mv.CustomerId.ToString(), mv.ContactNo, mv.CustomerName, mv.DepartmentId, mv.Address, mv.FunctionDate, mv.CreateDate, mv.CreateUser, "", "", "", mv.Amount, "", "", mv.BookingAmount, "", "", mv.CompanyCode, mv.FunctionDate, mv.ReadyTime, "1", mv.AppointmentType, mv.Action, mv.mytable, mv.mytable1).ToString();
                var response = Request.CreateResponse(HttpStatusCode.OK, mv);
                mv.success = "true";
            }
            catch (Exception e)
            {
                mv.success = "false";
                mv.Message = e.Message;
                obj.LogMessage("APIController", "BookingAppointmentInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(mv);
        }
        #endregion

        #region==> Booking Detail
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingDetailRetrieve")]
        public IHttpActionResult BookingDetailRetrieve([FromBody] BookingDetailViewModel bdvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    bdvm.BookingDetailList = db.PBBookingDetailRtr(bdvm.BookingId, bdvm.Action, bdvm.CompanyCode,bdvm.StaffUserCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, bdvm);
                    bdvm.success = "true";
                }
            }
            catch (Exception e)
            {
                bdvm.success = "false";
                bdvm.message = e.Message;
                obj.LogMessage("APIController", "BookingDetailRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(bdvm);
        }


        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingDetailInsUpd")]
        public IHttpActionResult BookingDetailInsUpd([FromBody] BookingDetailViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBBookingDetailInsUpd(sm.BookingDetailId, sm.BookingId, sm.CategoryId, sm.SubCategoryId, sm.AllocatedTo, sm.AllocationDate, sm.AppointmentDate, sm.AppointmentTime, sm.DoneBy, sm.DoneDate, sm.Amount, sm.Discount, sm.FinalAmount, sm.Status, sm.CreateDate, sm.CreateUser, sm.UpdateDate, sm.UpdateUser, sm.CustomerName, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "CategoryMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion
        #endregion

        #region==> ServiceComplition Master
        [HttpPost]
        [Route("api/parulbeautycareAPI/PaymentHistoryInsUpd")]
        public IHttpActionResult PaymentHistoryInsUpd([FromBody] PaymentHistoryViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBPaymentHistoryInsUpd(sm.BookingId, sm.BookingCode, sm.PaymentRecievedDate, sm.PaymentType, sm.ChequeNo, sm.GPayNo, sm.ReceivedAmount, sm.PreviousAmount, sm.CompanyCode, sm.CreateDate, sm.CreateUser, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "CategoryMasterInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region ==> Check In Out

        [HttpPost]
        [Route("api/parulbeautycareAPI/CheckInCheckOutRetrieve")]
        public IHttpActionResult CheckInCheckOutRetrieve([FromBody] CheckInCheckOutViewModel chkinout)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    chkinout.CheckInCheckOutList = db.PBCheckInCheckOutRetrieve(chkinout.Action, chkinout.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, chkinout);
                    chkinout.success = "true";
                }
            }
            catch (Exception e)
            {
                chkinout.success = "false";
                chkinout.message = e.Message;
                obj.LogMessage("APIController", "BookingHeaderRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(chkinout);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/CheckInCheckOutIns")]
        public IHttpActionResult CheckInCheckOutIns(CheckInCheckOutViewModel chkinout)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    StockDetailMasterViewModel sdmv = new StockDetailMasterViewModel();
                    //sdmv.CreateUser = User.Identity.Name;
                    //sdmv.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //sdmv.CreateUser = spvm.AllocateUser;
                    chkinout.result = db.PBCheckInCheckOutIns(chkinout.CheckId, chkinout.CustomerName, chkinout.ContactNo, chkinout.Address, chkinout.CheckinDateTime, chkinout.CheckoutDateTime, chkinout.WaitingTime, chkinout.BookingId, chkinout.CompanyCode, chkinout.CreateDate, chkinout.CreateUser, chkinout.UpdateDate, chkinout.UpdateUser,chkinout.Note ,chkinout.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, chkinout);
                    chkinout.success = "true";
                }
            }
            catch (Exception e)
            {
                chkinout.success = "false";
                chkinout.message = e.Message;
                obj.LogMessage("APIController", "CheckInCheckOutIns", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(chkinout);
        }


        #endregion

        #endregion

        #region ==> Enquiry
        [HttpPost]
        [Route("api/parulbeautycareAPI/EnquiryIns")]
        public IHttpActionResult EnquiryIns(EnquiryViewModel enqvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    enqvm.result = db.PBEnquiryIns(enqvm.PersonName,enqvm.PersonEmailId,enqvm.PersonContactNo, enqvm.PersonAddress, enqvm.PersonEnquiryFor,enqvm.CreateDate,enqvm.CreateUser,enqvm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, enqvm);
                    enqvm.success = "true";
                }
            }
            catch (Exception e)
            {
                enqvm.success = "false";
                enqvm.message = e.Message;
                obj.LogMessage("APIController", "EnquiryIns", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(enqvm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/EnquiryRetrieve")]
        public IHttpActionResult EnquiryRetrieve([FromBody] EnquiryViewModel envm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    envm.EnquiryList = db.PBEnquiryRtr(envm.Action).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, envm);
                    envm.success = "true";
                }
            }
            catch (Exception e)
            {
                envm.success = "false";
                envm.message = e.Message;
                obj.LogMessage("APIController", "EnquiryRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(envm);
        }
        #endregion

        #region ==> Invoice (Billing)
        [HttpPost]
        [Route("api/parulbeautycareAPI/BillingRetrieve")]
        public IHttpActionResult BillingRetrieve([FromBody] InvoiceDetailViewModel inm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    inm.BillRtr = db.PBBillingRetrieve(inm.Action, inm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, inm);
                    inm.success = "true";
                }
            }
            catch (Exception e)
            {
                inm.success = "false";
                inm.message = e.Message;
                obj.LogMessage("APIController", "BillingRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(inm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/BillingDetailRetrieve")]
        public IHttpActionResult BillingDetailRetrieve([FromBody] BillDetailViewModel inm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    inm.BillDetailList = db.PBBillingDetailRetrieve(inm.Action,inm.BookingId).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, inm);
                    inm.success = "true";
                }
            }
            catch (Exception e)
            {
                inm.success = "false";
                inm.message = e.Message;
                obj.LogMessage("APIController", "BillingDetailRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(inm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/BillingDetailInsUpd")]
        public IHttpActionResult BillingDetailInsUpd([FromBody] BillDetailViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.result = db.PBBillingDetailInsert(sm.BillingDetailId,sm.BillingId, sm.BookingId,sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.success = "true";
                }
            }
            catch (Exception e)
            {
                sm.success = "false";
                sm.message = e.Message;
                obj.LogMessage("APIController", "BillingDetailInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }

        [HttpPost]
        [Route("api/parulbeautycareAPI/GenBillingCode")]
        public IHttpActionResult GenBillingCode(InvoiceDetailViewModel inm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    inm.NewBillingCode = db.PBGenBillingCode(inm.DeptAbbr, inm.CompanyCode).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, inm);
                    inm.success = "true";
                }

            }
            catch (Exception e)
            {
                inm.success = "false";
                inm.message = e.Message;
                obj.LogMessage("APIController", "GenBillingCode", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(inm);
        }

        #region==> Invoice Insert Update
        [HttpPost]
        [Route("api/parulbeautycareAPI/InvoiceInsUpd")]
        public IHttpActionResult InvoiceInsUpd(InvoiceDetailViewModel inm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (QueriesTableAdapter hsb = new QueriesTableAdapter())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(
                      new DataColumn[8] {
                            new DataColumn("BillingDetailId", typeof(string)),
                            new DataColumn("CategoryId", typeof(string)),
                            new DataColumn("SubCategoryId", typeof(string)),
                            new DataColumn("ItemId", typeof(string)),
                            new DataColumn("Qty", typeof(string)),
                            new DataColumn("Remark", typeof(string)),
                            new DataColumn("Amount", typeof(string)),
                            new DataColumn("FinalAmount", typeof(string))

                      });
                    int cnt = 0;
                    foreach (var item in inm.BillingDetailsTable)
                    {
                        string BillingDetailId = item.BillingDetailId;
                        string CategoryId = item.CategoryId;
                        string SubCategoryId = item.SubCategoryId;
                        string ItemId = item.ItemId;
                        string Qty = item.Qty;
                        string Remark = item.Remark;
                        string Amount = item.Amount != null ? item.Amount.ToString() : null;
                        string FinalAmount = item.FinalAmount != null ? item.FinalAmount.ToString() : null;
                        dt.Rows.Add(BillingDetailId,CategoryId, SubCategoryId, ItemId, Qty, Amount, FinalAmount, Remark);
                        cnt++;
                    }
                    if(inm.Action== "insert")
                    {
                        inm.result = hsb.PBBillingInsertUpdate("", inm.BookingId, inm.BookingCode, inm.DepartmentId, inm.BillCode, inm.CustomerId, inm.ContactNo, inm.CustomerName, inm.Address, inm.BillDate, inm.CreateDate, inm.CreateUser, inm.UpdateDate, inm.UpdateUser, inm.BaseAmount != null ? inm.BaseAmount.ToString() : null, inm.DiscountPerc != null ? inm.DiscountPerc.ToString() : null, inm.Discount != null ? inm.Discount.ToString() : null, inm.GSTPerc != null ? inm.GSTPerc.ToString() : null, inm.GSTAmount != null ? inm.GSTAmount.ToString() : null, inm.FinalAmount != null ? inm.FinalAmount.ToString() : null, inm.CompanyCode, inm.Action, dt).ToString();
                        var response = Request.CreateResponse(HttpStatusCode.OK, inm);
                        inm.success = "true";
                    }
                    else
                    {
                        inm.result = hsb.PBBillingInsertUpdate(inm.BillId, inm.BookingId, "", "", inm.BillCode, "", "", "", "", "", "", "", inm.UpdateDate, inm.UpdateUser, inm.BaseAmount != null ? inm.BaseAmount.ToString() : null, inm.DiscountPerc != null ? inm.DiscountPerc.ToString() : null, inm.Discount != null ? inm.Discount.ToString() : null, inm.GSTPerc != null ? inm.GSTPerc.ToString() : null, inm.GSTAmount != null ? inm.GSTAmount.ToString() : null, inm.FinalAmount != null ? inm.FinalAmount.ToString() : null, "", inm.Action, dt).ToString();
                        var response = Request.CreateResponse(HttpStatusCode.OK, inm);
                        inm.success = "true";
                    }
                }
            }
            catch (Exception e)
            {
                inm.success = "false";
                inm.message = e.Message;
                obj.LogMessage("APIController", "InvoiceInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(inm);
        }
        #endregion

        [HttpPost]
        [Route("api/parulbeautycareAPI/InvoiceReport")]
        public IHttpActionResult InvoiceReport(BillReportViewModel inm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    inm.DateWiseBillReportList = db.PB_DateWiseBillReportRtr( inm.FromDate, inm.ToDate, string.IsNullOrEmpty(inm.BookingId) ? null : inm.BookingId, string.IsNullOrEmpty(inm.DepartmentId) ? null : inm.DepartmentId, inm.Action).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, inm);
                    inm.success = "true";
                }
            }
            catch (Exception e)
            {
                inm.success = "false";
                inm.message = e.Message;
                obj.LogMessage("APIController", "InvoiceReport", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(inm);
        }
        #endregion

        #region ==> Advance Book

        [HttpPost]
        [Route("api/parulbeautycareAPI/AdvanceBookingRtr")]
        public IHttpActionResult AdvanceBookingRtr([FromBody] AdvanceBookingViewModel itm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    itm.AdvanceBookingList = db.PB_AdvanceBookingRtr(itm.NewFromDate, itm.NewToDate, itm.AdvanceBookingId.ToString(), itm.DepartmentId, itm.Action).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, itm);
                    itm.Success = "true";
                }
            }
            catch (Exception e)
            {
                itm.Success = "false";
                itm.Message = e.Message;
                obj.LogMessage("APIController", "AdvanceBookingRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(itm);
        }


        [HttpPost]
        [Route("api/parulbeautycareAPI/AdvanceBookingInsUpd")]
        public IHttpActionResult AdvanceBookingInsUpd([FromBody] AdvanceBookingViewModel sm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    sm.Result = db.PBAdvanceBookingInsUpd(sm.AdvanceBookingId.ToString(), sm.Name, sm.ContactNo, sm.Address, sm.SPersonName, sm.SPersonContact, sm.SPersonAddress, sm.BridalDeposit, sm.SidersDeposit, sm.TotalDeposit, sm.NumOfSiders, sm.FunctionDate, sm.DepartmentId, sm.BeforeRemark, sm.AfterRemark, sm.CompanyCode, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.UpdateUser, sm.PaymentType, sm.ChequeNumber, sm.Gpayno, sm.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, sm);
                    sm.Success = "true";
                }
            }
            catch (Exception e)
            {
                sm.Success = "false";
                sm.Message = e.Message;
                obj.LogMessage("APIController", "AdvanceBookingInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(sm);
        }
        #endregion

        #region==> Staff Management API

        #region==> Staff Dashboard

        #region==> Today's Appointment Work Retrieve
        [HttpPost]
        [Route("api/parulbeautycareAPI/StaffDashboardTodayServicesRtr")]
        public IHttpActionResult StaffDashboardTodayServicesRtr([FromBody] StaffDashboardServicesViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.StaffDashboardTodayServicesList = db.PBStaffDashboardTodayServicesRtr(lvm.TodayDate, lvm.StaffCode, lvm.Action, lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "StaffDashboardTodayServicesRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion

        #region==> Today's Appointment Retrieve
        [HttpPost]
        [Route("api/parulbeautycareAPI/StaffDashboardAppointmentsRtr")]
        public IHttpActionResult StaffDashboardAppointmentsRtr([FromBody] StaffAppointmentsViewModel lvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {
                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    lvm.StaffAppointmentsList = db.PBStaffDashboardAppointmentsRtr( lvm.TodayDate, lvm.StaffCode, lvm.Action, lvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, lvm);
                    lvm.success = "true";
                }
            }
            catch (Exception e)
            {
                lvm.success = "false";
                lvm.message = e.Message;
                obj.LogMessage("APIController", "StaffDashboardAppointmentsRtr", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(lvm);
        }
        #endregion

        #endregion

        #region==> Staff Incentive Report
        [HttpPost]
        [Route("api/parulbeautycareAPI/IncentiveReport")]
        public IHttpActionResult IncentiveReport([FromBody] IncentiveReportViewModel savm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    savm.StaffIncentiveReportList = db.PBStaffIncentiveReport(savm.FromDate, savm.ToDate, savm.StaffCode, savm.Action).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, savm);
                    savm.success = "true";
                }
            }
            catch (Exception e)
            {
                savm.success = "false";
                savm.message = e.Message;
                obj.LogMessage("APIController", "IncentiveReport", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(savm);
        }
        #endregion

        #endregion
    }

}