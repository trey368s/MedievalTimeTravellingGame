using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatAgent : MonoBehaviour
{
    protected List<ChatMessage> _conversation = new();
    protected float _timeout = 0;
    protected ChatClient _client;

    public TMP_Text TextArea;
    public GameObject SpeechBubble;
    public float Timeout = 15;

    public virtual string GetSystemPrompt()
    {
        return "Tell me a joke.";
    }
    
    protected virtual void Start()
    {
        _client = FindObjectOfType<ChatClient>();
        ChatMessage systemMessage = new ChatMessage()
        {
            Role = ChatRole.system,
            Content = GetSystemPrompt(),
        };
        _conversation.Add(systemMessage);
    }

    protected virtual void FixedUpdate()
    {
        if (_timeout > 0) HideAfterTimeout();
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") SaySomething();
    }

    protected virtual void SaySomething()
    {
        _client.Chat(_conversation.ToArray(), HandleResponse);
    }

    protected virtual void HandleResponse(string response)
    {
        ChatMessage responseMessage = new()
        {
            Role = ChatRole.assistant,
            Content = response,
        };
        _conversation.Add(responseMessage);
        TextArea.text = response;
        SpeechBubble.SetActive(true);
        _timeout = Timeout;
    }

    protected virtual void HideAfterTimeout()
    {
        _timeout -= Time.deltaTime;
        if(_timeout <= 0)
        {
            _timeout= 0;
            SpeechBubble?.SetActive(false);
        }
    }
}
