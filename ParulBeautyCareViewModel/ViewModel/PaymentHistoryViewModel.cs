using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class PaymentHistoryViewModel
    {
        public string PaymentHistoryId { get; set; }
        public string BookingId { get; set; }
        public string BookingCode { get; set; }
        public string PaymentRecievedDate { get; set; }
        public string PaymentType { get; set; }
        public string ChequeNo { get; set; }  
        public string ReceivedAmount { get; set; }
        public string PreviousAmount { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string Action { get; set; }
        public string IsPaymentReceived { get; set; }
        public string result { get; set; }
        public string GPayNo { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        
    }
    public class ServiceDataModel
    {
        public List<BookingDetailViewModel> FormDataArray { get; set; }
        public List<PaymentHistoryViewModel> PaymentHistoryArray { get; set; }
    }
}
