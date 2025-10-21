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
        public async Task<IActionResult> SendMobileOtp([FromQuery] string phoneNumber)
        {
            var sent = await _otpService.SendMobileOtpAsync(phoneNumber);
            return sent ? Ok("Mobile OTP sent successfully.") : BadRequest("Failed to send Mobile OTP.");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyMobileOtp([FromQuery] string phoneNumber, [FromQuery] string code)
        {
            var verified = await _otpService.VerifyMobileOtpAsync(phoneNumber, code);
            return verified ? Ok("Mobile OTP verified successfully.") : BadRequest("Invalid Mobile OTP.");
        }
        [HttpPost("sendcustomerotp")]
        public async Task<IActionResult> SendCustomerOtp([FromQuery] string phoneNumber)
        {
            var sent = await _otpService.SendCustomerOtpAsync(phoneNumber);
            return sent ? Ok("Customer OTP sent successfully.") : BadRequest("Failed to send Customer OTP.");
        }

        
        [HttpPost("verifycustomerotp")]
        public async Task<IActionResult> VerifyCustomerOtp([FromQuery] string phoneNumber, [FromQuery] string code)
        {
            var verified = await _otpService.VerifyCustomerOtpAsync(phoneNumber, code);
            return verified ? Ok("Customer OTP verified successfully.") : BadRequest("Invalid Customer OTP.");
        }
    }
}
