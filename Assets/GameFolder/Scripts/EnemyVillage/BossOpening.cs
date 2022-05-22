using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOpening : MonoBehaviour
{
    public Image backPanel;
    public Text bossText;
    public Camera bossCam;
    public Animator anim;

    private void Awake()
    {
        bossCam.fieldOfView=27;
        backPanel.color = new Color(0,0,0,1);
        bossText.text = "";
    }
    private void Start()
    {
       
        StartCoroutine(BossScene());
        
    }
    IEnumerator BossScene()
    {
        float alpha = 1f;
        yield return new WaitForSeconds(2f);
        bossText.text = "마을에 쥐새끼가 숨어들었군..";
        yield return new WaitForSeconds(2f);
        for(int i=0; i<55; ++i)
        {
            yield return new WaitForSeconds(0.05f);
            alpha -= 0.003f;
            backPanel.color = new Color(0,0,0,alpha);
        }
        yield return new WaitForSeconds(2f);
        bossText.text = "쥐새끼는 죽어야지";
        //yield return new WaitForSeconds(2f);

        for(int i=0; i<200; ++i)
        {
            yield return new WaitForSeconds(0.01f);
            alpha -= 0.003f;
            backPanel.color = new Color(0,0,0,alpha);
        }
        bossText.text="";
        anim.SetTrigger("IsStandingUp");
        for(int i=0;i<165;++i)
        {
            yield return new WaitForSeconds(0.005f);
            bossCam.fieldOfView+=0.2f;
        }
        
    }
    public void TauntReact()
    {
        StartCoroutine(Taunt());
    }
    IEnumerator Taunt()
    {
        for(int i=0;i<40;++i)
        {
            yield return new WaitForSeconds(0.05f);
            bossCam.fieldOfView+=1.5f;
        }

    }
    public void JumpReact()
    {
        StartCoroutine(Jump());
    }
    IEnumerator Jump()
    {
        float alpha = 0f;
        for(int i=0;i<50;++i)
        {
            yield return new WaitForSeconds(0.05f);
            bossCam.fieldOfView+=2f;
            alpha += 0.05f;
            backPanel.color = new Color(0,0,0,alpha);
        }

    }

}
