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
    public partial class FrmKulup : Form
    {
        public FrmKulup()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-RS4668M;Initial Catalog=E-Okul;Integrated Security=True");
         void Liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT *FORM TBLKULUPLER", baglanti);
            DataTable dt = new DataTable();
           // da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmKulup_Load(object sender, EventArgs e)
        {
            Liste();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Liste();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(" INSERT INTO TBLKULUPLER (KULUPAD) VALUES (@P1)",baglanti);
            komut.Parameters.AddWithValue("@P1", TxtKulupAd.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Yellow;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKulupId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKulupAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From TBLKULUPLER WHERE KULUPID=P1", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtKulupId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp Silme İşlemi Gerçekleşti");
            Liste();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLKULUPLER SET KULUPAD =@P1 WHERE KULUPID=@P2",baglanti);
            komut.Parameters.AddWithValue("@P1", TxtKulupAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtKulupId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp Güncelleme İşlemi Gerçekleşti");
            Liste();
        }
    }
}
