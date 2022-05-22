using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    public enum DestructType
    {
        DESTROY,
        UNACTIVE,
    }
    public DestructType destructType;
    private void Awake()
    {
        if(destructType == DestructType.UNACTIVE)
        {
            StartCoroutine(UnActiveSelf());
        }
        else if(destructType == DestructType.DESTROY)
        {
            StartCoroutine(DestructSelf());
        }
    }
    IEnumerator DestructSelf()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    IEnumerator UnActiveSelf()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
