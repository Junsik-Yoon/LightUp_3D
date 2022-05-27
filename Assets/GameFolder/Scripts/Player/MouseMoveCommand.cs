
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMoveCommand : MoveCommand
{
    Player player;
    Vector3 prevPos;
    public MouseMoveCommand(Player player)
    {
        this.player = player;
    }
    public override void Execute()
    {
           
        if ( Input.GetMouseButtonDown(0) )
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            // player.navMeshAgent.isStopped=false;
            // player.navMeshAgent.updatePosition = true;
            // player.navMeshAgent.updateRotation = true;
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if ( Physics.Raycast(ray, out hit, Mathf.Infinity,LayerMask.GetMask("Ground")) )
            {
                Vector3 direction = hit.transform.position - Camera.main.transform.position;
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                player.navMeshAgent.SetDestination(hit.point);

                Vector3 lookrotation = player.navMeshAgent.steeringTarget-player.transform.position;
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation,Quaternion.LookRotation(lookrotation), 5*Time.deltaTime); 
                //Vector3 look  = hit.point - player.gameObject.transform.position;
                //player.gameObject.transform.LookAt(look);
            }
            GameObject.Instantiate(GameManager.instance.mouseEffect,new UnityEngine.Vector3(hit.point.x,hit.point.y+0.1f,hit.point.z),Quaternion.identity);
        }
        if(player.navMeshAgent.velocity == Vector3.zero)
        {
            player.anim.SetFloat("MoveSpeed",0f);
        }
        else
        {
            
            CheckEvent();
            player.anim.SetFloat("MoveSpeed",player.navMeshAgent.velocity.magnitude);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            //player.navMeshAgent.SetDestination(player.transform.position);
            // player.navMeshAgent.velocity = Vector3.zero;
            // player.navMeshAgent.isStopped=true;
            // player.navMeshAgent.updatePosition = false;
            // player.navMeshAgent.updateRotation = false;
            // player.anim.SetFloat("MoveSpeed",0f);
        }
        // if(prevPos == player.transform.position)
        // {
        //     player.anim.SetFloat("MoveSpeed",0f);
        // }
        // prevPos = player.transform.position;
    }
    private void CheckEvent()
    {
        if(!GameManager.instance.isEventChecked)
        {
            //IEnumerator dd = GameManager.instance.WaitForNextEvent();

            Collider[] colls =Physics.OverlapSphere(player.hitCollider.position,player.hitRadius,LayerMask.GetMask("Event"));
            if(colls.Length>0)
            {
                //if(colls[0].gameObject.layer == LayerMask.GetMask("Event"))
                //{
                    Debug.Log("이벤트발생");
                    colls[0].gameObject.GetComponent<Iinteractable>().Interact();

                    GameManager.instance.isEventChecked = true;
                    GameManager.instance.StartCoroutine(GameManager.instance.WaitForNextEvent());
                    //colls[0].gameObject.PlayEvent();
                //}

            }
        }
    }
}
