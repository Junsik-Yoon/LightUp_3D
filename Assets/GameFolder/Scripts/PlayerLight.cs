using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerLight : MonoBehaviour
{
    public event UnityAction OnChangeLight;
    public Light playerLight;
    bool isDead=false;
    private float _lightPower;
    public float lightPower
    {
        get
        {
            return _lightPower;
        }
        set
        {    
            _lightPower = value;
            if(_lightPower<=1.1f&&!isDead)
            {
                isDead=true;
                BattleStageManager.instance.PlayerDeadAndReRoll();
            }
            OnChangeLight?.Invoke();
        }
    }
    public float lightRunoutSpeed=1f;
    private void Awake()
    {   
        playerLight = GetComponent<Light>();
    }
    private void Update()
    {
        Dim();
    }
    public void Dim()
    {
        if(playerLight.spotAngle<=0.1f) return;
        playerLight.spotAngle -= lightRunoutSpeed*Time.deltaTime;
        lightPower = playerLight.spotAngle;
    }
    public void LightUp(float lightChunk)
    {
        playerLight.spotAngle += lightChunk;
        lightPower = playerLight.spotAngle;
    }
}
