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
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="screen"></param>
        public void SetPosition(Screen screen, Form form) {
            /*
             * マルチ画面等で対象の画面サイズがFHD以下でFormのサイズがFHDであれば、WindowsStateをMaximizedにする
             */
            if (screen.Bounds.Width <= 1920 && screen.Bounds.Height <= 1080) {
                if (form.Size.Width == 1920 && form.Size.Height == 1080) {
                    form.StartPosition = FormStartPosition.Manual;
                    form.WindowState = FormWindowState.Maximized;
                } else {
                    form.StartPosition = FormStartPosition.Manual;
                    form.Location = GetCenterForm(screen, form);
                }
            } else {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = GetCenterForm(screen, form);
            }
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
