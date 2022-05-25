using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    public GameObject moneyUI;
    public event UnityAction OnMoneyStatusChanged;
    public GameObject moneyDrop;

    public static MoneyManager instance {get; private set;}
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OnMoneyStatusChanged+=UpdateMoneyData;
        moneyText.text = _money.ToString();
    }
    private int _money = 100;
    public int money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            OnMoneyStatusChanged?.Invoke();
            StartCoroutine(MoneyTextShowing());
        }
    }
    public void UpdateMoneyData()
    {
        moneyText.text = _money.ToString();
    }
    IEnumerator MoneyTextShowing()
    {
        moneyUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        moneyUI.SetActive(false);
    }
    
    public void MoneyDrop(Transform dropPos, int dropCount)
    {
        //난이도 상황에 따라 갯수 다르게 하기
        float posXoffSeted;
        float posZoffSeted;
        

        for (int i=0; i<dropCount; ++i)
        {
            Vector3 pos = dropPos.position;
            posXoffSeted = pos.x + Random.Range(-1.0f,1.0f);
            posZoffSeted = pos.z + Random.Range(-1.0f,1.0f);
            Instantiate(moneyDrop,new Vector3(posXoffSeted,0.1f,posZoffSeted),Quaternion.identity);
        }
    }


}
