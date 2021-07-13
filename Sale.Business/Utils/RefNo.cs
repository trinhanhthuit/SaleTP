using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Business.Utils
{
    public static class RefNo
    {
        //Module Patient
        public const string PATIENT = "PATIENT";

        //ORDER
        public const string EXAMCODE = "EXAMCODE";
        public const string ORDERCODE = "ORDERCODE";

        //ABSENT
        public const string ABSENT = "ABSENT";
        public const string CUSTOMER = "CUSTOMER";

        //SALE INVOICE
        public const string SALEINVOICE = "SALEINVOICE";
        public const string SALEINVOICE_EXAM = "SALEINVOICE_EXAM";
        public const string SALEINVOICE_BACKEND = "SALEINVOICE_BACKEND";

        //SALE RETURN
        public const string SALERETURN = "SALERETURN";
        public const string SALERETURN_EXAM = "SALERETURN_EXAM";

        //PURCHASE
        public const string PURCHASEINVOICE = "PURCHASE_INVOICE";
        public const string PURCHASERETURN = "PURCHASE_RETURN";

        //MASTER BILL
        public const string MASTERBILL = "MASTERBILL";

        //MASTER BILL
        public const string CONTRACT = "CONTRACT";

        //PAYMENT
        public const string PAYMENT_EXAM = "PAYMENT_EXAM";
        public const string CASHIN = "CASHIN";
        public const string CASHOUT = "CASHOUT";
        public const string CASHOUT_COST = "CASHOUT_COST";                  //Phiếu chi chi phí
        public const string CASHOUT_DISTRIBUTE = "CASHOUT_DISTRIBUTE";      //Phiếu chi phân bổ
        public const string CASHOUT_RETURN = "CASHOUT_RETURN";              //306: Phiếu chi hủy dịch vụ

        public const string CUSTOMER_PAYMENT = "CUSTOMER_PAYMENT";
        public const string VENDOR_PAYMENT = "VENDOR_PAYMENT";

        //Phân bổ chi phí/TSCĐ,CCDC
        public const string FIXEDASSET = "FIXEDASSET";                      //501: Tài sản cố định
        public const string INSTRUMENT = "INSTRUMENT";                    //502: Công cụ dụng cụ

        //STOCKIN 
        public const string STOCKIN_PURCHASE = "INV_STOCKIN_PURCHASE";               //601: Nhập mua hàng
        public const string STOCKIN_SALERETURN = "INV_STOCKIN_SALERETURN";           //602: Nhập hàng bán trả lại
        public const string STOCKIN_TRANSFER = "INV_STOCKIN_TRANSFER";               //603: Nhập chuyển kho

        //STOCKOUT
        public const string STOCKOUT_SALEINVOICE = "INV_STOCKOUT_SALEINVOICE";       //701: Xuất bán hàng 
        public const string STOCKOUT_PURCHASERETURN = "INV_STOCKOUT_PURCHASERETURN"; //702: Xuất trả hàng
        public const string STOCKOUT_TRANSFER = "INV_STOCKOUT_TRANSFER";             //703: Xuất chuyển kho
        public const string STOCKOUT_USE = "INV_STOCKOUT_USE";                       //704: Xuất sử dụng
        public const string STOCKOUT_CANCEL = "INV_STOCKOUT_CANCEL";

        //STOCK_TRANSFER_REQUEST
        public const string STOCK_TRANSFER_REQUEST = "INV_STOCK_TRANSFER_REQUEST";   // yêu cầu chuyển kho

        //STOCK_STOCKTAKE
        public const string STOCK_STOCKTAKE = "STOCK_STOCKTAKE";                 // Phiếu kiểm kho



        //KKT: Kho kế toán
        //public const string STOCKOUT_SALEINVOICE = "INV_STOCKOUT_SALEINVOICE";       //901: Xuất bán hàng 
        //public const string STOCKOUT_PURCHASERETURN = "INV_STOCKOUT_PURCHASERETURN"; //902: Xuất trả hàng
        //public const string STOCKOUT_TRANSFER = "INV_STOCKOUT_TRANSFER";             //703: Xuất chuyển kho
        //public const string STOCKOUT_USE = "INV_STOCKOUT_USE";                       //704: Xuất sử dụng
        //public const string STOCKOUT_CANCEL = "INV_STOCKOUT_CANCEL";                 //705: Xuất hủy
    }
}
