using UnityEngine;
using System.Collections;
using StarterAssets;

public class SearchCharacter : MonoBehaviour
{

    private EnemyController moveEnemy;

    void Start()
    {
        moveEnemy = GetComponentInParent<EnemyController>();
    }

    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //　敵キャラクターの状態を取得
            EnemyController.EnemyState state = moveEnemy.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state != EnemyController.EnemyState.Chase)
            {
                Debug.Log("プレイヤー発見");
                moveEnemy.SetState(EnemyController.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("見失う");
            moveEnemy.SetState(EnemyController.EnemyState.Wait);
        }
    }
}