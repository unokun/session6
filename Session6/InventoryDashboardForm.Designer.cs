namespace Session6
{
    partial class InventoryDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.emSpendingView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mostUsedPartsView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.costlyAssetsView = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartView = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.inventoryControlButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pieChartView = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.languageBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.emSpendingView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mostUsedPartsView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.costlyAssetsView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChartView)).BeginInit();
            this.SuspendLayout();
            // 
            // emSpendingView
            // 
            this.emSpendingView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.emSpendingView.Location = new System.Drawing.Point(6, 18);
            this.emSpendingView.Name = "emSpendingView";
            this.emSpendingView.RowTemplate.Height = 21;
            this.emSpendingView.Size = new System.Drawing.Size(580, 125);
            this.emSpendingView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.emSpendingView);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 157);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EM Spending by Department";
            // 
            // mostUsedPartsView
            // 
            this.mostUsedPartsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mostUsedPartsView.Location = new System.Drawing.Point(6, 18);
            this.mostUsedPartsView.Name = "mostUsedPartsView";
            this.mostUsedPartsView.RowTemplate.Height = 21;
            this.mostUsedPartsView.Size = new System.Drawing.Size(580, 125);
            this.mostUsedPartsView.TabIndex = 2;
            this.mostUsedPartsView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mostUsedPartsView_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mostUsedPartsView);
            this.groupBox2.Location = new System.Drawing.Point(12, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 157);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monthly Report for Most-Used  Parts";
            // 
            // costlyAssetsView
            // 
            this.costlyAssetsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.costlyAssetsView.Location = new System.Drawing.Point(6, 18);
            this.costlyAssetsView.Name = "costlyAssetsView";
            this.costlyAssetsView.RowTemplate.Height = 21;
            this.costlyAssetsView.Size = new System.Drawing.Size(580, 125);
            this.costlyAssetsView.TabIndex = 4;
            this.costlyAssetsView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.costlyAssetsView);
            this.groupBox3.Location = new System.Drawing.Point(12, 339);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(600, 157);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Monthly Report of Costly Assets";
            // 
            // chartView
            // 
            chartArea3.Name = "ChartArea1";
            this.chartView.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartView.Legends.Add(legend3);
            this.chartView.Location = new System.Drawing.Point(635, 339);
            this.chartView.Name = "chartView";
            series3.ChartArea = "ChartArea1";
            series3.CustomProperties = "PixelPointWidth=10";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartView.Series.Add(series3);
            this.chartView.Size = new System.Drawing.Size(310, 194);
            this.chartView.TabIndex = 6;
            this.chartView.Text = "chart1";
            // 
            // inventoryControlButton
            // 
            this.inventoryControlButton.Location = new System.Drawing.Point(18, 512);
            this.inventoryControlButton.Name = "inventoryControlButton";
            this.inventoryControlButton.Size = new System.Drawing.Size(131, 23);
            this.inventoryControlButton.TabIndex = 7;
            this.inventoryControlButton.Text = "Inventory Control";
            this.inventoryControlButton.UseVisualStyleBackColor = true;
            this.inventoryControlButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(170, 512);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(120, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(396, 517);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Language:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pieChartView
            // 
            chartArea4.Name = "ChartArea1";
            this.pieChartView.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.pieChartView.Legends.Add(legend4);
            this.pieChartView.Location = new System.Drawing.Point(635, 30);
            this.pieChartView.Name = "pieChartView";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.CustomProperties = "PieLabelStyle=Disabled";
            series4.Legend = "Legend1";
            series4.LegendText = "#VALX";
            series4.Name = "Series1";
            this.pieChartView.Series.Add(series4);
            this.pieChartView.Size = new System.Drawing.Size(310, 243);
            this.pieChartView.TabIndex = 12;
            this.pieChartView.Text = "chart2";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(618, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(336, 287);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Department Spending Ratio";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(619, 321);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(335, 224);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Monthly Department Spending";
            // 
            // languageBox
            // 
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Location = new System.Drawing.Point(458, 512);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(154, 20);
            this.languageBox.TabIndex = 15;
            // 
            // InventoryDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 551);
            this.Controls.Add(this.languageBox);
            this.Controls.Add(this.chartView);
            this.Controls.Add(this.pieChartView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.inventoryControlButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Name = "InventoryDashboardForm";
            this.Text = "InventoryDashboard";
            this.Load += new System.EventHandler(this.InventoryDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.emSpendingView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mostUsedPartsView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.costlyAssetsView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChartView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView emSpendingView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView mostUsedPartsView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView costlyAssetsView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartView;
        private System.Windows.Forms.Button inventoryControlButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChartView;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox languageBox;
    }
}