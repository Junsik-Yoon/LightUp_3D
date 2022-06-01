using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatShop : MonoBehaviour, Iinteractable
{
    int requiredCash = 10;
    public GameObject statUI;
    public Text villageLevelText;
    public Text expText;
    public Text showLightCashText;

    public Text strText;
    public Text agiText;
    public Text HPText;

    private void Start()
    {
        VillageLevelManager.instance.OnChangeLightCash+=ChangeMoneyText;
    }
    public void Interact()
    {
        VillageManager.instance.StartCoroutine(VillageManager.instance.ShowText(gameObject,"더글라스\n 어서 오게"));
        StartCoroutine(StatUIOperate());
    }
    public void InteractFinished()
    {
        
    }
    IEnumerator StatUIOperate()
    {
        yield return new WaitForSeconds(2f);
        UpdateStatUI();
        statUI.SetActive(true);
    }
    public void UpdateStatUI()
    {
        villageLevelText.text = VillageLevelManager.instance.villageLevel.ToString();
        expText.text = 
            VillageLevelManager.instance.curExp + " / " + VillageLevelManager.instance.expRequired;
        showLightCashText.text = VillageLevelManager.instance.lightCash.ToString();
        strText.text = VillageLevelManager.instance.strMulti.ToString("F1");
        agiText.text = VillageLevelManager.instance.agilMulti.ToString("F1");
        HPText.text = VillageLevelManager.instance.hpMulti.ToString("F1");
    }

    public void OnConfirmButton()
    {
        //스탯 바뀐 것 저장 & 스탯이 바뀐 것이 있을 때만 활성화

        VillageLevelManager.instance.strMulti = float.Parse(strText.text);
        VillageLevelManager.instance.agilMulti = float.Parse(agiText.text);
        VillageLevelManager.instance.hpMulti = float.Parse(HPText.text);
   
        VillageLevelManager.instance.SetPlayerStatMultiplier();
        statUI.SetActive(false);
        
    }
    public void OnCancelButton()
    {
        statUI.SetActive(false);
        //저장값으로 초기화하기
    }
    public void OnUpButton(Text type)
    {
        //실제 수치도 변하도록 작업하기
        if(VillageLevelManager.instance.lightCash>= requiredCash)
        {
            VillageLevelManager.instance.lightCash -= requiredCash;
            //Debug.Log(type.text);
            double value = Convert.ToDouble(type.text);
            type.text = (value += 0.1).ToString("F1");
        }
        
        
    }
    public void OnDownButton(Text type)
    {
        double value = Convert.ToDouble(type.text);
        if(value >1.0)
        {
            VillageLevelManager.instance.lightCash += requiredCash;
            type.text = (value -= 0.1).ToString("F1");

        }
    }
    public void ChangeMoneyText()
    {
        showLightCashText.text = VillageLevelManager.instance.lightCash.ToString();
    }
}
