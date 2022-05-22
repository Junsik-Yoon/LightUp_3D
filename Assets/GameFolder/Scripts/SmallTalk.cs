﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTalk : MonoBehaviour,Iinteractable
{
    [TextArea]
    public string[] conversation;
    int converIndex;
    public void Interact()
    {
        converIndex = Random.Range(0,conversation.Length);
        TextManager.instance.ShowText(conversation[converIndex]);
        Debug.Log(conversation[converIndex]);
    }
    public void InteractFinished()
    {
        
    }

}
