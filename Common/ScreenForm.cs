/*
 * 2024-09-30
 */
namespace Common {
    public class ScreenForm {
        private int _monitorUnits = 0;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ScreenForm() {
            // 接続されているモニターの台数をセット
            MonitorUnits = Screen.AllScreens.Length;
        }


        /// <summary>
        /// システム上のすべてのディスプレイを取得
        /// </summary>
        /// <returns></returns>
        public List<Screen> GetAllScreen() {
            List<Screen> listScreen = new();
            foreach (Screen screen in Screen.AllScreens)
                listScreen.Add(screen);
            return listScreen;
        }

        /// <summary>
        /// カーソルのあるScreenを取得
        /// </summary>
        /// <returns></returns>
        public Screen GetCurrentScreen() {
            // カーソルの位置を取得
            Point cursorPosition = Cursor.Position;
            // カーソルがあるモニターを取得
            return Screen.FromPoint(cursorPosition);
        }

        /// <summary>
        /// 接続されているモニターの台数
        /// </summary>
        public int MonitorUnits {
            get => this._monitorUnits;
            set => this._monitorUnits = value;
        }

        /// <summary>
        /// マルチ画面環境でフォームを適切に配置する。
        /// ・画面サイズがFHD以下でフォームがFHDサイズなら最大化
        /// ・フォームが WorkArea を超える場合は高さを WorkArea に合わせ、位置も WorkArea.Top に揃える
        /// ・それ以外は中央に配置
        /// </summary>
        public void SetPosition(Screen screen, Form form) {
            form.StartPosition = FormStartPosition.Manual;

            // タスクバーを除いた実際の表示可能領域
            Rectangle workArea = screen.WorkingArea;

            // ------------------------------------------------------------
            // ① フォームの高さが WorkArea を超える場合は、
            //    高さを WorkArea に合わせ、位置も WorkArea.Top に揃える
            // ------------------------------------------------------------
            if (form.Height > workArea.Height) {
                form.Height = workArea.Height;

                // 上端を WorkArea.Top に合わせる（画面いっぱいに表示）
                form.Location = new Point(
                    workArea.Left + (workArea.Width - form.Width) / 2,  // 横は中央
                    workArea.Top                                       // 縦は上端に固定
                );

                return;
            }

            // ------------------------------------------------------------
            // ② FHD 以下の画面で、フォームが FHD サイズなら最大化
            // ------------------------------------------------------------
            if (screen.Bounds.Width <= 1920 && screen.Bounds.Height <= 1080) {
                if (form.Size.Width == 1920 && form.Size.Height == 1080) {
                    form.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            // ------------------------------------------------------------
            // ③ 上記以外は中央に配置
            // ------------------------------------------------------------
            form.Location = GetCenterForm(screen, form);
        }

        /// <summary>
        /// 表示先スクリーンの作業領域(タスクバーを除いた範囲)にFormを表示する
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="form">対象となるForm</param>
        public void SetPositionInScreen(Form form) {
            form.StartPosition = FormStartPosition.Manual;
            Point point = Cursor.Position;                                              // カーソル位置（スクリーン座標）
            /*
             * 表示したい位置（初期値）
             */
            int x = point.X + 1;
            int y = point.Y + 1;
            Rectangle rectangle = Screen.GetWorkingArea(point);                         // 表示先スクリーンの作業領域（タスクバーを除いた範囲）
            if (x + form.Width > rectangle.Right)                                       // 右にはみ出す場合
                x = rectangle.Right - form.Width;
            if (x < rectangle.Left)                                                     // 左にはみ出す場合
                x = rectangle.Left;
            if (y + form.Height > rectangle.Bottom)                                     // 下にはみ出す場合
                y = rectangle.Bottom - form.Height;
            if (y < rectangle.Top)                                                      // 上にはみ出す場合
                y = rectangle.Top;
            form.Location = new Point(x, y);
        }

        /// <summary>
        /// FormをScreenの中央に表示するための座標を取得
        /// </summary>
        /// <returns></returns>
        public Point GetCenterForm(Screen screen, Form form) {
            Rectangle rectangle = screen.Bounds;
            Point point = new();
            point.X = rectangle.X + (rectangle.Width / 2) - (form.Width / 2);
            point.Y = rectangle.Y + (rectangle.Height / 2) - (form.Height / 2);
            return point;
        }
    }
}
