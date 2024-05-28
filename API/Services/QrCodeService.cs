using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace API.Services;

public class QrCodeService
{
    private readonly string secretKey = "ThisIsASecretKey";

    public string GenerateQrCode(DateTime visitDate)
    {
        string dateData = visitDate.ToString("yyyy-MM-dd");
        string signature = GenerateSignature(dateData);

        return $"{dateData}|{signature}";
    }

    public bool ValidateQrCode(string qrCodeData, out DateTime visitDate)
    {
        visitDate = DateTime.MinValue;

        string[] parts = qrCodeData.Split('|');
        if(parts.Length != 2) return false;

        string dateData = parts[0];
        string recievedSignature = parts[1];

        if(DateTime.TryParse(dateData, out visitDate))
        {
            string expectedSignature = GenerateSignature(dateData);
            return ((recievedSignature.Replace(' ', '+')).Trim()) == expectedSignature &&
                   visitDate.Date == DateTime.UtcNow.Date;
        }

        return false;
    }

    private string GenerateSignature(string data)
    {
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        byte[] bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(bytes);
    }
}