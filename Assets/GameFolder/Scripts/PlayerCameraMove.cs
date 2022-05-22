using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        MoveCamera();
    }
    public void MoveCamera()
    {
        Vector3 adjustPos = new Vector3(player.position.x,player.position.y+10f,player.position.z-10f);
        transform.position = adjustPos;
    }
}
