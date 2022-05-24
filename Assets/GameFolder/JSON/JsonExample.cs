using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonExample : MonoBehaviour
{

    private void Start()
    {
        JsonTestClass jTest1 = new JsonTestClass();
        string jsonData = JsonConvert.SerializeObject(jTest1);
    }
    public class JsonTestClass
    {
        public int i;
        public float f;
        public bool b;
        public string str;
        public int[] iArray;
        public List<int> iList = new List<int>();
        public Dictionary<string,float> fDictionary = new Dictionary<string, float>();
        public IntVector2 iVector;
        public JsonTestClass()
        {
            i = 10;
            f = 99.9f;
            b=true;
            str = "JsonTest";
            iArray = new int[]{1,1,2,2,3,3,4};
            for(int i=0; i<5; ++i)
            {
                iList.Add(2*i);
            }
            fDictionary.Add("PIE",Mathf.PI);
            fDictionary.Add("Epsilon",Mathf.Epsilon);
            iVector = new IntVector2(3,2);
        }
        public void Print()
        {
            Debug.Log(i+" "+f+" "+b+" "+str);
        }
        public class IntVector2
        {
            public int x;
            public int y;
            public IntVector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
