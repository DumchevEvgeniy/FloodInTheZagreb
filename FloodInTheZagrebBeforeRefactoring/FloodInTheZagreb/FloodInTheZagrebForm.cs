using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FloodInTheZagreb {
    public partial class FloodInTheZagrebForm : Form {
        private City city;

        public FloodInTheZagrebForm() {
            InitializeComponent();
        }

        private void Btn_loadDataFromFile_Click(Object sender, EventArgs e) {
            var openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            using(var cityFileReader = new CityFileReader(openFileDialog.FileName))
                city = cityFileReader.TryLoadCity();
            if(city == null) {
                MessageBox.Show("File contains bad data!", "Information window");
                return;
            }
            rtb_resultView.Clear();
            var cityArea = city.GetArea();
            AdjustmentChart(chart_cityBeforeFlood, cityArea);
            AdjustmentChart(chart_cityAfterFlood, cityArea);
            city.Draw(chart_cityBeforeFlood, Color.Red, Color.Black);
        }
        private void Btn_makeFlood_Click(Object sender, EventArgs e) {
            if(city == null) {
                MessageBox.Show("Please load data from file", "Information window");
                return;
            }
            city.DestroyByFlood();
            city.Draw(chart_cityAfterFlood, Color.Red, Color.Black);
            PrintResult(rtb_resultView);
            city = null;
        }

        private void PrintResult(RichTextBox rtb) {
            rtb.AppendText(city.Walls.Count.ToString() + Environment.NewLine);
            city.Walls.ForEach(w => rtb.AppendText(w.ID.ToString() + Environment.NewLine));
        }
        private void AdjustmentChart(Chart chart, RectangleF field) {
            chart.Series.Clear();
            chart.ResetAutoValues();
            SetAxisMaxAndMinValue(chart, field);
        }
        private void SetAxisMaxAndMinValue(Chart chart, RectangleF field) {
            chart.ChartAreas[0].AxisX.Minimum = field.Left - 1;
            chart.ChartAreas[0].AxisX.Maximum = field.Right + 1;
            chart.ChartAreas[0].AxisY.Minimum = field.Y - field.Height - 1;
            chart.ChartAreas[0].AxisY.Maximum = field.Top + 1;
        }
    }
}
