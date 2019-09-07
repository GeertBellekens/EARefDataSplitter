using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EARefDataSplitter
{
    public class Script
    {
        
        public Script(string scriptName, ScriptGroup scriptGroup, XElement xElement, SplitterSettings settings) 
            :this(scriptName, scriptGroup.id, xElement,settings)
        {
            this.group = scriptGroup;
            this.group.scripts.Add(this);
        }
        public Script(string scriptName, string groupID, XElement xElement, SplitterSettings settings)
        {
            this.name = scriptName;
            this.xElement = xElement;
            this.groupID = groupID;
            this.settings = settings;
        }

        public string name { get; private set; }
        public ScriptGroup group { get; private set; }
        public XElement xElement { get; private set; }
        public List<ScriptInclude> includedScripts { get; private set; } = new List<ScriptInclude>();
        private SplitterSettings settings { get; set; }
        private string _groupID;
        public string groupID
        {
            get => this.group != null ? this.group.id : this._groupID;
            private set
            {
                this._groupID = value;
            }
        }
        private bool _selected;
        public bool? selected
        {
            get => this._selected;
            set
            {
                if (!value.HasValue)
                {
                    //toggle
                    this._selected = !this._selected;
                }
                else
                {
                    this._selected = value.Value;
                }
                if (this.settings.addIncludedScripts)
                {
                    foreach (var includedScript in this.includedScripts)
                    {
                        if (includedScript.selected != this.selected)
                        {
                            includedScript.selected = this.selected;
                        }
                    }
                }
            }
        }
    }
}
