using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGame : MonoBehaviour, Iinteractable
{
    uint enterCounter = 0;
    public void Interact()
    {
        ++enterCounter;
        if(enterCounter == 1)
        {
            TextManager.instance.ShowText("마을 문지기 아돌프\n  빛이 함께하기를..");
        }
        else if (enterCounter == 2)
        {
            enterCounter = 0;
            LoadingHelper.LoadScene("Stage");
        }
    }
    public void InteractFinished()
    {
        
    }
}
