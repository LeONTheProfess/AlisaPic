using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string path1 = File.ReadAllText(@"path.txt", Encoding.Default);
        string path2 = File.ReadAllText(@"path2.txt", Encoding.Default);
        int count = 0;
        string copyErrors = "";

        // считаем, что chekmin == thue при условии, что функция вызывается из buttonUploadIB
        void AddIndex_Copy(string[] paths, bool chekmin = false)
        {
            string temp;
            for (int i=0; i < paths.Length; i++)
            {
                try
                {
                    if (paths[i].IndexOf('_') < 0)
                    {
                        System.IO.FileInfo fileinf = new System.IO.FileInfo(paths[i]);
                        if (fileinf.Length < 500000 && chekmin) throw new Exception("Файл меньше 500 Кб");
                        if (fileinf.Length > 6000000 && 
                            (fileinf.Extension == ".jpg" || fileinf.Extension == ".jpeg" || fileinf.Extension == ".png"))
                        {
                            if (chekmin)
                            {
                                deliteEXIF(paths[i], path2 + 
                                    Path.GetFileNameWithoutExtension(paths[i]) + @"_IB01" + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                fileinf = new FileInfo(path2 + 
                                    Path.GetFileNameWithoutExtension(paths[i]) + @"_IB01" + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                if (fileinf.Length > 6000000)
                                {
                                    File.Delete(path2 + 
                                        Path.GetFileNameWithoutExtension(paths[i]) + @"_IB01" + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    throw new Exception("Файл больше 6 Мб");
                                }
                            }
                            else
                            {
                                deliteEXIF(paths[i], path2 + 
                                    Path.GetFileNameWithoutExtension(paths[i]) + @"_IN01" + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                fileinf = new FileInfo(path2 + 
                                    Path.GetFileNameWithoutExtension(paths[i]) + @"_IN01" + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                if (fileinf.Length > 6000000)
                                {
                                    File.Delete(path2 + 
                                        Path.GetFileNameWithoutExtension(paths[i]) + @"_IN01" + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    throw new Exception("Файл больше 6 Мб");
                                }
                            }
                            count++;
                        }
                        else if (fileinf.Length > 6000000)
                        {
                            throw new Exception("Файл больше 6 Мб");
                        }
                        else
                        {
                            if(chekmin)
                                File.Copy(paths[i], path2 + 
                                    Path.GetFileNameWithoutExtension(paths[i]) + @"_IB01" + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')), true);
                            else
                                File.Copy(paths[i], path2 + 
                                    Path.GetFileNameWithoutExtension(paths[i]) + @"_IN01" + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')), true);
                            count++;
                        }
                    }
                }
                catch(Exception ex)
                {
                    copyErrors += Path.GetFileName(paths[i]) + " - " + ex.Message + '\n';
                }
                if (paths[i].IndexOf('_') >= 0)
                {
                    temp = Path.GetFileNameWithoutExtension(paths[i]);
                    string index = temp.Substring(temp.IndexOf('_') + 1);
                    if (index.Length == 4) index = index.Substring(2, 2);
                    
                    try
                    {
                        int Indx = Convert.ToInt32(index);
                        FileInfo fileinf = new FileInfo(paths[i]);
                        if (fileinf.Length < 500000 && chekmin) throw new Exception("Файл меньше 500 Кб");
                        if (fileinf.Length > 6000000 && (fileinf.Extension == ".jpg" || fileinf.Extension == ".jpeg"))
                        {
                            if (chekmin)
                            {
                                if (Indx >= 10)
                                {
                                    deliteEXIF(paths[i], path2 + 
                                        temp.Substring(0, temp.IndexOf('_')) + @"_IB" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    fileinf = new FileInfo(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IB" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    if (fileinf.Length > 6000000)
                                    {
                                        File.Delete(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IB" + Convert.ToString(Indx) + 
                                            Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                        throw new Exception("Файл больше 6 Мб");
                                    }
                                    count++;
                                }
                                else if (Indx < 10)
                                {
                                    deliteEXIF(paths[i], path2 + 
                                        temp.Substring(0, temp.IndexOf('_')) + @"_IB" + @"0" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    fileinf = new FileInfo(path2 + 
                                        temp.Substring(0, temp.IndexOf('_')) + @"_IB" + @"0" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    if (fileinf.Length > 6000000)
                                    {
                                        File.Delete(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IB" + @"0" + Convert.ToString(Indx) + 
                                            Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                        throw new Exception("Файл больше 6 Мб");
                                    }
                                    count++;
                                }
                            }
                            else
                            {
                                if (Indx >= 10)
                                {
                                    deliteEXIF(paths[i], path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    fileinf = new FileInfo(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    if (fileinf.Length > 6000000)
                                    {
                                        File.Delete(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + Convert.ToString(Indx) + 
                                            Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                        throw new Exception("Файл больше 6 Мб");
                                    }
                                    count++;
                                }
                                else if (Indx < 10)
                                {
                                    deliteEXIF(paths[i], path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + @"0" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    fileinf = new FileInfo(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + @"0" + Convert.ToString(Indx) + 
                                        Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                    if (fileinf.Length > 6000000)
                                    {
                                        File.Delete(path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + @"0" + Convert.ToString(Indx) + 
                                            Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')));
                                        throw new Exception("Файл больше 6 Мб");
                                    }
                                    count++;
                                }
                            }
                        }
                        else if (fileinf.Length > 6000000) throw new Exception("Файл больше 6 Мб");
                        else if (chekmin)
                        {
                            if (Indx >= 10)
                            {

                                File.Copy(paths[i], path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IB" + Convert.ToString(Indx) + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')), true);
                                count++;
                            }
                            else if (Indx < 10)
                            {
                                File.Copy(paths[i], path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IB" + @"0" + Convert.ToString(Indx) + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')), true);
                                count++;
                            }
                        }
                        else
                        {
                            if (Indx >= 10)
                            {

                                File.Copy(paths[i], path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + Convert.ToString(Indx) + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')), true);
                                count++;
                            }
                            else if (Indx < 10)
                            {
                                File.Copy(paths[i], path2 + temp.Substring(0, temp.IndexOf('_')) + @"_IN" + @"0" + Convert.ToString(Indx) + 
                                    Path.GetFileName(paths[i]).Substring(Path.GetFileName(paths[i]).IndexOf('.')), true);
                                count++;
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        string Mes = ex.Message;
                        copyErrors += Path.GetFileName(paths[i]) + " - " + Mes + '\n';
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        // удаление метаданных из изображений
        private void deliteEXIF(string inPic, string outPic)
        {
            Bitmap bmp = new Bitmap(inPic);

            foreach (PropertyItem item in bmp.PropertyItems)
            {
                if (item.Id == 0x0112 || item.Id == 0x0132)
                    continue;

                PropertyItem modItem = item;
                modItem.Value = new byte[] { 0 };

                bmp.SetPropertyItem(modItem);
            }
            bmp.Save(outPic);
        }

        private void buttonSearchIndex(object sender, EventArgs e)
        {
            try
            {

                if (textBox1.Text.Length == 0) throw new Exception("Пустой запрос.");
                if (Directory.Exists(path1 + textBox1.Text))
                    System.Diagnostics.Process.Start("explorer", path1 + textBox1.Text);
                else throw new Exception("Артикул не найден");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка! {ex.Message}");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // нормализация путей
            path1 = path1.Replace("\t", @""); 
            path1 = path1.Replace("\r", @"");
            path1 = path1.Replace("\n", @"");
            path2 = path2.Replace("\t", @"");
            path2 = path2.Replace("\r", @"");
            path2 = path2.Replace("\n", @"");
            if (path1[path1.Length - 1] != '\\') path1 += @"\";
            if (path2[path2.Length - 1] != '\\') path2 += @"\";

            label1.Text = path1;
            if (!Directory.Exists(path1))
            {
                MessageBox.Show("Предупреждение! Директория с изображениями не найдена.");
            }
            if (!Directory.Exists(path2))
            {
                MessageBox.Show("Предупреждение! Каталог загрузки не найден.");
            }
        }

        private void buttonShowPath2(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", path2);
        }

        // выгрузка в path2 с удалением exif 
        private void buttonUploadIB(object sender, EventArgs e)
        {
            count = 0;
            try
            {
                openFileDialog1.Multiselect = true;
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Изображения|*.jpg;*.jpeg;*.png";
                if (Directory.Exists(path1))
                    openFileDialog1.InitialDirectory = path1;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] filenames = openFileDialog1.FileNames;
                    AddIndex_Copy(filenames, true);
                }


                if (copyErrors.Length > 0)
                {
                    MessageBox.Show(copyErrors, "Ошибка при копировании файлов:");
                    copyErrors = "";
                }
                if (count==1)
                    MessageBox.Show($"Загружен 1 файл");
                else if (count > 1 && count < 5)
                    MessageBox.Show($"Загружено {count} файла");
                else
                    MessageBox.Show($"Загружено {count} файлов");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка! {ex.Message}");
            }
        }

        // для новинок
        private void buttonUploadIN(object sender, EventArgs e)
        {
            count = 0;
            try
            {
                openFileDialog1.Multiselect = true;
                openFileDialog1.Filter = "Изображения|*.jpg;*.jpeg;*.png";
                openFileDialog1.FileName = "";
                if (Directory.Exists(path1))
                    openFileDialog1.InitialDirectory = path1;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] filenames = openFileDialog1.FileNames;
                    AddIndex_Copy(filenames);
                }

                if (copyErrors.Length > 0)
                {
                    MessageBox.Show(copyErrors, "Ошибка при копировании файлов:");
                    copyErrors="";
                }
                if (count == 1)
                    MessageBox.Show($"Загружен 1 файл");
                else if (count > 1 && count < 5)
                    MessageBox.Show($"Загружено {count} файла");
                else
                    MessageBox.Show($"Загружено {count} файлов");
                count = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка! {ex.Message}");
            }
        }
    }
}
