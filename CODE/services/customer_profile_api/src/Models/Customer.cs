using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Enterprise.CX.CustomerProfile.Models
{
    /// <summary>
    /// Represents a customer profile in the Enterprise CX system
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Unique identifier for the customer
        /// </summary>
        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Customer's primary email address
        /// </summary>
        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Customer's first name
        /// </summary>
        [JsonPropertyName("firstName")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Customer's last name
        /// </summary>
        [JsonPropertyName("lastName")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Customer's primary phone number
        /// </summary>
        [JsonPropertyName("phoneNumber")]
        [Phone]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Customer's date of birth
        /// </summary>
        [JsonPropertyName("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Customer's address information
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Customer's communication preferences
        /// </summary>
        [JsonPropertyName("preferences")]
        public CustomerPreferences? Preferences { get; set; }

        /// <summary>
        /// Customer segment classification
        /// </summary>
        [JsonPropertyName("segment")]
        public CustomerSegment Segment { get; set; } = CustomerSegment.Standard;

        /// <summary>
        /// Customer account status
        /// </summary>
        [JsonPropertyName("status")]
        public CustomerStatus Status { get; set; } = CustomerStatus.Active;

        /// <summary>
        /// Custom tags for customer categorization
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        /// <summary>
        /// Timestamp when the customer was created
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp when the customer was last updated
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets the customer's full name
        /// </summary>
        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets the customer's age based on date of birth
        /// </summary>
        [JsonIgnore]
        public int? Age
        {
            get
            {
                if (DateOfBirth == null) return null;
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Value.Year;
                if (DateOfBirth.Value.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }

    /// <summary>
    /// Represents customer address information
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Street address
        /// </summary>
        [JsonPropertyName("street")]
        [MaxLength(200)]
        public string? Street { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [JsonPropertyName("city")]
        [MaxLength(100)]
        public string? City { get; set; }

        /// <summary>
        /// State or province
        /// </summary>
        [JsonPropertyName("state")]
        [MaxLength(50)]
        public string? State { get; set; }

        /// <summary>
        /// Postal or ZIP code
        /// </summary>
        [JsonPropertyName("postalCode")]
        [MaxLength(20)]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [JsonPropertyName("country")]
        [MaxLength(50)]
        public string? Country { get; set; }
    }

    /// <summary>
    /// Represents customer communication preferences
    /// </summary>
    public class CustomerPreferences
    {
        /// <summary>
        /// Preferred communication channels
        /// </summary>
        [JsonPropertyName("communicationChannels")]
        public List<CommunicationChannel> CommunicationChannels { get; set; } = new();

        /// <summary>
        /// Preferred language
        /// </summary>
        [JsonPropertyName("language")]
        [MaxLength(10)]
        public string Language { get; set; } = "en-US";

        /// <summary>
        /// Preferred timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        [MaxLength(50)]
        public string Timezone { get; set; } = "UTC";

        /// <summary>
        /// Marketing communication opt-in status
        /// </summary>
        [JsonPropertyName("marketingOptIn")]
        public bool MarketingOptIn { get; set; } = false;

        /// <summary>
        /// Notification preferences
        /// </summary>
        [JsonPropertyName("notificationPreferences")]
        public NotificationPreferences NotificationPreferences { get; set; } = new();
    }

    /// <summary>
    /// Represents notification preferences
    /// </summary>
    public class NotificationPreferences
    {
        /// <summary>
        /// Email notifications enabled
        /// </summary>
        [JsonPropertyName("email")]
        public bool Email { get; set; } = true;

        /// <summary>
        /// SMS notifications enabled
        /// </summary>
        [JsonPropertyName("sms")]
        public bool Sms { get; set; } = false;

        /// <summary>
        /// Push notifications enabled
        /// </summary>
        [JsonPropertyName("push")]
        public bool Push { get; set; } = true;
    }

    /// <summary>
    /// Customer segment classification
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CustomerSegment
    {
        /// <summary>
        /// Premium customer segment
        /// </summary>
        Premium,

        /// <summary>
        /// Standard customer segment
        /// </summary>
        Standard,

        /// <summary>
        /// Basic customer segment
        /// </summary>
        Basic
    }

    /// <summary>
    /// Customer account status
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CustomerStatus
    {
        /// <summary>
        /// Active customer account
        /// </summary>
        Active,

        /// <summary>
        /// Inactive customer account
        /// </summary>
        Inactive,

        /// <summary>
        /// Suspended customer account
        /// </summary>
        Suspended
    }

    /// <summary>
    /// Communication channel options
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CommunicationChannel
    {
        /// <summary>
        /// Phone communication
        /// </summary>
        Phone,

        /// <summary>
        /// Chat communication
        /// </summary>
        Chat,

        /// <summary>
        /// Email communication
        /// </summary>
        Email,

        /// <summary>
        /// SMS communication
        /// </summary>
        Sms,

        /// <summary>
        /// Social media communication
        /// </summary>
        Social
    }
}
