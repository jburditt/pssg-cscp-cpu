## Generate Entities

Prerequisite: [XrmToolbox](https://www.xrmtoolbox.com/) with [Early Bound Generator V2 plugin](https://www.xrmtoolbox.com/plugins/DLaB.Xrm.EarlyBoundGeneratorV2/)
Download the latest version on the XrmToolbox home page. To install the plugin, open XrmToolbox, click on "Configuration -> Tool Library" and search for "Early Bound Generator V2" and install it.
You can also use Tool Library to update the existing plugins.

1. Open XrmToolbox, add connection, select connection string, and then add the following connection string replacing the placeholders with your IDIR login:
`authtype=AD;url=https://cscp-vs.dev.jag.gov.bc.ca;domain=https://ststest.gov.bc.ca/adfs/oauth2/token;username=<idir_username>@gov.bc.ca;password=<password>`
2. Open DLaB.EarlyBoundGeneratorV2.DefaultSettings.xml and then click "Generate" button.
This will generate the entities, messages, optionsets in their corresponding folders and DatabaseContext.

NOTE in theory, you could add your authentication profile to PAC using your connection string and then use the command lines found in the generated code. If you do try this, please update this ReadMe.md with your findings.