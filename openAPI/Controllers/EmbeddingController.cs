using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using openAPI.ViewModels;

namespace openAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbeddingController : ControllerBase//轉向量API!  輸入{"Q":"要轉換的句子"}
    {
        [HttpPost]
        public IActionResult Post([FromBody] EmbeddingModel sentence)
        {
            OpenAIClient client = new(new Uri("https://hacker6.openai.azure.com/"), new AzureKeyCredential("50520d3c1ceb4e79aefe93a6550b9eba"));
            var options = new EmbeddingsOptions(sentence.Q);
            var client_embedding = client.GetEmbeddings("embedding", options).Value.Data[0].Embedding;
            return Ok(client_embedding);
        }

    }


}
