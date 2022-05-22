using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillBtnInteract : MonoBehaviour
{
    Image btnImage;

    public Player player;

    private void Awake()
    {
        btnImage = GetComponent<Image>();
        player.OnCoolTime+=CoolTimeLoad;
    }
    public void CoolTimeLoad()
    {
        if(gameObject.name == "Skill_A_Btn")
        {
            btnImage.fillAmount = player.coolTimeA / player.coolMaxA;
        } 
        else if (gameObject.name == "Skill_B_Btn")
        {
            btnImage.fillAmount = player.coolTimeB / player.coolMaxB;
        }
        else if (gameObject.name == "Dodge_Btn")
        {
            btnImage.fillAmount = player.coolTimeDodge / player.coolMaxDodge;
        }
        if(btnImage.fillAmount <=0.02f)
        {
            btnImage.fillAmount =1;
        }
    }
    
}
