namespace LSPatchUI
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MAIN = new Panel();
            checkBoxAPKDebug = new CheckBox();
            checkBoxNewVersion = new CheckBox();
            ButtonSave = new Button();
            label6 = new Label();
            textBoxOUTPath = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            ButtonJava = new Button();
            textBoxJavaPath = new TextBox();
            comboBoxLv = new ComboBox();
            checkBoxUseManager = new CheckBox();
            textBoxJarPath = new TextBox();
            ButtonDelete = new Button();
            ButtonRUN = new Button();
            ButtonJar = new Button();
            label2 = new Label();
            ButtonAPKadd = new Button();
            textBoxAPKPath = new TextBox();
            label1 = new Label();
            ButtonAdd = new Button();
            listBoxMod = new ListBox();
            MAIN.SuspendLayout();
            SuspendLayout();
            // 
            // MAIN
            // 
            MAIN.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MAIN.Controls.Add(checkBoxAPKDebug);
            MAIN.Controls.Add(checkBoxNewVersion);
            MAIN.Controls.Add(ButtonSave);
            MAIN.Controls.Add(label6);
            MAIN.Controls.Add(textBoxOUTPath);
            MAIN.Controls.Add(label5);
            MAIN.Controls.Add(label4);
            MAIN.Controls.Add(label3);
            MAIN.Controls.Add(ButtonJava);
            MAIN.Controls.Add(textBoxJavaPath);
            MAIN.Controls.Add(comboBoxLv);
            MAIN.Controls.Add(checkBoxUseManager);
            MAIN.Controls.Add(textBoxJarPath);
            MAIN.Controls.Add(ButtonDelete);
            MAIN.Controls.Add(ButtonRUN);
            MAIN.Controls.Add(ButtonJar);
            MAIN.Controls.Add(label2);
            MAIN.Controls.Add(ButtonAPKadd);
            MAIN.Controls.Add(textBoxAPKPath);
            MAIN.Controls.Add(label1);
            MAIN.Controls.Add(ButtonAdd);
            MAIN.Controls.Add(listBoxMod);
            MAIN.Location = new Point(12, 12);
            MAIN.Name = "MAIN";
            MAIN.Size = new Size(519, 419);
            MAIN.TabIndex = 0;
            // 
            // checkBoxAPKDebug
            // 
            checkBoxAPKDebug.AutoSize = true;
            checkBoxAPKDebug.Location = new Point(196, 269);
            checkBoxAPKDebug.Name = "checkBoxAPKDebug";
            checkBoxAPKDebug.Size = new Size(114, 24);
            checkBoxAPKDebug.TabIndex = 7;
            checkBoxAPKDebug.Text = "Debug 模式";
            checkBoxAPKDebug.UseVisualStyleBackColor = true;
            // 
            // checkBoxNewVersion
            // 
            checkBoxNewVersion.AutoSize = true;
            checkBoxNewVersion.Location = new Point(308, 239);
            checkBoxNewVersion.Name = "checkBoxNewVersion";
            checkBoxNewVersion.Size = new Size(121, 24);
            checkBoxNewVersion.TabIndex = 7;
            checkBoxNewVersion.Text = "重定义版本号";
            checkBoxNewVersion.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            ButtonSave.Location = new Point(415, 324);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(94, 29);
            ButtonSave.TabIndex = 15;
            ButtonSave.Text = "选择...";
            ButtonSave.UseVisualStyleBackColor = true;
            ButtonSave.Click += ButtonSave_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(196, 303);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 14;
            label6.Text = "保存到 :";
            // 
            // textBoxOUTPath
            // 
            textBoxOUTPath.Location = new Point(196, 326);
            textBoxOUTPath.Name = "textBoxOUTPath";
            textBoxOUTPath.ReadOnly = true;
            textBoxOUTPath.Size = new Size(213, 27);
            textBoxOUTPath.TabIndex = 0;
            textBoxOUTPath.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(196, 182);
            label5.Name = "label5";
            label5.Size = new Size(99, 20);
            label5.TabIndex = 13;
            label5.Text = "签名绕过级别";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(196, 116);
            label4.Name = "label4";
            label4.Size = new Size(179, 20);
            label4.TabIndex = 12;
            label4.Text = "使用的 LSPatch.jar 文件 :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(196, 58);
            label3.Name = "label3";
            label3.Size = new Size(172, 20);
            label3.TabIndex = 11;
            label3.Text = "使用的 JAVA 执行环境 : ";
            // 
            // ButtonJava
            // 
            ButtonJava.Location = new Point(415, 81);
            ButtonJava.Name = "ButtonJava";
            ButtonJava.Size = new Size(94, 29);
            ButtonJava.TabIndex = 5;
            ButtonJava.Text = "选择...";
            ButtonJava.UseVisualStyleBackColor = true;
            ButtonJava.Click += ButtonJava_Click;
            // 
            // textBoxJavaPath
            // 
            textBoxJavaPath.AllowDrop = true;
            textBoxJavaPath.Location = new Point(196, 81);
            textBoxJavaPath.Name = "textBoxJavaPath";
            textBoxJavaPath.ReadOnly = true;
            textBoxJavaPath.Size = new Size(213, 27);
            textBoxJavaPath.TabIndex = 9;
            textBoxJavaPath.DragDrop += Public_textBoxPath_DragDrop;
            textBoxJavaPath.DragEnter += Public_DragEnter;
            // 
            // comboBoxLv
            // 
            comboBoxLv.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLv.FormattingEnabled = true;
            comboBoxLv.Items.AddRange(new object[] { "LV0 关闭", "LV1 绕过 PM", "LV2 绕过 PM +openat (libc)" });
            comboBoxLv.Location = new Point(196, 205);
            comboBoxLv.MaxDropDownItems = 5;
            comboBoxLv.Name = "comboBoxLv";
            comboBoxLv.Size = new Size(313, 28);
            comboBoxLv.TabIndex = 6;
            // 
            // checkBoxUseManager
            // 
            checkBoxUseManager.AutoSize = true;
            checkBoxUseManager.Location = new Point(196, 239);
            checkBoxUseManager.Name = "checkBoxUseManager";
            checkBoxUseManager.Size = new Size(106, 24);
            checkBoxUseManager.TabIndex = 7;
            checkBoxUseManager.Text = "管理器模式";
            checkBoxUseManager.UseVisualStyleBackColor = true;
            checkBoxUseManager.CheckedChanged += checkBoxUseManager_CheckedChanged;
            // 
            // textBoxJarPath
            // 
            textBoxJarPath.AllowDrop = true;
            textBoxJarPath.Location = new Point(196, 143);
            textBoxJarPath.Name = "textBoxJarPath";
            textBoxJarPath.ReadOnly = true;
            textBoxJarPath.Size = new Size(213, 27);
            textBoxJarPath.TabIndex = 0;
            textBoxJarPath.TabStop = false;
            textBoxJarPath.DragDrop += Public_textBoxPath_DragDrop;
            textBoxJarPath.DragEnter += Public_DragEnter;
            // 
            // ButtonDelete
            // 
            ButtonDelete.Location = new Point(159, 116);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(31, 29);
            ButtonDelete.TabIndex = 4;
            ButtonDelete.Text = "-";
            ButtonDelete.UseVisualStyleBackColor = true;
            ButtonDelete.Click += ButtonDelete_Click;
            // 
            // ButtonRUN
            // 
            ButtonRUN.Location = new Point(196, 359);
            ButtonRUN.Name = "ButtonRUN";
            ButtonRUN.Size = new Size(319, 52);
            ButtonRUN.TabIndex = 8;
            ButtonRUN.Text = "构建";
            ButtonRUN.UseVisualStyleBackColor = true;
            ButtonRUN.Click += ButtonRUN_Click;
            // 
            // ButtonJar
            // 
            ButtonJar.Location = new Point(415, 141);
            ButtonJar.Name = "ButtonJar";
            ButtonJar.Size = new Size(94, 29);
            ButtonJar.TabIndex = 5;
            ButtonJar.Text = "选择...";
            ButtonJar.UseVisualStyleBackColor = true;
            ButtonJar.Click += ButtonJar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 58);
            label2.Name = "label2";
            label2.Size = new Size(47, 20);
            label2.TabIndex = 5;
            label2.Text = "插件 :";
            // 
            // ButtonAPKadd
            // 
            ButtonAPKadd.Location = new Point(415, 22);
            ButtonAPKadd.Name = "ButtonAPKadd";
            ButtonAPKadd.Size = new Size(94, 29);
            ButtonAPKadd.TabIndex = 1;
            ButtonAPKadd.Text = "浏览...";
            ButtonAPKadd.UseVisualStyleBackColor = true;
            ButtonAPKadd.Click += ButtonAPKAdd_Click;
            // 
            // textBoxAPKPath
            // 
            textBoxAPKPath.AllowDrop = true;
            textBoxAPKPath.Location = new Point(3, 23);
            textBoxAPKPath.Name = "textBoxAPKPath";
            textBoxAPKPath.ReadOnly = true;
            textBoxAPKPath.Size = new Size(406, 27);
            textBoxAPKPath.TabIndex = 0;
            textBoxAPKPath.TabStop = false;
            textBoxAPKPath.DragDrop += Public_textBoxPath_DragDrop;
            textBoxAPKPath.DragEnter += Public_DragEnter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(145, 20);
            label1.TabIndex = 2;
            label1.Text = "被修改的 APK 文件 :";
            // 
            // ButtonAdd
            // 
            ButtonAdd.Location = new Point(159, 81);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new Size(31, 29);
            ButtonAdd.TabIndex = 2;
            ButtonAdd.Text = "+";
            ButtonAdd.UseVisualStyleBackColor = true;
            ButtonAdd.Click += ButtonAdd_Click;
            // 
            // listBoxMod
            // 
            listBoxMod.AllowDrop = true;
            listBoxMod.FormattingEnabled = true;
            listBoxMod.ItemHeight = 20;
            listBoxMod.Location = new Point(3, 81);
            listBoxMod.Name = "listBoxMod";
            listBoxMod.Size = new Size(150, 324);
            listBoxMod.TabIndex = 3;
            listBoxMod.DragDrop += listBoxMod_DragDrop;
            listBoxMod.DragEnter += Public_DragEnter;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 441);
            Controls.Add(MAIN);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form";
            Text = "LSPatchUI";
            Load += Main_Load;
            MAIN.ResumeLayout(false);
            MAIN.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel MAIN;
        private Button ButtonAdd;
        private ListBox listBoxMod;
        private TextBox textBoxAPKPath;
        private Label label1;
        private Button ButtonAPKadd;
        private Label label2;
        private Button ButtonDelete;
        private Button ButtonRUN;
        private Button ButtonJar;
        private TextBox textBoxJarPath;
        private CheckBox checkBoxUseManager;
        private ComboBox comboBoxLv;
        private Label label3;
        private Button ButtonJava;
        private TextBox textBoxJavaPath;
        private Label label4;
        private Label label5;
        private TextBox textBoxOUTPath;
        private Button ButtonSave;
        private Label label6;
        private CheckBox checkBoxNewVersion;
        private CheckBox checkBoxAPKDebug;
    }
}
