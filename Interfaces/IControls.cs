namespace Interfaces {
    /// <summary>
    /// SetControl/SetLabel/CarLabel/StaffLabelに継承されるインターフェース
    /// </summary>
    public interface IControls {
        /// <summary>
        /// 管理地域コード
        /// 0:該当なし 1:足立 2:三郷
        /// </summary>
        public int ManagedSpaceCode { get; set; }


    }
}
