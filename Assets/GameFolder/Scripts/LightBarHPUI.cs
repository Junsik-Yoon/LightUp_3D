using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LightBarHPUI : MonoBehaviour
{
    Scrollbar   scrollbar;
    Image       image;
    public PlayerLight pLight;
    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
        image = GetComponent<Image>();
        pLight.OnChangeLight += LightChanged;
    }
    public void LightChanged()
    {
        scrollbar.value = pLight.lightPower/180f;
        image.fillAmount = pLight.lightPower/180f;
    }
    
}
