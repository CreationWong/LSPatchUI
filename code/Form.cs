using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LSPatchUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        string confPath = Path.Combine(Application.StartupPath + "\\conf.ini");
        private void Main_Load(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(confPath);

            if (iniFile.Read("User Configuration", "JarPath") != null) {
                textBoxJarPath.Text = iniFile.Read("User Configuration", "JarPath");
            }

            if (iniFile.Read("User Configuration", "JavaPath") != null)
            {
                textBoxJavaPath.Text = iniFile.Read("User Configuration", "JavaPath");
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "APK files (*.apk)|*.apk",
                Title = "��ѡ��Ƕ��ģ��"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);

                if (listBoxMod.Items.Cast<string>().Any(item => item.StartsWith($"{fileName}")))
                {
                    MessageBox.Show($"�б����Ѵ�����Ϊ {fileName} ��ģ��!", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    listBoxMod.Items.Add($"{fileName} ({filePath})");
                }
            }
        }

        private void ButtonAPKAdd_Click(object sender, EventArgs e)
        {
            textBoxAPKPath.Text = OpenFileFunc("ѡ���޸ĵ� APK �ļ�", "APK Files (*.apk)|*.apk");
        }

        private void ButtonJar_Click(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(confPath);

            string Path = OpenFileFunc("ѡ�� LSPatch.jar �ļ�", "LSPatch.jar |lspatch.jar");

            iniFile.Write("User Configuration", "JarPath", Path);

            textBoxJarPath.Text = Path;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxMod.SelectedItem != null)
            {
                listBoxMod.Items.Remove(listBoxMod.SelectedItem);
            }
            else
            {
                MessageBox.Show("��ѡ��ģ��", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonRUN_Click(object sender, EventArgs e)
        {
            bool useManager = checkBoxUseManager.Checked;
            bool newVersion = checkBoxNewVersion.Checked;
            bool APKDebug = checkBoxAPKDebug.Checked;

            int lvIndex = comboBoxLv.SelectedIndex;

            string javaPath = textBoxJavaPath.Text;
            string jarPath = textBoxJarPath.Text;
            string apkPath = textBoxAPKPath.Text;
            string outPath = textBoxOUTPath.Text;

            var modItems = listBoxMod.Items;

            if (string.IsNullOrWhiteSpace(javaPath))
            {
                MessageBox.Show("��ѡ�� java.exe �ļ�·����", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string command = $" -jar \"{jarPath}\" \"{apkPath}\"";

            if (string.IsNullOrWhiteSpace(jarPath))
            {
                MessageBox.Show("��ѡ�� LSPatch.jar �ļ�·����", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(apkPath))
            {
                MessageBox.Show("��ѡ�� APK �ļ�·����", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (modItems.Count == 0 && useManager == false)
            {
                MessageBox.Show("��û�����ñ���ģʽ�������ģ�顣", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (useManager)
            {
                command += " -manager";
            }
            else
            {
                foreach (var item in modItems)
                {
                    string itemText = item.ToString();
                    string filePath = ExtractFilePath(itemText); // ��ȡfilePath
                    command += $" -m \"{filePath}\"";
                }
            }

            if (APKDebug)
            {
                command += " -debuggable";
            }

            if (newVersion)
            {
                command += " -allowdown";
            }

            command += $" -l {lvIndex}";

            if (string.IsNullOrWhiteSpace(outPath))
            {
                string TempOutPath = Path.GetDirectoryName(apkPath);
                DialogResult state = MessageBox.Show($"δָ������·��! ����ļ���Ҫ���浽 {TempOutPath} �ļ����С���ȷ��!", "�����û���������", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (state == DialogResult.No)
                {
                    return;
                }
                command += $" -o {TempOutPath}";
            }
            else
            {
                command += $" -o {outPath}";
            }

            ExecuteCommand(command, javaPath);

        }

        private string ExtractFilePath(string itemText)
        {
            int startIndex = itemText.LastIndexOf('(') + 1;
            int endIndex = itemText.LastIndexOf(')');

            if (startIndex >= 0 && endIndex > startIndex)
            {
                return itemText.Substring(startIndex, endIndex - startIndex).Trim();
            }

            return itemText;
        }

        private void checkBoxUseManager_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseManager.CheckState == 0)
            {
                listBoxMod.Enabled = true;
                ButtonAdd.Enabled = true;
                ButtonDelete.Enabled = true;
            }
            else
            {
                listBoxMod.Enabled = false;
                ButtonAdd.Enabled = false;
                ButtonDelete.Enabled = false;
            }
        }

        private void ExecuteCommand(string command,string JavaPath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    //FileName = "cmd.exe",
                    FileName = JavaPath,
                    Arguments = $"{command}",
                };

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ִ������ʱ����: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonJava_Click(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(confPath);

            string path = OpenFileFunc("ѡ�� Java ִ�л���", "java |java.exe");

            iniFile.Write("User Configuration", "JavaPath", path);

            textBoxJavaPath.Text = path;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDialog = new FolderBrowserDialog();
            FBDialog.ShowNewFolderButton = true;

            if (FBDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxOUTPath.Text = FBDialog.SelectedPath;
            }
        }

        private void Public_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Public_textBoxPath_DragDrop(object sender, DragEventArgs e)
        {
            IniFile iniFile = new IniFile(confPath);

            TextBox textBoxPath = sender as TextBox;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string filePath = files[0];

                    string extension = Path.GetExtension(filePath).ToLower();
                    string fileName = Path.GetFileName(filePath);

                    switch (textBoxPath.Name)
                    {
                        case "textBoxAPKPath":
                            if (extension == ".apk")
                            {
                                textBoxPath.Text = filePath;
                            }
                            else
                            {
                                MessageBox.Show($"�����ļ�����,������Ҫ��! ������: APK �ļ�! \n�����ļ�Ϊ: {filePath}", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;

                        case "textBoxJarPath":
                            if (fileName == "lspatch.jar")
                            {
                                iniFile.Write("User Configuration", "JarPath", filePath);
                                textBoxPath.Text = filePath;
                            }
                            else
                            {
                                MessageBox.Show($"�����ļ�����,������Ҫ��! ������: lspatch.jar �ļ�! \n�����ļ�Ϊ: {filePath}", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;

                        case "textBoxJavaPath":
                            if (fileName == "java.exe")
                            {
                                iniFile.Write("User Configuration", "JavaPath", filePath);
                                textBoxPath.Text = filePath;
                            }
                            else
                            {
                                MessageBox.Show($"�����ļ�����,������Ҫ��! ������: java.exe �ļ�! \n�����ļ�Ϊ: {filePath}", "�û���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;

                        default:
                            MessageBox.Show($"�ó����������֮��Ĵ���\n����λ��Ϊ: Public_textBoxPath_DragDrop -> switch (textBoxPath.Name) ��\nֵΪ{textBoxPath.Name} ��", "�����ڲ����� (BUG)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }

        private void listBoxMod_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (global::System.Int32 i = 0; i < files.Length; i++)
                {
                    string filePath = files[i];

                    string extension = Path.GetExtension(filePath).ToLower();
                    string fileName = Path.GetFileName(filePath);

                    if (extension == ".apk")
                    {
                        if (!(listBoxMod.Items.Cast<string>().Any(item => item.StartsWith($"{fileName}"))))
                        {
                            listBoxMod.Items.Add($"{fileName} ({filePath})");
                        }
                    }
                }
            }
        }

        private static string OpenFileFunc(string title, string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = title;

            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            // ����û�ȡ�������򷵻� NULL
            return null;
        }
    }
}
