using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using openAPI.Helper;
using HKDB.Models;
using openAPI.Services;
using openAPI.ViewModels;
using System.Net;
using System.Text.RegularExpressions;
using HKDB.Data;
using Newtonsoft.Json;

namespace openAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pdfController : ControllerBase
    {
        private readonly HKContext _hkcontext;
        private readonly AnswerService _answerService;
        private readonly TranslateService _translateService;
        public pdfController(HKContext hkcontext, AnswerService answerService,TranslateService translateService)
        {
            _hkcontext = hkcontext;
            _answerService = answerService;
            _translateService = translateService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Post(PdfViewModel pdf)
        {
            int fileId = _hkcontext.AiFiles.Max(x => x.AifileId);
            PdfReader reader = new PdfReader($"../HK_project/Upload/{pdf.AifileName}");
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
                if (clear_done != string.Empty)
                {
                    List<float> result = await _answerService.EmbeddingAsync(clear_done);
                    _hkcontext.Embeddings.Add(new Embedding { AifileId = fileId, EmbeddingQuestion = "test", EmbeddingAnswer = "test", Qa = clear_done, EmbeddingVector = string.Join(",", result) });
                    await _hkcontext.SaveChangesAsync();
                }

            }
            reader.Close();
            var lan = await _translateService.Getlunguage(_hkcontext.Embeddings.FirstOrDefault(x => x.AifileId == fileId).Qa);
            List<Dictionary<string, object>> response = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(lan);
            string l = response[0]["language"].ToString();
            _hkcontext.AiFiles.FirstOrDefault(x => x.AifileId == fileId).Language = l;
            await _hkcontext.SaveChangesAsync();
            return Ok("成功");
        }
    }
}