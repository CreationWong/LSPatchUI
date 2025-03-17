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
                Title = "请选择嵌入模块"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);

                if (listBoxMod.Items.Cast<string>().Any(item => item.StartsWith($"{fileName}")))
                {
                    MessageBox.Show($"列表中已存在名为 {fileName} 的模块!", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    listBoxMod.Items.Add($"{fileName} ({filePath})");
                }
            }
        }

        private void ButtonAPKAdd_Click(object sender, EventArgs e)
        {
            textBoxAPKPath.Text = OpenFileFunc("选择被修改的 APK 文件", "APK Files (*.apk)|*.apk");
        }

        private void ButtonJar_Click(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(confPath);

            string Path = OpenFileFunc("选择 LSPatch.jar 文件", "LSPatch.jar |lspatch.jar");

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
                MessageBox.Show("请选择模块", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("请选择 java.exe 文件路径！", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string command = $" -jar \"{jarPath}\" \"{apkPath}\"";

            if (string.IsNullOrWhiteSpace(jarPath))
            {
                MessageBox.Show("请选择 LSPatch.jar 文件路径！", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(apkPath))
            {
                MessageBox.Show("请选择 APK 文件路径！", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (modItems.Count == 0 && useManager == false)
            {
                MessageBox.Show("你没有启用本地模式，请添加模块。", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string filePath = ExtractFilePath(itemText); // 提取filePath
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
                DialogResult state = MessageBox.Show($"未指定保存路径! 输出文件将要保存到 {TempOutPath} 文件夹中。请确认!", "更改用户操作警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                MessageBox.Show($"执行命令时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonJava_Click(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(confPath);

            string path = OpenFileFunc("选择 Java 执行环境", "java |java.exe");

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
                                MessageBox.Show($"拖入文件错误,不符合要求! 请拖入: APK 文件! \n拖入文件为: {filePath}", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                MessageBox.Show($"拖入文件错误,不符合要求! 请拖入: lspatch.jar 文件! \n拖入文件为: {filePath}", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                MessageBox.Show($"拖入文件错误,不符合要求! 请拖入: java.exe 文件! \n拖入文件为: {filePath}", "用户操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;

                        default:
                            MessageBox.Show($"该程序出现意料之外的错误。\n错误位置为: Public_textBoxPath_DragDrop -> switch (textBoxPath.Name) 。\n值为{textBoxPath.Name} 。", "程序内部错误 (BUG)", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // 如果用户取消操作则返回 NULL
            return null;
        }
    }
}
