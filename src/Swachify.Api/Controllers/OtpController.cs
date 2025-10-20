using Microsoft.AspNetCore.Mvc;
using Swachify.Application.Interfaces;

namespace Swachify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;

        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromQuery] string phoneNumber)
        {
            var sent = await _otpService.SendOtpAsync(phoneNumber);
            return sent ? Ok("OTP sent successfully.") : BadRequest("Failed to send OTP.");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromQuery] string phoneNumber, [FromQuery] string code)
        {
            var verified = await _otpService.VerifyOtpAsync(phoneNumber, code);
            return verified ? Ok("OTP verified successfully.") : BadRequest("Invalid OTP.");
        }
    }
}
