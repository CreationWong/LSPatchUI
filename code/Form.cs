using System.Diagnostics;

namespace LSPatchUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "APK Files (*.apk)|*.apk";

            openFileDialog.Title = "ѡ���޸ĵ� APK �ļ�";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                textBoxAPKPath.Text = selectedFilePath;
            }
        }

        private void ButtonJar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "LSPatch.jar |lspatch.jar";

            openFileDialog.Title = "ѡ�� LSPatch.jar �ļ�";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                textBoxJarPath.Text = selectedFilePath;
            }
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

            string command = $"{javaPath} -jar \"{jarPath}\" \"{apkPath}\"";

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

            ExecuteCommand(command);

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

        private void ExecuteCommand(string command)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "java |java.exe";

            openFileDialog.Title = "ѡ�� Java ִ�л���";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                textBoxJavaPath.Text = selectedFilePath;
            }
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
    }
}
