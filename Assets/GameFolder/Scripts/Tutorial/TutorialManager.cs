using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject portal;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject startGenerate;
    public Text tutorialText;
    public GameObject TextUISwitch;
    public PlayerLight playerLight;
    bool isDead=false;

    private void Start()
    {
        AdjustTextPos();
        tutorialText.text = "...";
        StartCoroutine(Dialogue());
    }
    public void AdjustTextPos()
    {     
        Vector3 playerPos = player.transform.position;
        Vector3 textPos = new Vector3(playerPos.x-3.5f,playerPos.y,playerPos.z-3);
        Vector3 pos = Camera.main.WorldToScreenPoint(textPos);
        TextUISwitch.transform.position = pos;
        TextUISwitch.SetActive(true); 
    }
    private void Update()
    {
        if(isDead)return;
        if(playerLight.playerLight.spotAngle<=1.1f)
        {
            isDead = true;
            StartCoroutine(RestartScene());
        }
    }
    IEnumerator Dialogue()
    {
        virtualCamera.Priority = 0;
        yield return new WaitForSeconds(5f);
        tutorialText.text = "이정도의 빛이라면 10분도 버티지 못해..";
        yield return new WaitForSeconds(3f);
        tutorialText.text = "얼른 마을로 돌아가야겠어..";
        yield return new WaitForSeconds(2f);
        tutorialText.text = "";
        TextUISwitch.SetActive(false);
        startGenerate.SetActive(true);
        yield return new WaitForSeconds(30f);
        OpenPortal();
    }
    public void OpenPortal()
    {
        Debug.Log("포탈열림");
        GameObject obj = Instantiate(portal,new Vector3(0,0.1f,0),Quaternion.identity);
        obj.GetComponent<Portal>().SetDestination("MainQuestScene");
    }
    IEnumerator RestartScene()
    {
        AdjustTextPos();
        tutorialText.text = "으아아악!!";
        yield return new WaitForSeconds(3f);
        tutorialText.text = "내게 빛을...";
        yield return new WaitForSeconds(2f);
        LoadingHelper.LoadScene("Tutorial"); 
    }
}
