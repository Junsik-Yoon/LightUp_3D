using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using Newtonsoft.Json;

public class JsonUtilityExample : MonoBehaviour
{
    private void Start()
    {
       // JsonExample.JsonTestClass jTest1 = new JsonExample.JsonTestClass();
        //string jsonData = JsonUtility.ToJson(jTest1);
        //jsonutility는 기본 데이터타입과 배열, 리스트만 지원 ->딕셔너리,직접생성한 클래스 불가



    //writeJson
        FileStream stream = new FileStream(Application.dataPath + "/GameFolder/JSON/test.json",FileMode.OpenOrCreate);
        JsonExample.JsonTestClass jTest1 = new JsonExample.JsonTestClass();
        string jsonData = JsonConvert.SerializeObject(jTest1);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data,0,data.Length); 
        stream.Close();


    //loadJson
        FileStream stream2 = new FileStream(Application.dataPath+"//GameFolder/JSON/test.json",FileMode.Open);
        byte[] data2 = new byte[stream2.Length];
        stream2.Read(data2,0,data2.Length);
        stream2.Close();
        string jsonData2 = Encoding.UTF8.GetString(data2);
        JsonExample.JsonTestClass jTest2 = JsonConvert.DeserializeObject<JsonExample.JsonTestClass>(jsonData2);
        jTest2.Print();
    }
    
}
