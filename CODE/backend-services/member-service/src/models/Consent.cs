using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberService.Models
{
    /// <summary>
    /// Consent entity for tracking GDPR consent management
    /// </summary>
    public class Consent
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid MemberId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ConsentType { get; set; } = string.Empty; // DataProcessing, Marketing, Analytics, etc.

        [Required]
        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty; // Specific purpose for data processing

        [Required]
        public bool IsGranted { get; set; } = false;

        [Required]
        public DateTime ConsentDate { get; set; } = DateTime.UtcNow;

        public DateTime? WithdrawnDate { get; set; }

        [MaxLength(100)]
        public string? WithdrawnReason { get; set; }

        [MaxLength(255)]
        public string ConsentMethod { get; set; } = string.Empty; // Web, Email, Phone, etc.

        [MaxLength(500)]
        public string ConsentText { get; set; } = string.Empty; // Exact consent text shown

        [MaxLength(50)]
        public string IpAddress { get; set; } = string.Empty; // For audit trail

        [MaxLength(500)]
        public string UserAgent { get; set; } = string.Empty; // For audit trail

        public DateTime? ExpiryDate { get; set; } // For consent that expires

        [MaxLength(50)]
        public string LegalBasis { get; set; } = string.Empty; // GDPR legal basis

        [MaxLength(100)]
        public string DataCategory { get; set; } = string.Empty; // Personal, Sensitive, etc.

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; } = null!;

        // Computed Properties
        [NotMapped]
        public bool IsValid => IsGranted && IsActive && (!ExpiryDate.HasValue || ExpiryDate.Value > DateTime.UtcNow);

        [NotMapped]
        public bool IsWithdrawn => WithdrawnDate.HasValue;

        [NotMapped]
        public TimeSpan ConsentDuration => (WithdrawnDate ?? DateTime.UtcNow) - ConsentDate;

        // Methods
        public void GrantConsent(string purpose, string method, string consentText, 
            string ipAddress, string userAgent, string legalBasis, string dataCategory)
        {
            IsGranted = true;
            Purpose = purpose;
            ConsentMethod = method;
            ConsentText = consentText;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            LegalBasis = legalBasis;
            DataCategory = dataCategory;
            ConsentDate = DateTime.UtcNow;
            WithdrawnDate = null;
            WithdrawnReason = null;
            IsActive = true;
        }

        public void WithdrawConsent(string reason)
        {
            IsGranted = false;
            WithdrawnDate = DateTime.UtcNow;
            WithdrawnReason = reason;
        }

        public void ExpireConsent()
        {
            IsActive = false;
            WithdrawnDate = DateTime.UtcNow;
            WithdrawnReason = "Consent Expired";
        }

        public void RenewConsent(DateTime newExpiryDate)
        {
            ExpiryDate = newExpiryDate;
            IsActive = true;
            ConsentDate = DateTime.UtcNow;
        }

        public ConsentStatus GetConsentStatus()
        {
            if (!IsActive) return ConsentStatus.Inactive;
            if (IsWithdrawn) return ConsentStatus.Withdrawn;
            if (ExpiryDate.HasValue && ExpiryDate.Value <= DateTime.UtcNow) return ConsentStatus.Expired;
            if (IsGranted) return ConsentStatus.Granted;
            return ConsentStatus.Pending;
        }

        public Dictionary<string, object> ExportConsentData()
        {
            return new Dictionary<string, object>
            {
                { "Id", Id },
                { "ConsentType", ConsentType },
                { "Purpose", Purpose },
                { "IsGranted", IsGranted },
                { "ConsentDate", ConsentDate },
                { "WithdrawnDate", WithdrawnDate },
                { "WithdrawnReason", WithdrawnReason },
                { "ConsentMethod", ConsentMethod },
                { "ConsentText", ConsentText },
                { "ExpiryDate", ExpiryDate },
                { "LegalBasis", LegalBasis },
                { "DataCategory", DataCategory },
                { "IsActive", IsActive },
                { "ConsentStatus", GetConsentStatus().ToString() }
            };
        }
    }

    /// <summary>
    /// Consent status enumeration
    /// </summary>
    public enum ConsentStatus
    {
        Pending,
        Granted,
        Withdrawn,
        Expired,
        Inactive
    }

    /// <summary>
    /// GDPR consent types
    /// </summary>
    public static class ConsentTypes
    {
        public const string DataProcessing = "DataProcessing";
        public const string Marketing = "Marketing";
        public const string Analytics = "Analytics";
        public const string DataSharing = "DataSharing";
        public const string Notifications = "Notifications";
        public const string Cookies = "Cookies";
        public const string Profiling = "Profiling";
        public const string ThirdPartyData = "ThirdPartyData";
    }

    /// <summary>
    /// GDPR legal basis enumeration
    /// </summary>
    public static class LegalBasis
    {
        public const string Consent = "Consent";
        public const string Contract = "Contract";
        public const string LegalObligation = "LegalObligation";
        public const string VitalInterests = "VitalInterests";
        public const string PublicTask = "PublicTask";
        public const string LegitimateInterests = "LegitimateInterests";
    }

    /// <summary>
    /// Data categories for GDPR compliance
    /// </summary>
    public static class DataCategories
    {
        public const string Personal = "Personal";
        public const string Sensitive = "Sensitive";
        public const string Financial = "Financial";
        public const string Health = "Health";
        public const string Biometric = "Biometric";
        public const string Behavioral = "Behavioral";
        public const string Location = "Location";
        public const string Communication = "Communication";
    }
}
