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
        public RefDataSplitterForm()
        {
            InitializeComponent();
            setDelegates();
            enableDisable();
            
        }
        private void enableDisable()
        {
            this.exportButton.Enabled = this.refdataTreeView.Objects.Cast<object>().Any();
        }
        private void setDelegates()
        {
            //tell the control who can expand 
            TreeListView.CanExpandGetterDelegate canExpandGetter = delegate (object x)
            {
                var scriptGroup = x as ScriptGroup;
                return scriptGroup != null && scriptGroup.scripts.Any();
            };
            this.refdataTreeView.CanExpandGetter = canExpandGetter;
            //tell the control how to expand
            TreeListView.ChildrenGetterDelegate childrenGetter = delegate (object o)
            {
                var scriptGroup = o as ScriptGroup;
                return scriptGroup?.scripts.OrderBy(x => x.name);
            };
            this.refdataTreeView.ChildrenGetter = childrenGetter;
            //tell the control which image to show
            ImageGetterDelegate imageGetter = delegate (object rowObject)
            {
                if (rowObject is Script)
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
                //if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.refdataTextBox.Text = browseImportFileDialog.FileName;
                //move cursor to end to make sure the end of the path is visible
                this.refdataTextBox.Select(this.refdataTextBox.Text.Length, 0);
            }
        }

        private void refdataTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.refdataTextBox.Text))
            {
                this.parser = new RefDataParser();
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
                this.parser.unparse(filePath);
            }
        }
    }
}
