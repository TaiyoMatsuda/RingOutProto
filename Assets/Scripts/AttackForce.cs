using StarterAssets;
using UniRx;
using UnityEngine;
using static IMortality;

public class AttackForce : MonoBehaviour
{
    public GameObject _me;
    public float _forceHeight = 0;       //êÅÇ´îÚÇŒÇ∑çÇÇ≥í≤êÆíl
    public float _forcePower = 0;        //êÅÇ´îÚÇŒÇ∑ã≠Ç≥í≤êÆíl

    public IReactiveCollection<SecondPlayerController> Players => _players;
    private readonly ReactiveCollection<SecondPlayerController> _players = new ReactiveCollection<SecondPlayerController>();


    private void OnTriggerEnter(Collider other)
    {
        //é©ï™ÇÃëÃÇÕèúäO
        if (_me && _me == other.gameObject)
        {
            return;
        }

        if (other.gameObject.tag != "Enemy")
        {
            return;
        }

        var target = other.GetComponent<IMortality>();
        if (target is null)
        {
            return;
        }

        Vector3 toVec = GetAngleVec(_me, other.gameObject);
        toVec = toVec + new Vector3(0, _forceHeight, 0);

        Animator animator = _me.GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftPunch"))
        {
            if (target.GetState() == State.Block)
            {
                target.AddDamage(20, toVec);
            }
            else
            {
                target.AddDamage(50, toVec);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightPunch"))
        {
            if (target.GetState() == State.Block)
            {
                target.AddDamage(35, toVec);
            }
            else
            {
                target.AddDamage(100, toVec);
            }
        }
    }

    Vector3 GetAngleVec(GameObject _from, GameObject _to)
    {
        Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
        Vector3 toVec = new Vector3(_to.transform.position.x, 0, _to.transform.position.z);

        return Vector3.Normalize(toVec - fromVec);
    }
}
