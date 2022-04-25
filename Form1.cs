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

namespace W10_Praktikum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string sqlConnection = "server=localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter mySqlAdapter;
        public string sqlQuery;
        private void btn_check_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable detailMatch = new DataTable();
                DataTable DateandMatchScore = new DataTable();

                // Buat tanggal dan skor
                sqlQuery = $"select date_format(m.match_date, '%e %M %Y') as Tanggal, concat(m.goal_home, ' - ', m.goal_away) as Skor from `match` m where m.team_home = '{cb_timHome.SelectedValue.ToString()}' and m.team_away = '{cb_timAway.SelectedValue.ToString()}'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                mySqlAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlAdapter.Fill(DateandMatchScore);
                lbl_isitanggal.Text = DateandMatchScore.Rows[0][0].ToString();
                lbl_isiskor.Text = DateandMatchScore.Rows[0][1].ToString();
                // Buat Detail
                sqlQuery = $"select dm.minute as Minute, if (dm.type = 'GW', if(dm.team_id != m.team_home, p.player_name,''), if (p.team_id = m.team_home, p.player_name, '' )) as 'Player Name 1', if (dm.type = 'GW', if(dm.team_id != m.team_home, 'OWN GOAL' ,''), if(p.team_id = m.team_home, if(dm.type = 'CY', 'YELLOW CARD', if(dm.type = 'CR', 'RED CARD', if(dm.type = 'GO', 'GOAL', if(dm.type = 'GP', 'GOAL PENALTY', if(dm.type = 'GW', 'OWN GOAL', if(dm.type = 'PM', 'PENALTY MISS', p.player_name)))))),'')) as 'Tipe 1', if (dm.type = 'GW', if(dm.team_id != m.team_away, p.player_name,''), if (p.team_id = m.team_away, p.player_name, '' )) as 'Player Name 2', if (dm.type = 'GW', if(dm.team_id != m.team_away, 'OWN GOAL' ,''), if(p.team_id = m.team_away, if(dm.type = 'CY', 'YELLOW CARD', if(dm.type = 'CR', 'RED CARD', if(dm.type = 'GO', 'GOAL', if(dm.type = 'GP', 'GOAL PENALTY', if(dm.type = 'GW', 'OWN GOAL', if(dm.type = 'PM', 'PENALTY MISS', p.player_name)))))), '')) as 'Tipe 2' from dmatch dm, player p, `match` m where dm.match_id = m.match_id and p.player_id = dm.player_id and m.team_home = '{cb_timHome.SelectedValue.ToString()}' and m.team_away = '{cb_timAway.SelectedValue.ToString()}' order by 1";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                mySqlAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlAdapter.Fill(detailMatch);
                dataGridView1.DataSource = detailMatch;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nTidak ada data!");
            }
        }

        private void cb_timHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dtStadium = new DataTable();
            //DataTable dtCapacity = new DataTable(); 
            //DataTable dtCaptain = new DataTable();
            //DataTable dtManager = new DataTable();


            //sqlQuery = "SELECT t.team_name, t.team_id, t.home_stadium, t.capacity , m.manager_name, p.player_name FROM team t, manager m, player p where t.manager_id = m.manager_id and p.player_id = t.captain_id";
            //sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            //mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            //mySqlAdapter.Fill(dtStadium);
            //mySqlAdapter.Fill(dtCapacity);
            //mySqlAdapter.Fill(dtManager);
            //mySqlAdapter.Fill(dtCaptain);



            //lbl_isiStadium.Text = dtStadium.Rows[cb_timHome.SelectedIndex][2].ToString();
            //lbl_isiKapasitas.Text  = dtCapacity.Rows[cb_timHome.SelectedIndex][3].ToString();
            //lbl_isicaptainHome.Text = dtCaptain.Rows[cb_timHome.SelectedIndex][5].ToString();
            //lbl_isimanagerHome.Text = dtManager.Rows[cb_timHome.SelectedIndex][4].ToString();
           
            DataTable managerAndCaptainHome = new DataTable();

            sqlQuery = $"select m.manager_name as `Manager Name`, p.player_name as `Captain` from manager m, player p, team t where m.manager_id = t.manager_id and t.captain_id = p.player_id and t.team_id = '{cb_timHome.SelectedValue.ToString()}'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlAdapter.Fill(managerAndCaptainHome);
            lbl_isimanagerHome.Text = managerAndCaptainHome.Rows[0]["Manager Name"].ToString();
            lbl_isicaptainHome.Text = managerAndCaptainHome.Rows[0]["Captain"].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DataTable dtTeam = new DataTable();
            //DataTable dtTeam2 = new DataTable();
            //sqlQuery = "SELECT t.team_name as 'Nama Tim', t.team_id as 'ID Team', t.home_stadium as 'Home Stadium', t.capacity as 'Capacity', m.manager_name as  `Manager`, p.player_name FROM team t, manager m, player p where t.manager_id = m.manager_id and p.player_id = t.captain_id";
            //sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            //mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            //mySqlAdapter.Fill(dtTeam);
            //mySqlAdapter.Fill(dtTeam2);
            //cb_timHome.DataSource = dtTeam;
            //cb_timAway.DataSource = dtTeam2;
            //cb_timAway.DisplayMember = "Nama Tim";
            //cb_timHome.DisplayMember = "Nama Tim";
           
            DataTable teamNameHome = new DataTable();
            sqlQuery = "SELECT team_name as `Team Name`, team_id as `Team ID` FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlAdapter.Fill(teamNameHome);
            cb_timHome.ValueMember = "Team ID";
            cb_timHome.DisplayMember = "Team Name";
            cb_timHome.DataSource = teamNameHome;
            DataTable teamNameAway = new DataTable();
            sqlQuery = "SELECT team_name as `Team Name`, team_id as `Team ID` FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlAdapter.Fill(teamNameAway);
            cb_timAway.ValueMember = "Team ID";
            cb_timAway.DisplayMember = "Team Name";
            cb_timAway.DataSource = teamNameAway;
        }

        private void cb_timAway_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sqlQuery = "SELECT t.team_name, t.team_id, t.home_stadium, t.capacity , m.manager_name, p.player_name FROM team t, manager m, player p where t.manager_id = m.manager_id and p.player_id = t.captain_id";
            //sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            //mySqlAdapter = new MySqlDataAdapter(sqlCommand);

            //lbl_isicaptainAway.Text = dtCaptain.Rows[cb_timAway.SelectedIndex][5].ToString();
            //lbl_isimanagerAway.Text = dtManager.Rows[cb_timAway.SelectedIndex][4].ToString();
            
            DataTable managerAndCaptainAway = new DataTable();
            DataTable StadiumandCapacity = new DataTable();

            //Buat nampilin captain dan manager Away
            sqlQuery = $"select m.manager_name as `Manager Name`, p.player_name as `Captain` from manager m, player p, team t where m.manager_id = t.manager_id and t.captain_id = p.player_id and t.team_id = '{cb_timAway.SelectedValue.ToString()}'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlAdapter.Fill(managerAndCaptainAway);
            lbl_isimanagerAway.Text = managerAndCaptainAway.Rows[0][0].ToString();
            lbl_isicaptainHome.Text = managerAndCaptainAway.Rows[0][1].ToString();
            //Buat nampilin stadium dan capacity
            sqlQuery = $"select concat(t.home_stadium, ', ', t.city) as `Stadium`, t.capacity as `Capacity` from team t where t.team_id = '{cb_timHome.SelectedValue.ToString()}'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            mySqlAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlAdapter.Fill(StadiumandCapacity);
            lbl_isiStadium.Text = StadiumandCapacity.Rows[0]["Stadium"].ToString();
            lbl_isiKapasitas.Text = StadiumandCapacity.Rows[0]["Capacity"].ToString();
        }
    }
}
