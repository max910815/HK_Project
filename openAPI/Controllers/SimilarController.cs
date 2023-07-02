using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using openAPI.Helper;
using openAPI.Models;
using openAPI.Services;
using openAPI.ViewModels;

namespace openAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimilarController : ControllerBase//餘弦相似 輸入{"ChatId":"聊天室的ID","Question":"要問的問題","DataId":"餘弦要參照的DataId"}
    {
        private readonly HKContext _hkcontext;
        private readonly AnswerService _AnswerService;
        public SimilarController(HKContext hkcontext, AnswerService AnswerService)
        {
            _hkcontext = hkcontext;
            _AnswerService = AnswerService;
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="msg">name</param>
        /// <param name="msg">id</param>
        /// <returns>
        /// 0
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SimilarModel msg)
        {

            //問題轉向量
            var Question_result = await _AnswerService.EmbeddingAsync(msg.Question);

            //取得資料庫要餘弦比對的向量集及中文集
            List<GetData> Data = await _hkcontext.Embeddings.Where(x => x.AifileId == msg.DataId).Select(x => new GetData { QA = x.Qa, Vector = x.EmbeddingVectors }).ToListAsync();
            if (Data.Count() == 0)
            {
                return BadRequest("找無DataId");
            }

            //餘弦比對及排序取得相似最高
            float[] Sim = new float[Data.Count];
            Parallel.For(0, Data.Count, i =>
            {
                Sim[i] = ExtensionVectorOperation.CosineSimilarity(Question_result, Data[i].Vector.Split(",").Select(float.Parse));
            });
            float[][] Order = Sim.Select((value, index) => new[] { index, value }).OrderByDescending(o => o[1]).Take(3).ToArray();
            //將於弦最高資料排成字串 生成答案用
            string Anser_string = "";
            if (Order[0][1] < 0.75)
            {
                return Ok("無相似資料");
            }
            else
            {
                for (int i = 0; i < Order.Length; i++)
                {
                    int num_index = (int)Order[i][0];
                    GetData dt = Data[num_index];
                    Anser_string += dt.QA;
                }
            }
            //return Ok(Anser_string); //看最相關字串 
            //取得要使用的應用設定 模型 maxtoken等
            Application Set = await _hkcontext.Applications.FirstOrDefaultAsync(x => x.ApplicationId == msg.ApplicationId);
            if (Set == null)
            {
                return BadRequest("應用ID不存在,ApplicationId doesn't exist");
            }

            //根據應用設定使用不同的模型生成答案 得到的回傳值即為答案
            TurboModel Anser_Model = new TurboModel { DataId = msg.DataId, Question = msg.Question, Sim_Anser = Anser_string, Setting = Set, temperature = msg.temperature, ChatId = msg.ChatId };
            string Ans = "";
            if (Set.Model == "gpt-35-turbo")
            {
                Ans = await _AnswerService.TurboChatAsync(Anser_Model);

            }
            else if (Set.Model == "text-davinci-003" || Set.Model == "text-curie-001" || Set.Model == "text-babbage-001" || Set.Model == "text-ada-001")
            {
                Ans = await _AnswerService.OtherChatAsync(Anser_Model);
            }
            else
            {
                return Content("錯誤模型名稱");
            }

            //取得資料庫ID最大值 存取資料用
            string maxId;
            if (_hkcontext.Qahistories.Any())
            {
                var maxHistoryId = await _hkcontext.Qahistories.MaxAsync(q => q.QahistoryId);
                maxId = $"H{int.Parse(maxHistoryId.Substring(1)) + 1:D5}";
            }
            else
            {
                maxId = "H00001";
            }

            //將總資料存進資料庫
            Qahistory qahistory = new Qahistory()
            {
                QahistoryId = maxId,
                QahistoryQ = msg.Question,
                QahistoryA = Ans,
                QahistoryVectors = string.Join(",", Question_result),
                ChatId = msg.ChatId
            };

            _hkcontext.Add(qahistory);
            await _hkcontext.SaveChangesAsync();
            return Ok(new TurboAnserModel { Ans = Ans });
        }
    }

}