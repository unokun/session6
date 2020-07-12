using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }

}
