using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EARefDataSplitter
{
    public class RefDataParser
    {
        public Dictionary<string, ScriptGroup> scriptGroups { get; private set; } = new Dictionary<string, ScriptGroup>();
        public List<Script> scripts { get; private set; } = new List<Script>();
        public List<Script> individualScripts { get; private set; } = new List<Script>();
        private SplitterSettings settings { get; set; }
        private Dictionary<string, Script> includableScripts { get; set; }
        private XDocument xDoc;

        public RefDataParser(SplitterSettings settings)
        {
            this.settings = settings;
        }

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
            //to avoid useless \t\t\r\n in the file, open it again and remove those
            string text = File.ReadAllText(targetFilePath);
            text = text.Replace("\t\t\r\n", "");
            File.WriteAllText(targetFilePath, text);

        }

        public void parseRefdata (string refDataFilePath)
        {
            //need to parse this way to avoid tabs in the script code to be changed into spaces (normalization rules)
            var reader = new XmlTextReader(new FileStream(refDataFilePath, FileMode.Open,
                                            FileAccess.Read, FileShare.ReadWrite));
            // parse file into XDocument
            this.xDoc = XDocument.Load(reader);
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
                    script = new Script(scriptName, scriptGroup, scriptNode, this.settings);
                }
                else
                {
                    script = new Script(scriptName, groupID, scriptNode, this.settings);
                    this.individualScripts.Add(script);
                }
                this.scripts.Add(script);
            }
            // get the includable scripts
            getIncludableScripts();
            //get the script dependencies
            foreach(var script in this.scripts)
            {
                this.getScriptDependencies(script);
            }

         }
        public void getScriptDependencies(Script script)
        {
            var scriptContent = script.xElement.Elements("Column")
                            .SingleOrDefault(e => e.Attribute("name").Value == "Script")
                            .Attribute("value").Value;
            //get includes
            var includes = this.getIncludes(scriptContent);
            foreach (var includeString in includes)
            {
                if (this.includableScripts.TryGetValue(includeString, out var includedScript))
                {
                    script.includedScripts.Add(new ScriptInclude(includedScript));
                }
            }
        }
        /// <summary>
        /// creates a dictionary of scripts with the !INC statement as key
        /// </summary>
        private void getIncludableScripts()
        {
            this.includableScripts = new Dictionary<string, Script>();
            foreach(var group in this.scriptGroups.Values)
            {
                foreach(var script in group.scripts)
                {
                    includableScripts[$"!INC {group.name}.{script.name}"] = script;
                }
            }
        }

        /// <summary>
		/// finds each instance of "!INC" and returns the whole line
		/// </summary>
		/// <param name="code">the code to search</param>
		/// <returns>the contents of each line starting with "!INC"</returns>
		private List<string> getIncludes(string code)
        {
            List<string> includes = new List<string>();
            using (StringReader reader = new StringReader(code))
            {
                string line;
                while ((line = reader.ReadLine()?.Trim()) != null)
                {
                    if (line.StartsWith("!INC"))
                    {
                        includes.Add(line);
                    }
                }
            }
            return includes;
        }

    }
}
