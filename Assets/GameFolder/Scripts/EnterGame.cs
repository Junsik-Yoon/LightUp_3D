using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGame : MonoBehaviour//, Iinteractable
{
    public GameObject chiefGuard;
    public GameObject door1;
    public GameObject door2;
    public GameObject sideCam;
    
    private void Awake()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.isStunned=true;
    }
    public void Interact()
    {

        StartCoroutine(ChiefOpenDoor());

        // else if (enterCounter == 3)
        // {
        //     enterCounter = 0;
        //     TextManager.instance.ShowText("마을 문지기 아돌프\n  어둠을 조심하시게...");
        //     LoadingHelper.LoadScene("Stage");
        // }
    }
    // public void InteractFinished()
    // {
        
    // }
    IEnumerator ChiefOpenDoor()
    {
        chiefGuard.transform.rotation = Quaternion.Euler(0,80,0);
        chiefGuard.GetComponent<Animator>().SetTrigger("PushDoor");
        yield return new WaitForSeconds(3f);
        chiefGuard.transform.rotation = Quaternion.Euler(0,140,0);
        chiefGuard.GetComponent<Animator>().SetTrigger("IsOpeningDoor");
        door1.GetComponent<Animator>().SetTrigger("IsOpeningDoor");
        door2.GetComponent<Animator>().SetTrigger("IsOpeningDoor");
        
        yield return new WaitForSeconds(7f);       
        sideCam.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f); 
        //플레이어움직일 수 있게
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.isStunned=false;

        VillageManager.instance.StartCoroutine(VillageManager.instance.ShowText(chiefGuard,"빛이 함께 하기를..."));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(VillageManager.instance.playerInVillage>0)
            {
                LoadingHelper.LoadScene("Stage");
            }
            else
            {
                door1.GetComponent<Animator>().SetTrigger("IsClosingDoor");
                door2.GetComponent<Animator>().SetTrigger("IsClosingDoor"); 
                VillageManager.instance.playerInVillage+=1;
                gameObject.transform.position = new Vector3(-13.65f,2f,22.45f);
            }

        }
    }
}

