# CPU
[Repository](https://github.com/bcgov/pssg-cscp-cpu)

## PREREQUISITES
- XrmToolbox
- VPN connection to access Dynamics

## DEVELOP
- Visual Studio IDE 2022 (not tested with earlier versions)
- .NET 8.0 SDK

## DEBUG
- You can use Dynamics oData API to query Dynamics. First login to Dynamics so the API calls will authenticate. Then you can query the API with GET requests like:
https://cscp-vs.dev.jag.gov.bc.ca/api/data/v9.0/vsd_invoicelinedetails?$filter=vsd_invoicelinedetailid eq '00000000-e81b-ec11-b82d-00505683fbf4'
- [Splunk](https://splunk.jag.gov.bc.ca/)

## RESOURCES
[Dynamics](https://cscp-vs.dev.jag.gov.bc.ca)

## TEST
You can use the integration and unit tests but keep in mind the integration tests currently update a live database and are also not idempotent and will fail. 
Use them to test specific scenarios when developing until they are updated to be real integration tests that can be re-run
See file CPU.postman_collection.json for Postman requests to use for testing the Dataversion scheduled jobs

## Mappings

Most field names from database to business layer have an obvious naming convention. For fields that do not have an obvious naming convention, the mapping is explicitly defined below
Keep the table below comma separated for use with CSV applications e.g. Excel

### ScheduleG

Database,					Business Layer
Vsd_Cpu_ReportingPeriod,	Quarter
