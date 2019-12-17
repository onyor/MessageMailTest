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
    public partial class YeniHesap : Form
    {
        public YeniHesap()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD,SOYAD,TARIH,IL,ILCE,NUMARA,RESIM from TBL_KISILER", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void temizle()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTarih.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtNumara.Text = "";
            TxtSifre.Text = "";

        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex+1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_KISILER (AD,SOYAD,TARIH,IL,ILCE,NUMARA,SIFRE,RESIM) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTarih.Text);
            komut.Parameters.AddWithValue("@p4", CmbIl.Text);
            komut.Parameters.AddWithValue("@p5", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p6", TxtNumara.Text);
            komut.Parameters.AddWithValue("@p7", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p8", TxtResim.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Sistemde Kayıt Açıldı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();
            listele();

            
        }

        private void YeniHesap_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirlistesi();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_KISILER where NUMARA=@p1 AND ID=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtNumara.Text);
            komut.Parameters.AddWithValue("@p2", LblId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtNumara.Text = dr["NUMARA"].ToString();
                LblId.Text = dr["ID"].ToString();
            }
        }
        

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle(); ;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_KISILER set AD=@p1,SOYAD=@p2,TARIH=@p3,IL=@p4,ILCE=@p5,SIFRE=@p7,NUMARA=@p6,RESIM=@p9 where ID=@p8", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTarih.Text);
            komut.Parameters.AddWithValue("@p4", CmbIl.Text);
            komut.Parameters.AddWithValue("@p5", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p6", TxtNumara.Text);
            komut.Parameters.AddWithValue("@p7", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p8", LblId.Text);
            komut.Parameters.AddWithValue("@p9", TxtResim.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kaydınız Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            TxtResim.Text = openFileDialog1.FileName;
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Resim frm = new Resim();
                frm.veri = dr["RESIM"].ToString();
                frm.Show();
            }
        }

        private void RndButton_Click(object sender, EventArgs e)
        {
            //Kayıt Engelleme
            Random rnd = new Random();
            int sayi = rnd.Next(1000, 9999);
            SqlCommand komut = new SqlCommand("select * from TBL_KISILER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if (sayi.ToString() == dr["NUMARA"].ToString())
                {
                    sayi = rnd.Next(1000, 9999);
                }
            }
            TxtNumara.Text = sayi.ToString();
        }

        //Kullancı Adı Tekrar Etmemesi için
        /*private void TxtNumara_EditValueChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from TBL_KISILER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if (TxtNumara.Text == dr["NUMARA"].ToString())
                {
                    MessageBox.Show("Yeni bir kullanıcı adı giriniz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }*/
    }
}
