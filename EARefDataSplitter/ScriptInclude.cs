using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EARefDataSplitter
{
    public class ScriptInclude
    {
        public ScriptInclude(Script script)
        {
            this.script = script;
        }

        public string name { get => $"{script.group.name}.{script.name}"; }
        public bool hasIncludes { get => this.script.includedScripts.Any(); }
        private List<ScriptInclude> _scriptIncludes;
        public List<ScriptInclude> scriptIncludes
        {
            get
            {
                if (this._scriptIncludes == null)
                {
                    this._scriptIncludes = new List<ScriptInclude>();
                    foreach(var include in this.script.includedScripts)
                    {
                        this.scriptIncludes.Add(new ScriptInclude(include.script));
                    }
                }
                return this._scriptIncludes;
            }
        }
        public bool? selected
        {
            get
            {
                return this.script.selected;
            }
            set
            {
                if (this.script.selected != value)
                {
                    this.script.selected = value;
                }
            }
        }
  
        private Script script { get; set; }
    }
}
