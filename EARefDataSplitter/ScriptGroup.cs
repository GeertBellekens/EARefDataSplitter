using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EARefDataSplitter
{
    public class ScriptGroup
    {
        public ScriptGroup(string name, string id, XElement xElement)
        {
            this.name = name;
            this.id = id;
            this.xElement = xElement;
        }
        public XElement xElement { get; private set; }
        public string name { get; private set; }
        public string id { get; private set; }
        public List<Script> scripts { get; } = new List<Script>();
        public bool? selected
        {
            get
            {
                if (this.scripts.Any())
                {
                    if (this.scripts.All(x => x.selected == true))
                    {
                        return true;
                    }
                    if (this.scripts.All(x => x.selected == false))
                    {
                        return false;
                    }
                }
                //not all selected and not all not selected
                return null;

            }
            set
            {

                if (value != null)
                {
                    this.scripts.ForEach(x => x.selected = value);
                }
                else
                {
                    var currentValue = this.selected;
                    this.scripts.ForEach(x => x.selected = !currentValue);
                }
            }
        }
    }
}
