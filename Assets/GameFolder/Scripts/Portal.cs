using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    float portalActive = 0f;
    public GameObject timeBarUI;
    public Image timeBarFill;
    public GameObject shockWave;
    private string destination;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log(portalActive);
            if(portalActive>=0f)
            {
                timeBarUI.SetActive(true);
                timeBarFill.fillAmount = portalActive/3f;
                portalActive+=Time.deltaTime;
            }            
        }
        if(portalActive>=3f)
        {
            portalActive = -1f;
            if(SceneManager.GetActiveScene().name=="Stage")
            {
                Player player = other.gameObject?.GetComponent<Player>();
                Light light = (player.playerLight.gameObject)?.GetComponent<Light>();
                player.playerLightLeft = light.spotAngle;
                BattleStageManager.instance.SetNeededPlayerData(player);
                BattleStageManager.dungeonStatus+=1;
            }

            StartCoroutine(ScnChange());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag  == "Player")
        {
            timeBarUI.SetActive(false);
            portalActive = 0f;
            timeBarFill.fillAmount = portalActive/3f;
        }
    }
    IEnumerator ScnChange()
    {
        shockWave.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        LoadingHelper.LoadScene(destination);
    }
    public void SetDestination(string destination)
    {
        this.destination = destination;
    }
    
}
