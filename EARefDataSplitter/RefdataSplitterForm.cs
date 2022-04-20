using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EARefDataSplitter
{
    public partial class RefDataSplitterForm : Form
    {
        private RefDataParser parser;
        private SplitterSettings settings = new SplitterSettings();
        public RefDataSplitterForm()
        {
            InitializeComponent();
            setDelegates();
            enableDisable();
            //set settings values
            this.AddIncludedScriptsCheckBox.Checked = this.settings.addIncludedScripts;
        }
        private Dictionary<string, string> environments { get; set; }
        public void setEnvironments(Dictionary<string, string> environments)
        {
            this.environments = environments;
            this.environmentsDropdown.DataSource = this.environments?.Keys.ToList();
        }
        private void enableDisable()
        {
            var anySelected = this.refdataTreeView.CheckedObjects.Count > 0;
            this.exportButton.Enabled = this.refdataTreeView.Objects.Cast<object>().Any() && anySelected;
            this.transferToButton.Enabled = this.environments?.Count > 0 && anySelected;
            this.environmentsDropdown.Enabled = this.environments?.Count > 0 && anySelected;
        }
        private void setDelegates()
        {
            //tell the control who can expand 
            TreeListView.CanExpandGetterDelegate canExpandGetter = delegate (object o)
            {
                var scriptGroup = o as ScriptGroup;
                if (scriptGroup != null)
                {
                    return scriptGroup.scripts.Any();
                }
                var script = o as Script;
                if (script != null)
                {
                    return script.includedScripts.Any();
                }
                return ((ScriptInclude)o).hasIncludes;                
            };
            this.refdataTreeView.CanExpandGetter = canExpandGetter;
            //tell the control how to expand
            TreeListView.ChildrenGetterDelegate childrenGetter = delegate (object o)
            {
                var scriptGroup = o as ScriptGroup;
                if (scriptGroup != null)
                {
                    return scriptGroup.scripts.OrderBy(x => x.name);
                }
                var script = o as Script;
                if (script != null)
                {
                    return script.includedScripts;
                }
                return ((ScriptInclude)o).scriptIncludes;
            };
            this.refdataTreeView.ChildrenGetter = childrenGetter;
            //tell the control which image to show
            ImageGetterDelegate imageGetter = delegate (object rowObject)
            {
                if (rowObject is Script
                || rowObject is ScriptInclude)
                {
                    return "Script";
                }
                else
                {
                    return "ScriptGroup";
                }
            };
            this.nameColumn.ImageGetter = imageGetter;
        }

        public void loadTempfile(string fileName)
        {
            this.refdataTextBox.Text = fileName; //loading happens automatically
        }

        private void browseRefDataFileButton_Click(object sender, EventArgs e)
        {
            //let the user select a file
            var browseImportFileDialog = new OpenFileDialog();
            browseImportFileDialog.Filter = "Refdata files|*.xml";
            browseImportFileDialog.FilterIndex = 1;
            browseImportFileDialog.Multiselect = false;
            var dialogResult = browseImportFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                //reset to empty
                this.refdataTextBox.Text = string.Empty;
                //before setting to filename, to make sure the loadfile is triggered
                this.refdataTextBox.Text = browseImportFileDialog.FileName;
                //move cursor to end to make sure the end of the path is visible
                this.refdataTextBox.Select(this.refdataTextBox.Text.Length, 0);
            }
        }

        private void refdataTextBox_TextChanged(object sender, EventArgs e)
        {
            loadFile();
        }
        private void loadFile()
        {
            if (System.IO.File.Exists(this.refdataTextBox.Text))
            {
                this.parser = new RefDataParser(this.settings);
                parser.parseRefdata(this.refdataTextBox.Text);
                //Load data in Treeview
                var contents = new List<object>();
                contents.AddRange(this.parser.scriptGroups.Values.OrderBy(x => x.name));
                contents.AddRange(this.parser.individualScripts.OrderBy(x => x.name));
                //first clear object 
                this.refdataTreeView.ClearObjects();
                //set object
                this.refdataTreeView.Objects = contents;
            }
            this.enableDisable();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            //let the user select a file
            var browseExportFileDialog = new SaveFileDialog();
            browseExportFileDialog.Title = "Save export file";
            browseExportFileDialog.Filter = "Refdata files|*.xml";
            browseExportFileDialog.FilterIndex = 1;
            var dialogResult = browseExportFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                var filePath = browseExportFileDialog.FileName;
                //export to file
                this.exportToFile(filePath);
                if (!filePath.Equals(refdataTextBox.Text, StringComparison.InvariantCultureIgnoreCase))
                {
                    
                    this.refdataTextBox.Text = filePath;
                }
                else
                {
                    //refresh the file in case the user choose to save to the same path
                    this.loadFile();
                }               
            }            
        }
        public void exportToFile(string filePath)
        {
            this.parser.unparse(filePath);
        }
        public string selectedEnvironment => this.environmentsDropdown.Text;

        private void AddIncludedScriptsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.settings.addIncludedScripts = this.AddIncludedScriptsCheckBox.Checked;
        }
        public event EventHandler transferToButtonClick;
        private void transferToButton_Click(object sender, EventArgs e)
        {
            if (this.transferToButtonClick != null)
            {
                transferToButtonClick(sender, e);
            }
        }

        private void refdataTreeView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.enableDisable();
        }
    }
}
