using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatShop : MonoBehaviour, Iinteractable
{
    int requiredMoney = 10;
    public GameObject statUI;
    public Text showLightCashText;
    private void Awake()
    {
        GameManager.instance.OnChangeLightCash+=ChangeMoneyText;
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
        statUI.SetActive(true);
    }

    public void OnConfirmButton()
    {
        //스탯 바뀐 것 저장 & 스탯이 바뀐 것이 있을 때만 활성화
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
        if(GameManager.instance.lightCash>=requiredMoney)
        {
            GameManager.instance.lightCash -=requiredMoney;
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
            GameManager.instance.lightCash +=requiredMoney;
            type.text = (value -= 0.1).ToString("F1");

        }
    }
    public void ChangeMoneyText()
    {
        showLightCashText.text = GameManager.instance.lightCash.ToString();
    }
}
