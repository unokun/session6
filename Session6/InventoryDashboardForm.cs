using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Session6.Repository;

namespace Session6
{
    public partial class InventoryDashboardForm : Form
    {
        public InventoryDashboardForm()
        {
            InitializeComponent();
        }

        private void InventoryDashboardForm_Load(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            // GM Expediture
            initEmSpendingView(inventory);

            // Most-Used Part
            initMostUsedPart(inventory);

            // Costly Assets
            initCostlyAssets(inventory);

            //  compare departmental spending on a pie chart
            //pie chart
            initPieChart();

            //  how much each month is spent in each of the departments
            initChart();
        }
        private void initChart()
        {

            chartView.Series.Clear();
            chartView.ChartAreas.Clear();

            chartView.ChartAreas.Add(new ChartArea("Area1"));
            //chartView.ChartAreas[0].AxisX.Interval = 1;
            chartView.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            //chartView.ChartAreas[0].AxisX.IntervalOffsetType = DateTimeIntervalType.Hours;
            chartView.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM";

            // ChartにSeriesを追加します
            string legend = "Department";
            chartView.Series.Add(legend);

            chartView.Series[legend].ChartType = SeriesChartType.Column;
            chartView.Series[legend].XValueType = ChartValueType.Date;
            // barの幅
            chartView.Series[legend]["PixelPointWidth"] = "20";
            int count = emSpendingView.RowCount;
            Dictionary<string, Series> seriesList = new Dictionary<string, Series>();
            for (int i = 0; i < count; i++)
            {
                string department = (string)emSpendingView.Rows[i].Cells[0].Value;
                for (int j = 1; j < emSpendingView.ColumnCount; j++)
                {
                    string month = emSpendingView.Columns[j].HeaderText;
                    int expend = (int)emSpendingView.Rows[i].Cells[j].Value;

                    Series series;
                    if (seriesList.TryGetValue(department, out series) == false)
                    {
                        seriesList.Add(department, series = new Series(department));
                        chartView.Series.Add(series);
                    }
                    DataPoint dp = new DataPoint(expend, expend);
                    dp.SetValueXY(month, expend);
                    series.Points.Add(dp);
                }
            }
        }
        private void initPieChart()
        {
            // pieChartView
            pieChartView.Series.Clear();
            pieChartView.ChartAreas.Clear();

            pieChartView.ChartAreas.Add(new ChartArea("Area1"));
            // ChartにSeriesを追加します
            string legend1 = "Department";
            pieChartView.Series.Add(legend1);
            // グラフの種別を指定
            pieChartView.Series[legend1].ChartType = SeriesChartType.Pie; 


            // 部門ごとの支出をカウント
            int count = emSpendingView.RowCount;
            double[] values = new double[count];
            for (int i = 0; i < count; i++)
            {
                double value = 0.0;
                for (int j = 1; j < emSpendingView.ColumnCount; j++)
                {
                    value += (int)emSpendingView.Rows[i].Cells[j].Value;
                }
                values[i] = value;

            }

            // 各項目の値を加算して合計(全体の大きさ)を算出します
            double total = 0.0;
            foreach (double d in values)
            {
                total += d;
            }
            // データをシリーズにセットします
            for (int i = 0; i < values.Length; i++)
            {
                double rate = (values[i] / total) * 100.0;  // <-- ここで割合を算出します
                DataPoint dp = new DataPoint(rate, rate);
                dp.SetValueXY((string)emSpendingView.Rows[i].Cells[0].Value, rate);
                pieChartView.Series[legend1].Points.Add(dp);
            }
        }
        private void initCostlyAssets(Inventory inventory)
        {
            var resultsAsset = inventory.GetMostCostlyAssets();

            // 列(日付)作成
            // HashSetを使って重複を除く
            var ymSet = new SortedSet<String>();
            foreach (var key in resultsAsset[0].Keys)
            {
                ymSet.Add(key.ToString());
            }
            // sort(降順)
            var sortedYms = ymSet.Reverse().ToList();
            // カラム数を指定
            costlyAssetsView.ColumnCount = sortedYms.Count + 1;
            // 行数を設定
            costlyAssetsView.RowCount = 2;

            // カラム名を指定
            mostUsedPartsView.Columns[0].HeaderText = "Asset Names";
            var j = 1;
            foreach (var ym in sortedYms)
            {
                costlyAssetsView.Columns[j++].HeaderText = ym;
            }
            // セルに値を設定
            var i = 0;
            costlyAssetsView.Rows[i].Cells[0].Value = "Asset";
            foreach (var key in resultsAsset[0].Keys)
            {
                j = 1;
                foreach (var ym in sortedYms)
                {
                    String partName = "";
                    if (resultsAsset[0].ContainsKey(ym))
                    {
                        partName = (string)resultsAsset[0][ym];
                    }
                    costlyAssetsView.Rows[i].Cells[j++].Value = partName;
                }
            }
            i = 1;
            costlyAssetsView.Rows[i].Cells[0].Value = "Department";
            foreach (var key in resultsAsset[1].Keys)
            {
                j = 1;
                foreach (var ym in sortedYms)
                {
                    String partName = "";
                    if (resultsAsset[1].ContainsKey(ym))
                    {
                        partName = (string)resultsAsset[1][ym];
                    }
                    costlyAssetsView.Rows[i].Cells[j++].Value = partName;
                }
            }
        }
        private void initMostUsedPart(Inventory inventory)
        {
            var resultsCost = inventory.GetMostlyUsedPartsByCost();

            // 列(日付)作成
            // HashSetを使って重複を除く
            var ymSet = new SortedSet<String>();
            foreach (var key in resultsCost.Keys)
            {
                ymSet.Add(key.ToString());
            }
            // sort(降順)
            var sortedYms = ymSet.Reverse().ToList();

            // カラム数を指定
            mostUsedPartsView.ColumnCount = sortedYms.Count + 1;
            // 行数を設定
            mostUsedPartsView.RowCount = 2;

            // カラム名を指定
            mostUsedPartsView.Columns[0].HeaderText = "Notes";
            var j = 1;
            foreach (var ym in sortedYms)
            {
                mostUsedPartsView.Columns[j++].HeaderText = ym;
            }

            // セルに値を設定
            var i = 0;
            mostUsedPartsView.Rows[i].Cells[0].Value = "Highest Cost";
            foreach (var key in resultsCost.Keys)
            {
                j = 1;
                foreach (var ym in sortedYms)
                {
                    String partName = "";
                    if (resultsCost.ContainsKey(ym))
                    {
                        partName = (string)resultsCost[ym];
                    }
                    mostUsedPartsView.Rows[i].Cells[j++].Value = partName;
                }
            }

            var resultsAmount = inventory.GetMostlyUsedPartsByAmount();
            i = 1;
            mostUsedPartsView.Rows[i].Cells[0].Value = "Most Number";
            foreach (var key in resultsAmount.Keys)
            {
                j = 1;
                foreach (var ym in sortedYms)
                {
                    String partName = "";
                    if (resultsAmount.ContainsKey(ym))
                    {
                        partName = (string)resultsAmount[ym];
                    }
                    mostUsedPartsView.Rows[i].Cells[j++].Value = partName;
                }
            }
        }
        /// <summary>
        /// emSpendingView初期化
        /// </summary>
        /// <param name="inventory"></param>
        private void initEmSpendingView(Inventory inventory)
        {
            // GM Expediture
            var results = inventory.GetEmExpediture();
            Dictionary<String, Hashtable> dic = convertFrom(results);

            // 列(日付)作成
            // HashSetを使って重複を除く
            var ymSet = new SortedSet<String>();
            foreach (var result in results)
            {
                ymSet.Add(result.RequestMonth);
            }
            // sort(降順)
            var sortedYms = ymSet.Reverse().ToList();

            // カラム数を指定
            emSpendingView.ColumnCount = sortedYms.Count + 1;
            // 行数を設定
            emSpendingView.RowCount = dic.Count;

            // カラム名を指定
            emSpendingView.Columns[0].HeaderText = "Department";
            var i = 1;
            foreach (var ym in sortedYms)
            {
                emSpendingView.Columns[i++].HeaderText = ym;
            }

            // セルに値を設定
            i = 0;
            foreach (var department in dic.Keys)
            {
                var j = 0;
                emSpendingView.Rows[i].Cells[j++].Value = department;
                Hashtable hashTable = null;
                dic.TryGetValue(department, out hashTable);
                foreach (var ym in sortedYms)
                {
                    int spending = 0;
                    if (hashTable.ContainsKey(ym))
                    {
                        spending = (int)hashTable[ym];
                    }
                    emSpendingView.Rows[i].Cells[j++].Value = spending;
                }
                i++;
            }

            // 最大値・最小値の色づけを行う。
            for (var jj = 1; jj < emSpendingView.ColumnCount; jj++)
            {
                int min = Int32.MaxValue;
                int max = 0;
                for (var ii = 0; ii < emSpendingView.RowCount; ii++)
                {
                    var value = (int)emSpendingView.Rows[ii].Cells[jj].Value;
                    if (value < min) { min = value;  }
                    if (value > max) { max = value; }
                }
                for (var ii = 0; ii < emSpendingView.RowCount; ii++)
                {
                    var value = (int)emSpendingView.Rows[ii].Cells[jj].Value;
                    if (value == max) {
                        emSpendingView.Rows[ii].Cells[jj].Style.ForeColor = Color.Red;
                    }
                    else if (value == min && value != max && value != 0 )
                    {
                        emSpendingView.Rows[ii].Cells[jj].Style.ForeColor = Color.Green;
                    }
                }
            }
        }
        /// <summary>
        /// DBから取得したレコードを部門ごとの月次支出データ(HashTable)に変換する
        /// </summary>
        /// <param name="emExpeditures"></param>
        /// <returns></returns>
        private Dictionary<String, Hashtable> convertFrom(List<EmExpediture> emExpeditures)
        {
            Dictionary<String, Hashtable> dic = new Dictionary<String, Hashtable>();
            foreach (var expediture in emExpeditures)
            {
                var key = expediture.Department;
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, new Hashtable());
                }
                Hashtable hashTable = null;
                dic.TryGetValue(key, out hashTable);
                if (hashTable != null)
                {
                    hashTable.Add(expediture.RequestMonth, expediture.Expenditure);
                }
            }
            return dic;
        }

        private void mostUsedPartsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InventoryControlForm form = new InventoryControlForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
