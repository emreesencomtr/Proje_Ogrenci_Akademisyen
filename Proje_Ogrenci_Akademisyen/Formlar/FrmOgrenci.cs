using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proje_Ogrenci_Akademisyen.Entity;

namespace Proje_Ogrenci_Akademisyen.Formlar
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            TxtResim.Text = openFileDialog1.FileName;  //path + file name
        }
        OgrenciSinavEntities1 db = new OgrenciSinavEntities1();

        void listele()
        {
            var degerler = from x in db.TblOgrenci
                           select new
                           {
                               x.OgrID,
                               x.OgrAd,
                               x.OgrSoyad,
                               x.OgrNumara,
                               x.OgrSifre,
                               x.OgrMail,
                               x.OgrResim,
                               x.OgrBolum,
                               x.TblBolum.BolumAd,
                               x.OgrDurum
                              
                           };
            dataGridView1.DataSource = degerler.Where(x=>x.OgrDurum==true).ToList();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-83L42UFV\SQLEXPRESS;Initial Catalog=OgrenciSinav;Integrated Security=True");
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Columns["OgrBolum"].Visible = false;
            dataGridView1.Columns["OgrDurum"].Visible = false;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblBolum", baglanti);
            //SqlDataReader dr = komut.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox1.ValueMember = "BolumID";
            comboBox1.DisplayMember = "BolumAd";
            comboBox1.DataSource = ds;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtNumara.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtResim.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TxtID.Text);
            var x = db.TblOgrenci.Find(id);
            x.OgrDurum = false;
            db.SaveChanges();
            MessageBox.Show("Öğrenci başarılı bir şekilde sistemden silindi, silinen öğrencilere pasif listesi üzerinden erişim sağlayabilirsiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TxtID.Text);
            var x = db.TblOgrenci.Find(id);
            x.OgrAd = TxtAd.Text;
            x.OgrSoyad = TxtSoyad.Text;
            x.OgrNumara = TxtNumara.Text;
            x.OgrMail = TxtMail.Text;
            x.OgrResim = TxtResim.Text;
            x.OgrSifre = TxtSifre.Text;
            x.OgrBolum = int.Parse(comboBox1.SelectedValue.ToString());
            db.SaveChanges();
            MessageBox.Show("Öğrenci başarılı bir şekilde sistemden güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
    }
}
