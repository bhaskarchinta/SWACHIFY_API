using System.Threading.Tasks;
using Swachify.Application.Interfaces;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Swachify.Infrastructure.Services
{
    public class TwilioOtpService : IOtpService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _verifyServiceSid;

        public TwilioOtpService(string accountSid, string authToken, string verifyServiceSid)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _verifyServiceSid = verifyServiceSid;
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task<bool> SendMobileOtpAsync(string phoneNumber)
        {
            var verification = await VerificationResource.CreateAsync(
                to: phoneNumber,
                channel: "sms",
                pathServiceSid: _verifyServiceSid
            );
            return verification.Status == "pending";
        }

        public async Task<bool> VerifyMobileOtpAsync(string phoneNumber, string code)
        {
            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: phoneNumber,
                code: code,
                pathServiceSid: _verifyServiceSid
            );
            return verificationCheck.Status == "approved";
        }

        public async Task<bool> SendCustomerOtpAsync(string phoneNumber)
        {
            
            return await SendMobileOtpAsync(phoneNumber);
        }

        public async Task<bool> VerifyCustomerOtpAsync(string phoneNumber, string code)
        {
            
            return await VerifyMobileOtpAsync(phoneNumber, code);
        }
        private string Generate6DigitOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
