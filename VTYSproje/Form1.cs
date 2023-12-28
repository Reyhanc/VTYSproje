using Npgsql;
using System.Windows.Forms;

namespace VTYSproje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432;Database=Proje; user ID=postgres; password=5234");
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void VerileriGuncelle()
        {
            try
            {
                baglanti.Open();

                string sorgu = "SELECT * FROM \"Yurt\".\"�grenci\"";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti))
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    // DataGridView'i g�ncellenmi� verilerle doldur
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // Kullan�c�dan al�nan de�erler
                string ad = txtAd.Text;
                string soyad = txtSoyad.Text;
                string telefon = txtTelefon.Text;
                int yurt = Convert.ToInt32(txtYurt.Text);
                int oda = Convert.ToInt32(txtOda.Text);
                int blok = Convert.ToInt32(txtBlok.Text);
                int ogrenciId = Convert.ToInt32(txtParmakiziId.Text);
                string tc = txtTc.Text;

                // INSERT sorgusu
                string sorgu = "INSERT INTO \"Yurt\".\"�grenci\" (\"Ad\", \"Soyad\", \"Telefon\", \"Yurt\", \"Oda\", \"Blok\", \"ParmakiziId\", \"Tc\") " +
                                  $"VALUES ('{ad}', '{soyad}', '{telefon}', '{yurt}', '{oda}', '{blok}', '{ogrenciId}', '{tc}')";



                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti))
                {
                    cmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@telefon", telefon);
                    cmd.Parameters.AddWithValue("@yurt", yurt);
                    cmd.Parameters.AddWithValue("@oda", oda);
                    cmd.Parameters.AddWithValue("@blok", blok);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("��renci ba�ar�yla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("��renci eklenirken bir hata olu�tu.");
                    }
                }

                Temizle();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
            VerileriGuncelle();
        }

        private void Temizle()
        {
            // TextBox'lar� temizle
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelefon.Clear();
            txtYurt.Clear();
            txtOda.Clear();
            txtBlok.Clear();
            txtParmakiziId.Clear();
            txtTc.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();

                // Kullan�c�dan al�nan de�erler
                string ad = txtAd.Text;
                string soyad = txtSoyad.Text;
                int ogrenciId = Convert.ToInt32(txtParmakiziId.Text);

                // DELETE sorgusu
                string sorgu = "DELETE FROM \"Yurt\".\"�grenci\" " +
                               $"WHERE \"Ad\" = '{ad}' AND \"Soyad\" = '{soyad}' AND \"ParmakiziId\" = {ogrenciId}";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti))
                {
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("��renci ba�ar�yla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Silinecek ��renci bulunamad� veya silme i�lemi s�ras�nda bir hata olu�tu.");
                    }
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }


            Temizle();
            VerileriGuncelle();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();

                // Kullan�c�dan al�nan de�erler
                string ad = txtAd.Text;
                string soyad = txtSoyad.Text;
                int ogrenciId = Convert.ToInt32(txtParmakiziId.Text);

                // SELECT sorgusu
                string sorgu = "SELECT * FROM \"Yurt\".\"�grenci\" " +
                               $"WHERE \"Ad\" = '{ad}' AND \"Soyad\" = '{soyad}' AND \"ParmakiziId\" = {ogrenciId}";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // ��renci bulundu, bilgileri ekrana yazd�r
                            string ogrenciBilgileri = $"Ad: {reader["Ad"]}\n" +
                                                      $"Soyad: {reader["Soyad"]}\n" +
                                                      $"Telefon: {reader["Telefon"]}\n" +
                                                      $"Yurt: {reader["Yurt"]}\n" +
                                                      $"Oda: {reader["Oda"]}\n" +
                                                      $"Blok: {reader["Blok"]}\n" +
                                                      $"ParmakiziId: {reader["ParmakiziId"]}\n" +
                                                      $"Tc: {reader["Tc"]}";

                            MessageBox.Show(ogrenciBilgileri);
                        }
                        else
                        {
                            MessageBox.Show("Arad���n�z ��renci bulunamad�.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }

            Temizle();
            VerileriGuncelle();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();

                // Kullan�c�dan al�nan de�erler
                int ogrenciId = Convert.ToInt32(txtParmakiziId.Text);

                // SQL sorgusunun ba�lang�c�
                string sorgu = "UPDATE \"Yurt\".\"�grenci\" SET ";

                // Hangi alanlar g�ncellenecekse onlar� kontrol et
                if (!string.IsNullOrEmpty(txtAd.Text))
                {
                    sorgu += $"\"Ad\" = '{txtAd.Text}', ";
                }

                if (!string.IsNullOrEmpty(txtSoyad.Text))
                {
                    sorgu += $"\"Soyad\" = '{txtSoyad.Text}', ";
                }

                if (!string.IsNullOrEmpty(txtTelefon.Text))
                {
                    sorgu += $"\"Telefon\" = '{txtTelefon.Text}', ";
                }

                if (!string.IsNullOrEmpty(txtYurt.Text))
                {
                    sorgu += $"\"Yurt\" = '{txtYurt.Text}', ";
                }

                if (!string.IsNullOrEmpty(txtOda.Text))
                {
                    sorgu += $"\"Oda\" = '{txtOda.Text}', ";
                }

                if (!string.IsNullOrEmpty(txtBlok.Text))
                {
                    sorgu += $"\"Blok\" = '{txtBlok.Text}', ";
                }

                // Son iki karakteri silerek gereksiz virg�l� kald�r
                sorgu = sorgu.Remove(sorgu.Length - 2);

                // WHERE ko�ulu ekleniyor
                sorgu += $" WHERE \"ParmakiziId\" = {ogrenciId}";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti))
                {
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("��renci bilgileri ba�ar�yla g�ncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("��renci bilgileri g�ncellenirken bir hata olu�tu.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }


            Temizle();
            VerileriGuncelle();
        }

        private void VerileriListele()
        {
            try
            {
                baglanti.Open();

                string sorgu = "SELECT * FROM \"Yurt\".\"�grenci\"";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti))
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    // DataGridView'i g�ncellenmi� verilerle doldur
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VerileriListele();
            Temizle();
            VerileriGuncelle();
        }
    }

}
