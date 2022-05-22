using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button[] btns;
    public Light scenePointLight;
    private float targetIntensity;
    private float currentIntensity;
    private float targetRange=0f;

    public bool lightOn = true;
    private void Start()
    {
        currentIntensity = scenePointLight.intensity;
        targetIntensity = 2.5f;
    }
    private void Update()
    {
        if(lightOn)
        {
            lightOn = false;
            scenePointLight.intensity = currentIntensity;
            scenePointLight.range = targetRange + (currentIntensity/3f)+10f;
        }
        else
        {
            lightOn = true;
            scenePointLight.intensity = targetIntensity;
            scenePointLight.range = targetRange + (targetIntensity/3f)+10f;
        }
    }
    
    public void OnStartButton()
    {
        StartCoroutine(WaitForNextMove(0.1f,"OpeningScene"));
    }
    public void OnContinueButton()
    {
        StartCoroutine(WaitForNextMove(0.1f,"VillageScene"));
    }
    public void OnEndZone()
    {
        StartCoroutine(EnlargingLight());
    }

    IEnumerator WaitForNextMove(float time, string scene)
    {
        for(int i = 0; i< 20; ++i)
        {
            yield return new WaitForSeconds(time);
            targetRange-=30f;
        }
        SceneManager.LoadScene(scene);
    }

    IEnumerator EnlargingLight()
    {
        for(int i = 0; i< 30; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            targetRange+=10f;
        }
        for(int i=0; i<btns.Length;++i)
        {
            btns[i].interactable = true;
        }
    }
}
