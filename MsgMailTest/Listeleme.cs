using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MsgMailTest
{
    public partial class Listeleme : Form
    {
        public Listeleme()
        {
            InitializeComponent();
        }
        public void gelenkutusu()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MESAJLAR where ALICI=" + veri, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void gidenkutusu()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MESAJLAR where GONDERICI=" + veri, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string veri;
        private void Listeleme_Load(object sender, EventArgs e)
        {
            SqlCommand kmt=new SqlCommand("select RESIM from TBL_KISILER where NUMARA="+veri, bgl.baglanti());
            SqlDataReader dr1 = kmt.ExecuteReader();
            while (dr1.Read())
            {
                pictureBox3.ImageLocation = dr1["RESIM"].ToString();
            }
            LblNumara.Text = veri;
            gelenkutusu();
            gidenkutusu();

            //AD SOYAD ÇEKME
            SqlCommand komut = new SqlCommand("Select AD,SOYAD from TBL_KISILER where NUMARA=" + veri, bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();
        }


        private void BtnYenile_Click(object sender, EventArgs e)
        {
            gidenkutusu();
            gelenkutusu();
        }

        private void BtnMail_Click(object sender, EventArgs e)
        {
            MailPanel frm = new MailPanel();
            frm.gonderen = veri.ToString();
            frm.alici = "0000";
            frm.Show();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                MailPanel frm = new MailPanel();
                frm.alici = dr["GONDERICI"].ToString();
                frm.gonderen = dr["ALICI"].ToString();
                frm.Show();
            }
        }
    }
}
