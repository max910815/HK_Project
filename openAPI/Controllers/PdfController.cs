using Azure.AI.OpenAI;
using Azure;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using openAPI.Helper;
using openAPI.Models;
using openAPI.Services;
using openAPI.ViewModels;
using System.Net;
using System.Text.RegularExpressions;
using openAPI.Data;

namespace openAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pdfController : ControllerBase
    {
        private readonly HKContext _hkcontext;
        private readonly AnswerService _answerService;
        public pdfController(HKContext hkcontext, AnswerService answerService)
        {
            _hkcontext = hkcontext;
            _answerService = answerService;
        }
        [HttpGet]
        public ActionResult<string> Get()//先放著 可能會廢棄OuOb 下面有升級版
        {
            PdfReader reader = new PdfReader("./PDF/華電.pdf");
            List<string> groupedSentences = new List<string>();
            
            string[] uselessWords = { "is", "some", "with", "it", "contains" };

            for (int page = 1; page <= reader.NumberOfPages; page++) //reader.NumberOfPages
            {
                string text = PdfTextExtractor.GetTextFromPage(reader, page, new LocationTextExtractionStrategy()); // 讀取當頁的字
            
                string cleanedText = Regex.Replace(text, @"[\p{P}-[.]]|\r|\n", "");

                foreach (string word in uselessWords)
            {
                    cleanedText = cleanedText.Replace(word, "");
                }

                groupedSentences.Add(cleanedText.Trim());
            }


            reader.Close();
            
            foreach (var sentences in groupedSentences)
            {
                string maxId;
                if (_hkcontext.Embeddings.Any())
                {
                    maxId = $"E{int.Parse(_hkcontext.Embeddings.Max(q => q.EmbeddingId).Substring(1)) + 1:D5}";
                }
                else
                {
                    maxId = "E00001";
                }
                var result = _answerService.EmbeddingAsync(sentences);
                _hkcontext.Embeddings.Add(new Embedding { EmbeddingId = maxId, AifileId = "1", EmbeddingQuestion = "test", EmbeddingAnswer = "test", Qa = sentences, EmbeddingVectors = string.Join(",", result) });
                _hkcontext.SaveChanges();
            }
            return Ok("成功");
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(PdfViewModel pdf)
            {
            string filePath = @$"../HK_project/Upload/{pdf.AifileName}.pdf";
            PdfReader reader = new PdfReader(filePath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("文件不存在");
            }
            List<string> groupedSentences = new List<string>(); // 中文集
                
            HashSet<string> stopWords = new HashSet<string>();
            using (StreamReader sr = new StreamReader("./Assest/Stop_word/Stop_word.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = WebUtility.HtmlDecode(line);
                    stopWords.Add(line);
                }
            }

            for (int page = 1; page <= reader.NumberOfPages; page++) //reader.NumberOfPages
            {
                string text = PdfTextExtractor.GetTextFromPage(reader, page, new LocationTextExtractionStrategy()); // 讀取當頁的字

                // 去除标点符号和换行符
                text = Regex.Replace(text, @"[\p{P}-[.]]|\r|\n", "");
                string clear_done = PdfControllerHelpers.RemoveStopWords(text, stopWords);
                string maxId;
                if (_hkcontext.Embeddings.Any())
                {
                    maxId = $"E{int.Parse(_hkcontext.Embeddings.Max(q => q.EmbeddingId).Substring(1)) + 1:D5}";
                }
                else
                {
                    maxId = "E00001";
                }
                var result = await _answerService.EmbeddingAsync(clear_done);
                _hkcontext.Embeddings.Add(new Embedding { EmbeddingId = maxId, AifileId = pdf.AifileId, EmbeddingQuestion = "test", EmbeddingAnswer = "test", Qa = clear_done, EmbeddingVectors = string.Join(",", result) });
                await _hkcontext.SaveChangesAsync();
                
            }
            reader.Close();
            return Ok("成功");
        }
    }
}