using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proje_Ogrenci_Akademisyen.Entity;

namespace Proje_Ogrenci_Akademisyen.Formlar
{
    public partial class FrmYeniDers : Form
    {
        public FrmYeniDers()
        {
            InitializeComponent();
        }
        OgrenciSinavEntities1 db = new OgrenciSinavEntities1();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TblDersler t = new TblDersler();
            t.DersAd = TxtDersAd.Text;
            db.TblDersler.Add(t);
            db.SaveChanges();
            MessageBox.Show("Yeni bir ders ekleme işlemi başarılı bir şekilde gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
