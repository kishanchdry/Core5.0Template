using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IManageService
    {
        string GenrateOTP();
        Task<bool> VerifyEmailOTP(string OTP, string userId);
        Task<bool> VerifyPhoneOTP(string OTP, string userId);
    }
}