using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collision"))
        {
            Debug.Log("Collision");
            SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
            sceneSwitcher.SwitchSceme("DesertScene");
        }
    }
}
