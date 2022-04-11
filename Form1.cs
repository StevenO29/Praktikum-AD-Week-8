using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Praktikum_AD_Week_8__Hasil_Pertandingan_
{
    public partial class FormHasilPertandingan : Form
    {
        public FormHasilPertandingan()
        {
            InitializeComponent();
        }

        public static string sqlConnection = "server = localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection); //Sebagai data koneksi ke DBMS
        public MySqlCommand sqlCommand; //Sebagai perintah SQL
        public MySqlDataAdapter sqlAdapter; //Sebagai pengelola data (select, insert, update, delete)
        string sqlQuery;

        private void FormHasilPertandingan_Load(object sender, EventArgs e)
        {
            DataTable teamName = new DataTable();
            sqlQuery = "SELECT team_name as `Team Name`, team_id as `Team ID` FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(teamName);
            cbBoxLeft.ValueMember = "Team ID";
            cbBoxLeft.DisplayMember = "Team Name";
            cbBoxLeft.DataSource = teamName;
            DataTable teamName2 = new DataTable();
            sqlQuery = "SELECT team_name as `Team Name`, team_id as `Team ID` FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(teamName2);
            cbBoxRight.ValueMember = "Team ID";
            cbBoxRight.DisplayMember = "Team Name";
            cbBoxRight.DataSource = teamName2;
        }

        private void cbBoxLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable managerAndCaptainLeft = new DataTable();
            sqlQuery = "select m.manager_name as `Manager Name`, p.player_name as `Captain` from manager m, player p, team t where m.manager_id = t.manager_id and t.captain_id = p.player_id and t.team_id = '" + cbBoxLeft.SelectedValue.ToString() + "'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(managerAndCaptainLeft);
            lblOutputManagerLeft.Text = managerAndCaptainLeft.Rows[0]["Manager Name"].ToString();
            lblOutputCaptainLeft.Text = managerAndCaptainLeft.Rows[0]["Captain"].ToString();
        }

        private void cbBoxRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable managerAndCaptainRight = new DataTable();
            sqlQuery = "select m.manager_name as `Manager Name`, p.player_name as `Captain` from manager m, player p, team t where m.manager_id = t.manager_id and t.captain_id = p.player_id and t.team_id = '" + cbBoxRight.SelectedValue.ToString() + "'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(managerAndCaptainRight);
            lblOutputManagerRight.Text = managerAndCaptainRight.Rows[0]["Manager Name"].ToString();
            lblOutputCaptainRight.Text = managerAndCaptainRight.Rows[0]["Captain"].ToString();
            DataTable stadiumCapacity = new DataTable();
            sqlQuery = "select concat(t.home_stadium, ', ', t.city) as `Stadium`, t.capacity as `Capacity` from team t where team_id = '" + cbBoxLeft.SelectedValue.ToString() + "'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(stadiumCapacity);
            lblOutputStadium.Text = stadiumCapacity.Rows[0]["Stadium"].ToString();
            lblOutputCapacity.Text = stadiumCapacity.Rows[0]["Capacity"].ToString();
        }
    }
}
