/*
 * 2026-03-05
 * APIから取得した法令の情報を格納するクラス
 */
namespace Vo {

    /// <summary>
    /// Top Vo
    /// </summary>
    public class LawItem {
        private LawInfo _lawInfo = new();
        private RevisionInfo _revisionInfo = new();
        private List<Sentence> _sentences = new();

        public LawInfo LawInfo {
            get => this._lawInfo;
            set => this._lawInfo = value;
        }
        public RevisionInfo RevisionInfo {
            get => this._revisionInfo;
            set => this._revisionInfo = value;
        }
        public List<Sentence> Sentences {
            get => this._sentences;
            set => this._sentences = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LawInfo {
        private string _lawType = string.Empty;
        private string _lawId = string.Empty;
        private string _lawNum = string.Empty;
        private string _lawNumEra = string.Empty;
        private string _lawNumYear = string.Empty;
        private string _lawNumType = string.Empty;
        private string _lawNumNum = string.Empty;
        private DateTime? _promulgationDate;

        public string LawType {
            get => this._lawType;
            set => this._lawType = value;
        }
        public string LawId {
            get => this._lawId;
            set => this._lawId = value;
        }
        public string LawNum {
            get => this._lawNum;
            set => this._lawNum = value;
        }
        public string LawNumEra {
            get => this._lawNumEra;
            set => this._lawNumEra = value;
        }
        public string LawNumYear {
            get => this._lawNumYear;
            set => this._lawNumYear = value;
        }
        public string LawNumType {
            get => this._lawNumType;
            set => this._lawNumType = value;
        }
        public string LawNumNum {
            get => this._lawNumNum;
            set => this._lawNumNum = value;
        }
        public DateTime? PromulgationDate {
            get => this._promulgationDate;
            set => this._promulgationDate = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RevisionInfo {
        public string LawRevisionId {
            get; set;
        }
        public string LawType {
            get; set;
        }
        public string LawTitle {
            get; set;
        }
        public string LawTitleKana {
            get; set;
        }
        public string Abbrev {
            get; set;
        }
        public string Category {
            get; set;
        }
        public DateTime? Updated {
            get; set;
        }
        public DateTime? AmendmentPromulgateDate {
            get; set;
        }
        public DateTime? AmendmentEnforcementDate {
            get; set;
        }
        public string AmendmentEnforcementComment {
            get; set;
        }
        public DateTime? AmendmentScheduledEnforcementDate {
            get; set;
        }
        public string AmendmentLawId {
            get; set;
        }
        public string AmendmentLawTitle {
            get; set;
        }
        public string AmendmentLawTitleKana {
            get; set;
        }
        public string AmendmentLawNum {
            get; set;
        }
        public string AmendmentType {
            get; set;
        }
        public string RepealStatus {
            get; set;
        }
        public DateTime? RepealDate {
            get; set;
        }
        public bool? RemainInForce {
            get; set;
        }
        public string Mission {
            get; set;
        }
        public string CurrentRevisionStatus {
            get; set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Sentence {
        public string Position {
            get; set;
        }
        public string Text {
            get; set;
        }
    }
}
