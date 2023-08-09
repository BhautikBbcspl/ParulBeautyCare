using ParulBeautyCareAPI.Models.Logs;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareDbClasses.DataModels.ParulBeautyCareDatasetTableAdapters;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
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
                    sm.result = db.PBProductMasterInsUpd(sm.ProductId, sm.ProductCode, sm.ProductName, sm.NumberOfPerson, sm.ProductTypeId, sm.IsActive, sm.CompanyCode, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.UpdateUser, sm.Action).FirstOrDefault();
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
                    lvm.SubCategoryMasterList = db.PBSubCategoryMasterRetrieve(lvm.Action, lvm.CompanyCode, lvm.SubCategoryId).ToList();
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
                    sm.result = db.PBSubCategoryMasterInsUpd(sm.SubCategoryId, sm.SubCategoryName, sm.CategoryId, sm.IsMultiPerson, sm.NumberOfPerson, sm.YearId, sm.NoOfSitting, sm.TimeDuraion, sm.Amount, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
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
                    sm.result = db.PBDepartmentMasterInsUpd(sm.DepartmentId, sm.DepartmentName, sm.CompanyCode, sm.IsActive, sm.CreateDate, sm.UpdateDate, sm.CreateUser, sm.Action).FirstOrDefault();
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

                    spvm.result = db.PBStockPurchaseMasterInsUpd(spvm.PurchaseId, spvm.CompanyCode, spvm.CreateDate, spvm.CreateUser, spvm.UpdateDate, spvm.UpdateUser, spvm.ProductId, spvm.ProductTypeId, spvm.Quantity, spvm.MfgDate, spvm.ExpDate, spvm.PurchaseDate, spvm.Vendor, spvm.DepartmentId, spvm.Action).FirstOrDefault();
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

                    spvm.result = db.PBStockAllocationMasterInsUpd(spvm.StockAllocationId, spvm.ProductId, spvm.Qty.ToString(), spvm.AllocationDate, spvm.AllocateUser, spvm.StaffId, spvm.UpdateDate, spvm.UpdateUser, sdmv.StockUsed, sdmv.CreateDate, sdmv.CreateUser, sdmv.NoOfPerson, sdmv.PersonUsed, spvm.Action).FirstOrDefault();
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
                //        DataTable dt = new DataTable();
                //        dt.Columns.AddRange(
                //            new DataColumn[8] {
                //new DataColumn("StockDetailId", typeof(string)),
                //new DataColumn("StockHeaderId", typeof(string)),
                //new DataColumn("ProductId", typeof(string)),
                //new DataColumn("Qty", typeof(string)),
                //new DataColumn("TotalPerson", typeof(string)),
                //new DataColumn("PersonAvailable", typeof(string)),
                //new DataColumn("AutoSrNo", typeof(string)),
                //new DataColumn("Barcode", typeof(string))
                //            });

                //        if (sthvm.StockTransferTypeTable != null && sthvm.StockTransferTypeTable.Any())
                //        {
                //            foreach (var item in sthvm.StockTransferTypeTable)
                //            {
                //                dt.Rows.Add(
                //                    item.StockDetailId,
                //                    item.StockHeaderId,
                //                    item.ProductId,
                //                    item.Qty,
                //                    item.TotalPerson,
                //                    item.PersonAvailable,
                //                    item.AutoSrNo,
                //                    item.Barcode
                //                );
                //            }
                //        }
                DataTable dt = new DataTable();
                dt.Columns.AddRange(
                       new DataColumn[8] {
                new DataColumn("StockDetailId", typeof(string)),
                new DataColumn("StockHeaderId", typeof(string)),
                new DataColumn("ProductId", typeof(string)),
                new DataColumn("Qty", typeof(string)),
                new DataColumn("TotalPerson", typeof(string)),
                new DataColumn("PersonAvailable", typeof(string)),
                new DataColumn("AutoSrNo", typeof(string)),
                new DataColumn("Barcode", typeof(string))
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
                        dt.Rows.Add(StockDetailId, StockHeaderId, ProductId, Qty, TotalPerson, PersonAvailable, AutoSrNo, Barcode);
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

        #endregion

        #region=====> Booking Management API

        #region ==> Booking Staff Allocation
        [HttpPost]
        [Route("api/parulbeautycareAPI/BookingHeaderRetrieve")]
        public IHttpActionResult BookingHeaderRetrieve([FromBody] BookingStaffAllocationViewModel spvm)
        {
            iNotifyLogger obj = new iNotifyLogger();
            try
            {

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    spvm.BookingHeaderList = db.PBBookingHeaderRtr(spvm.BookingId, spvm.Action, spvm.CompanyCode).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.OK, spvm);
                    spvm.success = "true";
                }
            }
            catch (Exception e)
            {
                spvm.success = "false";
                spvm.message = e.Message;
                obj.LogMessage("APIController", "BookingHeaderRetrieve", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(spvm);
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
                    chkinout.result = db.PBCheckInCheckOutIns(chkinout.CheckId, chkinout.CustomerName, chkinout.ContactNo, chkinout.HouseNoSociety, chkinout.Landmark, chkinout.City, chkinout.Pincode, chkinout.CheckinDateTime, chkinout.CheckoutDateTime, chkinout.WaitingTime, chkinout.BookingId, chkinout.CompanyCode, chkinout.CreateDate, chkinout.CreateUser, chkinout.UpdateDate, chkinout.UpdateUser,chkinout.Note ,chkinout.Action).FirstOrDefault();
                    var response = Request.CreateResponse(HttpStatusCode.OK, chkinout);
                    chkinout.success = "true";
                }
            }
            catch (Exception e)
            {
                chkinout.success = "false";
                chkinout.message = e.Message;
                obj.LogMessage("APIController", "StockPurchaseInsUpd", e.Message, iNotifyLogger.LogType.Exception);
            }
            return Json(chkinout);
        }


        #endregion

        #endregion


    }

}