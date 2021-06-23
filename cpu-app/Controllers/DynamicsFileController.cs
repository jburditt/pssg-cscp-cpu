using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Serilog;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsFileController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly IDocumentMergeService _documentMergeService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public DynamicsFileController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDynamicsResultService dynamicsResultService, IDocumentMergeService documentMergeService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._configuration = configuration;
            this._dynamicsResultService = dynamicsResultService;
            this._documentMergeService = documentMergeService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/documents/contract/{contractId}")]
        public async Task<IActionResult> GetContractDocuments(string userBceid, string businessBceid, string contractId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUContractDocuments";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting contract documents info 'vsd_GetCPUContractDocuments'. contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("{businessBceid}/{userBceid}/documents/account/{accountId}")]
        public async Task<IActionResult> GetAccountDocuments(string userBceid, string businessBceid, string accountId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "accounts(" + accountId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUAccountDocuments";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting account documents info 'vsd_GetCPUAccountDocuments'. account id = {accountId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("{businessBceid}/{userBceid}/document/{docId}")]
        public async Task<IActionResult> DownloadDocument(string userBceid, string businessBceid, string docId)
        {
            try
            {
                string endpointUrl = "vsd_sharepointurls(" + docId + ")/Microsoft.Dynamics.CRM.vsd_DownloadDocumentFromSharePoint";
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, "");
                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting document info 'vsd_DownloadDocumentFromSharePoint'. Document id = {docId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("contract_package/{businessBceid}/{userBceid}/{taskId}")]
        public async Task<IActionResult> GetContractPackage(string userBceid, string businessBceid, string taskId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUContractPackage";
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);
                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting contract package info 'vsd_GetCPUContractPackage'. Task id = {taskId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpPost("signed_contract/{taskId}")]
        public async Task<IActionResult> UploadSignedContract([FromBody] SignedContractPostFromPortal portalModel, string taskId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error(new Exception(ModelState.ToString()), $"API call to 'UploadSignedContract' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                DocMergeRequest docMergeRequest = new DocMergeRequest();
                docMergeRequest.documents = new JAGDocument[portalModel.DocumentCollection.Length];
                docMergeRequest.options = new JAGOptions();

                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_UploadCPUContractPackage";
                SignedContractPostToDynamics data = new SignedContractPostToDynamics();
                data.BusinessBCeID = portalModel.BusinessBCeID;
                data.UserBCeID = portalModel.UserBCeID;

                bool addedSignature = false;
                int merge_insert_index = 0;

                //signature page is always the last doc sent over
                //get signature page and stamp it with the signature image
                int signatureIndex = portalModel.DocumentCollection.Length - 1;
                byte[] signaturePage = System.Convert.FromBase64String(portalModel.DocumentCollection[signatureIndex].body);
                string signatureString = portalModel.Signature.vsd_authorizedsigningofficersignature;
                var offset = signatureString.IndexOf(',') + 1;
                var signatureImage = System.Convert.FromBase64String(signatureString.Substring(offset));
                string signedPage = stampSignaturePage(signaturePage, signatureImage, portalModel.Signature.vsd_signingofficersname, portalModel.Signature.vsd_signingofficertitle);


                for (int i = 0; i < portalModel.DocumentCollection.Length - 1; ++i)
                {
                    docMergeRequest.documents[merge_insert_index] = new JAGDocument(portalModel.DocumentCollection[i].body, merge_insert_index);
                    ++merge_insert_index;

                    if (portalModel.DocumentCollection[i].filename.Contains("TUA") && !addedSignature)
                    {
                        //Signature page should display in contract package immediately after the TUA page
                        docMergeRequest.documents[merge_insert_index] = new JAGDocument(signedPage, merge_insert_index);
                        addedSignature = true;
                        ++merge_insert_index;
                    }
                }

                //if there was no TUA page, then the signature page should be at the end
                if (!addedSignature)
                {
                    docMergeRequest.documents[signatureIndex] = new JAGDocument(signedPage, signatureIndex);
                }

                JsonSerializerOptions mergeOptions = new JsonSerializerOptions();
                mergeOptions.IgnoreNullValues = true;
                string mergeString = System.Text.Json.JsonSerializer.Serialize(docMergeRequest, mergeOptions);

                HttpClientResult mergeResult = await _documentMergeService.Post(mergeString);
                if (mergeResult.statusCode != HttpStatusCode.OK)
                {
                    return StatusCode((int)mergeResult.statusCode, mergeResult.result.ToString());
                }

                string combinedDoc = GetJArrayValue(mergeResult.result, "document");
                //for testing document merge
                // return StatusCode((int)mergeResult.statusCode, mergeResult.result.ToString());

                data.SignedContract = new DynamicsDocumentPost();
                data.SignedContract.body = combinedDoc;
                data.SignedContract.filename = "Contract Package Signed by Service Provider.pdf";

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IgnoreNullValues = true;
                string modelString = System.Text.Json.JsonSerializer.Serialize(data, options);

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);
                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting contract package. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        private string GetJArrayValue(Newtonsoft.Json.Linq.JObject yourJArray, string key)
        {
            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> keyValuePair in yourJArray)
            {
                if (key == keyValuePair.Key)
                {
                    return keyValuePair.Value.ToString();
                }
            }

            return string.Empty;
        }

        [HttpPost("account/{accountId}")]
        public async Task<IActionResult> UploadAccountDocument([FromBody] FilePost model, string accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error(new Exception(ModelState.ToString()), $"API call to 'UploadAccountDocument' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = endpointUrl = "accounts(" + accountId + ")/Microsoft.Dynamics.CRM.vsd_UploadCPUAccountDocuments";

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IgnoreNullValues = true;
                string modelString = System.Text.Json.JsonSerializer.Serialize(model, options);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while uploading account document. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpPost("contract/{contractId}")]
        public async Task<IActionResult> UploadContractDocument([FromBody] FilePost model, string contractId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error(new Exception(ModelState.ToString()), $"API call to 'UploadContractDocument' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_UploadCPUContractDocuments";

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IgnoreNullValues = true;
                string modelString = System.Text.Json.JsonSerializer.Serialize(model, options);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while uploading contract document. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        public static string stampSignaturePage(byte[] signaturePage, byte[] signature, String signingOfficerName, String signingOfficerTitle)
        {
            using (var ms = new MemoryStream())
            {
                PdfReader pdfr = new PdfReader(signaturePage);
                PdfStamper pdfs = new PdfStamper(pdfr, ms);
                Image image = iTextSharp.text.Image.GetInstance(signature);
                Rectangle rect;
                PdfContentByte content;

                rect = pdfr.GetPageSize(1);
                content = pdfs.GetOverContent(1);

                image.SetAbsolutePosition(84.0F, 475.0F);
                image.ScalePercent(29.0F, 25.0F);

                content.AddImage(image);

                PdfLayer layer = new PdfLayer("info-layer", pdfs.Writer);
                content.BeginLayer(layer);
                content.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 20);

                String[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                DateTime today = DateTime.Now;
                String monthString = months[today.Month - 1];
                String yearString = today.Year.ToString().Substring(2);
                var now = DateTime.Now;
                String daySuffix = (now.Day % 10 == 1 && now.Day != 11) ? "st"
                : (now.Day % 10 == 2 && now.Day != 12) ? "nd"
                : (now.Day % 10 == 3 && now.Day != 13) ? "rd"
                : "th";
                String dayString = today.Day.ToString() + daySuffix;

                content.SetColorFill(BaseColor.BLACK);
                content.BeginText();
                content.SetFontAndSize(BaseFont.CreateFont(), 9);
                if (!String.IsNullOrEmpty(signingOfficerName)) content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, signingOfficerName, 84.0F, 420.0F, 0.0F);
                if (!String.IsNullOrEmpty(signingOfficerTitle)) content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, signingOfficerTitle, 84.0F, 370.0F, 0.0F);
                if (!String.IsNullOrEmpty(dayString)) content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, dayString, 152.0F, 624.0F, 0.0F);
                if (!String.IsNullOrEmpty(monthString)) content.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, monthString, 285.0F, 624.0F, 0.0F);
                if (!String.IsNullOrEmpty(yearString)) content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, yearString, 304.0F, 624.5F, 0.0F);
                content.EndText();

                content.EndLayer();

                pdfs.Close();

                byte[] signedPage = ms.ToArray();
                return System.Convert.ToBase64String(signedPage);
            }
        }
    }
}