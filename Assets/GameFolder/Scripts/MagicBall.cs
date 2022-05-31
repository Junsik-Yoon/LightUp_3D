using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public bool isMovable=false;
    public float damage;
    public Vector3 dir;
    float moveSpeed = 10f;

    private void Start()
    {
        Invoke("DestroySelf",5f);
    }
    void Update()
    {
        if(isMovable)Move(dir);

    }
    public void Move(Vector3 dirVec)
    {
        transform.Translate(dirVec*moveSpeed*Time.deltaTime ,Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Damaged(damage);
            player.Stunned(transform.position,0.1f,1f);
            Debug.Log("매직볼에 맞음");
            CancelInvoke();
            Destroy(gameObject,0.5f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,2f);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
