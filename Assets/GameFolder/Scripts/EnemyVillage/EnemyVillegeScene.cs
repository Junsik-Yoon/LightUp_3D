using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVillegeScene : MonoBehaviour
{
    public Camera cam;
    private void Start()
    {
        StartCoroutine(SceneSetUp());
    }
    IEnumerator SceneSetUp()
    {
        yield return new WaitForSeconds(1f);
        for(int i=0; i<300; ++i)
        {
        
            yield return new WaitForEndOfFrame();
            cam.transform.position += cam.transform.forward * -0.1f ;
        }

      
    }
}
