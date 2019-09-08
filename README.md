# EARefDataSplitter
Select scripts and groups from a Sparx EA refdata file to create a new refdata file with only the selected scriptsscripts

After developing scripts in Sparx Enterprise Architect you probably want to move them from your development environment to the test and production environments.
The standard way to do this is to export the scripts using Reference Data Export.
However, when exporting scripts this way, EA always includes **all** scripts. There is no option to select only a few scripts.

The EARefDataSplitter tool will read such a refdata export, and allow you to select the script you need.
With the export button you can then create a new refdata file with only the selected scripts.

The tool also has the option to add all included scripts (included using the **!INC** statement)
