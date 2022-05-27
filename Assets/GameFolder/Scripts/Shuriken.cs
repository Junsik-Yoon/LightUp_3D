using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public Vector3 dirVec;
    float speed =10f;
    // public Shuriken(Vector3 dirVec)
    // {
    //     this.dirVec = dirVec;
    // }
    private void Start()
    {
        Destroy(gameObject,2f);
    }
    private void Update()
    {
        transform.Translate(dirVec*speed*Time.deltaTime,Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
         if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
        //    Debug.Log("ㅅㄷㄴㅁㄷㅇ");
            Enemy enemy = other.gameObject?.GetComponent<Enemy>();
            enemy.Hit(1f,0.2f);
            Destroy(gameObject);  
        }    
     
    }

}
