using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Business
{
    public static class Constant
    {
        public const string DATEFORMAT_ddMMyyyy = "dd/MM/yyyy";
        public const string DATEFORMAT_yyyyMMdd = "yyyy-MM-dd";
        public const int STORECOMPANY = -1;
        public const string EXAMFEE_CLS = "9B6D2E4B-1E60-43FC-9279-F18D627E2270";
        public struct SystemOption
        {
            public const string IS_SHOW_AMOUNT = "IS_SHOW_AMOUNT";               //Show field amount ở tiếp nhận
            public const string IS_SHOW_AMOUNT_EXAM = "IS_SHOW_AMOUNT_EXAM";     //Show field amount trên hệ thống
            public const string IS_SHOW_AMOUNT_TRIAGE = "IS_SHOW_AMOUNT_TRIAGE"; //Show field amount trên hệ thống
            public const string DEFAULT_STOCKID = "DEFAULT_STOCKID";             //Show field amount trên hệ thống


            public const string IS_GENERATE_CASHIN = "IS_GENERATE_CASHIN"; //Kiêm phiếu thu
            public const string IS_GENERATE_CASHOUT = "IS_GENERATE_CASHOUT"; //Kiêm phiếu chi

            public const string IS_GENERATE_STOCKOUT_FROMSALE = "IS_GENERATE_STOCKOUT_FROMSALE"; //Kiêm phiếu xuất kho
            public const string IS_GENERATE_STOCKIN_FROMSALERETURN = "IS_GENERATE_STOCKIN_FROMSALERETURN"; //Kiêm phiếu nhập kho

            public const string IS_GENERATE_STOCKIN_FROMPURCHASE = "IS_GENERATE_STOCKIN_FROMPURCHASE"; //Kiêm phiếu nhập kho
            public const string IS_GENERATE_STOCKOUT_FROMPURCHASE = "IS_GENERATE_STOCKOUT_FROMPURCHASE"; //Kiêm phiếu xuất kho
        }

        public struct ReportHeader
        {
            public const string STORE = "StoreName";
            public const string REPORT_NAME = "ReportName";
            public const string REPORT_DATE = "ReportDate";
        }

        public struct ProductCategoryROOT
        {
            public const string SERVICE = "67351C58-9621-4D6D-97C5-5D28562E58DA";// nhóm dịch vụ - ROOT
            public const string PHARMACY = "5E4A10FD-A247-4B94-A3E1-984F0EF826DF"; // DƯỢC - ROOT
            public const string LABTEST = "78361af2-a1d2-420e-8dba-f134f817aead"; // nhóm xét nghiệm
          
            public const string LAB = "D71FFFEC-E926-4002-8CC1-00B58EAD34D4";  // Nhóm cận lâm sàn
            public const string IMAGE_ANALYSATION = "126B5796-5535-4D3D-974A-0C8E83597E36";  // Nhóm chẩn đoán hình ảnh
            public const string SURGICAL = "86148581-042F-424C-A2D9-1DD24E48A29E"; // Nhóm thủ thuật
            /// <summary>
            /// PHARMACEUTICAL/Nhóm dược: Thuốc, VTTH, TPCN, VACCIN, MY PHAM, SUA BOT
            /// </summary>
            public const string PHARMACEUTICAL = "5E4A10FD-A247-4B94-A3E1-984F0EF826DF"; // Nhóm dược: Thuốc, VTTH, TPCN, VACCIN, MY PHAM, SUA BOT
            /// <summary>
            /// DRUG/Nhóm thuốc
            /// </summary>
            public const string DRUG = "112E1923-66D8-412C-BB25-5222EB26F282";           // nhóm thuốc
            /// <summary>
            /// 
            /// </summary>
            public const string CONSUMABLES = "3ACC789A-D25F-4F35-A838-616D73A70504";    // vật tư tiêu hao
            public const string FUNCTION_FOOD = "220f411a-c819-4863-90b6-f9269fe14351";  // thực phẩm chức năng
            public const string VACCIN = "02bd0232-b164-4bc4-9050-0e273147d9b4";         // vaccin
            public const string SUA_BOT_DD = "17712c62-2e43-4003-86b1-41be6056b3a2";     // sữa bột
            public const string DUOC_MP = "81054747-1d2c-4dd9-b197-73616ae7a1d6";        // dược mỹ phẩm
            public const string PACKAGE = "57E52DEF-AC2B-4D3B-997F-409464881C60";        // GÓI KHÁM
            public const string PROCEDURE = "86148581-042f-424c-a2d9-1dd24e48a29e";      // Nhóm thủ thuật

            public const string EXAMFEE = "B4459A82-F7AF-4C79-B4FC-134C376686D5";      // Phí khám
            
        }

        public struct VendorROOT
        {
            public const string DRUG = "F847DF00-85EA-4D67-BCB9-1D44563640FB";
            //public const string CONSUMABLES = "3ACC789A-D25F-4F35-A838-616D73A70504";
        }

        public struct SalePurchaseStatus
        {
            public const int UnPaid = 1;
            public const int PaidPart = 2;
            public const int Paid = 3;
        }

        public struct ContractStatus
        {
            public const int New = 1;
            public const int Submit = 2;
            public const int Approved = 3;
            public const int Reject = 3;
        }

        public struct DepartmentOption
        {
            public const string Obgy = "SAN";
            public const string Childrent = "NHI";
            public const string Generate = "NOI";
            public const string Emergency = "CAPCUU";
            public const string Immunization = "NOI";
        }

        public struct Inventory
        {
            #region Inventory Constant
            //Status of StockIn & StockOut: 0: Creating, 1: Cancel;  2: Created, 3: Edited; 4: Deleted; 5: Shipping; 6: ReceivePart; 7:Requested; 10:Finish
            public const int INV_STATUS_CREATING = 0;
            public const int INV_STATUS_CANCEL = 1;
            public const int INV_STATUS_CREATED = 2;
            public const int INV_STATUS_EDITED = 3;
            public const int INV_STATUS_DELETED = 4;
            public const int INV_STATUS_SHIPPING = 5;
            public const int INV_STATUS_RECEIVEPART = 6;
            public const int INV_STATUS_REQUESTED = 7;
            public const int INV_STATUS_CANCELREQUESTED = 8;
            public const int INV_STATUS_FINISH = 10;

            //StockIn Type: 2: Purchase (ledger), 3: SaleReturn (Ledger, Ledger Physical),  11: Số dư đầu (Ledger Physical), 12: Purchase(Physical), 13: SaleReturn (Physical), 14:Transfer (Ledger Physical)
            //public const int INV_STOCKINTYPE_OPENINGCOST = 1;
            public const int INV_STOCKINTYPE_PURCHASECOST = 2;
            public const int INV_STOCKINTYPE_SALERETURNCOST = 3;
            public const int INV_STOCKINTYPE_OPENINGBALANCE = 11;
            public const int INV_STOCKINTYPE_PURCHASE = 12;
            public const int INV_STOCKINTYPE_SALERETURN = 13;
            public const int INV_STOCKINTYPE_TRANSFER = 14;
            public const int INV_STOCKINTYPE_SAMPLE = 19;

            //StockOut Type:  2: SaleInvoice (ledger), 3: SaleReturn (Ledger, Ledger Physical), 12: SaleInvoice(Physical), 13: SaleReturn (Physical), 
            //14:Transfer (Ledger Physical), 15:Cancel (All)
            public const int INV_STOCKOUTTYPE_SALEINVOICECOST = 2;
            public const int INV_STOCKOUTTYPE_PURCHASERETURNCOST = 3;
            public const int INV_STOCKOUTTYPE_SALEINVOICE = 12;
            public const int INV_STOCKOUTTYPE_PURCHASERETURN = 13;
            public const int INV_STOCKOUTTYPE_TRANSFER = 14;
            public const int INV_STOCKOUTTYPE_CANCEL = 15; //xuat huy
            public const int INV_STOCKOUTTYPE_PROMOTIONSALEINVOICE = 16; //kmai
            public const int INV_STOCKOUTTYPE_DONATE = 17; //xuất biếu tặng
            public const int INV_STOCKOUTTYPE_USE = 18; //xuất sử dụng (tính chi phí)
            public const int INV_STOCKOUTTYPE_SAMPLE = 19; //xuất mẫu-dùng thử
            public const int INV_STOCKOUTTYPE_USENONCOST = 20; //xuất sử dụng (ko tính chi phí, cho những sản phẩm xuất để hình thành giá vốn cho item dịch vụ)

            /// <summary>
            /// Hàng hóa & Nguyên vật liệu
            /// </summary>
            public const int PRODUCTPRICING_OPENINGCOST = 1;
            /// <summary>
            /// Dịch vụ
            /// </summary>
            public const int PRODUCTPRICING_SERVICECOST = 2;
            #endregion
        }

        public struct InventoryType
        {
            /// <summary>
            /// Nhập mua hàng
            /// </summary>
            public const int STOCKIN_PURCHASE = 1;
            /// <summary>
            /// Nhập hàng bán trả lại
            /// </summary>
            public const int STOCKIN_SALERETURN = 2;
            /// <summary>
            /// Nhập chuyển kho
            /// </summary>
            public const int STOCKIN_TRANSFER = 3;


            /// <summary>
            /// Xuất bán hàng
            /// </summary>
            public const int STOCKOUT_SALEINVOICE = 4;
            /// <summary>
            /// Xuất trà hàng
            /// </summary>
            public const int STOCKOUT_PURCHASERETURN = 5;
            /// <summary>
            /// xuất chuyển kho
            /// </summary>
            public const int STOCKOUT_TRANSFER = 6;
            /// <summary>
            /// xuất sử dụng
            /// </summary>
            public const int STOCKOUT_USE = 7;
            /// <summary>
            /// xuất hủy
            /// </summary>
            public const int STOCKOUT_CANCEL = 8;
        }

        public struct LabHistory
        {
            public const string RootXetNghiem = "78361AF2-A1D2-420E-8DBA-F134F817AEAD";
            public const string RootSieuAm = "8FADEFC7-96F3-4CBB-BA8F-EE90DE3A1B36";
            public const string RootCDHA = "062496E2-DA9D-475E-8D3D-1D05EBDD11FF"; // xquang
        }

        public struct PriceListStatus
        {
            public const int WorkingOn = 1;    //Đang soạn
            public const int Submit = 2;       //Chờ duyệt
            public const int Reject = 3;       //Từ chối
            public const int Approved = 4;     //Duyệt
            public const int Applied = 5;      //Đã áp dụng
        }

        public struct FiscalStatus
        {
            public const int OPENING = 1;       //Đang mở
            public const int REFRESH_COST = 2;  //Chờ duyệt
            public const int CLOSE_PRICE = 3;   //Chốt giá vốn
            public const int CLOSE_FISCAL = 4;  //Đã đóng kỳ
            public const int CANCEL_PRICE = 5;  //Hủy giá vốn
        }

        public struct Department
        {
            /// <summary>
            /// Khoa sản
            /// </summary>
            public const string OBGY = "6EAF2CD6-D9D5-40C8-B6DE-868FB37F70DE";
            /// <summary>
            /// Khoa nội
            /// </summary>
            public const string GENERAL = "063D274D-2BD0-4D5C-8522-DEBFB0BC14C0";
            /// <summary>
            /// Khoa khám sức khỏe
            /// </summary>
            public const string GENERAL_TQ = "25D45DBA-4772-41D5-BB54-A978E030D97F";
            /// <summary>
            /// Khoa nhi
            /// </summary>
            public const string CHILDRENT = "B8AFBDBB-A6DE-4F7C-BEBA-3B97AB98C48A";
            /// <summary>
            /// Khoa cấp cứu
            /// </summary>
            public const string EMERGENCY = "33A52B5A-9219-4288-8B6F-C89A0CF28750";
            /// <summary>
            /// Khoa tiêm ngừa
            /// </summary>
            public const string IMMUNIZATION = "48D04C9E-545D-4940-BA2C-0C7F8F48DA66";


            /// <summary>
            /// Khoa tim mạch
            /// </summary>
            public const string CARDIOLOGY = "A764982D-2030-4EAB-BF7C-71BBEC04802B";
            /// <summary>
            /// Khoa nha
            /// </summary>
            public const string DENTISTRY = "1C06D27B-0AB2-4186-BF60-8E24AD77CBAD";
            /// <summary>
            /// Khoa mắt
            /// </summary>
            public const string EYE = "7F4E5B08-173A-4D51-9835-BD71CA30AD63";
            /// <summary>
            /// Khoa da liễu
            /// </summary>
            public const string DERMATOLOGY = "94F470A8-568D-45DC-84C5-CA7C8D2F5D63";
            /// <summary>
            /// Khoa tai mũi họng
            /// </summary>
            public const string EAR_NOSE = "06BB848A-2338-47F9-8734-EF8ADBD2A0CF";
        }

        public struct FixedAssetStatus
        {
            public const int ITEMNEW = 1;            //Mới tạo
            public const int DISTRIBUTING = 2;       //Đang phân bổ
            public const int DISTRIBUTED = 3;        //Đã phân bổ xong
            public const int TERMINATION = 4;        //Đã phân bổ xong
        }
    }
}
