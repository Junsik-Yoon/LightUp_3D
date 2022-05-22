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

    public event UnityAction OnChangeLightCash;
    
    private int _lightCash = 100;
    public int lightCash
    {
        get
        {
            return _lightCash;
        }
        set
        {
              
            _lightCash = value;
            OnChangeLightCash?.Invoke();
            
        }
    }
    public GameObject mouseEffect;

    public bool isEventChecked = false;

    
    public IEnumerator WaitForNextEvent()
    {
        yield return new WaitForSeconds(2f);
        isEventChecked = false;
    }
}
