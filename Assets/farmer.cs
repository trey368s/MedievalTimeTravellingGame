using System.Collections;
using UnityEngine;
using TMPro;

public class farmer : MonoBehaviour
{
    public GameObject d_template;
    public GameObject canva;
    public TextMeshProUGUI dialogueText;
    bool player_detection = false;

    // Update is called once per frame (RenderStep essentially)
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.F))
        {
            NewDialouge("Lumberjack: Test");
            StartCoroutine(ShowCanvasForSeconds(10));
        }
    }

    void NewDialouge(string text)
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }

    IEnumerator ShowCanvasForSeconds(float seconds)
    {
        // Activate the canvas
        canva.transform.GetChild(1).gameObject.SetActive(true);

        // Wait for the specified seconds
        yield return new WaitForSeconds(seconds);

        // Deactivate the canvas after the specified time
        canva.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}
