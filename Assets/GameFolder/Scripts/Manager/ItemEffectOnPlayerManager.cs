using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemEffectOnPlayerManager : MonoBehaviour
{
    public event UnityAction OnNormalAttack;
    private bool _isNormalAttack;
    public bool isNormalAttack {get{return _isNormalAttack;}set{OnNormalAttack?.Invoke();_isNormalAttack=value;}}
    public static ItemEffectOnPlayerManager instance {get; private set;}
    private void Awake()
    {
        instance = this;
    }
    
    Player player;
    private void Start()
    {
        OnNormalAttack+=Shuriken;
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        player = obj?.GetComponent<Player>();
    }
    [Header("Shuriken")]
    public bool isEquipShuriken=false;
    public GameObject prefShuriken;
   // float shurikenDelay=0.2f;

    void Shuriken()
    {
        if(!isEquipShuriken)return;

        Vector3 fixedPos = new Vector3(player.transform.position.x,player.transform.position.y+1f,player.transform.position.z);
        GameObject obj = Instantiate(prefShuriken,fixedPos, Quaternion.identity);
        Vector3 dirVec = player.transform.forward;//(player.hitCollider.position - player.transform.position).normalized;
        obj.GetComponent<Shuriken>().dirVec = dirVec;
        

    }
}
