using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Business
{
    public static class MessageError
    {
        #region General message
        public const string MSGCODE_000001 = "000001"; //Load dữ liệu không thành công
        public const string MSGCODE_DEL_000002 = "000002"; //Xóa dữ liệu không thành công 
        public const string MSGCODE_SAVE_000003 = "000003"; //Lưu không thành công          
        public const string MSGCODE_DEL_000004 = "000004"; //Dữ liệu này đã sử dụng nên không thể xóa!
        public const string MSGCODE_EXISTCODE_000005 = "000005"; //Tồn tại Mã   
        public const string MSGCODE_LOAD_000001 = "000006"; //Mã chứng từ không tồn tại!
        public const string MSGCODE_POST_000001 = "000007"; //Ghi sổ không thành công!
        public const string MSGCODE_UNPOST_000001 = "000008"; //Bỏ ghi sổ không thành công!
        public const string MSGCODE_DEL_000009 = "000009"; //Chứng từ đã ghi sổ không thể xóa   
        public const string MSGCODE_REFCODE_000001 = "000010"; //Get số chứng từ không thành công  
        public const string EXAM_OPTION_00001 = "EXAM-OPTION-00001"; // Ma ton tai    
        public const string ABSENT_00001 = "ABSENT-00001"; // Thời gian này bác sĩ không làm việc, vui lòng kiểm tra lại lịch làm việc!   



        #endregion

        #region Patient message
        public const string MSGCODE_PAT_00001 = "PAT-00001"; //Get patient number không thành công 
        public const string MSGCODE_PAT_00002 = "PAT-00002"; //Thêm bệnh nhân không thành công          
        public const string MSGCODE_PAT_00003 = "PAT-00003"; //Bệnh nhân này không tồn tại, vui lòng kiểm tra lại thông tin bệnh nhân!   
        public const string MSGCODE_PAT_00004 = "PAT-00004"; //Đã có bệnh nhân đặt hẹn trong thời gian này rồi!       
        #endregion

        #region Security message
        public const string MSGCODE_USE_000001 = "USE-00001"; //User name đã tồn tại.    
        public const string MSGCODE_SEC_000001 = "SEC-00001"; //Role này đã hiện tại đã cấp quyền cho User, không thể xóa. 
        public const string MSGCODE_SEC_000002 = "SEC-00002"; //Mã nhóm quyền đã tồn tại trong hệ thống, vui lòng kiểm tra lại!. 
        public const string MSGCODE_SEC_000003 = "SEC-00003"; //Xét quyền cho user không thành công, vui lòng thử lại!. 
        public const string MSGCODE_SEC_000004 = "SEC-00004"; //Gỡ bỏ quyền chỉnh sửa chứng từ không thành công!. 
        #endregion

        #region List message
        public const string MSGCODE_STORE_000001 = "STORE-00001"; //Mã phòng khám đã tồn tại
        public const string MSGCODE_STORE_000002 = "STORE-00002"; //Profit đã tồn tại.     
        #endregion

        #region Tiếp nhận bệnh nhân
        public const string MSGCODE_REC_00001 = "REC-00001"; //Tiếp nhận bệnh nhân không thành công
        public const string MSGCODE_REC_00002 = "REC-00002"; //Thông tin tiếp nhận bệnh nhân không tồn tại
        public const string MSGCODE_REC_00003 = "REC-00003"; //Bệnh nhân này tồn tại 1 hồ sơ bệnh án đang mở, không thể tiếp nhận!
        public const string MSGCODE_REC_00004 = "REC-00004"; //Bệnh nhân này đã thực hiện một số dịch vụ không thể xóa!
        #endregion

        #region Triage
        public const string MSGCODE_TRIA_00001 = "TRIA-00001"; //Thông tin đo dấu hiệu sinh tồn không tồn tại
        #endregion

        #region Hàng đợi bệnh nhân
        public const string MSGCODE_QUE_00001 = "QUE-00001"; //Load danh sách hàng đợi bệnh nhân không thành công!        
        #endregion

        #region Phiếu thu viện phí      
        public const string MSGCODE_PAY_00001 = "PAY-00001"; //Lưu phiếu thu viện phí không thành công!
        public const string MSGCODE_PAY_00002 = "PAY-00002"; //Một số dịch vụ đã thu viện phí trước kia, vui lòng kiểm tra lại
        #endregion

        #region POST Voucher
        public const string MSGCODE_POST_00003 = "POST-0003"; //Ghi sổ không thành công!    
        public const string MSGCODE_POST_00004 = "POST-0004"; //Bỏ ghi sổ không thành công!           
        #endregion

        #region Appointment
        public const string MSGCODE_APP_00001 = "APP-00001"; //Tạo lịch hẹn không thành công!     
        public const string MSGCODE_APP_00002 = "APP-00002"; //Appointment không tồn tại!    
        #endregion 

        #region Khám bệnh
        public const string MSGCODE_EXM_0001 = "EXM-0001"; //Load thông tin khám không thành công!
        public const string MSGCODE_EXM_0002 = "EXM-0002"; //Lưu thông tin khám không thành công!
        public const string MSGCODE_EXM_0003 = "EXM-0003"; //Cập nhập trạng thái không thành công
        public const string MSGCODE_EXM_0004 = "EXM-0004"; //Một số CLS chưa finish!
        #endregion

        #region Mua hàng
        public const string MSGCODE_PUR_00001 = "PAT-00001"; //Get patient number không thành công 
        #endregion

        #region Import
        // cơ số kho
        public const string MSGCODE_IMPORT_00001 = "IMPORT-00001"; // Cập nhật dữ liệu không thành công, Cơ số kho tối thiểu <= cơ số kho tối đa
        public const string MSGCODE_IMPORT_00002 = "IMPORT-00002"; // Cập nhật dữ liệu không thành công, vui lòng xem lại cấu trúc file excel chưa đúng định dạng!
        public const string MSGCODE_IMPORT_00003 = "IMPORT-00003"; // Cập nhật dữ liệu không thành công, Giá KM <= Giá bán
        public const string MSGCODE_IMPORT_00004 = "IMPORT-00004"; // Dữ liệu không đúng nhóm mà bạn cần thêm!
        #endregion


        #region Inventory
        /// <summary>
        /// Thiếu hàng trong kho(Không cho xuất âm)
        /// </summary>
        public const string MSGCODE_INV_001 = "INV-POST-50008"; // Thiếu hàng trong kho
        #endregion

        #region Promotion
        public const string PROMO_00001 = "PROMO-00001"; // Chương trình khuyến mãi này không tồn tại!
        #endregion

        public const string MSGCODE_CLOSING_001 = "CLOSING-001"; // Cập nhật giá vốn không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_002 = "CLOSING-002"; // Khóa sổ kỳ kế toán không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_003 = "CLOSING-003"; // Có một số chứng từ trước ngày {0} chưa ghi sổ.<br/>Muốn thực hiện khóa sổ, bạn phải xử lý các chứng từ này.<br/><a target='_blank' href="{1}">Nhấn vào đây để xử lý chứng từ</a>
        public const string MSGCODE_CLOSING_004 = "CLOSING-004"; // Giá vốn hàng hóa đã được cập nhật mới nhất
        public const string MSGCODE_CLOSING_005 = "CLOSING-005"; // Ngày khóa sổ mới phải lớn hơn ngày khóa sổ hiện thời
        public const string MSGCODE_CLOSING_006 = "CLOSING-006"; // Khóa kỳ kế toán thành công
        public const string MSGCODE_CLOSING_007 = "CLOSING-007"; // Có hàng hóa quy đổi 2 chiều. vui lòng kiểm tra lại 
        public const string MSGCODE_CLOSING_008 = "CLOSING-008"; // Cập nhật đơn giá cho chứng từ trong kỳ không thành công
        public const string MSGCODE_CLOSING_009 = "CLOSING-009"; // Chứng từ trong kỳ đã được cập nhật lại đơn giá theo giá vốn mới nhất
        public const string MSGCODE_CLOSING_010 = "CLOSING-010"; // Chốt giá vốn không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_011 = "CLOSING-011"; // Chốt giá vốn thành công
        public const string MSGCODE_CLOSING_012 = "CLOSING-012"; // Hủy chốt giá vốn không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_013 = "CLOSING-013"; // Hủy chốt giá vốn thành công.
        public const string MSGCODE_CLOSING_014 = "CLOSING-014"; // Cập nhật và chốt giá vốn không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_015 = "CLOSING-015"; // Cập nhật và Chốt giá vốn thành công
        public const string MSGCODE_CLOSING_016 = "CLOSING-016"; // Khóa tạm kỳ kế toán không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_017 = "CLOSING-017"; // Khóa tạm kỳ kế toán thành công
        public const string MSGCODE_CLOSING_018 = "CLOSING-018"; // Hủy Khóa tạm kỳ kế toán không thành công. Vui lòng thử lại
        public const string MSGCODE_CLOSING_019 = "CLOSING-019"; // Hủy Khóa tạm kỳ kế toán thành công
    }
}
