using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMoveCommand : MoveCommand
{
    Player player;
    public NormalMoveCommand(Player player)
    {
        this.player = player;
    }
    public override void Execute()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(hor,0,ver);
        float curSpeed;
        float absHor = Mathf.Abs(hor);
        float absVer = Mathf.Abs(ver);
        curSpeed = absHor > absVer ?   absHor :  absVer;
        
        player.anim.SetFloat("MoveSpeed",curSpeed);
        player.characterController.Move(dir * player.moveSpeed * Time.deltaTime);

        // if(!player.characterController.isGrounded)
        // {
        //     player.gameObject.transform.position = new Vector3(player.transform.position.x,0,player.transform.position.z);
        // }

        if(dir == Vector3.zero)return;
        player.transform.rotation = Quaternion.LookRotation(dir);
       // player.transform.rotation = Quaternion.Slerp(,);
    }



}