using Microsoft.AspNetCore.Mvc;
using MemberService.Models;
using MemberService.Services;
using MemberService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Controllers
{
    /// <summary>
    /// Member management controller with GDPR compliance
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IConsentService _consentService;
        private readonly IAuditService _auditService;
        private readonly ILogger<MemberController> _logger;

        public MemberController(
            IMemberService memberService,
            IConsentService consentService,
            IAuditService auditService,
            ILogger<MemberController> logger)
        {
            _memberService = memberService;
            _consentService = consentService;
            _auditService = auditService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new member with GDPR-compliant consent
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(MemberDto), 201)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterMemberDto request)
        {
            try
            {
                _logger.LogInformation("Member registration attempt for email: {Email}", request.Email);

                // Validate GDPR consent
                if (!request.HasDataProcessingConsent)
                {
                    return BadRequest(new { Error = "Data processing consent is required for registration" });
                }

                // Check if member already exists
                var existingMember = await _memberService.GetMemberByEmailAsync(request.Email);
                if (existingMember != null)
                {
                    return BadRequest(new { Error = "Member with this email already exists" });
                }

                // Create member
                var member = await _memberService.CreateMemberAsync(request);
                
                // Create initial consents
                await _consentService.CreateConsentAsync(new CreateConsentDto
                {
                    MemberId = member.Id,
                    ConsentType = ConsentTypes.DataProcessing,
                    Purpose = "Service provision and account management",
                    IsGranted = true,
                    ConsentMethod = "Web Registration",
                    ConsentText = "I consent to the processing of my personal data for service provision",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = GetUserAgent(),
                    LegalBasis = LegalBasis.Consent,
                    DataCategory = DataCategories.Personal
                });

                // Optional consents
                if (request.HasMarketingConsent)
                {
                    await _consentService.CreateConsentAsync(new CreateConsentDto
                    {
                        MemberId = member.Id,
                        ConsentType = ConsentTypes.Marketing,
                        Purpose = "Marketing communications and promotional offers",
                        IsGranted = true,
                        ConsentMethod = "Web Registration",
                        ConsentText = "I consent to receive marketing communications",
                        IpAddress = GetClientIpAddress(),
                        UserAgent = GetUserAgent(),
                        LegalBasis = LegalBasis.Consent,
                        DataCategory = DataCategories.Personal
                    });
                }

                // Audit log
                await _auditService.LogAsync(new AuditLogDto
                {
                    MemberId = member.Id,
                    Action = "Member Registration",
                    Details = $"Member registered with email: {request.Email}",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = GetUserAgent()
                });

                _logger.LogInformation("Member registered successfully: {MemberId}", member.Id);
                
                return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering member");
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        /// <summary>
        /// Get member profile
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MemberDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMember(Guid id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                // Audit log
                await _auditService.LogAsync(new AuditLogDto
                {
                    MemberId = id,
                    Action = "Profile View",
                    Details = "Member profile accessed",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = GetUserAgent()
                });

                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving member: {MemberId}", id);
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        /// <summary>
        /// Update member profile
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MemberDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> UpdateMember(Guid id, [FromBody] UpdateMemberDto request)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                // Update member
                var updatedMember = await _memberService.UpdateMemberAsync(id, request);

                // Audit log
                await _auditService.LogAsync(new AuditLogDto
                {
                    MemberId = id,
                    Action = "Profile Update",
                    Details = $"Member profile updated: {string.Join(", ", request.GetChangedFields())}",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = GetUserAgent()
                });

                _logger.LogInformation("Member profile updated: {MemberId}", id);

                return Ok(updatedMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating member: {MemberId}", id);
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete member account (GDPR Right to be Forgotten)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMember(Guid id, [FromBody] DeleteMemberDto request)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                // Delete or anonymize based on request
                if (request.IsHardDelete)
                {
                    await _memberService.DeleteMemberAsync(id);
                    
                    // Audit log (before deletion)
                    await _auditService.LogAsync(new AuditLogDto
                    {
                        MemberId = id,
                        Action = "Account Deletion",
                        Details = $"Member account permanently deleted. Reason: {request.Reason}",
                        IpAddress = GetClientIpAddress(),
                        UserAgent = GetUserAgent()
                    });
                }
                else
                {
                    await _memberService.AnonymizeMemberAsync(id);
                    
                    // Audit log
                    await _auditService.LogAsync(new AuditLogDto
                    {
                        MemberId = id,
                        Action = "Account Anonymization",
                        Details = $"Member account anonymized. Reason: {request.Reason}",
                        IpAddress = GetClientIpAddress(),
                        UserAgent = GetUserAgent()
                    });
                }

                _logger.LogInformation("Member account deleted/anonymized: {MemberId}", id);

                return Ok(new { Message = "Account has been successfully deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting member: {MemberId}", id);
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        /// <summary>
        /// Verify member email
        /// </summary>
        [HttpPost("{id}/verify-email")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> VerifyEmail(Guid id, [FromBody] VerifyEmailDto request)
        {
            try
            {
                var result = await _memberService.VerifyEmailAsync(id, request.VerificationCode);
                if (!result)
                {
                    return BadRequest(new { Error = "Invalid verification code" });
                }

                // Audit log
                await _auditService.LogAsync(new AuditLogDto
                {
                    MemberId = id,
                    Action = "Email Verification",
                    Details = "Email address verified successfully",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = GetUserAgent()
                });

                return Ok(new { Message = "Email verified successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying email: {MemberId}", id);
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        /// <summary>
        /// Update member consent preferences
        /// </summary>
        [HttpPut("{id}/consent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateConsent(Guid id, [FromBody] UpdateConsentDto request)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                // Update consent preferences
                await _consentService.UpdateConsentPreferencesAsync(id, request);

                // Audit log
                await _auditService.LogAsync(new AuditLogDto
                {
                    MemberId = id,
                    Action = "Consent Update",
                    Details = $"Consent preferences updated: Marketing={request.HasMarketingConsent}, Analytics={request.HasAnalyticsConsent}",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = GetUserAgent()
                });

                return Ok(new { Message = "Consent preferences updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating consent: {MemberId}", id);
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        /// <summary>
        /// Get member's consent history
        /// </summary>
        [HttpGet("{id}/consent-history")]
        [ProducesResponseType(typeof(List<ConsentDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetConsentHistory(Guid id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                var consentHistory = await _consentService.GetConsentHistoryAsync(id);
                return Ok(consentHistory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving consent history: {MemberId}", id);
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }

        // Helper methods
        private string GetClientIpAddress()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private string GetUserAgent()
        {
            return HttpContext.Request.Headers["User-Agent"].ToString();
        }
    }
}
