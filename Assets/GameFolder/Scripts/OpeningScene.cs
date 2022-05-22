//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    //public Camera cam;
    public Light sceneLight;

    public Light middleLight;
    Vector3 mousePosition;

    int imageCounter=0;

    public GameObject[] images;

    private void OnMouseOver()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if ( Physics.Raycast(ray, out hit, Mathf.Infinity,LayerMask.GetMask("Ground")) )
        {
            Vector3 direction = hit.transform.position - Camera.main.transform.position;
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
        }
        Vector3 pos = new Vector3(hit.point.x,hit.point.y+1f,hit.point.z);

        sceneLight.gameObject.transform.position = pos;
    }
    private void OnMouseDown()
    {
        Debug.Log(imageCounter);
        if(imageCounter<1) return;
        if(imageCounter==4)
        {
            StartCoroutine(StartGame());
            return;
        }
        images[imageCounter-1].GetComponent<MeshRenderer>().enabled=false;
        images[imageCounter].GetComponent<MeshRenderer>().enabled=true;
        ++imageCounter;
    }

    private void Start()
    {
        StartCoroutine(StartOpening());
    }
    IEnumerator StartOpening()
    {
        yield return new WaitForSeconds(2f);
       
        ++imageCounter;
        images[0].GetComponent<MeshRenderer>().enabled=true;
    }
    IEnumerator StartGame()
    {
        sceneLight.gameObject.SetActive(false);
        for(int i=0; i<20; ++i)
        {
            middleLight.intensity+=0.1f;
            middleLight.range+=30f;
            yield return new WaitForSeconds(0.1f);
        }
        for(int i=0; i<20; ++i)
        {
            middleLight.intensity-=0.15f;
            middleLight.range-=40f;
            yield return new WaitForSeconds(0.1f);
        }
        
        LoadingHelper.LoadScene("Tutorial");
    }
}
