using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SQLDataViewer.Helper;
using SQLDataViewer.Model;
using static System.Net.Mime.MediaTypeNames;
using static SQLDataViewer.Helper.RichTextBoxHelper;

namespace SQLDataViewer;

public partial class frmDataViewer : Form
{
    private bool isLoaded = false;
    private string connectionString = string.Empty;
    public frmDataViewer()
    {
        InitializeComponent();
    }

    private void BtnFetchData_Click(object sender, EventArgs e)
    {
        connectionString = $"Server={txtServer.Text};Database={txtDatabase.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";

        try
        {
            if (txtServer.Text == "")
            {
                MessageBox.Show("Server is empty", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDatabase.Text == "")
            {
                MessageBox.Show("Database is empty", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Username is empty", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password is empty", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                DataTable tablesAndViews = new DataTable();
                string query = @"SELECT TABLE_NAME, TABLE_SCHEMA, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(tablesAndViews);
                }

                dataGridViewTables.DataSource = tablesAndViews;
                dataGridViewData.DataSource = null;
                cmdClear_Click(sender, e);
                cmdMakeInsert.Enabled = false;
                cmdMakeUpdate.Enabled = false;
                richClass.Text = string.Empty;
                cmdCopyClass.Enabled = false;

                if (tablesAndViews.Rows.Count > 0)
                {
                    var response = ConnectionHelper.SaveConnection(connectionString);
                }
            }

            isLoaded = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dataGridView_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridViewTables.SelectedRows.Count > 0 && isLoaded)
        {
            //Muestra un formulario de espera hasta que finaliza el proceso
            Form waitForm = new Form();
            waitForm.StartPosition = FormStartPosition.CenterScreen;
            waitForm.Size = new System.Drawing.Size(200, 100);
            Label label = new Label();
            label.Text = "Loading data...";
            label.Dock = DockStyle.Fill;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            waitForm.Controls.Add(label);
            waitForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            waitForm.ControlBox = false; // Disable the close button
            waitForm.TopMost = true; // Keep the wait form on top
            waitForm.ShowInTaskbar = false; // Hide from taskbar
            waitForm.StartPosition = FormStartPosition.CenterScreen; // Center the form on the screen
            waitForm.BackColor = System.Drawing.Color.White; // Set background color
            waitForm.FormBorderStyle = FormBorderStyle.None; // Remove border
            waitForm.ShowIcon = false; // Hide icon
            waitForm.ShowInTaskbar = false; // Hide from taskbar
            waitForm.MaximizeBox = false; // Disable maximize button
            waitForm.MinimizeBox = false; // Disable minimize button

            waitForm.Show();
            waitForm.Refresh();

            string tableName = dataGridViewTables.SelectedRows[0].Cells["TABLE_NAME"].Value.ToString();
            string tableSchema = dataGridViewTables.SelectedRows[0].Cells["TABLE_SCHEMA"].Value.ToString();
            string query = $"SELECT * FROM {tableSchema}.{tableName}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewData.DataSource = dataTable;
                    cmdMakeInsert.Enabled = true;
                    cmdMakeUpdate.Enabled = true;

                    GetClass();
                }

                waitForm.Close();
            }
        }
        else
        {
            cmdMakeInsert.Enabled = false;
        }
    }

    private void GetClass()
    {
        //Obtener el nombre, tamaño y tipos de datos de los campos de una tabla
        string tableName = dataGridViewTables.SelectedRows[0].Cells["TABLE_NAME"].Value.ToString();
        string tableSchema = dataGridViewTables.SelectedRows[0].Cells["TABLE_SCHEMA"].Value.ToString();
        string columnQuery = $"SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' AND TABLE_SCHEMA = '{tableSchema}'";
        string primaryKey = GetPrimaryKey();
        bool toTitleCase = chkTotitleCase.Checked;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand columnCommand = new SqlCommand(columnQuery, connection))
            using (SqlDataAdapter columnAdapter = new SqlDataAdapter(columnCommand))
            {
                DataTable columnsTable = new DataTable();
                columnAdapter.Fill(columnsTable);

                this.richClass.Text = "using System.ComponentModel.DataAnnotations;\n" +
                    "using System.ComponentModel.DataAnnotations.Schema;\n\n";
                this.richClass.Text += "public partial class " + (toTitleCase ? UtilityHelper.ToTitle(tableName) : tableName) + "\n";
                this.richClass.Text += "{\n";
                foreach (DataRow row in columnsTable.Rows)
                {
                    string nullTag = row["IS_NULLABLE"].ToString() == "NO" ? "" : "?";
                    string dataType = row["DATA_TYPE"].ToString();

                    if (primaryKey != string.Empty)
                    {
                        if (row["COLUMN_NAME"].ToString() == primaryKey)
                        {
                            this.richClass.Text += "\t[Key]\n";
                            this.richClass.Text += "\t[DatabaseGenerated(DatabaseGeneratedOption.Identity)]\n";
                        }
                    }

                    if (row["IS_NULLABLE"].ToString() == "NO")
                    {
                        this.richClass.Text += "\t[Required]\n";
                    }

                    if (row["DATA_TYPE"].ToString() == "varchar" || row["DATA_TYPE"].ToString() == "nvarchar")
                    {
                        this.richClass.Text += "\t[StringLength(" + row["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")]\n";
                    }

                    if (chkOriginalColumnName.Checked)
                        this.richClass.Text += "\t[Column(\"" + row["COLUMN_NAME"].ToString() + "\")]\n";

                    string columName = (toTitleCase ? UtilityHelper.ToPascalCase(row["COLUMN_NAME"].ToString()) : UtilityHelper.ToLowerCase(row["COLUMN_NAME"].ToString()));

                    if (dataType.Equals("bigint"))
                    {
                        this.richClass.Text += "\tpublic long" + nullTag + " " + columName + " {get; set;}\n";
                    }
                    else if (dataType.Equals("int"))
                    {
                        this.richClass.Text += "\tpublic int" + nullTag + " " + columName + " {get; set;}\n";
                    }
                    else if (dataType.Equals("varchar") || dataType.Equals("nvarchar"))
                    {
                        this.richClass.Text += "\tpublic string" + nullTag + " " + columName + " {get; set;}\n";
                    }
                    else if (dataType.Equals("datetime"))
                    {
                        this.richClass.Text += "\tpublic DateTime" + nullTag + " " + columName + " {get; set;}\n";
                    }
                    else if (dataType.Equals("bit"))
                    {
                        this.richClass.Text += "\tpublic bool" + nullTag + " " + columName + " {get; set;}\n";
                    }
                    else
                        this.richClass.Text += "\tpublic string" + nullTag + " " + columName + " {get; set;}\n";
                }
                this.richClass.Text += "}";

                RichTextBoxHelper.ApplyTextHighlighting(richClass, CSharpClassHelper.HighlightText(new string[] { tableName }));

                cmdCopyClass.Enabled = true;
            }

        }
    }

    private string GetPrimaryKey()
    {
        //Obtener el nombre, tamaño y tipos de datos de los campos de una tabla
        string tableName = dataGridViewTables.SelectedRows[0].Cells["TABLE_NAME"].Value.ToString();
        string tableSchema = dataGridViewTables.SelectedRows[0].Cells["TABLE_SCHEMA"].Value.ToString();
        string keyQuery = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 AND TABLE_NAME = '{tableName}' AND TABLE_SCHEMA = '{tableSchema}'";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand primaryKeyCommand = new SqlCommand(keyQuery, connection))
            using (SqlDataAdapter columnAdapter = new SqlDataAdapter(primaryKeyCommand))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(primaryKeyCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0]["COLUMN_NAME"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }

    private void cmdMakeInsert_Click(object sender, EventArgs e)
    {
        string primaryKeyColumn = GetPrimaryKey();
        string tableName = dataGridViewTables.SelectedRows[0].Cells["TABLE_NAME"].Value.ToString();
        string tableSchema = dataGridViewTables.SelectedRows[0].Cells["TABLE_SCHEMA"].Value.ToString();
        List<string> columns = dataGridViewData.Columns.Cast<DataGridViewColumn>().Select(c => c.Name).ToList();
        string values = string.Empty;
        int cellCount = 0;
        
        //Get index selected row in dataGridViewData
        int indexRow = dataGridViewData.SelectedCells[0].RowIndex;

        foreach (DataGridViewCell cell in dataGridViewData.Rows[indexRow].Cells.Cast<DataGridViewCell>())
        {
            if (!columns[cellCount].Equals(primaryKeyColumn))
            {
                // Check the type of the cell value and format accordingly
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    values += "NULL,";
                }
                else if (cell.ValueType == typeof(string) && string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    values += "NULL,";
                }
                else
                if (cell.ValueType == typeof(string))
                {
                    values += $"'{cell.Value.ToString().Trim()}',";
                }
                else if (cell.ValueType == typeof(int) || cell.ValueType == typeof(Int16) || cell.ValueType == typeof(Int32) || cell.ValueType == typeof(Int64))
                {
                    values += $"{cell.Value},";
                }
                else if (cell.ValueType == typeof(DateTime))
                {
                    values += $"'{((DateTime)cell.Value).ToString("yyyy-MM-dd HH:mm:ss")}',";
                }
                else if (cell.ValueType == typeof(bool))
                {
                    values += $"'{cell.Value.ToString().ToLower()}',";
                }
                else if (cell.ValueType == typeof(decimal))
                {
                    values += $"'{cell.Value}',";
                }
                else if (cell.ValueType == typeof(byte))
                {
                    values += $"NULL,";
                }
                else if (cell.ValueType == typeof(byte[]))
                {
                    values += $"NULL,";
                }
                else if (cell.ValueType == typeof(Guid))
                {
                    values += $"'{cell.Value.ToString().Trim()}',";
                }
                else
                {
                    values += $"NULL,";
                }

            }


            cellCount++;
        }

        values = values.Substring(0, values.Length - 1);
        if(!string.IsNullOrEmpty(primaryKeyColumn))
        {
            columns.Remove(primaryKeyColumn);
        }
        
        string queyInsert = $"INSERT INTO {tableSchema}.{tableName} ({string.Join(", ", columns)}) VALUES ({values});";

        richTextSQL.Text = queyInsert;
        cmdClear.Enabled = true;
        cmdCopy.Enabled = true;
    }

    private void cmdMakeUpdate_Click(object sender, EventArgs e)
    {
        string primaryKeyColumn = GetPrimaryKey();
        string tableName = dataGridViewTables.SelectedRows[0].Cells["TABLE_NAME"].Value.ToString();
        string tableSchema = dataGridViewTables.SelectedRows[0].Cells["TABLE_SCHEMA"].Value.ToString();
        List<string> columns = dataGridViewData.Columns.Cast<DataGridViewColumn>().Select(c => c.Name).ToList();

        string globalQueryUpdate = string.Empty;
        string setColumns = string.Empty;
        string values = string.Empty;

        int cellCount = 0;

        //Get index selected row in dataGridViewData
        int indexRow = dataGridViewData.SelectedCells[0].RowIndex;

        foreach (DataGridViewCell cell in dataGridViewData.Rows[indexRow].Cells.Cast<DataGridViewCell>())
        {
            if(!columns[cellCount].Equals(primaryKeyColumn))
            {
                // Check the type of the cell value and format accordingly
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    setColumns += $" {columns[cellCount]} = NULL,";
                }
                else if (cell.ValueType == typeof(string) && string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    setColumns += $" {columns[cellCount]} = NULL,";
                }
                else
                if (cell.ValueType == typeof(string))
                {
                    setColumns += $" {columns[cellCount]} = '{cell.Value.ToString().Trim()}',";
                }
                else if (cell.ValueType == typeof(int) || cell.ValueType == typeof(Int16) || cell.ValueType == typeof(Int32) || cell.ValueType == typeof(Int64))
                {
                    setColumns += $" {columns[cellCount]} = {cell.Value.ToString().Trim()},";
                }
                else if (cell.ValueType == typeof(DateTime))
                {
                    setColumns += $" {columns[cellCount]} = '{((DateTime)cell.Value).ToString("yyyy-MM-dd HH:mm:ss")}',";
                }
                else if (cell.ValueType == typeof(bool))
                {
                    setColumns += $" {columns[cellCount]} = '{cell.Value.ToString().ToLower()}',";
                }
                else if (cell.ValueType == typeof(decimal))
                {
                    setColumns += $" {columns[cellCount]} = '{cell.Value.ToString().Trim()}',";
                }
                else if (cell.ValueType == typeof(byte))
                {
                    setColumns += $" {columns[cellCount]} = NULL,";
                }
                else if (cell.ValueType == typeof(byte[]))
                {
                    setColumns += $" {columns[cellCount]} = NULL,";
                }
                else if (cell.ValueType == typeof(Guid))
                {
                    setColumns += $" {columns[cellCount]} = '{cell.Value.ToString().Trim()}',";
                }
                else
                {
                    setColumns += $" {columns[cellCount]} = '{cell.Value.ToString().Trim()}',";
                }
            }
            
            cellCount++;
        }

        setColumns = setColumns.Substring(0, setColumns.Length - 1);
        string queyUpdate = $"UPDATE {tableSchema}.{tableName} SET {setColumns} WHERE {dataGridViewData.Columns[0].Name} = {dataGridViewData.Rows[0].Cells[0].Value};";
        globalQueryUpdate += queyUpdate + "\n";

        richTextSQL.Text = globalQueryUpdate;
        cmdClear.Enabled = true;
        cmdCopy.Enabled = true;
    }

    private void cmdClear_Click(object sender, EventArgs e)
    {
        richTextSQL.Text = string.Empty;
        cmdClear.Enabled = false;
        cmdCopy.Enabled = false;
        chkTotitleCase.Checked = false;
        chkOriginalColumnName.Checked = false;
    }

    private void cmdCopy_Click(object sender, EventArgs e)
    {
        //Copiar el texto del richTextBox al portapapeles
        if (!string.IsNullOrEmpty(richTextSQL.Text))
        {
            Clipboard.SetText(richTextSQL.Text);
            MessageBox.Show("Query copiado al portapapeles", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Query se pudo copiar", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void cmdCopyClass_Click(object sender, EventArgs e)
    {
        //Copiar el texto del richTextBox al portapapeles
        if (!string.IsNullOrEmpty(richClass.Text))
        {
            Clipboard.SetText(richClass.Text);
            MessageBox.Show("Query copiado al portapapeles", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Query se pudo copiar", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void cmdOpen_Click(object sender, EventArgs e)
    {
        //Cuadro de dialogo para abrir el archivo de conexion de base de datos .json
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        openFileDialog.Title = "Abrir archivo de conexión";
        openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        openFileDialog.RestoreDirectory = true;
        openFileDialog.ShowReadOnly = true;
        openFileDialog.ShowHelp = true;
        openFileDialog.ShowDialog();
        if (openFileDialog.FileName != "")
        {
            string json = System.IO.File.ReadAllText(openFileDialog.FileName);
            var connectionString = Newtonsoft.Json.JsonConvert.DeserializeObject<ConnectionString>(json);
            txtServer.Text = connectionString.Server;
            txtDatabase.Text = connectionString.Database;
            txtUsername.Text = connectionString.User;
            txtPassword.Text = connectionString.Password;

            BtnFetchData_Click(string.Empty, EventArgs.Empty);

            MessageBox.Show("Archivo de conexion cargado", "Abrir", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void chkOriginalColumnName_CheckedChanged(object sender, EventArgs e)
    {
        this.GetClass();
    }

    private void chkTotitleCase_CheckedChanged(object sender, EventArgs e)
    {
        this.GetClass();
    }

    private void frmDataViewer_Load(object sender, EventArgs e)
    {

    }
}
