using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject TextUI;
    public Text text; 
    public static TextManager instance { get; private set;}
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void ShowText(string words)
    {
        TextUI.SetActive(true);
        text.text = words;
        StartCoroutine(TextShown());
    }
    IEnumerator TextShown()
    {
        yield return new WaitForSeconds(2f); 
        TextUI.SetActive(false);
        text.text = "";
    }
}
