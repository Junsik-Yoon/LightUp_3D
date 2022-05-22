using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    [Header("Exits")]
    public GameObject rightDoor;
    public bool rightOpen=false;
    public GameObject leftDoor;
    public bool leftOpen=false;
    public GameObject upDoor;
    public bool upOpen=false;
    public GameObject downDoor;
    public bool downOpen=false;
    [Header("Check")]
    public RoomChecker up;
    public RoomChecker down;
    public RoomChecker left;
    public RoomChecker right;
    public bool isStartRoom=false;
    public bool isBossRoom=false;
    public bool isShopRoom=false;
    public bool isItemRoom=false;
    public bool isHiddenRoom=false;
    [Header("Index")]
    public int x;
    public int y;
    [Header("prefab")]
    public GameObject prefPortal;
    public GameObject shopGuyInBattle;
    [Header("Light")]
    public Light roomLight;
    //public GameObject roomCheck;
    public void CheckRooms(List<GameObject> rooms)
    {
        CheckUp(rooms);
        CheckDown(rooms);
        CheckRight(rooms);
        CheckLeft(rooms);
    }

    public void SetStage(string stageType)
    {
        switch(stageType)
        {
            case "Boss":
            {

                
                roomLight.color = Color.gray;
            }break;
            case "Start":
            {
                //주인공 나오게 하고 세팅하기
                
                //roomLight.color=Color.magenta;
                roomLight.gameObject.SetActive(false);
            }break;
            case "Shop":
            {
                //Vector3 curPos = new Vector3(transform.position.x,gameObject.transform.position.y+0.1f,transform.position.z);
                GameObject obj = Instantiate(shopGuyInBattle,transform.position,Quaternion.Euler(0f,180f,0f));
                roomLight.color=Color.green;
            }break;
            case "Item":
            {
                //임시로 상인 해놓음
                GameObject obj = Instantiate(shopGuyInBattle,transform.position,Quaternion.identity);
                roomLight.color=Color.blue;
            }break;
            case "Hidden":
            {
                //좋은 보상 혹은 싸울 필요 없는 귀찮은 준보스급 적
                //좌표계산해서 히든방으로 들어갈 수 있는 포탈 만들기
                //임시로 상인 
                GameObject obj = Instantiate(shopGuyInBattle,transform.position,Quaternion.Euler(270f,90f,0f));
                roomLight.color=Color.red;
            }break;
            default:
            {
                Debug.Log("잘못된 스테이지값 입력");
            }break;
        }
    }
    public void SetPortal()
    {
        Vector3 curPos = new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z);
        GameObject obj = Instantiate(prefPortal,curPos,Quaternion.identity);
        obj.GetComponent<Portal>().SetDestination("Stage"); //->보스방으로 지정하기
    }
    public void SetDoor()
    {
        if(rightOpen) rightDoor.SetActive(false);
        if(leftOpen) leftDoor.SetActive(false);
        if(upOpen) upDoor.SetActive(false);
        if(downOpen) downDoor.SetActive(false);
    }
    public void SetDoor(int time)
    {

    }
    public bool CheckRight(List<GameObject> rooms)
    {
        for(int i=0; i<rooms.Count;++i)
        {
            RoomChecker roomChecker = rooms[i].GetComponent<RoomChecker>();
            if(roomChecker.x == x+1 && roomChecker.y == y)
            {
                //rightDoor.SetActive(false);
                rightOpen=true;
                return true;
            }
        }
        return false;
    }
    public bool CheckLeft(List<GameObject> rooms)
    {
        for(int i=0; i<rooms.Count;++i)
        {
            RoomChecker roomChecker = rooms[i].GetComponent<RoomChecker>();
            if(roomChecker.x == x-1 && roomChecker.y == y)
            {
                //leftDoor.SetActive(false);
                leftOpen=true;
                return true;
            }
        }
        return false;
    }
    public bool CheckUp(List<GameObject> rooms)
    {
        for(int i=0; i<rooms.Count;++i)
        {
            RoomChecker roomChecker = rooms[i].GetComponent<RoomChecker>();
            if(roomChecker.x == x && roomChecker.y == y+1)
            {
                //upDoor.SetActive(false);
                upOpen=true;
                return true;
            }
        }
        return false;
    }
    public bool CheckDown(List<GameObject> rooms)
    {
        for(int i=0; i<rooms.Count;++i)
        {
            RoomChecker roomChecker = rooms[i].GetComponent<RoomChecker>();
            if(roomChecker.x == x && roomChecker.y == y-1)
            {
                //downDoor.SetActive(false);
                downOpen=true;
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {

            GetComponentInChildren<EnemyGenerator>().enabled=true;
        }
    }
}
