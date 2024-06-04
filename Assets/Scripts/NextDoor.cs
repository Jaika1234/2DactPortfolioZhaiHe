using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextDoor : MonoBehaviour
{
    [SerializeField] protected TextMeshPro interactText;
    private bool playerEntered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");
  
            playerEntered = true;
            interactText.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {

            playerEntered = false;
            interactText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerEntered)
        {
            interactText.text = "Push E go next stage";

            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("NextScene");
            }
        }
        else
        {
            interactText.text = ""; 
        }
    }
}

