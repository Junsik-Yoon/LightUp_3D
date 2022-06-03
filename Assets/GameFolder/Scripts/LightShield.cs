using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShield : MonoBehaviour
{
    public GameObject lightShieldEffect;
    public Transform capsuleTop;
    public Transform capsuleBottom;
    
    float rad = 2f;

    private void Start()
    {
        StartCoroutine(hit());
    }
    private void Update()
    {
        hit();
    }

    IEnumerator hit()
    {
        while(true)
        {
            yield return new WaitForSeconds(4f);
            Collider[] colls = Physics.OverlapCapsule(capsuleTop.position,capsuleBottom.position,rad,LayerMask.GetMask("Enemy"));
            if(colls.Length>0)
            {
                for(int i=0; i<colls.Length; ++i)
                {
                    Vector3 pos = colls[i].gameObject.transform.position;
                    Vector3 fixedPos = new Vector3(pos.x,pos.y+1f,pos.z);
                    GameObject effectObj = Instantiate(lightShieldEffect,fixedPos,Quaternion.Euler(0,0,0));
                    Destroy(effectObj , 1f);
                    colls[i].gameObject.GetComponent<Enemy>().Hit(3f,0f);
                }
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,rad);
    }
}
