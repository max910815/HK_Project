/*  Getlunguage(input) 判斷輸入文字的語系
 *  ||input|輸入文字|type:string
    ******************************************************
    using openAPI.Services;
    ******************************************************        
    private readonly TranslateService _TranslateService;
    public EmbeddingController(TranslateService TranslateService)
    {
        _TranslateService = TranslateService;
    }
    ******************************************************
    var output = _TranslateService.Getlunguage("test");
    ******************************************************
    string[] test = { "test", "test2", "test3" };
    foreach (var item in test) { var output = _TranslateService.Getlunguage(item); }
*/
//======================================================
/*  GetTranslate(input, form, to) 翻譯文字
 *  ||input|輸入文字|type:string
 *  ||form |來源語系|type:string
 *  ||to   |目標語系|type:string
 *  ******************************************************
 *  using openAPI.Services;
 *  ******************************************************
 *  private readonly TranslateService _TranslateService;
    public EmbeddingController(TranslateService TranslateService)
    {
        _TranslateService = TranslateService;
    }
    ******************************************************
    var output = _TranslateService.GetTranslate("test", "en", "zh-Hans"); //翻譯英文到中文
    ******************************************************
    string[] test = {"test", "test2", "test3" };
    foreach (var item in test) { var output = _TranslateService.GetTranslate(item, "en", "zh-Hans"); }
 */
namespace openAPI.Services
{
    public class TranslateService
    {
        public async Task<string> Getlunguage(string input)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.cognitive.microsofttranslator.com/detect?api-version=3.0");
            request.Headers.Add("Ocp-Apim-Subscription-Key", "8624f20b84474f5a841a7c5952e62153");
            request.Headers.Add("Ocp-Apim-Subscription-Region", "eastasia");

            var send = $"[{{'Text': '{input}'}}]";
            var content = new StringContent(send, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> GetTranslate(string input, string from, string to)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from={from}&to={to}");
            request.Headers.Add("Ocp-Apim-Subscription-Key", "8624f20b84474f5a841a7c5952e62153");
            request.Headers.Add("Ocp-Apim-Subscription-Region", "eastasia");

            var send = $"[{{'Text': '{input}'}}]";
            var content = new StringContent(send, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
        
    }
}
