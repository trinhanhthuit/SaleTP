using System.Collections.Generic;
using System;
//using Sale.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml;


namespace Sale.Business.Utils
{
    public class HistoryHelper
    {
        public HistoryHelper()
        {
            //
            // Add constructor logic here
            //
        }

        /// <summary>
        /// Check Voucher Write history
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>True: write history. False: not write</returns>
        //public static bool CheckVoucherIsWriteHistory(Guid voucherID, bool? isLoginSecondPassword)
        //{
        //    if (isLoginSecondPassword == null || isLoginSecondPassword == true)
        //        return false;

        //    return new InvoiceHistoryBO().CheckVoucherIsExist(voucherID);
        //}

        /// <summary>
        /// Check Voucher Write history
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>True: write history. False: not write</returns>
        //public static bool CheckVoucherIsWriteHistory(int refType,  string voucherCode, bool? isLoginSecondPassword)
        //{
        //    if (isLoginSecondPassword == null || isLoginSecondPassword == true)
        //        return false;

        //    return new InvoiceHistoryBO().CheckVoucherIsExist(refType, voucherCode);
        //}

        /// <summary>
        ///  Write History Of Invoices
        /// </summary>
        /// <returns></returns>
        public static void WriteHistory<T1, T2>(bool? isLoginSecondPassword, string actionName, HistoryInforStruct historyInfor, T1 voucherMaster, List<T2> voucherDetails) where T1 : class where T2 : class
        {
            try
            {
                //Không ghi log khi set là secondpassword
                //if (isLoginSecondPassword == null || isLoginSecondPassword == true)
                //    return;

                //var invoiceHistoryBO = new InvoiceHistoryBO();

                //// 1. Kiểm tra VoucherID có tồn tại hay chưa
                //long? masterID = invoiceHistoryBO.CheckVoucher(historyInfor.VoucherID);
                //if (masterID == null)
                //{
                //    Logger.Error("WriteHistory-> CheckVoucherIsExist throw exception");
                //    return;
                //}

                //// 2. Add Data into Audit DB
                ////tính quantity new & total amount new từ voucherMaster
                //decimal quantity = historyInfor.Quantity ?? 0;
                //decimal totalAmount = historyInfor.TotalAmount ?? 0;
                //string jsonMaster = JsonConvert.SerializeObject(voucherMaster);
                //string jsonDetail = JsonConvert.SerializeObject(voucherDetails);

                //var auditMaster = new AuditMaster();
                //var auditDetail = new AuditDetail
                //{
                //    Action = actionName,
                //    ModifyBy = historyInfor.UserID,
                //    ModifyDate = DateTime.Now,
                //    JsonMaster = jsonMaster,
                //    jsonDetail = jsonDetail
                //};

                //if (masterID != -1)
                //{
                //    //-----Update----
                //    //Existed: Voucher đã tồn tại trong AuditMaster => chỉ Add AuditDetail, update AuditMaster
                //    auditMaster = invoiceHistoryBO.GetAuditMaster((long)masterID);
                //    auditMaster.Action = auditDetail.Action;
                //    auditMaster.LastedModifyBy = auditDetail.ModifyBy;
                //    auditMaster.LastedModifyDate = auditDetail.ModifyDate;

                //    //Chuyen new -> old, và set lại new = master hiện tại
                //    auditMaster.QuantityOld = auditMaster.QuantityNew;
                //    auditMaster.QuantityNew = quantity;
                //    auditMaster.TotalAmountOld = auditMaster.TotalAmountNew;
                //    auditMaster.TotalAmountNew = totalAmount;

                //    //detail
                //    auditDetail.MasterID = (long)masterID;
                //    auditDetail.QuantityOld = auditMaster.QuantityOld;
                //    auditDetail.QuantityNew = auditMaster.QuantityNew;
                //    auditDetail.TotalAmountOld = auditMaster.TotalAmountOld;
                //    auditDetail.TotalAmountNew = auditMaster.TotalAmountNew;

                //    invoiceHistoryBO.SaveAudit(auditMaster, auditDetail, false);
                //}
                //else
                //{
                //    //----new----
                //    //Not Exist: Voucher chưa tồn tại trong AuditMaster -> Add vào AuditMaster + AuditDetail
                //    auditMaster.Action = auditDetail.Action;
                //    auditMaster.CreatedBy = historyInfor.CreatedBy;
                //    auditMaster.CreatedDate = historyInfor.CreatedDate;
                //    auditMaster.LastedModifyBy = auditDetail.ModifyBy;
                //    auditMaster.LastedModifyDate = auditDetail.ModifyDate;
                //    auditMaster.QuantityOld = quantity;
                //    auditMaster.QuantityNew = quantity;
                //    auditMaster.TotalAmountOld = totalAmount;
                //    auditMaster.TotalAmountNew = totalAmount;
                //    auditMaster.VoucherID = historyInfor.VoucherID;
                //    auditMaster.VoucherCode = historyInfor.VoucherCode;
                //    auditMaster.VoucherDate = historyInfor.VoucherDate;
                //    auditMaster.AccountingDate = historyInfor.AccountingDate;
                //    auditMaster.Type = historyInfor.VoucherType;
                //    auditMaster.RefType = historyInfor.VoucherRefType;
                //    auditMaster.StoreID = historyInfor.StoreID;
                //    auditMaster.ObjectID = historyInfor.ObjectID;
                //    auditMaster.ObjectType = historyInfor.ObjectType;

                //    //detail
                //    auditDetail.MasterID = (long)masterID;
                //    auditDetail.QuantityOld = auditMaster.QuantityOld;
                //    auditDetail.QuantityNew = auditMaster.QuantityNew;
                //    auditDetail.TotalAmountOld = auditMaster.TotalAmountOld;
                //    auditDetail.TotalAmountNew = auditMaster.TotalAmountNew;

                //    invoiceHistoryBO.SaveAudit(auditMaster, auditDetail, true);
                //}
            }
            catch (Exception ex)
            {
                Logger.Error("WriteHistory:" + ex);
            }
        }

        public struct HistoryInforStruct
        {
            public Guid UserID;
            public Guid VoucherID;
            public Guid CreatedBy;
            public DateTime CreatedDate;  
            public string VoucherCode;
            public DateTime VoucherDate;
            public DateTime AccountingDate;
            public int? VoucherType;
            public int? VoucherRefType;
            public int StoreID;
            public Guid? ObjectID;
            public int? ObjectType;
            public decimal? Quantity;
            public decimal? TotalAmount;
        }

        public struct HistoryAction
        {
            public const string INSERT = "insert";
            public const string UPDATE = "update";
            public const string DELETE = "delete";
            public const string POST = "post";
            public const string UNPOST = "unpost";
            public const string SENDREQUEST = "sendrequest";
            public const string CANCELREQUEST = "cancelrequest";
        }

        public enum VoucherType
        {
            SaleInvoice = 11,
            SaleReturn = 11,
            PurchaseInvoice = 12,
            PurchaseReturn = 12,
            CashInOut = 13,
            StockIn = 1,
            StockOut = 2,
            StockOutExtent = 21,
            Combine = 3,
            Split = 4,
            StockTake = 5,
            ProductExchange = 6,
            StockTransfer = 7,
            ProductPricing = 8
        }

        public static Dictionary<string, string> LoadDictonaryCaption()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XMLConfig/CompareDictionary.xml");
            if (System.IO.File.Exists(xmlPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);
                XmlNodeList resources = xml.SelectNodes("root/resources/resource");

                if (resources != null)
                {
                    foreach (XmlNode node in resources)
                    {
                        dictionary.Add(node.Attributes["key"].Value, node.InnerText.Trim());
                    }
                }
            }
            return dictionary;
        }

        public static Dictionary<string, string> LoadDictonaryColumnDisplay()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XMLConfig/CompareDictionary.xml");
            if (System.IO.File.Exists(xmlPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);
                XmlNodeList resources = xml.SelectNodes("root/columndisplay/column");

                if (resources != null)
                {
                    foreach (XmlNode node in resources)
                    {
                        dictionary.Add(node.InnerText.Trim(), node.InnerText.Trim());
                    }
                }
            }
            return dictionary;
        }
    }
}
