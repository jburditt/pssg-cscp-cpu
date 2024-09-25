#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database.Model
{
	
	
	/// <summary>
	/// Status of the transaction currency.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum TransactionCurrency_StateCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 1,
	}
	
	/// <summary>
	/// Reason for the status of the transaction currency.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum TransactionCurrency_StatusCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 2,
	}
	
	/// <summary>
	/// Currency in which a financial transaction is carried out.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("transactioncurrency")]
	public partial class TransactionCurrency : Microsoft.Xrm.Sdk.Entity
	{
		
		/// <summary>
		/// Available fields, a the time of codegen, for the transactioncurrency entity
		/// </summary>
		public partial class Fields
		{
			public const string CreatedBy = "createdby";
			public const string CreatedByName = "createdbyname";
			public const string CreatedByYomiName = "createdbyyominame";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedOnBehalfByName = "createdonbehalfbyname";
			public const string CreatedOnBehalfByYomiName = "createdonbehalfbyyominame";
			public const string CurrencyName = "currencyname";
			public const string CurrencyPrecision = "currencyprecision";
			public const string CurrencySymbol = "currencysymbol";
			public const string EntityImage = "entityimage";
			public const string EntityImage_Timestamp = "entityimage_timestamp";
			public const string EntityImage_Url = "entityimage_url";
			public const string EntityImageId = "entityimageid";
			public const string ExchangerAte = "exchangerate";
			public const string ImportSequenceNumber = "importsequencenumber";
			public const string IsoCurrencyCode = "isocurrencycode";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedByName = "modifiedbyname";
			public const string ModifiedByYomiName = "modifiedbyyominame";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedOnBehalfByName = "modifiedonbehalfbyname";
			public const string ModifiedOnBehalfByYomiName = "modifiedonbehalfbyyominame";
			public const string OrganizationId = "organizationid";
			public const string OverriddenCreatedOn = "overriddencreatedon";
			public const string StateCode = "statecode";
			public const string StateCodename = "statecodename";
			public const string StatusCode = "statuscode";
			public const string StatusCodename = "statuscodename";
			public const string TransactionCurrency_Account = "TransactionCurrency_Account";
			public const string TransactionCurrency_Contact = "TransactionCurrency_Contact";
			public const string TransactionCurrency_Task = "TransactionCurrency_Task";
			public const string TransactionCurrency_Vsd_Contract = "TransactionCurrency_Vsd_Contract";
			public const string TransactionCurrency_Vsd_Invoice = "TransactionCurrency_Vsd_Invoice";
			public const string TransactionCurrency_Vsd_InvoiceLineDetail = "TransactionCurrency_Vsd_InvoiceLineDetail";
			public const string TransactionCurrency_Vsd_Payment = "TransactionCurrency_Vsd_Payment";
			public const string TransactionCurrency_Vsd_Program = "TransactionCurrency_Vsd_Program";
			public const string TransactionCurrency_Vsd_ScheduleG = "TransactionCurrency_Vsd_ScheduleG";
			public const string TransactionCurrencyId = "transactioncurrencyid";
			public const string Id = "transactioncurrencyid";
			public const string VersionNumber = "versionnumber";
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public TransactionCurrency(System.Guid id) : 
				base(EntityLogicalName, id)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public TransactionCurrency(string keyName, object keyValue) : 
				base(EntityLogicalName, keyName, keyValue)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public TransactionCurrency(Microsoft.Xrm.Sdk.KeyAttributeCollection keyAttributes) : 
				base(EntityLogicalName, keyAttributes)
		{
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public TransactionCurrency() : 
				base(EntityLogicalName)
		{
		}
		
		public const string PrimaryIdAttribute = "transactioncurrencyid";
		
		public const string PrimaryNameAttribute = "currencyname";
		
		public const string EntitySchemaName = "TransactionCurrency";
		
		public const string EntityLogicalName = "transactioncurrency";
		
		public const string EntityLogicalCollectionName = "transactioncurrencies";
		
		public const string EntitySetName = "transactioncurrencies";
		
		/// <summary>
		/// Unique identifier of the user who created the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyname")]
		public string CreatedByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdby"))
				{
					return this.FormattedValues["createdby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyyominame")]
		public string CreatedByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdby"))
				{
					return this.FormattedValues["createdby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time when the transaction currency was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the transactioncurrency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("createdonbehalfby", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyname")]
		public string CreatedOnBehalfByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdonbehalfby"))
				{
					return this.FormattedValues["createdonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyyominame")]
		public string CreatedOnBehalfByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdonbehalfby"))
				{
					return this.FormattedValues["createdonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Name of the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("currencyname")]
		public string CurrencyName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("currencyname");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("currencyname", value);
			}
		}
		
		/// <summary>
		/// Number of decimal places that can be used for currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("currencyprecision")]
		public System.Nullable<int> CurrencyPrecision
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("currencyprecision");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("currencyprecision", value);
			}
		}
		
		/// <summary>
		/// Symbol for the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("currencysymbol")]
		public string CurrencySymbol
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("currencysymbol");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("currencysymbol", value);
			}
		}
		
		/// <summary>
		/// The default image for the entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimage")]
		public byte[] EntityImage
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<byte[]>("entityimage");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("entityimage", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimage_timestamp")]
		public System.Nullable<long> EntityImage_Timestamp
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("entityimage_timestamp");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimage_url")]
		public string EntityImage_Url
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("entityimage_url");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("entityimageid")]
		public System.Nullable<System.Guid> EntityImageId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("entityimageid");
			}
		}
		
		/// <summary>
		/// Exchange rate between the transaction currency and the base currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("exchangerate")]
		public System.Nullable<decimal> ExchangerAte
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<decimal>>("exchangerate");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("exchangerate", value);
			}
		}
		
		/// <summary>
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
		public System.Nullable<int> ImportSequenceNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("importsequencenumber", value);
			}
		}
		
		/// <summary>
		/// ISO currency code for the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isocurrencycode")]
		public string IsoCurrencyCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("isocurrencycode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("isocurrencycode", value);
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who last modified the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyname")]
		public string ModifiedByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedby"))
				{
					return this.FormattedValues["modifiedby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyyominame")]
		public string ModifiedByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedby"))
				{
					return this.FormattedValues["modifiedby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time when the transaction currency was last modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public System.Nullable<System.DateTime> ModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who last modified the transactioncurrency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("modifiedonbehalfby", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyname")]
		public string ModifiedOnBehalfByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedonbehalfby"))
				{
					return this.FormattedValues["modifiedonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyyominame")]
		public string ModifiedOnBehalfByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedonbehalfby"))
				{
					return this.FormattedValues["modifiedonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier of the organization associated with the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		public Microsoft.Xrm.Sdk.EntityReference OrganizationId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("organizationid");
			}
		}
		
		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
		public System.Nullable<System.DateTime> OverriddenCreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("overriddencreatedon", value);
			}
		}
		
		/// <summary>
		/// Status of the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
		public virtual TransactionCurrency_StateCode? StateCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((TransactionCurrency_StateCode?)(EntityOptionSetEnum.GetEnum(this, "statecode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("statecode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecodename")]
		public string StateCodename
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("statecode"))
				{
					return this.FormattedValues["statecode"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Reason for the status of the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual TransactionCurrency_StatusCode? StatusCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((TransactionCurrency_StatusCode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("statuscode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscodename")]
		public string StatusCodename
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("statuscode"))
				{
					return this.FormattedValues["statuscode"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier of the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("transactioncurrencyid")]
		public System.Nullable<System.Guid> TransactionCurrencyId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("transactioncurrencyid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("transactioncurrencyid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("transactioncurrencyid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.TransactionCurrencyId = value;
			}
		}
		
		/// <summary>
		/// Version number of the transaction currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
		public System.Nullable<long> VersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
			}
		}
		
		/// <summary>
		/// 1:N transactioncurrency_account
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("transactioncurrency_account")]
		public System.Collections.Generic.IEnumerable<Database.Model.Account> TransactionCurrency_Account
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Account>("transactioncurrency_account", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Account>("transactioncurrency_account", null, value);
			}
		}
		
		/// <summary>
		/// 1:N transactioncurrency_contact
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("transactioncurrency_contact")]
		public System.Collections.Generic.IEnumerable<Database.Model.Contact> TransactionCurrency_Contact
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Contact>("transactioncurrency_contact", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Contact>("transactioncurrency_contact", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_Task
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_Task")]
		public System.Collections.Generic.IEnumerable<Database.Model.Task> TransactionCurrency_Task
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Task>("TransactionCurrency_Task", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Task>("TransactionCurrency_Task", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_vsd_contract
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_vsd_contract")]
		public System.Collections.Generic.IEnumerable<Database.Model.Vsd_Contract> TransactionCurrency_Vsd_Contract
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Vsd_Contract>("TransactionCurrency_vsd_contract", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Vsd_Contract>("TransactionCurrency_vsd_contract", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_vsd_invoice
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_vsd_invoice")]
		public System.Collections.Generic.IEnumerable<Database.Model.Vsd_Invoice> TransactionCurrency_Vsd_Invoice
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Vsd_Invoice>("TransactionCurrency_vsd_invoice", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Vsd_Invoice>("TransactionCurrency_vsd_invoice", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_vsd_invoicelinedetail
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_vsd_invoicelinedetail")]
		public System.Collections.Generic.IEnumerable<Database.Model.Vsd_InvoiceLineDetail> TransactionCurrency_Vsd_InvoiceLineDetail
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Vsd_InvoiceLineDetail>("TransactionCurrency_vsd_invoicelinedetail", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Vsd_InvoiceLineDetail>("TransactionCurrency_vsd_invoicelinedetail", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_vsd_payment
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_vsd_payment")]
		public System.Collections.Generic.IEnumerable<Database.Model.Vsd_Payment> TransactionCurrency_Vsd_Payment
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Vsd_Payment>("TransactionCurrency_vsd_payment", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Vsd_Payment>("TransactionCurrency_vsd_payment", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_vsd_program
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_vsd_program")]
		public System.Collections.Generic.IEnumerable<Database.Model.Vsd_Program> TransactionCurrency_Vsd_Program
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Vsd_Program>("TransactionCurrency_vsd_program", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Vsd_Program>("TransactionCurrency_vsd_program", null, value);
			}
		}
		
		/// <summary>
		/// 1:N TransactionCurrency_vsd_scheduleg
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_vsd_scheduleg")]
		public System.Collections.Generic.IEnumerable<Database.Model.Vsd_ScheduleG> TransactionCurrency_Vsd_ScheduleG
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Database.Model.Vsd_ScheduleG>("TransactionCurrency_vsd_scheduleg", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<Database.Model.Vsd_ScheduleG>("TransactionCurrency_vsd_scheduleg", null, value);
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public TransactionCurrency(object anonymousType) : 
				this()
		{
            foreach (var p in anonymousType.GetType().GetProperties())
            {
                var value = p.GetValue(anonymousType, null);
                var name = p.Name.ToLower();
            
                if (name.EndsWith("enum") && value.GetType().BaseType == typeof(System.Enum))
                {
                    value = new Microsoft.Xrm.Sdk.OptionSetValue((int) value);
                    name = name.Remove(name.Length - "enum".Length);
                }
            
                switch (name)
                {
                    case "id":
                        base.Id = (System.Guid)value;
                        Attributes["transactioncurrencyid"] = base.Id;
                        break;
                    case "transactioncurrencyid":
                        var id = (System.Nullable<System.Guid>) value;
                        if(id == null){ continue; }
                        base.Id = id.Value;
                        Attributes[name] = base.Id;
                        break;
                    case "formattedvalues":
                        // Add Support for FormattedValues
                        FormattedValues.AddRange((Microsoft.Xrm.Sdk.FormattedValueCollection)value);
                        break;
                    default:
                        Attributes[name] = value;
                        break;
                }
            }
		}
	}
}
#pragma warning restore CS1591
