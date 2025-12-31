using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class MoMoPaymentHelper
    {
        private const string PARTNER_CODE = "MOMO";
        private const string ACCESS_KEY = "F8BBA842ECF85";
        private const string SECRET_KEY = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
        private const string ENDPOINT = "https://test-payment.momo.vn/v2/gateway/api/create";
        private const string QUERY_ENDPOINT = "https://test-payment.momo.vn/v2/gateway/api/query";
        private const string IPN_URL = "https://webhook.site/unique-id";

        // ✅ SỬA: Dùng URL tự động đóng tab
        private const string REDIRECT_URL = "https://momo.vn/success";

        /// <summary>
        /// ✅ SỬA: Tạo QR Payment với requestType = "payWithMethod"
        /// </summary>
        public static async Task<MoMoPaymentResponse> CreateQRPaymentAsync(int orderId, decimal amount, string orderInfo)
        {
            try
            {
                string requestId = Guid.NewGuid().ToString();
                string orderId_str = $"ORDER{orderId}";
                long amount_long = (long)amount;

                // ✅ SỬA: Dùng "captureWallet" để nhận cả payUrl và deeplink
                string requestType = "captureWallet";

                string rawSignature = $"accessKey={ACCESS_KEY}&amount={amount_long}&extraData=&ipnUrl={IPN_URL}&orderId={orderId_str}&orderInfo={orderInfo}&partnerCode={PARTNER_CODE}&redirectUrl={REDIRECT_URL}&requestId={requestId}&requestType={requestType}";

                string signature = GenerateSignature(rawSignature, SECRET_KEY);

                var requestBody = new
                {
                    partnerCode = PARTNER_CODE,
                    partnerName = "Test Partner",
                    storeId = "MoMoTestStore",
                    requestId = requestId,
                    amount = amount_long,
                    orderId = orderId_str,
                    orderInfo = orderInfo,
                    redirectUrl = REDIRECT_URL,
                    ipnUrl = IPN_URL,
                    lang = "vi",
                    extraData = "",
                    requestType = requestType,
                    signature = signature,
                    autoCapture = true,
                    orderExpireTime = 15
                };

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    var content = new StringContent(
                        JsonConvert.SerializeObject(requestBody),
                        Encoding.UTF8,
                        "application/json"
                    );

                    System.Diagnostics.Debug.WriteLine($"[MoMo] ===== REQUEST =====");
                    System.Diagnostics.Debug.WriteLine($"[MoMo] Body: {JsonConvert.SerializeObject(requestBody, Formatting.Indented)}");

                    var response = await client.PostAsync(ENDPOINT, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    System.Diagnostics.Debug.WriteLine($"[MoMo] ===== RESPONSE =====");
                    System.Diagnostics.Debug.WriteLine($"[MoMo] Body: {responseBody}");

                    var momoResponse = JsonConvert.DeserializeObject<MoMoPaymentResponse>(responseBody);

                    if (momoResponse == null)
                    {
                        return new MoMoPaymentResponse
                        {
                            resultCode = -1,
                            message = "Failed to parse MoMo response"
                        };
                    }

                    momoResponse.requestId = requestId;

                    // ✅ QUAN TRỌNG: Ưu tiên payUrl (HTTP) thay vì deeplink (momo://)
                    if (momoResponse.resultCode == 0)
                    {
                        // Ưu tiên: payUrl (web) > deeplink (app)
                        if (!string.IsNullOrEmpty(momoResponse.payUrl))
                        {
                            momoResponse.qrCodeUrl = momoResponse.payUrl;
                            System.Diagnostics.Debug.WriteLine($"[MoMo] Using payUrl: {momoResponse.payUrl}");
                        }
                        else if (!string.IsNullOrEmpty(momoResponse.deeplink))
                        {
                            momoResponse.qrCodeUrl = momoResponse.deeplink;
                            System.Diagnostics.Debug.WriteLine($"[MoMo] Using deeplink: {momoResponse.deeplink}");
                        }
                    }

                    return momoResponse;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo] Exception: {ex.Message}");
                return new MoMoPaymentResponse
                {
                    resultCode = -1,
                    message = $"Error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Query transaction status từ MoMo
        /// </summary>
        public static async Task<MoMoQueryResponse> QueryTransactionStatusAsync(string orderId, string requestId)
        {
            try
            {
                string rawSignature = $"accessKey={ACCESS_KEY}&orderId={orderId}&partnerCode={PARTNER_CODE}&requestId={requestId}";
                string signature = GenerateSignature(rawSignature, SECRET_KEY);

                var requestBody = new
                {
                    partnerCode = PARTNER_CODE,
                    requestId = requestId,
                    orderId = orderId,
                    signature = signature,
                    lang = "vi"
                };

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var content = new StringContent(
                        JsonConvert.SerializeObject(requestBody),
                        Encoding.UTF8,
                        "application/json"
                    );

                    System.Diagnostics.Debug.WriteLine($"[MoMo Query] Request: {JsonConvert.SerializeObject(requestBody)}");

                    var response = await client.PostAsync(QUERY_ENDPOINT, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    System.Diagnostics.Debug.WriteLine($"[MoMo Query] Response: {responseBody}");

                    var queryResponse = JsonConvert.DeserializeObject<MoMoQueryResponse>(responseBody);

                    return queryResponse ?? new MoMoQueryResponse { resultCode = -1, message = "Parse error" };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo Query] Error: {ex.Message}");
                return new MoMoQueryResponse { resultCode = -1, message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// ✅ Generate QR Code từ URL (deeplink hoặc payUrl)
        /// </summary>
        private static string GenerateQRCodeFromUrl(string url)
        {
            try
            {
                using (var qrGenerator = new QRCoder.QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new QRCoder.PngByteQRCode(qrCodeData);
                    byte[] qrCodeBytes = qrCode.GetGraphic(20);

                    string base64 = Convert.ToBase64String(qrCodeBytes);
                    return $"data:image/png;base64,{base64}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ QR Generation Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ✅ Fallback: Generate mock QR nếu không có URL từ MoMo
        /// </summary>
        private static string GenerateMockQRCode(string orderId, long amount)
        {
            string mockData = $"MOMO|{orderId}|{amount}|VND";
            return GenerateQRCodeFromUrl(mockData);
        }

        public static bool VerifyCallback(string receivedSignature, Dictionary<string, string> parameters)
        {
            try
            {
                string rawSignature = $"accessKey={parameters["accessKey"]}&amount={parameters["amount"]}&extraData={parameters["extraData"]}&message={parameters["message"]}&orderId={parameters["orderId"]}&orderInfo={parameters["orderInfo"]}&orderType={parameters["orderType"]}&partnerCode={parameters["partnerCode"]}&payType={parameters["payType"]}&requestId={parameters["requestId"]}&responseTime={parameters["responseTime"]}&resultCode={parameters["resultCode"]}&transId={parameters["transId"]}";

                string expectedSignature = GenerateSignature(rawSignature, SECRET_KEY);

                return expectedSignature.Equals(receivedSignature, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo] Verify Error: {ex.Message}");
                return false;
            }
        }

        private static string GenerateSignature(string data, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

    public class MoMoPaymentResponse
    {
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public long amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; }
        public int resultCode { get; set; }
        public string payUrl { get; set; }
        public string deeplink { get; set; }
        public string qrCodeUrl { get; set; }
    }

    public class MoMoQueryResponse
    {
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public long amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; }
        public int resultCode { get; set; }
        public string transId { get; set; }
        public string payType { get; set; }
    }
}