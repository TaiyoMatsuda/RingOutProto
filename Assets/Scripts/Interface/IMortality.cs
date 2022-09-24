using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortality
{
    public enum State
    {
        Walk,
        Wait,
        Chase,
        Block
    };

    public void AddDamage(int damage, Vector3 damageVec);
    public State GetState();

    public void SetState(State tempState, Transform targetObj = null);
}
