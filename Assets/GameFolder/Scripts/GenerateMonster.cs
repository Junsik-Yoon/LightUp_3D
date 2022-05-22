using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMonster : MonoBehaviour
{
    public Transform[] spots;
    public GameObject tutorialMonter;
    private void OnEnable()
    {
        GenMonster();
    }
    public void GenMonster()
    {
        Instantiate(tutorialMonter,spots[Random.Range(0,4)].position,Quaternion.identity);
    }
}
