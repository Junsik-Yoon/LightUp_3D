using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableConversation : MonoBehaviour
{
    public GameObject otherPerson;
    public GameObject dialogue;
    public Text sceneText;
    new public Text name;
    public string nameString;
    public Text userText;
    public GameObject ReactableBtn;
    public int branch =0;

    [TextArea]
    public string[] conversation;
    int converIndex=0;
    public bool React()
    {
        if(converIndex<conversation.Length)
        {
            SetActiveDialogue(true);
            SetDialogueContent(nameString,conversation[converIndex]);
            ++converIndex;
            
            return true;
        }
        //if(converIndex == conversation.Length)
        else
        {
            SetActiveDialogue(false);
            converIndex=0;
            return false;
        }
    }

    public void SetActiveDialogue(bool active)
    {
        dialogue.SetActive(active);
    }    
    public void SetDialogueContent(string name , string text)
    {
        sceneText.text = text;
        this.name.text = name;
    }
    public void OnClickButton()
    {
        SetActiveDialogue(false);
        ReactableBtn.SetActive(false);
        if(branch == 0)
        {
            StartCoroutine(WaitForNextConver());
        }
        else if (branch == 1)
        {
            StartCoroutine(WaitForNextConver2());
        }
        else if (branch == 2)
        {
            StartCoroutine(WaitForNextConver3());
        }
        else if (branch == 3)
        {
            StartCoroutine(WaitForNextConver4());
        }
        
    }
    IEnumerator WaitForNextConver()
    {
        yield return new WaitForSeconds(2f);
        React();
        otherPerson?.GetComponent<Italkable>().NextMove();
    }
    IEnumerator WaitForNextConver2()
    {
        yield return new WaitForSeconds(2f);
        React();
        yield return new WaitForSeconds(3f);
        React();
        yield return new WaitForSeconds(3f);
        React();
        otherPerson?.GetComponent<Italkable>().NextMove();
    }
    IEnumerator WaitForNextConver3()
    {
        yield return new WaitForSeconds(2f);
        React();
        yield return new WaitForSeconds(4f);
        React();
        otherPerson?.GetComponent<Italkable>().NextMove();
    }
    IEnumerator WaitForNextConver4()
    {
        yield return new WaitForSeconds(2f);
        React();
        yield return new WaitForSeconds(2f);
        SetActiveDialogue(false);
        otherPerson?.GetComponent<Italkable>().NextMove();
    }
}
