using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum EntityState{
        Idle,
        Moving,
        Attack,
        Attack2,
        Attack3,
        Attack4,
        Jump,
        Hit,
        Daze,
        Dead
    }
    public EntityState State;
}
