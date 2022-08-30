using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StarterAssets.EnemyController;

public class AttackForce : MonoBehaviour
{
    public GameObject _me;
    //public string _earthTag;
    public float _forceHeight = 0;       //������΂����������l
    public float _forcePower = 0;        //������΂����������l

    private void OnTriggerEnter(Collider other)
    {

        ////���O�Ώۂ̃^�O�������Q�[���I�u�W�F�N�g�������牽�����Ȃ�
        //if (_earthTag == other.tag)
        //{
        //    return;
        //}
        //�����̑̂͏��O
        if (_me && _me == other.gameObject)
        {
            return;
        }

        EnemyController enemyController = other.GetComponent<EnemyController>();
        if (!enemyController)
        {
            return;
        }

        Vector3 toVec = GetAngleVec(_me, other.gameObject);
        toVec = toVec + new Vector3(0, _forceHeight, 0);

        Animator animator = _me.GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftPunch"))
        {
            if (enemyController.GetState() == EnemyState.Block)
            {
                enemyController._damage = 20;
            }
            else
            {
                enemyController._damage = 50;
            }
            enemyController._damageVec = toVec;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightPunch"))
        {
            if (enemyController.GetState() == EnemyState.Block)
            {
                enemyController._damage = 35;
            }
            else
            {
                enemyController._damage = 100;
            }
            enemyController._damageVec = toVec;
        }
    }

    Vector3 GetAngleVec(GameObject _from, GameObject _to)
    {
        Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
        Vector3 toVec = new Vector3(_to.transform.position.x, 0, _to.transform.position.z);

        return Vector3.Normalize(toVec - fromVec);
    }
}
