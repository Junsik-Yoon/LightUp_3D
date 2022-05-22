using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingHelper : MonoBehaviour
{
    static string nextScene;
    public GameObject[] loadingBackGrounds;
    public Image loadImage;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    private void Awake()
    {
        loadingBackGrounds[Random.Range(0,loadingBackGrounds.Length)].SetActive(true);
    }
    private void Start()
    {
        StartCoroutine(LoadScenePrecess());
    }

    IEnumerator LoadScenePrecess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            // if(op.progress <0.1f)
            // {
            //     loadImage.color = new Color(0f,0f,0f,255f - (op.progress * 255f));
            // }
            // else
            // {
            //     timer +=Time.unscaledDeltaTime;
            //     loadImage.color =  new Color(0f,0f,0f,255f - (Mathf.Lerp(0.1f,1f,timer) * 255f));
            //     if(loadImage.color.a>=1)
            //     {
            //         op.allowSceneActivation = true;
            //         yield break;
            //     }
            // }
            //Debug.Log(timer);
            timer+=Time.unscaledDeltaTime;
            loadImage.color = new Color(0f,0f,0f,1f - (timer/3f));
            Debug.Log(timer);
            if(timer>=3f)
            {
                timer = 0f;
                op.allowSceneActivation = true;
                yield break;
            }
        }

    }
}
