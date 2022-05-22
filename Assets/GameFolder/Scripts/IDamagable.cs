using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void Hit(float damage,float knockBackRange);
}
