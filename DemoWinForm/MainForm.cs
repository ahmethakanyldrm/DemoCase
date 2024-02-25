using DevExpress.Export.Xl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using System.IO;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using DevExpress.XtraRichEdit.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using DevExpress.XtraRichEdit.API.Native;

namespace DemoWinForm
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public string filePath;

        private void openButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // OpenFileDialog nesnesi 
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Filtre ayarlama
            openFileDialog.Filter = "Word Dosyaları (*.docx)|*.docx";

            // Dialog penceresini gösterme
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu alma
                 filePath = openFileDialog.FileName;

                richEditControl1.LoadDocument(filePath, DocumentFormat.OpenXml);

            }
        }
       
        private async void saveButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

        }


        private void printButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Print dialog oluştur
            PrintDialog printDialog = new PrintDialog();

            // Eğer kullanıcı bir yazıcı seçerse
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Yazdırma işlemini başlat
                richEditControl1.Print(printDialog.PrinterSettings);
            }
        }

        private async void deleteButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          

        }

    }


}
