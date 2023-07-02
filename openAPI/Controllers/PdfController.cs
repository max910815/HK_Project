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
using openAPI.Data;

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
            var pdfpath = @$"../HK_project/Upload/{input.FileName}";
            PdfReader Reader;
            

            try { Reader = new PdfReader(pdfpath); }
            catch { return BadRequest("檔案不存在"); }
            
            var Groupedsentences = new List<string>(); // 中文集

            for (int page = 1; page <= Reader.NumberOfPages; page++) //reader.GetNumberOfPages()
            {
                string text = PdfTextExtractor.GetTextFromPage(Reader, page, new LocationTextExtractionStrategy()); //讀取當頁的字
                string cleanedText = Regex.Replace(text, @"[\p{P}-[.]]|\r|\n", "");//去除標點符號、換行、多於空格
                Groupedsentences.Add(cleanedText.Trim());
            }
            Reader.Close();

            

            var result = new Embedding();
            foreach (var sentences in Groupedsentences)
            {
                
                OpenAIClient client = new(new Uri("https://hacker6.openai.azure.com/"), new AzureKeyCredential("50520d3c1ceb4e79aefe93a6550b9eba"));
                var options = new EmbeddingsOptions(sentences);
                var client_embedding = client.GetEmbeddings("embedding", options).Value.Data[0].Embedding;

                var count = _hkcontext.Embeddings.Count();

                result = new Embedding { EmbeddingVectors = string.Join(",", client_embedding), Qa = sentences, EmbeddingId = "0123" };
                //_hkcontext.Embeddings.Add(new Embedding { EmbeddingVectors = string.Join(",", client_embedding), Qa = sentences, EmbeddingId = (count + 1).ToString().PadLeft(4, '0'), AifileId = (count + 1).ToString().PadLeft(4, '0') });
                //_hkcontext.Embeddings.Add(new Embedding {EmbeddingAnswer="0", EmbeddingQuestion = "0", Qa = sentences, EmbeddingVectors = string.Join(",", client_embedding), EmbeddingId = "0123"});
                //_hkcontext.SaveChanges();
                
            }
            return Ok(result);
        }
    }
}