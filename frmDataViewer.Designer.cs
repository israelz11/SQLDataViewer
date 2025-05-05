namespace SQLDataViewer;

partial class frmDataViewer
{
    private System.Windows.Forms.TextBox txtServer;
    private System.Windows.Forms.TextBox txtDatabase;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btnFetchData;

    private void InitializeComponent()
    {
        txtServer = new TextBox();
        txtDatabase = new TextBox();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnFetchData = new Button();
        groupBox1 = new GroupBox();
        dataGridViewTables = new DataGridView();
        TABLE_NAME = new DataGridViewTextBoxColumn();
        TABLE_SCHEMA = new DataGridViewTextBoxColumn();
        TABLE_TYPE = new DataGridViewTextBoxColumn();
        tabControl1 = new TabControl();
        tabPage1 = new TabPage();
        groupBox2 = new GroupBox();
        cmdMakeUpdate = new Button();
        richTextSQL = new RichTextBox();
        cmdCopy = new Button();
        cmdClear = new Button();
        cmdMakeInsert = new Button();
        dataGridViewData = new DataGridView();
        tabPage2 = new TabPage();
        chkOriginalColumnName = new CheckBox();
        chkTotitleCase = new CheckBox();
        cmdCopyClass = new Button();
        richClass = new RichTextBox();
        cmdOpen = new Button();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridViewTables).BeginInit();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridViewData).BeginInit();
        tabPage2.SuspendLayout();
        SuspendLayout();
        // 
        // txtServer
        // 
        txtServer.Location = new Point(14, 17);
        txtServer.Name = "txtServer";
        txtServer.PlaceholderText = "Server";
        txtServer.Size = new Size(200, 23);
        txtServer.TabIndex = 0;
        // 
        // txtDatabase
        // 
        txtDatabase.Location = new Point(220, 17);
        txtDatabase.Name = "txtDatabase";
        txtDatabase.PlaceholderText = "Database";
        txtDatabase.Size = new Size(200, 23);
        txtDatabase.TabIndex = 1;
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(426, 17);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Username";
        txtUsername.Size = new Size(200, 23);
        txtUsername.TabIndex = 2;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(630, 17);
        txtPassword.Name = "txtPassword";
        txtPassword.PlaceholderText = "Password";
        txtPassword.Size = new Size(200, 23);
        txtPassword.TabIndex = 3;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnFetchData
        // 
        btnFetchData.Location = new Point(836, 12);
        btnFetchData.Name = "btnFetchData";
        btnFetchData.Size = new Size(176, 30);
        btnFetchData.TabIndex = 4;
        btnFetchData.Text = "Conectar";
        btnFetchData.UseVisualStyleBackColor = true;
        btnFetchData.Click += BtnFetchData_Click;
        // 
        // groupBox1
        // 
        groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBox1.Controls.Add(dataGridViewTables);
        groupBox1.Location = new Point(14, 48);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(1107, 218);
        groupBox1.TabIndex = 9;
        groupBox1.TabStop = false;
        groupBox1.Text = "Tablas y Vistas";
        // 
        // dataGridViewTables
        // 
        dataGridViewTables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dataGridViewTables.ColumnHeadersHeight = 29;
        dataGridViewTables.Columns.AddRange(new DataGridViewColumn[] { TABLE_NAME, TABLE_SCHEMA, TABLE_TYPE });
        dataGridViewTables.Location = new Point(6, 22);
        dataGridViewTables.Name = "dataGridViewTables";
        dataGridViewTables.RowHeadersWidth = 51;
        dataGridViewTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewTables.Size = new Size(1096, 184);
        dataGridViewTables.TabIndex = 6;
        dataGridViewTables.SelectionChanged += dataGridView_SelectionChanged;
        // 
        // TABLE_NAME
        // 
        TABLE_NAME.DataPropertyName = "TABLE_NAME";
        TABLE_NAME.HeaderText = "TABLE_NAME";
        TABLE_NAME.MinimumWidth = 6;
        TABLE_NAME.Name = "TABLE_NAME";
        TABLE_NAME.ReadOnly = true;
        TABLE_NAME.Width = 210;
        // 
        // TABLE_SCHEMA
        // 
        TABLE_SCHEMA.DataPropertyName = "TABLE_SCHEMA";
        TABLE_SCHEMA.HeaderText = "TABLE_SCHEMA";
        TABLE_SCHEMA.MinimumWidth = 6;
        TABLE_SCHEMA.Name = "TABLE_SCHEMA";
        TABLE_SCHEMA.ReadOnly = true;
        TABLE_SCHEMA.Width = 210;
        // 
        // TABLE_TYPE
        // 
        TABLE_TYPE.DataPropertyName = "TABLE_TYPE";
        TABLE_TYPE.HeaderText = "TABLE_TYPE";
        TABLE_TYPE.MinimumWidth = 6;
        TABLE_TYPE.Name = "TABLE_TYPE";
        TABLE_TYPE.ReadOnly = true;
        TABLE_TYPE.Width = 210;
        // 
        // tabControl1
        // 
        tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Location = new Point(12, 272);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(1107, 507);
        tabControl1.TabIndex = 12;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(groupBox2);
        tabPage1.Controls.Add(dataGridViewData);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(1099, 479);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Datos";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox2.Controls.Add(cmdMakeUpdate);
        groupBox2.Controls.Add(richTextSQL);
        groupBox2.Controls.Add(cmdCopy);
        groupBox2.Controls.Add(cmdClear);
        groupBox2.Controls.Add(cmdMakeInsert);
        groupBox2.Location = new Point(6, 267);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(1087, 209);
        groupBox2.TabIndex = 15;
        groupBox2.TabStop = false;
        groupBox2.Text = "Query";
        // 
        // cmdMakeUpdate
        // 
        cmdMakeUpdate.Enabled = false;
        cmdMakeUpdate.Location = new Point(152, 16);
        cmdMakeUpdate.Name = "cmdMakeUpdate";
        cmdMakeUpdate.Size = new Size(140, 28);
        cmdMakeUpdate.TabIndex = 16;
        cmdMakeUpdate.Text = "Generar Update";
        cmdMakeUpdate.UseVisualStyleBackColor = true;
        cmdMakeUpdate.Click += cmdMakeUpdate_Click;
        // 
        // richTextSQL
        // 
        richTextSQL.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        richTextSQL.BorderStyle = BorderStyle.None;
        richTextSQL.Location = new Point(6, 52);
        richTextSQL.Name = "richTextSQL";
        richTextSQL.Size = new Size(1075, 151);
        richTextSQL.TabIndex = 15;
        richTextSQL.Text = "";
        // 
        // cmdCopy
        // 
        cmdCopy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cmdCopy.Enabled = false;
        cmdCopy.Location = new Point(775, 16);
        cmdCopy.Name = "cmdCopy";
        cmdCopy.Size = new Size(150, 30);
        cmdCopy.TabIndex = 14;
        cmdCopy.Text = "Copiar";
        cmdCopy.UseVisualStyleBackColor = true;
        cmdCopy.Click += cmdCopy_Click;
        // 
        // cmdClear
        // 
        cmdClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cmdClear.Enabled = false;
        cmdClear.Location = new Point(931, 16);
        cmdClear.Name = "cmdClear";
        cmdClear.Size = new Size(150, 28);
        cmdClear.TabIndex = 13;
        cmdClear.Text = "Limpiar";
        cmdClear.UseVisualStyleBackColor = true;
        cmdClear.Click += cmdClear_Click;
        // 
        // cmdMakeInsert
        // 
        cmdMakeInsert.Enabled = false;
        cmdMakeInsert.Location = new Point(7, 16);
        cmdMakeInsert.Name = "cmdMakeInsert";
        cmdMakeInsert.Size = new Size(140, 28);
        cmdMakeInsert.TabIndex = 9;
        cmdMakeInsert.Text = "Generar Insert";
        cmdMakeInsert.UseVisualStyleBackColor = true;
        cmdMakeInsert.Click += cmdMakeInsert_Click;
        // 
        // dataGridViewData
        // 
        dataGridViewData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dataGridViewData.ColumnHeadersHeight = 29;
        dataGridViewData.Location = new Point(6, 6);
        dataGridViewData.Name = "dataGridViewData";
        dataGridViewData.RowHeadersWidth = 51;
        dataGridViewData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewData.Size = new Size(1087, 254);
        dataGridViewData.TabIndex = 14;
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(chkOriginalColumnName);
        tabPage2.Controls.Add(chkTotitleCase);
        tabPage2.Controls.Add(cmdCopyClass);
        tabPage2.Controls.Add(richClass);
        tabPage2.Location = new Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(1099, 479);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "C# Clase";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // chkOriginalColumnName
        // 
        chkOriginalColumnName.AutoSize = true;
        chkOriginalColumnName.Location = new Point(172, 13);
        chkOriginalColumnName.Name = "chkOriginalColumnName";
        chkOriginalColumnName.Size = new Size(181, 19);
        chkOriginalColumnName.TabIndex = 17;
        chkOriginalColumnName.Text = "Anotacion: Columna Original";
        chkOriginalColumnName.UseVisualStyleBackColor = true;
        chkOriginalColumnName.CheckedChanged += chkOriginalColumnName_CheckedChanged;
        // 
        // chkTotitleCase
        // 
        chkTotitleCase.AutoSize = true;
        chkTotitleCase.Location = new Point(6, 13);
        chkTotitleCase.Name = "chkTotitleCase";
        chkTotitleCase.Size = new Size(132, 19);
        chkTotitleCase.TabIndex = 16;
        chkTotitleCase.Text = "Mayusculas al inicio";
        chkTotitleCase.UseVisualStyleBackColor = true;
        chkTotitleCase.CheckedChanged += chkTotitleCase_CheckedChanged;
        // 
        // cmdCopyClass
        // 
        cmdCopyClass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cmdCopyClass.Enabled = false;
        cmdCopyClass.Location = new Point(943, 6);
        cmdCopyClass.Name = "cmdCopyClass";
        cmdCopyClass.Size = new Size(150, 30);
        cmdCopyClass.TabIndex = 15;
        cmdCopyClass.Text = "Copiar";
        cmdCopyClass.UseVisualStyleBackColor = true;
        cmdCopyClass.Click += cmdCopyClass_Click;
        // 
        // richClass
        // 
        richClass.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        richClass.BackColor = Color.White;
        richClass.BorderStyle = BorderStyle.None;
        richClass.Location = new Point(6, 42);
        richClass.Name = "richClass";
        richClass.Size = new Size(1087, 430);
        richClass.TabIndex = 0;
        richClass.Text = "";
        // 
        // cmdOpen
        // 
        cmdOpen.Location = new Point(1034, 12);
        cmdOpen.Name = "cmdOpen";
        cmdOpen.Size = new Size(81, 30);
        cmdOpen.TabIndex = 13;
        cmdOpen.Text = "Abrir...";
        cmdOpen.UseVisualStyleBackColor = true;
        cmdOpen.Click += cmdOpen_Click;
        // 
        // frmDataViewer
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1133, 791);
        Controls.Add(cmdOpen);
        Controls.Add(tabControl1);
        Controls.Add(groupBox1);
        Controls.Add(txtServer);
        Controls.Add(txtDatabase);
        Controls.Add(txtUsername);
        Controls.Add(txtPassword);
        Controls.Add(btnFetchData);
        Name = "frmDataViewer";
        Text = "SQL Data Viewer";
        Load += frmDataViewer_Load;
        groupBox1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridViewTables).EndInit();
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridViewData).EndInit();
        tabPage2.ResumeLayout(false);
        tabPage2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
    private GroupBox groupBox1;
    private DataGridView dataGridViewTables;
    private DataGridViewTextBoxColumn TABLE_NAME;
    private DataGridViewTextBoxColumn TABLE_SCHEMA;
    private DataGridViewTextBoxColumn TABLE_TYPE;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private DataGridView dataGridViewData;
    private TabPage tabPage2;
    private GroupBox groupBox2;
    private Button cmdCopy;
    private Button cmdClear;
    private Button cmdMakeInsert;
    private RichTextBox richTextSQL;
    private Button cmdCopyClass;
    private RichTextBox richClass;
    private Button cmdOpen;
    private CheckBox chkTotitleCase;
    private CheckBox chkOriginalColumnName;
    private Button cmdMakeUpdate;
}
