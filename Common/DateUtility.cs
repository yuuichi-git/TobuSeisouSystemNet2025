/*
 * 2023-11-08
 */
namespace Common {
    public class DateUtility {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private readonly DateTime _todayDate = DateTime.Today;

        /// <summary>
        /// 年齢を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public int GetAge(DateTime dateTime) {
            int age = _todayDate.Year - dateTime.Year;
            if (dateTime.Date != _defaultDateTime.Date) {
                //誕生日がまだ来ていなければ、1引く
                if (_todayDate.Month < dateTime.Month || (_todayDate.Month == dateTime.Month && _todayDate.Day < dateTime.Day)) {
                    age--;
                }
            } else {
                age = 0;
            }
            return age;
        }

        /// <summary>
        /// 誕生日
        /// 1900/01/01の場合はstring.Emptyを返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GetBirthday(DateTime dateTime) {
            string birthday;
            if (dateTime.Date != _defaultDateTime.Date) {
                birthday = dateTime.Date.ToString("yyyy/MM/dd");
            } else {
                birthday = string.Empty;
            }
            return birthday;
        }

        /// <summary>
        /// 雇用年月日
        /// 1900/01/01の場合はstring.Emptyを返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GetEmploymentDate(DateTime dateTime) {
            string employmentDate;
            if (dateTime.Date != _defaultDateTime.Date) {
                employmentDate = dateTime.Date.ToString("yyyy/MM/dd");
            } else {
                employmentDate = string.Empty;
            }
            return employmentDate;
        }

        /// <summary>
        /// 勤続年数を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public int GetEmploymenteYear(DateTime dateTime) {
            DateTime nowDate = DateTime.Now.Date;
            TimeSpan elapsedSpan = nowDate - dateTime;
            return (int)elapsedSpan.TotalDays / 365;
        }

        /// <summary>
        /// 勤続月数を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public int GetEmploymenteMonth(DateTime dateTime) {
            DateTime nowDate = DateTime.Now.Date;
            TimeSpan elapsedSpan = nowDate - dateTime;
            return (int)elapsedSpan.TotalDays % 365 / 30;
        }

        /// <summary>
        /// 勤続日数を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public int GetEmploymenteDay(DateTime dateTime) {
            DateTime nowDate = DateTime.Now.Date;
            TimeSpan elapsedSpan = nowDate - dateTime;
            return (int)elapsedSpan.TotalDays;
        }

        /// <summary>
        /// 瞬間の日時を取得する
        /// </summary>
        /// <returns></returns>
        public string GetDateTimeNow() {
            return DateTime.Now.ToString("yyyy年MM月dd日(ddd)　HH時mm分ss秒");
        }

        /// <summary>
        /// 月初日を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime GetBeginOfMonth(DateTime dateTime) {
            return dateTime.AddDays((dateTime.Day - 1) * -1);
        }

        /// <summary>
        /// 月末日を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime GetEndOfMonth(DateTime dateTime) {
            return new DateTime(dateTime.Year, dateTime.Month, GetDaysInMonth(dateTime));
        }

        /// <summary>
        /// 該当月の日数を返す
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDaysInMonth(DateTime dateTime) {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        /// <summary>
        /// 会計年度を返す
        /// </summary>
        /// <returns></returns>
        public int GetFiscalYear() {
            return _todayDate.AddMonths(-3).Year;
        }

        /// <summary>
        /// 会計年度を返す
        /// </summary>
        /// <returns></returns>
        public int GetFiscalYear(DateTime dateTime) {
            return dateTime.AddMonths(-3).Year;
        }

        /// <summary>
        /// 会計年度の始めの日付を返す
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime GetFiscalYearStartDate(DateTime datetime) {
            int fiscalYear = datetime.AddMonths(-3).Year;
            return new DateTime(fiscalYear, 4, 1, 0, 00, 00, 000);
        }

        /// <summary>
        /// 会計年度の始めの日付を返す
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        public DateTime GetFiscalYearStartDate(int fiscalYear) {
            return new DateTime(fiscalYear, 4, 1, 0, 00, 00, 000);
        }

        /// <summary>
        /// 会計年度の最後の日付を返す
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime GetFiscalYearEndDate(DateTime datetime) {
            int fiscalYear = datetime.AddMonths(-3).Year;
            return new DateTime(fiscalYear, 4, 1, 0, 00, 00, 000).AddDays(365);
        }

        /// <summary>
        /// 会計年度の最後の日付を返す
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        public DateTime GetFiscalYearEndDate(int fiscalYear) {
            return new DateTime(fiscalYear, 4, 1, 0, 00, 00, 000).AddDays(365);
        }

        /// <summary>
        /// GetWorkingDays
        /// 稼働日(曜日配車・第五週)かどうかを判定する
        /// true:稼働 false:休車
        /// </summary>
        /// <param name="dayOfWeek">稼働日</param>
        /// <param name="workingDays">稼働する曜日</param>
        /// <param name="fiveLap">五週目フラグ</param>
        /// <returns></returns>
        public bool GetWorkingDays(DateTime dateTime, string workingDays, bool fiveLap) {
            if (workingDays.Contains(dateTime.ToString("ddd"))) {
                if (!fiveLap && dateTime.Day > 28) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }

        /// <summary>
        /// string型の時刻(HH:mm)をDateTime(operationDateを元に)に変換する
        /// Secondについては、原時点の時刻から
        /// </summary>
        /// <param name="operationDate"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DateTime GetStringTimeToDateTime(DateTime operationDate, string time) {
            DateTime lastRollCallYmdHms;
            lastRollCallYmdHms = new DateTime(operationDate.Year,
                                              operationDate.Month,
                                              operationDate.Day,
                                              DateTime.ParseExact(time, "HH:mm", null).Hour,
                                              DateTime.ParseExact(time, "HH:mm", null).Minute,
                                              DateTime.Now.Second);
            return lastRollCallYmdHms;
        }
    }
}
