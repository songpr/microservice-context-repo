using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MemberService.Services;
using MemberService.Models;
using MemberService.Repositories;
using MemberService.DTOs;
using System;
using System.Threading.Tasks;
using FluentAssertions;

namespace MemberService.Tests.Unit
{
    /// <summary>
    /// Unit tests for Member Service with GDPR compliance validation
    /// </summary>
    public class MemberServiceTests
    {
        private readonly Mock<IMemberRepository> _mockMemberRepository;
        private readonly Mock<IConsentRepository> _mockConsentRepository;
        private readonly Mock<IAuditRepository> _mockAuditRepository;
        private readonly Mock<IEncryptionService> _mockEncryptionService;
        private readonly Mock<ILogger<MemberService.Services.MemberService>> _mockLogger;
        private readonly MemberService.Services.MemberService _memberService;

        public MemberServiceTests()
        {
            _mockMemberRepository = new Mock<IMemberRepository>();
            _mockConsentRepository = new Mock<IConsentRepository>();
            _mockAuditRepository = new Mock<IAuditRepository>();
            _mockEncryptionService = new Mock<IEncryptionService>();
            _mockLogger = new Mock<ILogger<MemberService.Services.MemberService>>();

            _memberService = new MemberService.Services.MemberService(
                _mockMemberRepository.Object,
                _mockConsentRepository.Object,
                _mockAuditRepository.Object,
                _mockEncryptionService.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task CreateMemberAsync_WithValidData_ShouldCreateMemberSuccessfully()
        {
            // Arrange
            var registerDto = new RegisterMemberDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                HasDataProcessingConsent = true,
                HasMarketingConsent = false
            };

            var expectedMember = new Member
            {
                Id = Guid.NewGuid(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DateOfBirth = registerDto.DateOfBirth,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _mockMemberRepository
                .Setup(x => x.CreateAsync(It.IsAny<Member>()))
                .ReturnsAsync(expectedMember);

            _mockEncryptionService
                .Setup(x => x.EncryptPersonalData(It.IsAny<string>()))
                .Returns<string>(data => $"encrypted_{data}");

            // Act
            var result = await _memberService.CreateMemberAsync(registerDto);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(registerDto.FirstName);
            result.LastName.Should().Be(registerDto.LastName);
            result.Email.Should().Be(registerDto.Email);
            result.IsActive.Should().BeTrue();

            _mockMemberRepository.Verify(x => x.CreateAsync(It.IsAny<Member>()), Times.Once);
            _mockAuditRepository.Verify(x => x.LogAsync(It.IsAny<AuditLog>()), Times.Once);
        }

        [Fact]
        public async Task CreateMemberAsync_WithoutDataProcessingConsent_ShouldThrowException()
        {
            // Arrange
            var registerDto = new RegisterMemberDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                HasDataProcessingConsent = false // This should cause validation to fail
            };

            // Act & Assert
            await Assert.ThrowsAsync<GdprComplianceException>(
                () => _memberService.CreateMemberAsync(registerDto)
            );

            _mockMemberRepository.Verify(x => x.CreateAsync(It.IsAny<Member>()), Times.Never);
        }

        [Fact]
        public async Task CreateMemberAsync_WithMinorAge_ShouldSetIsMinorFlag()
        {
            // Arrange
            var registerDto = new RegisterMemberDto
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                DateOfBirth = DateTime.UtcNow.AddYears(-15), // 15 years old (minor)
                HasDataProcessingConsent = true
            };

            var expectedMember = new Member
            {
                Id = Guid.NewGuid(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                DateOfBirth = registerDto.DateOfBirth,
                IsMinor = true,
                IsActive = true
            };

            _mockMemberRepository
                .Setup(x => x.CreateAsync(It.IsAny<Member>()))
                .ReturnsAsync(expectedMember);

            // Act
            var result = await _memberService.CreateMemberAsync(registerDto);

            // Assert
            result.Should().NotBeNull();
            result.IsMinor.Should().BeTrue();
            
            // Verify that audit log includes minor status
            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Details.Contains("Minor"))), Times.Once);
        }

        [Fact]
        public async Task UpdateMemberAsync_WithValidData_ShouldUpdateSuccessfully()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var existingMember = new Member
            {
                Id = memberId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                IsActive = true
            };

            var updateDto = new UpdateMemberDto
            {
                FirstName = "Jonathan", // Changed name
                LastName = "Doe",
                PhoneNumber = "+1234567890"
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(existingMember);

            _mockMemberRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Member>()))
                .ReturnsAsync(existingMember);

            // Act
            var result = await _memberService.UpdateMemberAsync(memberId, updateDto);

            // Assert
            result.Should().NotBeNull();
            _mockMemberRepository.Verify(x => x.UpdateAsync(It.IsAny<Member>()), Times.Once);
            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action == "Profile Update")), Times.Once);
        }

        [Fact]
        public async Task DeleteMemberAsync_WithValidId_ShouldDeleteSuccessfully()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var existingMember = new Member
            {
                Id = memberId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                IsActive = true
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(existingMember);

            _mockMemberRepository
                .Setup(x => x.DeleteAsync(memberId))
                .Returns(Task.CompletedTask);

            // Act
            await _memberService.DeleteMemberAsync(memberId);

            // Assert
            _mockMemberRepository.Verify(x => x.DeleteAsync(memberId), Times.Once);
            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action == "Account Deletion")), Times.Once);
        }

        [Fact]
        public async Task AnonymizeMemberAsync_WithValidId_ShouldAnonymizeData()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var existingMember = new Member
            {
                Id = memberId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                IsActive = true
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(existingMember);

            _mockMemberRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Member>()))
                .ReturnsAsync(existingMember);

            // Act
            await _memberService.AnonymizeMemberAsync(memberId);

            // Assert
            _mockMemberRepository.Verify(x => x.UpdateAsync(
                It.Is<Member>(m => 
                    m.FirstName == "Anonymous" && 
                    m.LastName == "User" && 
                    m.Email.Contains("anonymous") &&
                    m.PhoneNumber == null)), Times.Once);

            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action == "Account Anonymization")), Times.Once);
        }

        [Fact]
        public async Task ExportPersonalDataAsync_WithValidId_ShouldReturnAllPersonalData()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var member = new Member
            {
                Id = memberId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                IsActive = true
            };

            var consents = new List<Consent>
            {
                new Consent
                {
                    Id = Guid.NewGuid(),
                    MemberId = memberId,
                    ConsentType = ConsentTypes.DataProcessing,
                    IsGranted = true,
                    ConsentDate = DateTime.UtcNow
                }
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(member);

            _mockConsentRepository
                .Setup(x => x.GetByMemberIdAsync(memberId))
                .ReturnsAsync(consents);

            // Act
            var result = await _memberService.ExportPersonalDataAsync(memberId);

            // Assert
            result.Should().NotBeNull();
            result.Should().ContainKey("PersonalData");
            result.Should().ContainKey("ConsentHistory");
            
            var personalData = result["PersonalData"] as Dictionary<string, object>;
            personalData.Should().ContainKey("FirstName");
            personalData.Should().ContainKey("LastName");
            personalData.Should().ContainKey("Email");

            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action == "Data Export")), Times.Once);
        }

        [Fact]
        public async Task VerifyEmailAsync_WithValidCode_ShouldVerifySuccessfully()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var verificationCode = "123456";
            var member = new Member
            {
                Id = memberId,
                Email = "john.doe@example.com",
                IsEmailVerified = false
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(member);

            _mockMemberRepository
                .Setup(x => x.ValidateEmailVerificationAsync(memberId, verificationCode))
                .ReturnsAsync(true);

            _mockMemberRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Member>()))
                .ReturnsAsync(member);

            // Act
            var result = await _memberService.VerifyEmailAsync(memberId, verificationCode);

            // Assert
            result.Should().BeTrue();
            _mockMemberRepository.Verify(x => x.UpdateAsync(
                It.Is<Member>(m => m.IsEmailVerified == true)), Times.Once);

            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action == "Email Verification")), Times.Once);
        }

        [Fact]
        public async Task VerifyEmailAsync_WithInvalidCode_ShouldReturnFalse()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var verificationCode = "invalid";
            var member = new Member
            {
                Id = memberId,
                Email = "john.doe@example.com",
                IsEmailVerified = false
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(member);

            _mockMemberRepository
                .Setup(x => x.ValidateEmailVerificationAsync(memberId, verificationCode))
                .ReturnsAsync(false);

            // Act
            var result = await _memberService.VerifyEmailAsync(memberId, verificationCode);

            // Assert
            result.Should().BeFalse();
            _mockMemberRepository.Verify(x => x.UpdateAsync(It.IsAny<Member>()), Times.Never);

            // Verify failed verification attempt is logged
            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action.Contains("Failed"))), Times.Once);
        }

        [Theory]
        [InlineData("", "Doe", "john@example.com")] // Empty first name
        [InlineData("John", "", "john@example.com")] // Empty last name
        [InlineData("John", "Doe", "")] // Empty email
        [InlineData("John", "Doe", "invalid-email")] // Invalid email format
        public async Task CreateMemberAsync_WithInvalidData_ShouldThrowValidationException(
            string firstName, string lastName, string email)
        {
            // Arrange
            var registerDto = new RegisterMemberDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                HasDataProcessingConsent = true
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(
                () => _memberService.CreateMemberAsync(registerDto)
            );

            _mockMemberRepository.Verify(x => x.CreateAsync(It.IsAny<Member>()), Times.Never);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            var memberId = Guid.NewGuid();

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync((Member)null);

            // Act
            var result = await _memberService.GetMemberByIdAsync(memberId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task IsGdprCompliant_WithValidConsent_ShouldReturnTrue()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var member = new Member
            {
                Id = memberId,
                ConsentUpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var consents = new List<Consent>
            {
                new Consent
                {
                    MemberId = memberId,
                    ConsentType = ConsentTypes.DataProcessing,
                    IsGranted = true,
                    IsActive = true,
                    ConsentDate = DateTime.UtcNow
                }
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(member);

            _mockConsentRepository
                .Setup(x => x.GetByMemberIdAsync(memberId))
                .ReturnsAsync(consents);

            // Act
            var result = await _memberService.IsGdprCompliantAsync(memberId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SetDataRetentionAsync_WithValidPeriod_ShouldSetExpiryDate()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var retentionPeriod = TimeSpan.FromDays(365); // 1 year
            var member = new Member
            {
                Id = memberId,
                IsActive = true
            };

            _mockMemberRepository
                .Setup(x => x.GetByIdAsync(memberId))
                .ReturnsAsync(member);

            _mockMemberRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Member>()))
                .ReturnsAsync(member);

            // Act
            await _memberService.SetDataRetentionAsync(memberId, retentionPeriod);

            // Assert
            _mockMemberRepository.Verify(x => x.UpdateAsync(
                It.Is<Member>(m => 
                    m.DataRetentionExpiry.HasValue && 
                    m.DataRetentionExpiry.Value > DateTime.UtcNow)), Times.Once);

            _mockAuditRepository.Verify(x => x.LogAsync(
                It.Is<AuditLog>(log => log.Action == "Data Retention Set")), Times.Once);
        }
    }

    /// <summary>
    /// Custom exception for GDPR compliance violations
    /// </summary>
    public class GdprComplianceException : Exception
    {
        public GdprComplianceException(string message) : base(message) { }
        public GdprComplianceException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Custom exception for validation errors
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
