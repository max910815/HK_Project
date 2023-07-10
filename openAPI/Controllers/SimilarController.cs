using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using openAPI.Helper;
using HKDB.Models;
using openAPI.Services;
using openAPI.ViewModels;
using HKDB.Data;

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
        public async Task<IActionResult> Post([FromBody] SimilarViewModel msg)
        {
            //問題轉向量
            Task<List<float>> Question_result_task = Task.Run(async () =>
            {
                return await _AnswerService.EmbeddingAsync(msg.Question);
            });
            //要做餘弦的原始資料
            Task<List<GetDataViewModel>> Data_task = Task.Run(async () =>
            {
                return await _AnswerService.GetData(msg.ApplicationId);
            });
            //抓到要使用的應用設置(模型、maxtoken等)
            Task<Application> Set_tesk = Task.Run(async () =>
            {
                return await _hkcontext.Applications.FirstOrDefaultAsync(x => x.ApplicationId == msg.ApplicationId);
            });


            Task.WaitAll(Data_task, Question_result_task);
            List<float> Question_result = Question_result_task.Result;
            List<GetDataViewModel> Data = Data_task.Result;
            if (Data.Count() == 0)
            {
                return BadRequest("DataID不存在,DataID doesn't exist");
            }
            float[] Sim = new float[Data.Count];
            Parallel.For(0, Data.Count, i =>
            {
                Sim[i] = ExtensionVectorOperation.CosineSimilarity(Question_result, Data[i].Vector.Split(",").Select(float.Parse));
            });
            float[][] Order = Sim.Select((value, index) => new[] { index, value })
                                 .OrderByDescending(o => o[1])
                                 .Take(3).ToArray();
            //將於弦最高資料排成字串 生成答案用
            string Anser_string = "";
            if (Order[0][1] < 0.75)
            {
                return BadRequest("無相似資料");
            }
            else
            {
                for (int i = 0; i < Order.Length; i++)
                {
                    int num_index = (int)Order[i][0];
                    GetDataViewModel dt = Data[num_index];
                    Anser_string += dt.QA;
                }
            }
            //return Ok(Anser_string); //看最相關字串 
            //取得要使用的應用設定 模型 maxtoken等
            await Set_tesk;
            Application Set = Set_tesk.Result;

            if (Set == null)
            {
                return BadRequest("應用ID不存在,ApplicationId doesn't exist");
            }

            //根據應用設定使用不同的模型生成答案 得到的回傳值即為答案
            TurboViewModel Anser_Model = new TurboViewModel { Question = msg.Question, Sim_Anser = Anser_string, Model=Set.Model,Parameter=Set.Parameter, temperature = msg.temperature, ChatId = msg.ChatId };
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
                return BadRequest("錯誤模型名稱");
            }

            //將總資料存進資料庫
            Qahistory qahistory = new Qahistory()
            {
                QahistoryQ = msg.Question,
                QahistoryA = Ans,
                QahistoryVector = string.Join(",", Question_result),
                ChatId = msg.ChatId
            };

            _hkcontext.Add(qahistory);
            await _hkcontext.SaveChangesAsync();
            return Ok(new TurboAnserViewModel { Ans = Ans });
        }
    }

}