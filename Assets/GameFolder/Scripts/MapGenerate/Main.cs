
using System.Security.AccessControl;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    [Header("Others")]
    List<GameObject> rooms;//인스턴스 되는 배틀스테이지 실체의 리스트
    public GameObject prefBattleGround;//인스턴스 되는 배틀스테이지 실체
    List<List<Node>> paths;//a스타 길 3개 담는 리스트
    List<Node> knots;//ends

    Grid grid;
    public Node start, end;

    int prevX , prevY;

    public bool finding;//찾는중 변수

    private void Awake()
    {
        rooms = new List<GameObject>();
        paths = new List<List<Node>>();
        knots = new List<Node>();
        grid = GetComponent<Grid>();

        StartGrid();
        StartFinding(true);

        ///코루틴없이 바로
        for(int i=0; i<2; ++i)
        {
            Setting();
            StartFinding(true);
        }
        ColorPath();
        MakeBattleStage();

        ///코루틴써서 절차적으로 보이게 -> startfinding에서도 코루틴으로 바꿔야함
        //StartCoroutine(Wait());
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        //StartGrid();
     Setting();
        StartFinding(true);

        yield return new WaitForSeconds(0.5f);
        //StartGrid();
     Setting();
        StartFinding(true);

        yield return new WaitForSeconds(0.5f);
        
        ColorPath();

        yield return new WaitForSeconds(0.5f);

        MakeBattleStage();
    }
    public void MakeBattleStage()
    {
        //GameObject [,] arrayObj = new GameObject[8,8];
        for(int i=0; i<paths.Count; ++i)
        {
            for( int j=0; j< paths[i].Count;++j)
            {
                int x = paths[i][j].gridX;
                int y = paths[i][j].gridY;
                GameObject obj = Instantiate(prefBattleGround,new Vector3(x*50,0,y*50),Quaternion.identity);
                if(paths[i][j].isBossNode==true)
                {
                    obj.GetComponent<RoomChecker>().isBossRoom = true;
                    obj.GetComponent<RoomChecker>().SetStage("Boss");
                }
                obj.GetComponent<RoomChecker>().x=x;
                obj.GetComponent<RoomChecker>().y=y;
                //arrayObj[i,j] = obj;
                rooms.Add(obj);
            }
            
        }
        GameObject start = Instantiate(prefBattleGround,new Vector3(0,0,0),Quaternion.identity);
        start.GetComponent<RoomChecker>().isStartRoom=true;
        start.GetComponent<RoomChecker>().SetStage("Start");
        rooms.Add(start);
        
//         List<int> tempIndexList = new List<int>();
//         //위치 같은 방 지우기
//         for(int i=0; i<rooms.Count; ++i)
//         {
//             for(int j=i+1; j<rooms.Count; ++j)
//             {
//                 if(rooms[i].transform.position == rooms[j].transform.position)
//                 {
// //                    Debug.Log(rooms[i].transform.position);
//                     //보스노드 예외처리
//                     if(!rooms[i].GetComponent<RoomChecker>().isBossRoom)
//                     {
//                         Destroy(rooms[i]);
//                         tempIndexList.Add(i);
//                     }
//                 }
//             }
//         }
        // for(int i=0; i<tempIndexList.Count;++i)
        // {
        //     int num = tempIndexList[i];
        //     rooms.RemoveAt(num);
        // }
        

        RoomChecker rcker; 
        while(true)
        {
            rcker = rooms[Random.Range(0,rooms.Count)].GetComponent<RoomChecker>();
            if(!rcker.isBossRoom && !rcker.isStartRoom)
            {
                rcker.isShopRoom=true;
                rcker.SetStage("Shop");
                break;
            }
        }
        while(true)
        {
            rcker = rooms[Random.Range(0,rooms.Count)].GetComponent<RoomChecker>();
            if(!rcker.isBossRoom && !rcker.isStartRoom && !rcker.isShopRoom)
            {
                rcker.isItemRoom=true;
                rcker.SetStage("Item");
                break;
            }
        }
        //룸 개수가 너무 적게 뽑히면 몇 개 더 생성하거나 아얘 처음부터 하는 방법 찾아보기
        //방과 방 잇기
        //보스방 배치
        //상점 , 아이템인터랙트 방 랜덤으로 배치
        //스타트,엔드,상점 모든 방 포함해서 룸 하나 골라서 사방을 레이캐스트로 쏴서 방이 없으면 시크릿 룸 하나 생성
        Debug.Log("BattleRoomsCount: "+rooms.Count);
        for(int i=0; i<rooms.Count; ++i)
        {
            rooms[i].GetComponent<RoomChecker>().CheckRooms(rooms); 
//            Debug.Log(rooms[i].GetComponent<RoomChecker>().x + ","+rooms[i].GetComponent<RoomChecker>().y);
        }

        //히든 룸 생성
        while(true)
        {
            rcker = rooms[Random.Range(0,rooms.Count)].GetComponent<RoomChecker>();
            //if(!rcker.isBossRoom && !rcker.isStartRoom && !rcker.isShopRoom && !rcker.isItemRoom)
           // {
                
            //    rcker.isHiddenRoom=true;
            //    rcker.SetStage("Hidden");
            //    break;
            //}
            Vector3 objPos = rcker.transform.position;
            GameObject obj;
            bool isOk=false;
            if(!rcker.CheckDown(rooms))
            {
                isOk=true;
                objPos = new Vector3(objPos.x,0,objPos.z-50f);
                for(int i=0; i<rooms.Count;++i)
                {
                    if(objPos == rooms[i].transform.position)
                    isOk=false;
                }

                if(isOk)
                {
                    //rcker.hasHiddenRoom=true;
                    obj = Instantiate(prefBattleGround,objPos,Quaternion.identity);
                    obj.GetComponent<RoomChecker>().isHiddenRoom=true;
                    obj.GetComponent<RoomChecker>().upOpen=true;
                    obj.GetComponent<RoomChecker>().SetStage("Hidden"); 
                    rcker.downDoor.GetComponent<MeshCollider>().enabled=false;
                    Debug.Log("did down");
                // obj.GetComponent<RoomChecker>().  
                    rooms.Add(obj);
                break;
                }
            }
            else if(!rcker.CheckLeft(rooms))
            {
                isOk=true;
                objPos = new Vector3(objPos.x-50f,0,objPos.z);
                for(int i=0; i<rooms.Count;++i)
                {
                    if(objPos == rooms[i].transform.position)
                    isOk=false;
                }

                if(isOk)
                {
                    //rcker.hasHiddenRoom=true;
                    obj = Instantiate(prefBattleGround,objPos,Quaternion.identity);
                    obj.GetComponent<RoomChecker>().isHiddenRoom=true;
                    obj.GetComponent<RoomChecker>().rightOpen=true;
                    obj.GetComponent<RoomChecker>().SetStage("Hidden"); 
                    rcker.leftDoor.GetComponent<MeshCollider>().enabled=false;
                    Debug.Log("did left");
                // obj.GetComponent<RoomChecker>().  
                    rooms.Add(obj);
                    break;
                }

            }
            else if(!rcker.CheckRight(rooms))
            {
                isOk=true;
                objPos = new Vector3(objPos.x+50f,0,objPos.z);
                for(int i=0; i<rooms.Count;++i)
                {
                    if(objPos == rooms[i].transform.position)
                    isOk=false;
                }

                if(isOk)
                {
                    
                    obj = Instantiate(prefBattleGround,objPos,Quaternion.identity);
                    obj.GetComponent<RoomChecker>().isHiddenRoom=true;
                    obj.GetComponent<RoomChecker>().leftOpen=true;
                    obj.GetComponent<RoomChecker>().SetStage("Hidden");
                    rcker.rightDoor.GetComponent<MeshCollider>().enabled=false; 
                    Debug.Log("did right");
                // obj.GetComponent<RoomChecker>().  
                    rooms.Add(obj);
                    break;
                }
            }
            else if(!rcker.CheckUp(rooms))
            {
                isOk=true;
                objPos = new Vector3(objPos.x,0,objPos.z+50f);
                for(int i=0; i<rooms.Count;++i)
                {
                    if(objPos == rooms[i].transform.position)
                    isOk=false;
                }
                if(isOk)
                {
                    //rcker.hasHiddenRoom=true;
                    obj = Instantiate(prefBattleGround,objPos,Quaternion.identity);
                    obj.GetComponent<RoomChecker>().isHiddenRoom=true;
                    obj.GetComponent<RoomChecker>().downOpen=true;
                    obj.GetComponent<RoomChecker>().SetStage("Hidden"); 
                    rcker.upDoor.GetComponent<MeshCollider>().enabled=false;
                    Debug.Log("did up");
                // obj.GetComponent<RoomChecker>().  
                    rooms.Add(obj);
                    break;
                }
            }



        }Debug.Log("히든룸생성 후 : "+rooms.Count);

        start.GetComponentInChildren<EnemyGenerator>().enabled=true;


        //위치 같은 방 지우기        
        List<int> tempIndexList = new List<int>();

        for(int i=0; i<rooms.Count; ++i)
        {
            for(int j=i+1; j<rooms.Count; ++j)
            {
                if(rooms[i].transform.position == rooms[j].transform.position)
                {
//                    Debug.Log(rooms[i].transform.position);
                    //특수노드 예외처리
                    RoomChecker check = rooms[i].GetComponent<RoomChecker>();
                    if(!check.isBossRoom ||
                        !check.isShopRoom||
                        !check.isItemRoom)
                    {
                        Destroy(rooms[i]);
                        tempIndexList.Add(i);
                    }
                }
            }
        }
        for(int i=0; i<tempIndexList.Count;++i)
        {
            int num = tempIndexList[i];
            rooms.RemoveAt(num);
        }



        Destroy(grid.parentGrid);//그리드 삭제
        
    }
    public void StartGrid()
    {
        StopCoroutine("FindPath");
        finding = false;

        bool success = grid.CreateGrid();

        if (success)
        {
            Setting();
        }
       // Debug.Log("된거아님?");
    }

    public void StartFinding(bool search)
    {
        StopCoroutine("FindPath");
        finding = false;
        //grid.ResetGrid();
        //if(search) StartCoroutine("FindPath");    
        FuncFindPath();    
    }
    public void Setting()
    {
            start = grid.StartNode;
            end = grid.EndNode;
            if(knots.Count>0)
            {
                for(int i=0; i<knots.Count;++i)
                {
                    if(end.gridX == knots[i].gridX && end.gridY == knots[i].gridY)
                    {
                         end = grid.EndNode;
                    }
                }
            }

            knots.Add(end);
            Debug.Log(end.gridX+" "+end.gridY);

            start.ChangeStart = true;
            end.ChangeEnd = true;
    }

    IEnumerator FindPath()
    {
        finding = true;
        bool pathSuccess = false;        

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i<openSet.Count; i++)
            {
//                Debug.Log(currentNode.gridX + " " +currentNode.gridY);
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == end)
            {
                pathSuccess = true;
                break;
            }

            yield return new WaitUntil(() => finding);
            //yield return new WaitForSeconds(0.1f);
            yield return new WaitForEndOfFrame();

            if (currentNode != start)
                currentNode.ChangeColor = Color.Lerp(Color.cyan, Color.white, 0.2f);

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable  || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, end);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        if (neighbour.walkable && !neighbour.end)
                            neighbour.ChangeColor = Color.Lerp(Color.green, Color.white, 0.2f);
                    }
                }
            }
        }
        if (pathSuccess)
        {
            
            SavePath(start,end);
        }
        finding = false;
    }
    public void FuncFindPath()
    {
        finding = true;
        bool pathSuccess = false;        

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i<openSet.Count; i++)
            {
//                Debug.Log(currentNode.gridX + " " +currentNode.gridY);
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == end)
            {
                pathSuccess = true;
                break;
            }

            //yield return new WaitUntil(() => finding);
            //yield return new WaitForSeconds(0.1f);
            //yield return new WaitForEndOfFrame();

            if (currentNode != start)
                currentNode.ChangeColor = Color.cyan;//Color.Lerp(Color.cyan, Color.white, 0.2f);

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable  || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, end);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        if (neighbour.walkable && !neighbour.end)
                            neighbour.ChangeColor=Color.green;// = Color.Lerp(Color.green, Color.white, 0.2f);
                    }
                }
            }
        }
        if (pathSuccess)
        {
            
            SavePath(start,end);
        }
        finding = false;
    }
    public void SavePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode.ChangeColor = Color.black;
            currentNode.additionalWeight=10;
            currentNode = currentNode.parent;
        }
        paths.Add(path);
       
    }
    public void ColorPath()
    {
        //paths[0].
        for(int i=0; i<paths.Count; ++i)
        {
            for( int j=0; j< paths[i].Count;++j)
            {
                paths[i][j].ChangeColor = Color.black;
            }
        }
        Debug.Log(paths.Count);
        int maxNum = 0;
        int maxIndex=0;
        for(int i=0; i<knots.Count; ++i)
        {
            if(maxNum<knots[i].gridX+knots[i].gridY)
            {
                maxNum = knots[i].gridX+knots[i].gridY;
                maxIndex = i;
            }
        }
        knots[maxIndex].ChangeColor = Color.red;
        knots[maxIndex].isBossNode=true;

    }



    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);

    }
}
