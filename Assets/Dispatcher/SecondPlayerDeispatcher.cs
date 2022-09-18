using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerDeispatcher : MonoBehaviour
{
    // Modelを提供するManager
    [SerializeField] private SecondPlayerManager _playerManager;

    // PlayerのPresenter
    [SerializeField] private SecondPlayerStockPresenter _presenter;

    // ViewのPrefab
    //[SerializeField] private PlayerView _viewPrefab;

    private void Start()
    {
        // 今リストにあるやつをDispatch
        //foreach (var p in _playerManager.Players)
        //{
        //    Dispatch(p);
        //}

        //// 以降新規作成されたものをDispatch
        //_playerManager.Players.ObserveAdd().Subscribe(x => Dispatch(x.Value)).AddTo(this);
    }

    private void Dispatch(SecondPlayerController player)
    {
        //// Playerの子要素としてViewを作成
        //var view = Instantiate(_viewPrefab, player.transform, true);

        //// 位置を調整
        //view.transform.localPosition = Vector3.up * 1.5f;

        // Presenterに組み合わせて通知
        //_presenter.OnCreatePlayer(player, view);
    }
}
