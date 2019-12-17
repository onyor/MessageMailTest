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
    public partial class MailPanel : Form
    {
        public string gonderen;
        public MailPanel()
        {
            InitializeComponent();
        }
        
        public string alici;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void MailPanel_Load(object sender, EventArgs e)
        {
            MskMailAdresi.Text = alici.ToString();
        }
        private void BtnGonder_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MESAJLAR (GONDERICI,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", gonderen);
            komut.Parameters.AddWithValue("@p2", MskMailAdresi.Text);
            komut.Parameters.AddWithValue("@p3", TxtKonu.Text);
            komut.Parameters.AddWithValue("@p4", RchMesaj.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Mesajınız İletildi...");

            var mfrm = (Listeleme)Application.OpenForms["Listeleme"];
            if (mfrm != null)
                mfrm.gelenkutusu();
                mfrm.gidenkutusu(); 
        }
    }
}
