using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get;private set;}

    private void Awake()
    {
        instance=this;
    }


    public GameObject mouseEffect;

    public bool isEventChecked = false;

    
    public IEnumerator WaitForNextEvent()
    {
        yield return new WaitForSeconds(2f);
        isEventChecked = false;
    }
}
