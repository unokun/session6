using Session6.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session6
{
    public partial class InventoryControlForm : Form
    {
        public InventoryControlForm()
        {
            InitializeComponent();

            Inventory inventory = new Inventory();
            #region assetNames(Combobox)
            //initAssetNames(inventory);
            {
                DataTable dt = inventory.GetAssetNames();
                cbAssetNames.DataSource = dt;
                cbAssetNames.DisplayMember = "AssetName";
                cbAssetNames.ValueMember = "ID";

                cbAssetNames.SelectedValue = "0";
            }
            #endregion

            #region warehouseNames(Combobox)
            {
                DataTable dt = inventory.GetWarehouseNames();
                cbWarehouseNames.DataSource = dt;
                cbWarehouseNames.DisplayMember = "Name";
                cbWarehouseNames.ValueMember = "ID";

                cbWarehouseNames.SelectedValue = "0";
            }
            #endregion
        }

        private void initAssetNames(Inventory inventory)
        {
            DataTable dt = inventory.GetAssetNames();
            cbAssetNames.DataSource = dt;
            cbAssetNames.DisplayMember = "AssetName";
            cbAssetNames.ValueMember = "ID";

            cbAssetNames.SelectedValue = "0";
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbWarehouseNames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
