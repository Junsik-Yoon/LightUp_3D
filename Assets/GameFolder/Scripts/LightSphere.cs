using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    float moveSpeed = 10f;
    GameObject enemyTarget;
    public GameObject hitEffect;
    private void Start()
    {
        Invoke("WaitForDestroy",5f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0; i<enemies.Length; ++i)
        {
            if(Vector3.Magnitude(transform.position - enemies[i].transform.position)<50f)
            {
                Enemy enemy = enemies[i].gameObject?.GetComponent<Enemy>();
                if(enemy.hp>0)
                {
                    enemyTarget = enemies[i];
                    return;
                }
            }
        }
    }
    private void Update() 
    {
        if(enemyTarget!=null)
        {
            FindTarget();
        }    
    }
    void WaitForDestroy()
    {
        Destroy(gameObject);
    }
    void FindTarget()
    {
        Vector3 dirVec = (enemyTarget.transform.position - transform.position).normalized;
        transform.Translate(dirVec * moveSpeed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject?.GetComponent<Enemy>();
            if(enemy.hp>0)
            {
                Player player = (GameObject.FindGameObjectWithTag("Player"))?.GetComponent<Player>();
                enemy.Hit(player.damage * 0.2f,0.2f);
                GameObject obj = Instantiate(hitEffect,transform.position,Quaternion.identity);
                Destroy(gameObject,1f);
                Destroy(obj,1f);
                CancelInvoke();
            }
        }    
    }
    
}
