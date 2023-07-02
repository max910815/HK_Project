using Azure;
using Azure.AI.OpenAI;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using openAPI.Models;
using openAPI.ViewModels;

namespace openAPI.Services
{
    public class AnswerService
    {
        private readonly HKContext _hkcontext;
        private readonly IConfiguration _configuration;
        public AnswerService(HKContext hkcontext, IConfiguration configuration)
        {
            _hkcontext = hkcontext;
            _configuration = configuration;
        }
        
        public async Task<List<float>> EmbeddingAsync(string q)
        {
            OpenAIClient client = new(new Uri(_configuration["endpoint"]), new AzureKeyCredential(_configuration["API_Key"]));
            var options = new EmbeddingsOptions(q);
            var client_embedding = await client.GetEmbeddingsAsync("embedding", options);
            return client_embedding.Value.Data[0].Embedding.ToList();
        }
        public async Task<string> TurboChatAsync(TurboModel msg)
        {
            OpenAIClient client = new OpenAIClient(new Uri(_configuration["endpoint"]), new AzureKeyCredential(_configuration["API_Key"]));

            List<Qahistory> qahistory = _hkcontext.Qahistories.Where(x => x.ChatId == msg.ChatId).OrderByDescending(y => y.QahistoryId).Take(3).Reverse().ToList();
            IList<ChatMessage> messages = new List<ChatMessage>();

            var options = new ChatCompletionsOptions()
            {
                Messages = { new ChatMessage(ChatRole.System, @"你是藍星金流的客服人員") ,
                                    new ChatMessage(ChatRole.Assistant, $"你是一個客服人員,問題不知道或不相關請回答\"無相關資料,請在營業時間聯絡客服人員\",你只能參照「」內的內容回答問題「{ msg.Sim_Anser}」.只根據「」內的內容回答下面問題不要添加任何其他資訊:"),},
                Temperature = msg.temperature,
                MaxTokens = int.Parse(msg.Setting.Parameter),
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
            Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync("gpt-35-turbo", options);

            string completions = responseWithoutStream.Value.Choices[0].Message.Content.ToString();

            return completions;
        }
        public async Task<string> OtherChatAsync(TurboModel msg)
        {
            OpenAIClient client = new OpenAIClient(new Uri(_configuration["endpoint"]), new AzureKeyCredential(_configuration["API_Key"]));

            List<Qahistory> qahistory = await _hkcontext.Qahistories.Where(x => x.ChatId == msg.ChatId).OrderByDescending(y => y.QahistoryId).Take(3).Reverse().ToListAsync();

            string prompt = "";
            prompt += $"問題不知道或不相關請回答\"無相關資料,請在營業時間聯絡客服人員\",你只能參照「」內的內容回答問題「{msg.Sim_Anser}」.只根據「」內的內容回答下面問題不要添加任何其他資訊:";



            //foreach (var content in qahistory)
            //{
            //    prompt += content.QahistoryQ;
            //    prompt += content.QahistoryA;
            //}
            prompt += msg.Question;

            CompletionsOptions options = new CompletionsOptions()
            {
                MaxTokens = int.Parse(msg.Setting.Parameter),
                Temperature = msg.temperature,
                Prompts = { $"{prompt}" },
            };

            Response<Completions> response = await client.GetCompletionsAsync(msg.Setting.Model, options);

            string completions = response.Value.Choices[0].Text.ToString();

            return completions;
        }
    }
}
