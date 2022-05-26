using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkeletonPile : MonoBehaviour
{
    public GameObject dustEffect;
    public GameObject skeleton;
    public GameObject transformChanged;
    public ItemData[] randomItems;
    bool isDestroyed=false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isDestroyed)
        {
            isDestroyed=true;
            //더스트이펙트 생성
            GameObject obj = Instantiate(dustEffect,new Vector3(transform.position.x,transform.position.y+1f,transform.position.z),Quaternion.identity);
            Destroy(obj,1.5f);           
            //칼없는 프리팹으로 변경
            skeleton.SetActive(false);
            transformChanged.SetActive(true);
            StartCoroutine(GenerateGameItem(other.gameObject));
        }
    }
    IEnumerator GenerateGameItem(GameObject player)
    {
        
        GameObject obj = Instantiate(randomItems[Random.Range(0,randomItems.Length)].prefab,new Vector3(transform.position.x,transform.position.y+1f,transform.position.z),Quaternion.identity);
        obj.GetComponent<InteractableItem>().ChangePrice(0);
        Vector3 dirVec = (player.transform.position - obj.transform.position).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);
        for(int i=0; i<30; ++i)
        {
            yield return new WaitForEndOfFrame();
            obj.transform.Translate(-fixedDirVec*5f*Time.deltaTime,Space.World);
        }
        
    }
}
