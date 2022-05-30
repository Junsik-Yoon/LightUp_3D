using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnUsingSkillUnit : MonoBehaviour
{
    public Button button;
    public Image icon;

    public SkillData curSkillData;
    public void AddItem(SkillData skillData)
    {
        curSkillData = skillData;

        icon.sprite = skillData.icon;
        icon.enabled = true;
        button.interactable = true;
    }
    public void RemoveItem()
    {
        
        curSkillData = null;

        icon.sprite = null;
        icon.enabled = false;
        button.interactable = false;
    }
    public void UseItem()
    {
        //Debug.Log(curSkillData.name + "가 사용되었습니다");
        curSkillData.Use();
    }
    public void OnButtonDown()
    {
        SkillManager.instance.UsingSkillAdd(curSkillData);
    }
}
