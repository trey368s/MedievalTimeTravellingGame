using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Converters;
using System.IO;
using Azure;

public class ChatClient : MonoBehaviour
{
    private HttpClient _client;
    private string _apiKey = "";
    private string _apiEndpoint = "https://api.openai.com/v1/chat/completions";
    public float temperature = 0.2f;
    public string model = "gtp-3.5-turbo";
    public delegate void ChatResponseDelegate(string ResponseMessage);

    // Start is called before the first frame update
    void Start()
    {
        _client = new();
    }

    public void Chat(ChatMessage[] messages, ChatResponseDelegate callback)
    {
        Chat(messages, callback, temperature);
    }

    public async void Chat(ChatMessage[] messages, ChatResponseDelegate callback, float temperature)
    {
        Debug.Log($"Chat: {messages[messages.Length -1].Content}, temp: {temperature}");

        ChatRequestBody body = new()
        {
            Model = model,
            Temperature = temperature,
            MaxTokens = 100,
            Messages = messages,
        };  

        string requestBodyJson = JsonConvert.SerializeObject(body);
        HttpRequestMessage request = new(HttpMethod.Post, _apiEndpoint);
        request.Headers.Add("Authorization", "Bearer " + _apiKey);
        request.Content = new StringContent(requestBodyJson, null, "application/json");
        HttpResponseMessage response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string responseData = await response.Content.ReadAsStringAsync();
        ChatResponseBody responseBody = JsonConvert.DeserializeObject<ChatResponseBody>(responseData);
        Response resp = responseBody.Choices[0];
        string responseMessage = resp.Message.Content;

        callback(responseMessage);
    }
}

public enum ChatRole
{
    user, system, assistant, function
}

public class ChatRequestBody
{
    [JsonProperty("model")]
    public string Model;
    [JsonProperty("max_tokens")]
    public int MaxTokens;
    [JsonProperty("temperature")]
    public float Temperature;
    [JsonProperty("messages")]
    public ChatMessage[] Messages;

}

public class ChatMessage
{
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("role")]
    public ChatRole Role;
    [JsonProperty("content")]
    public string Content;
}

public class ChatResponseBody
{
    [JsonProperty("id")]
    public string Id;
    [JsonProperty("object")]
    public string Object;
    [JsonProperty("created")]
    public double Created;
    [JsonProperty("model")]
    public string Model;
    [JsonProperty("choices")]
    public Response[] Choices;
    [JsonProperty("usage")]
    public Usage Usage;
}

public class Response
{
    [JsonProperty("index")]
    public int Index;
    [JsonProperty("Message")]
    public ChatMessage Message;
    [JsonProperty("finish_reason")]
    public string FinishReason;
}

public class Usage
{
    [JsonProperty("prompt_tokens")]
    public int PromptTokens;
    [JsonProperty("completion_tokens")]
    public int CompletionToekns;
    [JsonProperty("total_tokens")]
    public string TotalTokens;
}