/*
 * 2026-04-04
 */
namespace Vo {
    /// <summary>
    /// 任意保険情報テーブルのVO。
    /// DBテーブルの1レコードをそのまま保持する純粋なデータオブジェクト。
    /// </summary>
    public class VoluntaryAutomobileInsuranceVo {
        public VoluntaryAutomobileInsuranceVo() {
            Id = string.Empty;
            StaffCode = string.Empty;
            VehicleType = string.Empty;
            CompanyName = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;

            Image1 = Array.Empty<byte>();
            Image2 = Array.Empty<byte>();
            Image3 = Array.Empty<byte>();
            Image4 = Array.Empty<byte>();

            InsertPcName = string.Empty;
            InsertYmdHms = string.Empty;
            UpdatePcName = string.Empty;
            UpdateYmdHms = string.Empty;
            DeletePcName = string.Empty;
            DeleteYmdHms = string.Empty;
            DeleteFlag = string.Empty;
        }

        /// <summary>主キー。varchar(50)</summary>
        public string Id { get; set; }

        /// <summary>スタッフコード。int</summary>
        public string StaffCode { get; set; }

        /// <summary>車両種別。varchar(50)</summary>
        public string VehicleType { get; set; }

        /// <summary>会社名。varchar(100)</summary>
        public string CompanyName { get; set; }

        /// <summary>開始日。date</summary>
        public string StartDate { get; set; }

        /// <summary>終了日。date</summary>
        public string EndDate { get; set; }

        /// <summary>画像1。image</summary>
        public byte[] Image1 { get; set; }

        /// <summary>画像2。image</summary>
        public byte[] Image2 { get; set; }

        /// <summary>画像3。image</summary>
        public byte[] Image3 { get; set; }

        /// <summary>画像4。image</summary>
        public byte[] Image4 { get; set; }

        /// <summary>登録PC名。varchar(50)</summary>
        public string InsertPcName { get; set; }

        /// <summary>登録日時。datetime</summary>
        public string InsertYmdHms { get; set; }

        /// <summary>更新PC名。varchar(50)</summary>
        public string UpdatePcName { get; set; }

        /// <summary>更新日時。datetime</summary>
        public string UpdateYmdHms { get; set; }

        /// <summary>削除PC名。varchar(50)</summary>
        public string DeletePcName { get; set; }

        /// <summary>削除日時。datetime</summary>
        public string DeleteYmdHms { get; set; }

        /// <summary>削除フラグ。bit</summary>
        public string DeleteFlag { get; set; }


        /*
         * 2026-04-04
         * 画像の有無を表すプロパティ。DBのimage型はbyte[]で表現されるが、byte[]が空の場合は画像が存在しないとみなす。
         */
        public bool HasImage1 { get; set; }
        public bool HasImage2 { get; set; }
        public bool HasImage3 { get; set; }
        public bool HasImage4 { get; set; }
    }
}
