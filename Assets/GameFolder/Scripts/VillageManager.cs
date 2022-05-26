using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageManager : MonoBehaviour
{
    public static VillageManager instance {get; private set;}
    public Text villagerText;
    public int playerInVillage=0;
    public GameObject textUISwitch;
    public Camera sideCam;
    //public GameObject target;
    public GameObject targetLookat;

    public Camera playerCamera;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(StartCameraMove());
    }
    IEnumerator StartCameraMove()
    {
        Vector3 dirVec = (targetLookat.transform.position-sideCam.transform.position ).normalized;
        for(int i=0;i<220;++i)
        {
            yield return new WaitForEndOfFrame();
            sideCam.transform.Translate(dirVec * 0.1f,Space.World);
            transform.LookAt(targetLookat.transform);
        }
        gameObject.GetComponent<EnterGame>().Interact();
        
    }
    public IEnumerator ShowText(GameObject whereToShow, string whatToSay)
    {
       
        Vector3 textPos = new Vector3(whereToShow.transform.position.x,whereToShow.transform.position.y+3f,whereToShow.transform.position.z);
        //textUISwitch.transform.rotation = Quaternion.Euler(0,70f,0);//LookAt(Camera.main.transform.position);//playerCamera.gameObject.transform.position);
        textUISwitch.transform.position = textPos;
        villagerText.text = whatToSay;
        textUISwitch.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        textUISwitch.SetActive(true);
        yield return new WaitForSeconds(3f);
        textUISwitch.SetActive(false);
    }

}
