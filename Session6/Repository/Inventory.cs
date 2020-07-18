using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Session6.Repository
{
    class Inventory
    {
        const string server = "localhost";        // MySQLサーバホスト名
        const string user = "root";               // MySQLユーザ名
        const string pass = "hoge1234";           // MySQLパスワード
        const string database = "Session6";       // 接続するデータベース名
        string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);

        public DataTable GetWarehouseNames()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("select")
            .AppendLine("  ID")
            .AppendLine("  , Name")
            .AppendLine("from")
            .AppendLine("  warehouses")
            .AppendLine("order by ID");
            String sql = builder.ToString();

            DataTable dt = new DataTable();
            DataRow row;
            String id = "ID";
            String name = "Name";
            dt.Columns.Add(id);
            dt.Columns.Add(name);

            row = dt.NewRow();
            row[id] = "0";
            row[name] = "";
            try
            {
                // auto close
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    row = dt.NewRow();
                                    row[id] = reader.GetString(0);
                                    row[name] = reader.GetString(1);
                                    dt.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return dt;
        }

        public DataTable GetAssetNames()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("select")
            .AppendLine("  EM.ID as ID")
            .AppendLine("  , assets.AssetName as AssetName")
            .AppendLine("from")
            .AppendLine("  orders")
            .AppendLine("inner join emergencymaintenances EM ")
            .AppendLine("    on EM.ID = orders.EmergencyMaintenancesID")
            .AppendLine("	and EM.EMStartDate is not null")
            .AppendLine("--	and EM.EMEndDate is null")
            .AppendLine("inner join assets")
            .AppendLine("	on assets.ID = EM.AssetID")
            .AppendLine("group by EM.ID")
            .AppendLine("order by EM.ID");
            String sql = builder.ToString();

            DataTable dt = new DataTable();
            DataRow row;
            String id = "ID";
            String assetName = "AssetName";
            dt.Columns.Add(id);
            dt.Columns.Add(assetName);

            row = dt.NewRow();
            row[id] = "0";
            row[assetName] = "";
            try
            {
                // auto close
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    row = dt.NewRow();
                                    row[id] = reader.GetString(0);
                                    row[assetName] = reader.GetString(1) + "(" + reader.GetString(0) + ")";
                                    dt.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return dt;
        }
        /// <summary>
        /// 月ごとに最もコストのかかっているアセット
        /// </summary>
        /// <returns></returns>
        public List<Hashtable> GetMostCostlyAssets()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("select")
                .AppendLine(" T1.AssetName")
                .AppendLine(" , T1.DepartmentName")
                .AppendLine(" , max(T1.expenditure)")
                .AppendLine(" , T1.RequestMonth")
                .AppendLine("from")
                .AppendLine("(select")
                .AppendLine("  assets.AssetName as AssetName")
                .AppendLine("  ,DPT.Name as DepartmentName")
                .AppendLine("  ,SUM(ITEM.Amount * ITEM.UnitPrice) as expenditure")
                .AppendLine("  ,DATE_FORMAT(EM.EMRequestDate, '%Y-%m') as RequestMonth")
                .AppendLine("from")
                .AppendLine("  orders")
                .AppendLine("inner join emergencymaintenances EM ")
                .AppendLine("    on EM.ID = orders.EmergencyMaintenancesID")
                .AppendLine("	and EM.EMEndDate is not null")
                .AppendLine("inner join OrderItems ITEM")
                .AppendLine("	on ITEM.OrderID = orders.ID	")
                .AppendLine("inner join assets")
                .AppendLine("	on assets.ID = EM.AssetID")
                .AppendLine("inner join departmentlocations DL")
                .AppendLine("	on DL.ID = assets.DepartmentLocationID")
                .AppendLine("inner join departments DPT")
                .AppendLine("	on DPT.ID = DL.DepartmentID")
                .AppendLine("group by  RequestMonth, EM.AssetID")
                .AppendLine("order by RequestMonth desc, expenditure desc) as T1")
                .AppendLine("group by T1.RequestMonth");
            String sql = builder.ToString();
            var results = new List<Hashtable>();
            results.Add(new Hashtable());
            results.Add(new Hashtable());
            try
            {
                // auto close
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var expenditure = new EmExpediture();
                                    results[0].Add(reader.GetString(3), reader.GetString(0));
                                    results[1].Add(reader.GetString(3), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return results;
        }
        /// <summary>
        /// 月ごとに最も使われているパーツ(個数)
        /// </summary>
        /// <returns></returns>
        public Hashtable GetMostlyUsedPartsByAmount()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("select")
                .AppendLine(" T1.Name")
                .AppendLine(" , T1.RequestMonth")
                .AppendLine(" , max(T1.Amount)")
                .AppendLine("from")
                .AppendLine("(select")
                .AppendLine("  parts.Name as Name")
                .AppendLine("  ,SUM(ITEM.Amount) as Amount")
                .AppendLine("  ,DATE_FORMAT(EM.EMRequestDate, '%Y-%m') as RequestMonth")
                .AppendLine("from")
                .AppendLine("  orders")
                .AppendLine("inner join emergencymaintenances EM ")
                .AppendLine("    on EM.ID = orders.EmergencyMaintenancesID")
                .AppendLine("	and EM.EMEndDate is not null")
                .AppendLine("inner join OrderItems ITEM")
                .AppendLine("	on ITEM.OrderID = orders.ID	")
                .AppendLine("inner join parts")
                .AppendLine("	on parts.ID = ITEM.PartID")
                .AppendLine("group by  RequestMonth, ITEM.PartID")
                .AppendLine("order by RequestMonth desc, Amount desc) as T1")
                .AppendLine("group by T1.RequestMonth");


            return executeQuery(builder.ToString());
        }

        /// <summary>
        /// 月ごとに最も使われているパーツ(支出)
        /// </summary>
        /// <returns></returns>
        public Hashtable GetMostlyUsedPartsByCost()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("select")
                .AppendLine(" T1.Name")
                .AppendLine(" , T1.RequestMonth")
                .AppendLine(" , max(T1.expenditure)")
                .AppendLine("from")
                .AppendLine("(select")
                .AppendLine("  parts.Name as Name")
                .AppendLine("  ,SUM(ITEM.Amount * ITEM.UnitPrice) as expenditure")
                .AppendLine("  ,DATE_FORMAT(EM.EMRequestDate, '%Y-%m') as RequestMonth")
                .AppendLine("from")
                .AppendLine("  orders")
                .AppendLine("inner join emergencymaintenances EM ")
                .AppendLine("    on EM.ID = orders.EmergencyMaintenancesID")
                .AppendLine("	and EM.EMEndDate is not null")
                .AppendLine("inner join OrderItems ITEM")
                .AppendLine("	on ITEM.OrderID = orders.ID	")
                .AppendLine("inner join parts")
                .AppendLine("	on parts.ID = ITEM.PartID")
                .AppendLine("group by  RequestMonth, ITEM.PartID")
                .AppendLine("order by RequestMonth desc, expenditure desc) as T1")
                .AppendLine("group by T1.RequestMonth");
            return executeQuery(builder.ToString());

        }


        private Hashtable executeQuery(String sql)
        {
            var result = new Hashtable();
            try
            {
                // auto close
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var expenditure = new EmExpediture();
                                    result.Add(reader.GetString(1), reader.GetString(0));
                                }
                            }
                        }
                    }
                }


            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return result;
        }
        public List<EmExpediture> GetEmExpediture()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("select")
                .AppendLine("  DPT.Name as Department")
                .AppendLine("  ,LOC.Name as Location")
                .AppendLine("  ,SUM(ITEM.Amount * ITEM.UnitPrice) as Expenditure")
                .AppendLine("  ,DATE_FORMAT(EM.EMRequestDate, '%Y-%m') as RequestMonth")
                .AppendLine("from")
                .AppendLine("  orders")
                .AppendLine("inner join emergencymaintenances EM")
                .AppendLine("    on EM.ID = orders.EmergencyMaintenancesID")
                .AppendLine("    and EM.EMEndDate is not null")
                .AppendLine("inner join OrderItems ITEM")
                .AppendLine("	on ITEM.OrderID = orders.ID")
                .AppendLine("inner join assets")
                .AppendLine("	on assets.ID = EM.AssetID")
                .AppendLine("inner join departmentlocations DL")
                .AppendLine("	on DL.ID = assets.DepartmentLocationID")
                .AppendLine("inner join departments DPT")
                .AppendLine("	on DPT.ID = DL.DepartmentID")
                .AppendLine("inner join Locations LOC")
                .AppendLine("	on LOC.ID = DL.LocationID")
                .AppendLine("group by RequestMonth")
                .AppendLine("order by RequestMonth desc");

            string sql = builder.ToString();
            var results = new List<EmExpediture>();
            try
            {
                // auto close
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var expenditure = new EmExpediture();
                                    expenditure.Department = reader.GetString(0);
                                    expenditure.Location = reader.GetString(1);
                                    expenditure.Expenditure = reader.GetInt32(2);
                                    expenditure.RequestMonth = reader.GetString(3);
                                    results.Add(expenditure);
                                }
                            }
                        }
                    }
                }


            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return results;

        }
    }
}
