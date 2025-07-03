using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberService.Models
{
    /// <summary>
    /// Member entity representing a registered user with GDPR compliance
    /// </summary>
    public class Member
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        [PersonalData] // Mark as personal data for GDPR
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        [PersonalData]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [PersonalData]
        public string? PhoneNumber { get; set; }

        [PersonalData]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        [PersonalData]
        public string? Gender { get; set; }

        [MaxLength(500)]
        [PersonalData]
        public string? Address { get; set; }

        [MaxLength(100)]
        [PersonalData]
        public string? City { get; set; }

        [MaxLength(20)]
        [PersonalData]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        [PersonalData]
        public string? Country { get; set; }

        // Account Status
        public bool IsActive { get; set; } = true;
        public bool IsEmailVerified { get; set; } = false;
        public bool IsPhoneVerified { get; set; } = false;
        public bool IsMinor { get; set; } = false; // For GDPR age verification

        // GDPR Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public DateTime? ConsentUpdatedAt { get; set; }
        public DateTime? DataRetentionExpiry { get; set; }

        // Privacy Settings
        public bool HasMarketingConsent { get; set; } = false;
        public bool HasAnalyticsConsent { get; set; } = false;
        public bool HasDataSharingConsent { get; set; } = false;
        public bool HasNotificationConsent { get; set; } = true;

        // Audit Fields
        public string CreatedBy { get; set; } = "System";
        public string UpdatedBy { get; set; } = "System";
        public string DataProcessingPurpose { get; set; } = "Service Provision";

        // Navigation Properties
        public virtual ICollection<Consent> Consents { get; set; } = new List<Consent>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public virtual ICollection<PrivacyRequest> PrivacyRequests { get; set; } = new List<PrivacyRequest>();

        // Computed Properties
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [NotMapped]
        public int Age => DateOfBirth?.Date != null 
            ? DateTime.Today.Year - DateOfBirth.Value.Year - 
              (DateTime.Today.DayOfYear < DateOfBirth.Value.DayOfYear ? 1 : 0)
            : 0;

        [NotMapped]
        public bool IsGdprCompliant => 
            ConsentUpdatedAt.HasValue && 
            Consents.Any(c => c.ConsentType == "DataProcessing" && c.IsGranted);

        [NotMapped]
        public bool IsDataRetentionValid => 
            !DataRetentionExpiry.HasValue || DataRetentionExpiry.Value > DateTime.UtcNow;

        // Methods
        public void UpdateProfile(string firstName, string lastName, string email, 
            string? phoneNumber = null, DateTime? dateOfBirth = null, 
            string? gender = null, string? address = null, string? city = null, 
            string? postalCode = null, string? country = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
            UpdatedAt = DateTime.UtcNow;
            
            // Check if user is minor for GDPR compliance
            IsMinor = Age < 16; // GDPR age of consent
        }

        public void UpdateConsent(bool marketing, bool analytics, bool dataSharing, bool notifications)
        {
            HasMarketingConsent = marketing;
            HasAnalyticsConsent = analytics;
            HasDataSharingConsent = dataSharing;
            HasNotificationConsent = notifications;
            ConsentUpdatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDataRetentionExpiry(TimeSpan retentionPeriod)
        {
            DataRetentionExpiry = DateTime.UtcNow.Add(retentionPeriod);
        }

        public void VerifyEmail()
        {
            IsEmailVerified = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void VerifyPhone()
        {
            IsPhoneVerified = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate(string reason)
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
            DataProcessingPurpose = $"Deactivated: {reason}";
        }

        public void RecordLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        // GDPR Methods
        public Dictionary<string, object> ExportPersonalData()
        {
            return new Dictionary<string, object>
            {
                { "Id", Id },
                { "FirstName", FirstName },
                { "LastName", LastName },
                { "Email", Email },
                { "PhoneNumber", PhoneNumber },
                { "DateOfBirth", DateOfBirth },
                { "Gender", Gender },
                { "Address", Address },
                { "City", City },
                { "PostalCode", PostalCode },
                { "Country", Country },
                { "CreatedAt", CreatedAt },
                { "UpdatedAt", UpdatedAt },
                { "LastLoginAt", LastLoginAt },
                { "HasMarketingConsent", HasMarketingConsent },
                { "HasAnalyticsConsent", HasAnalyticsConsent },
                { "HasDataSharingConsent", HasDataSharingConsent },
                { "HasNotificationConsent", HasNotificationConsent },
                { "ConsentUpdatedAt", ConsentUpdatedAt },
                { "DataRetentionExpiry", DataRetentionExpiry }
            };
        }

        public void AnonymizeData()
        {
            FirstName = "Anonymous";
            LastName = "User";
            Email = $"anonymous_{Id}@deleted.com";
            PhoneNumber = null;
            DateOfBirth = null;
            Gender = null;
            Address = null;
            City = null;
            PostalCode = null;
            Country = null;
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
            DataProcessingPurpose = "Data Anonymized";
        }
    }

    /// <summary>
    /// Attribute to mark properties as personal data for GDPR compliance
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PersonalDataAttribute : Attribute
    {
        public string? Description { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsEncrypted { get; set; } = false;
    }
}
