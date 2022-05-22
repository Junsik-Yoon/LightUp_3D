using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    public GameObject portal;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject startGenerate;
    public Text tutorialText;
    public Image imageUI;

    public PlayerLight playerLight;

    private void Start()
    {
        tutorialText.text = "...";
        StartCoroutine(Dialogue());
    }
    private void Update()
    {
        //Debug.Log(playerLight.playerLight.spotAngle);
        if(playerLight.playerLight.spotAngle<=1.1f)
        {
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
        imageUI.gameObject.SetActive(false);
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
        imageUI.gameObject.SetActive(true);
        tutorialText.text = "으아아악!!";
        yield return new WaitForSeconds(3f);
        tutorialText.text = "빛이 함께하기를..";
        yield return new WaitForSeconds(2f);
        LoadingHelper.LoadScene("Tutorial"); 
    }
}
