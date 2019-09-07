using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EARefDataSplitter
{
    public class RefDataParser
    {
        public Dictionary<string, ScriptGroup> scriptGroups { get; private set; } = new Dictionary<string, ScriptGroup>();
        public List<Script> scripts { get; private set; } = new List<Script>();
        public List<Script> individualScripts { get; private set; } = new List<Script>();
        private XDocument xDoc;

        public void unparse(string targetFilePath)
        {
            if (this.xDoc == null)
                throw new Exception("Cannot unparse if a file wasn't parsed before");
            //get the dataset node
            var dataSet = xDoc.Descendants("DataSet").Single(x => x.Attribute("name").Value == "Automation Scripts");
            //remove all subnodes
            dataSet.Elements().Remove();
            //add the nodes for the selected items from the groups
            foreach(var scriptGroup in this.scriptGroups.Values.Where(x => x.selected != false))
            {
                dataSet.Add(scriptGroup.xElement);
                //add the selected scripts
                foreach (var script in scriptGroup.scripts.Where (x => x.selected == true))
                {
                    dataSet.Add(script.xElement);
                }
            }
            //add the individual script (without a group)
            foreach(var script in this.individualScripts.Where(x => x.selected == true))
            {
                dataSet.Add(script.xElement);
            }
            //save the export
            xDoc.Save(targetFilePath);

        }

        public void parseRefdata (string refDataFilePath)
        {
            // parse file into XDocument
            this.xDoc = XDocument.Load(refDataFilePath);
            var scriptNodes = new List<XElement>();
            //loop DataRow elements
            foreach(var dataRow in xDoc.Descendants("DataRow"))
            {
                var category = dataRow.Elements("Column")
                                .SingleOrDefault(e => e.Attribute("name").Value == "ScriptCategory")
                                .Attribute("value").Value;
                //figure out if this is script, Group or ScriptDebugging
                if (category == "3955A83E-9E54-4810-8053-FACC68CD4782")
                {
                    //Process Group
                    //get group name
                    var groupName = dataRow.Elements("Column")
                                .SingleOrDefault(e => e.Attribute("name").Value == "Script")
                                .Attribute("value").Value;
                    //get group id
                    var groupID = dataRow.Elements("Column")
                                .SingleOrDefault(e => e.Attribute("name").Value == "ScriptName")
                                .Attribute("value").Value;
                    //create group
                    var group = new ScriptGroup(groupName, groupID, dataRow);
                    //add group to dictionary
                    this.scriptGroups.Add(groupID, group);

                }
                else if (category == "605A62F7-BCD0-4845-A8D0-7DC45B4D2E3F")
                {
                    //Script
                    scriptNodes.Add(dataRow);
                }
            }
            //after all groups have been processed, process scripts
            foreach(var scriptNode in scriptNodes)
            {
                //get scriptname
                var scriptNameField = scriptNode.Elements("Column")
                            .SingleOrDefault(e => e.Attribute("name").Value == "Notes")
                            .Attribute("value").Value;
                //parse out script name. Should be between <Script Name="ScriptName" Type="Internal" Language="VBScript"/>
                var start = "<Script Name= ".Length;
                var end = scriptNameField.IndexOf('"', start);
                var scriptName = scriptNameField.Substring(start, end - start);
                Script script;
                //get groupID
                var groupID = scriptNode.Elements("Column")
                            .SingleOrDefault(e => e.Attribute("name").Value == "ScriptAuthor")
                            .Attribute("value").Value;
                if (this.scriptGroups.TryGetValue(groupID,out var scriptGroup))
                {
                    script = new Script(scriptName, scriptGroup, scriptNode);
                }
                else
                {
                    script = new Script(scriptName, groupID, scriptNode);
                    this.individualScripts.Add(script);
                }
                //get dependencies
                //TODO
                this.scripts.Add(script);
            }
         }

    }
}
