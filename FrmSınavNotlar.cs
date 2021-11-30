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

namespace Okul_projem
{
    public partial class FrmSınavNotlar : Form
    {
        public FrmSınavNotlar()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.TBLNOTLARTableAdapter ds = new DataSet1TableAdapters.TBLNOTLARTableAdapter();
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-RS4668M;Initial Catalog=E-Okul;Integrated Security=True");
        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotListesi(int.Parse(TxtID.Text));
        }

        private void FrmSınavNotlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERSLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ComBaxDers.DisplayMember = "DERSAD";
            ComBaxDers.ValueMember = "DERSID";
            ComBaxDers.DataSource = dt;
            baglanti.Close();
        }
        int notid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notid=int.Parse( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSınav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSınav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtSınav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
        int sinav1, sinav2, sinav3, proje;

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        double ortalama;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            
            //string durum;
            sinav1 = Convert.ToInt32(TxtSınav1.Text);
            sinav2 = Convert.ToInt32(TxtSınav1.Text);
            sinav3 = Convert.ToInt32(TxtSınav1.Text);
            proje = Convert.ToInt32(TxtSınav1.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            TxtOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                TxtDurum.Text = "True";
            }
            else
            {
                TxtDurum.Text = "False";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(ComBaxDers.SelectedValue.ToString()), int.Parse(TxtID.Text),Byte.Parse(TxtSınav1.Text), Byte.Parse(TxtSınav2.Text), Byte.Parse(TxtSınav3.Text), Byte.Parse(TxtProje.Text), Decimal.Parse(TxtOrtalama.Text), bool.Parse(TxtDurum.Text),notid);
        }
    }
}
