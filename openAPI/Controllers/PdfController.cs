using Azure.AI.OpenAI;
using Azure;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using openAPI.Models;
using openAPI.Services;
using openAPI.ViewModels;
using System.Text.RegularExpressions;

namespace openAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pdfController : ControllerBase//砍掉的未完成pdf處理專案 先幫你QQ
    {
        private readonly HKContext _hkcontext;
        private readonly AnswerService _answerService;
        public pdfController(HKContext hkcontext, AnswerService answerService)
        {
            _hkcontext = hkcontext;
            _answerService = answerService;
        }
        [HttpPost]
        public ActionResult<string> Post([FromBody] PdfModel input)
        {
            var Embedding = new EmbeddingController();
            var pdfpath = @$"D:\Python\ChatPDFtry\PDF\{input.FileName}";
            var Reader = new PdfReader(pdfpath);
            
            var Groupedsentences = new List<string>(); // 中文集

            for (int page = 1; page <= Reader.NumberOfPages; page++) //reader.GetNumberOfPages()
            {
                string text = PdfTextExtractor.GetTextFromPage(Reader, page, new LocationTextExtractionStrategy()); // 讀取當頁的字

                // 去除标点符号和换行符
                string cleanedText = Regex.Replace(text, @"[\p{P}-[.]]|\r|\n", "");

                Groupedsentences.Add(cleanedText.Trim());
            }

            Reader.Close();
            var result = new Embedding();
            foreach (var sentences in Groupedsentences)
            {
                
                OpenAIClient client = new(new Uri("https://hacker6.openai.azure.com/"), new AzureKeyCredential("50520d3c1ceb4e79aefe93a6550b9eba"));
                var options = new EmbeddingsOptions(sentences);
                var client_embedding = client.GetEmbeddings("embedding", options).Value.Data[0].Embedding;


                result = new Embedding { EmbeddingVectors = string.Join(",", client_embedding), EmbeddingAnswer = "0", EmbeddingQuestion = "0", Qa = sentences, EmbeddingId = "0123" };
                //_hkcontext.Embeddings.Add(new Embedding {EmbeddingAnswer="0", EmbeddingQuestion = "0", Qa = sentences, EmbeddingVectors = string.Join(",", client_embedding), EmbeddingId = "0123"});
                //_hkcontext.SaveChanges();
                
            }
            return Ok(result);
        }
    }
}