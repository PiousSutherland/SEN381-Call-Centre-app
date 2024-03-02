using System.Threading.Tasks;

namespace MatthiWare.SmsAndCallClient.Api
{
    public interface IPerson
    {
  
        bool IsInitialized { get; set; }
        bool CanSendSms { get; }
        bool CanCall { get; }
        bool FromNumberRequired { get; }
        void Init();

        Task<IResponse> CallAsync(string from, string to, string msg);
        Task<IResponse> SendSmsAsync(string form, string to, string msg);
        string ToString();
    }
}
