using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShop : MonoBehaviour,Iinteractable
{
    public void Interact()
    {
         VillageManager.instance.StartCoroutine(VillageManager.instance.ShowText(gameObject,"스킬상인 호프\n 오늘은 무엇을 배우겠나?"));
         StartCoroutine(StatUIOperate());
    }
    public void InteractFinished()
    {
        
    }
    
    IEnumerator StatUIOperate()
    {
        yield return new WaitForSeconds(2f);
        SkillManager.instance.UpdateSkillUI();
        SkillManager.instance.MoveSkillUIDown();
    }
}
