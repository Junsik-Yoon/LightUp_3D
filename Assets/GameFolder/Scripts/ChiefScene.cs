//using System.Numerics;
//using System.Reflection.PortableExecutable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiefScene : MonoBehaviour , Italkable
{
    public Image image;
    private float imageAlpha=0.6f;
    public Camera cam;
    float yRotation;
    InteractableConversation conver;

    int sceneCounter =0;
    private void Awake()
    {
        conver = GetComponent<InteractableConversation>();
        image.color = new Color(1,1,1,imageAlpha);
        StartCoroutine(SceneStart());
    }
    IEnumerator SceneStart()
    {
        if(sceneCounter ==0)
        {
            for(int i=0; i<20; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                image.color = new Color(1,1,1,imageAlpha);
                imageAlpha -=0.03f;
            }
            image.gameObject.SetActive(false);
            yield return new WaitForSeconds(3f);
            conver.React();
            yield return new WaitForSeconds(4f);
            conver.React();
            yield return new WaitForSeconds(1.5f);
            conver.userText.text = "아닙니다. 편하게 말씀하세요.";
            conver.ReactableBtn.SetActive(true);
        }
        else if(sceneCounter ==1)
        {
            yield return new WaitForSeconds(1.5f);
            conver.userText.text = "....";
            conver.ReactableBtn.SetActive(true);
            conver.branch = 1;
        }
        else if(sceneCounter == 2)
        {
            yield return new WaitForSeconds(2f);
            conver.userText.text = "빛을 약탈하란 말씀이십니까?";
            conver.ReactableBtn.SetActive(true);
            conver.branch = 2;
        }
        else if(sceneCounter == 3)
        {
            yield return new WaitForSeconds(3f);
            conver.SetActiveDialogue(false);
            yield return new WaitForSeconds(5f);
            conver.userText.text = "알겠습니다... ";
            conver.ReactableBtn.SetActive(true);
            conver.branch = 3;
        }
        else if(sceneCounter == 4)
        {
            yield return new WaitForSeconds(3f);
            conver.userText.text = "그럼 먼저 일어나겠습니다 ";
            conver.ReactableBtn.SetActive(true);
            conver.branch = 0;
            //cam.GetComponent<cameraMove>().rSpeed=0f;
            yield return StartCoroutine(StandAndTurn());
        }
        else if(sceneCounter == 5)
        {
            yield return new WaitForSeconds(2f);
            conver.SetActiveDialogue(false);
            image.gameObject.SetActive(true);
            for(int i=0; i<20; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                image.color = new Color(0,0,0,imageAlpha);
                imageAlpha +=0.05f;
            }
            LoadingHelper.LoadScene("VillageScene");
        }
    }
    IEnumerator StandAndTurn()
    {  
        for(int i=0; i<40;++i)
        {
            yield return new WaitForSeconds(0.05f);
            cam.transform.Translate(Vector3.up*0.015f);        
        }
        // yRotation = cam.transform.rotation.y;
        // for(int i=0; i<90;++i)
        // {
        //     yRotation += 1f;
        //     yield return new WaitForSeconds(0.03f);
        //     //cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.x,yRotation,cam.transform.rotation.z);
            
        // }
    }

    public void NextMove()
    {
        ++sceneCounter;
        StartCoroutine(SceneStart());
    }
}
