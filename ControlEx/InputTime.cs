namespace ControlEx {
    public partial class InputTime : DateTimePicker {
        public InputTime() {
            InitializeComponent();
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "HH:mm";
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
    }
}
