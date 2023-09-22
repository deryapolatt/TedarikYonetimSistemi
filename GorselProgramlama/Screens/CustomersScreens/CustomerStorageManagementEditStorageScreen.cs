﻿using GorselProgramlama.Models;
using GorselProgramlama.Screens.HomeAndLoginScreens;
using GorselProgramlama.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProgramlama.Screens.CustomersScreens
{
    public partial class CustomerStorageManagementEditStorageScreen : UserControl
    {
        public Storages SelectedStorage = new Storages();
        public CustomerStorageManagementEditStorageScreen()
        {
            InitializeComponent();
            //Depoların Listelendiği Kısım
            using (var db = new DbService())
            {
                var storages = db.GetList<Storages>($"{nameof(Storages.StorageOwnerUsername)}='{StaticEntities.ActiveUsername}'");
                dataGridView1.DataSource = storages;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Kaydete basınca deponun eklendiği kısım
            using (var db = new DbService())
            {
                var storage = db.FirstOrDefault<Storages>($"{nameof(Storages.Id)}={SelectedStorage.Id}");
                if (textBox2.Text != "" && textBox3.Text != "")
                {
                    storage.StorageLocation = textBox2.Text;
                    storage.StorageName = textBox3.Text;
                    db.AddOrUpdateEntity(storage);
                }
                else
                {
                    GerekliAlanlariDoldur gerekliAlanlariDoldur = new GerekliAlanlariDoldur();
                    gerekliAlanlariDoldur.Show();
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Tablo üzerinden bir kayıta çift tıklandığında otomatik olarak aktarılıyor.
            var selectedRowIndex = e.RowIndex;
            var selectedRow = dataGridView1.Rows[selectedRowIndex];
            SelectedStorage.Id = (int)selectedRow.Cells[4].Value;
            SelectedStorage.StorageName = selectedRow.Cells[0].Value.ToString();
            SelectedStorage.StorageOwnerType = "Müşteri";
            SelectedStorage.StorageOwnerUsername = StaticEntities.ActiveUsername;
            SelectedStorage.StorageLocation = selectedRow.Cells[3].Value.ToString();
            textBox2.Text = SelectedStorage.StorageLocation;
            textBox3.Text = SelectedStorage.StorageName;
        }
    }
}
