using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using XSystem.Security.Cryptography;

namespace Cafe_Management.Application.Services
{
    public class VNpayService
    {
        public string vnp_TmnCode = "6FULUY6X";
        private string vnp_HashSecret = "1V1WI5D2VSQ87VIG1B7AQROJC4WN9CTN";
        private string vnp_Url = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
        
        public string CreatePaymentUrl(decimal amount, string orderId)
        {
            var vnpParams = new Dictionary<string, string>
            {
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Amount", (amount * 100).ToString() },
                { "vnp_CurrCode", "VND" },
                { "vnp_OrderInfo", "Payment for order " + orderId },
                { "vnp_OrderType", "other" },
                { "vnp_ReturnUrl", "http://localhost:3000/user/checkout/vn-pay" },
                { "vnp_TxnRef", orderId },
                { "vnp_Locale", "vn" },
                { "vnp_IpAddr", "127.0.0.1" },
                { "vnp_Version","2.1.0"},
                { "vnp_Command","pay"}
             

            };
            string paymentUrl = "";
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // GMT+7 TimeZone
            DateTime currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
            string vnp_CreateDate = currentDateTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            vnpParams.Add("vnp_CreateDate", vnp_CreateDate);
            DateTime vnp_ExpireDateTime = currentDateTime.AddMinutes(15);
            string vnp_ExpireDate = vnp_ExpireDateTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            vnpParams.Add("vnp_ExpireDate", vnp_ExpireDate);
            List<string> fieldNames = vnpParams.Keys.ToList();
            fieldNames.Sort();
            StringBuilder hashData = new StringBuilder();
            StringBuilder query = new StringBuilder();

            foreach (string fieldName in fieldNames)
            {
                string fieldValue = vnpParams[fieldName];
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    // Xây dựng dữ liệu hash
                    hashData.Append(fieldName);
                    hashData.Append("=");
                    hashData.Append(WebUtility.UrlEncode(fieldValue));

                    // Xây dựng query string
                    query.Append(WebUtility.UrlEncode(fieldName));
                    query.Append("=");
                    query.Append(WebUtility.UrlEncode(fieldValue));

                    // Nếu không phải phần tử cuối cùng thì thêm dấu '&'
                    if (fieldNames.Last() != fieldName)
                    {
                        query.Append('&');
                        hashData.Append('&');
                    }
                }
            }
            string queryUrl = query.ToString();
            string vnp_SecureHash = GenerateHmacSHA512(vnp_HashSecret, hashData.ToString());
            queryUrl += "&vnp_SecureHash=" + vnp_SecureHash;
            paymentUrl = vnp_Url + "?" + queryUrl;

            // Tạo signature
            return paymentUrl;


        }

       

        public static string GenerateHmacSHA512(string secretKey, string data)
        {
            using (HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
