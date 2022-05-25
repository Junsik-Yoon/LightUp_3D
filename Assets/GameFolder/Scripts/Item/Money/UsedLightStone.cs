using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsedLightStone : MonoBehaviour
{
    float notInteracable=0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            notInteracable+=Time.deltaTime;
            if(notInteracable>=0.5f)
            {
                notInteracable=-1f;
                StartCoroutine(PickUpMoney(other.gameObject));
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        notInteracable = 0f;
    }
    IEnumerator PickUpMoney(GameObject player)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        float alpha = 1f;
        for(int i=0; i<50; ++i)
        {
            yield return new WaitForEndOfFrame();
            alpha -= 0.02f;
            render.color = new Color(1,1,1,alpha);
            Vector3 dirVec = (player.transform.position - transform.position).normalized;
            transform.Translate(dirVec*3f*Time.deltaTime,Space.World);
            transform.Translate(new Vector3(0,0.02f,0),Space.World);
        }
        MoneyManager.instance.money +=1;
        Destroy(gameObject);
    }
}
