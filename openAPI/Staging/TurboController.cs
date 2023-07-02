using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using openAPI.Models;
using openAPI.ViewModels;

namespace openAPI.Staging
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurboController : ControllerBase
    {
        private readonly HKContext _hkcontext;
        public TurboController(HKContext hkcontext)
        {
            _hkcontext = hkcontext;
        }
        [HttpPost]
        public IActionResult Turbo([FromBody] TurboModel msg)
        {
            OpenAIClient client = new OpenAIClient(new Uri("https://hacker6.openai.azure.com/"), new AzureKeyCredential("50520d3c1ceb4e79aefe93a6550b9eba"));

            List<Qahistory> qahistory = _hkcontext.Qahistories.Where(x => x.ChatId == msg.ChatId).OrderByDescending(y => y.QahistoryId).Take(3).Reverse().ToList();
            IList<ChatMessage> messages = new List<ChatMessage>();

            var options = new ChatCompletionsOptions()
            {
                Messages = { new ChatMessage(ChatRole.System, @"你是藍星金流的客服人員") ,
                                new ChatMessage(ChatRole.Assistant, $"你是一個客服人員,問題不知道或不相關請回答\"無相關資料,請在營業時間聯絡客服人員\",你只能參照「」內的內容回答問題「{ msg.Sim_Anser}」.只根據「」內的內容回答下面問題不要添加任何其他資訊:"),},
                Temperature = (float)0.7,
                MaxTokens = 350,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
            };
            foreach (var content in qahistory)
            {
                options.Messages.Add(new ChatMessage(ChatRole.User, content.QahistoryQ));
                options.Messages.Add(new ChatMessage(ChatRole.Assistant, content.QahistoryA));
            };
            options.Messages.Add(new ChatMessage(ChatRole.User, msg.Question));
            Response<ChatCompletions> responseWithoutStream = client.GetChatCompletionsAsync("gpt-35-turbo", options).GetAwaiter().GetResult();//azure的模型部屬名稱

            string completions = responseWithoutStream.Value.Choices[0].Message.Content.ToString();
            TurboAnserModel Anser = new TurboAnserModel { Ans = completions };
            return Ok(Anser);
        }
    }

}
