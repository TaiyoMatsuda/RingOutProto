using UnityEngine;
using System.Collections;
using StarterAssets;

public class SearchFirstPlayer : MonoBehaviour
{

    private SecondPlayerController _controller;

    void Start()
    {
        _controller = GetComponentInParent<SecondPlayerController>();
    }

    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //　敵キャラクターの状態を取得
            SecondPlayerController.SecondPlayerState state = _controller.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state != SecondPlayerController.SecondPlayerState.Chase)
            {
                Debug.Log("プレイヤー発見");
                _controller.SetState(SecondPlayerController.SecondPlayerState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("見失う");
            _controller.SetState(SecondPlayerController.SecondPlayerState.Wait);
        }
    }
}