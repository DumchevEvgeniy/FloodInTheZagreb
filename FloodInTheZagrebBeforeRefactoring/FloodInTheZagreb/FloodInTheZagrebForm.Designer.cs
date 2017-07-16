namespace FloodInTheZagreb {
    partial class FloodInTheZagrebForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.btn_loadDataFromFile = new System.Windows.Forms.Button();
            this.btn_makeFlood = new System.Windows.Forms.Button();
            this.chart_cityBeforeFlood = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_cityAfterFlood = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbl_cityBeforeFlood = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_resultView = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cityBeforeFlood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cityAfterFlood)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_loadDataFromFile
            // 
            this.btn_loadDataFromFile.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_loadDataFromFile.Location = new System.Drawing.Point(318, 12);
            this.btn_loadDataFromFile.Name = "btn_loadDataFromFile";
            this.btn_loadDataFromFile.Size = new System.Drawing.Size(147, 33);
            this.btn_loadDataFromFile.TabIndex = 1;
            this.btn_loadDataFromFile.Text = "Load map";
            this.btn_loadDataFromFile.UseVisualStyleBackColor = true;
            this.btn_loadDataFromFile.Click += new System.EventHandler(this.Btn_loadDataFromFile_Click);
            // 
            // btn_makeFlood
            // 
            this.btn_makeFlood.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_makeFlood.Location = new System.Drawing.Point(318, 51);
            this.btn_makeFlood.Name = "btn_makeFlood";
            this.btn_makeFlood.Size = new System.Drawing.Size(147, 33);
            this.btn_makeFlood.TabIndex = 2;
            this.btn_makeFlood.Text = "Destroy city";
            this.btn_makeFlood.UseVisualStyleBackColor = true;
            this.btn_makeFlood.Click += new System.EventHandler(this.Btn_makeFlood_Click);
            // 
            // chart_cityBeforeFlood
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.ScaleBreakStyle.Spacing = 1D;
            chartArea1.Name = "ChartArea1";
            this.chart_cityBeforeFlood.ChartAreas.Add(chartArea1);
            this.chart_cityBeforeFlood.Location = new System.Drawing.Point(12, 31);
            this.chart_cityBeforeFlood.Name = "chart_cityBeforeFlood";
            this.chart_cityBeforeFlood.Size = new System.Drawing.Size(300, 300);
            this.chart_cityBeforeFlood.TabIndex = 3;
            this.chart_cityBeforeFlood.Text = "chart1";
            // 
            // chart_cityAfterFlood
            // 
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.Interval = 1D;
            chartArea2.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.ScaleBreakStyle.Spacing = 1D;
            chartArea2.Name = "ChartArea1";
            this.chart_cityAfterFlood.ChartAreas.Add(chartArea2);
            this.chart_cityAfterFlood.Location = new System.Drawing.Point(471, 31);
            this.chart_cityAfterFlood.Name = "chart_cityAfterFlood";
            this.chart_cityAfterFlood.Size = new System.Drawing.Size(300, 300);
            this.chart_cityAfterFlood.TabIndex = 4;
            this.chart_cityAfterFlood.Text = "chart1";
            // 
            // lbl_cityBeforeFlood
            // 
            this.lbl_cityBeforeFlood.AutoSize = true;
            this.lbl_cityBeforeFlood.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_cityBeforeFlood.Location = new System.Drawing.Point(91, 9);
            this.lbl_cityBeforeFlood.Name = "lbl_cityBeforeFlood";
            this.lbl_cityBeforeFlood.Size = new System.Drawing.Size(112, 19);
            this.lbl_cityBeforeFlood.TabIndex = 5;
            this.lbl_cityBeforeFlood.Text = "City before flood";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(583, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "City after flood";
            // 
            // rtb_resultView
            // 
            this.rtb_resultView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtb_resultView.Location = new System.Drawing.Point(318, 90);
            this.rtb_resultView.Name = "rtb_resultView";
            this.rtb_resultView.ReadOnly = true;
            this.rtb_resultView.Size = new System.Drawing.Size(147, 240);
            this.rtb_resultView.TabIndex = 7;
            this.rtb_resultView.Text = "";
            // 
            // FloodInTheZagrebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 342);
            this.Controls.Add(this.rtb_resultView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_cityBeforeFlood);
            this.Controls.Add(this.chart_cityAfterFlood);
            this.Controls.Add(this.chart_cityBeforeFlood);
            this.Controls.Add(this.btn_makeFlood);
            this.Controls.Add(this.btn_loadDataFromFile);
            this.Name = "FloodInTheZagrebForm";
            this.Text = "Flood in the Zagreb";
            ((System.ComponentModel.ISupportInitialize)(this.chart_cityBeforeFlood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cityAfterFlood)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_loadDataFromFile;
        private System.Windows.Forms.Button btn_makeFlood;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_cityBeforeFlood;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_cityAfterFlood;
        private System.Windows.Forms.Label lbl_cityBeforeFlood;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtb_resultView;
    }
}

