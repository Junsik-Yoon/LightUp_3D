using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    
    
    public static SceneLoadManager instance {get; private set;}
    private void Awake()
    {
        instance = this;
    }

    
}
