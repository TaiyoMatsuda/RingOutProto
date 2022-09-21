using StarterAssets;
using UnityEngine;
using static StarterAssets.EnemyController;
using static StarterAssets.SecondPlayerController;
using UniRx;

public class AttackForce : MonoBehaviour
{
    public GameObject _me;
    //public string _earthTag;
    public float _forceHeight = 0;       //êÅÇ´îÚÇŒÇ∑çÇÇ≥í≤êÆíl
    public float _forcePower = 0;        //êÅÇ´îÚÇŒÇ∑ã≠Ç≥í≤êÆíl

    public IReactiveCollection<SecondPlayerController> Players => _players;
    private readonly ReactiveCollection<SecondPlayerController> _players = new ReactiveCollection<SecondPlayerController>();


    private void OnTriggerEnter(Collider other)
    {
        //é©ï™ÇÃëÃÇÕèúäO
        //if (_me && _me == other.gameObject)
        //{
        //    return;
        //}

        if (other.gameObject.tag != "Enemy")
        {
            return;
        }

        SecondPlayerController enemyController = other.GetComponent<SecondPlayerController>();
        if (!enemyController)
        {
            return;
        }

        Vector3 toVec = GetAngleVec(_me, other.gameObject);
        toVec = toVec + new Vector3(0, _forceHeight, 0);

        Animator animator = _me.GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftPunch"))
        {
            if (enemyController.GetState() == SecondPlayerState.Block)
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
            if (enemyController.GetState() == SecondPlayerState.Block)
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
