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
    public partial class Resim : Form
    {
        public Resim()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string veri;
        private void Resim_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = veri.ToString();
        }
    }
}
