using Cysharp.Threading.Tasks;
using StarterAssets;
using UniRx;
using UnityEngine;
using System.Linq;

public class SecondPlayerStockPresenter : MonoBehaviour
{
    [SerializeField]
    private SecondPlayerController _secondPlayerPrefab;

    [SerializeField]
    private GameObject _stock;

    public void OnPlayerDead()
    {
        var childGameObjects = _stock.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Last();
        Destroy(childGameObjects);
    }

    //private void Start()
    //{
    //    //_secondPlayerPrefab
    //    //    .PlayerDeadAsync
    //    //    .Subscribe(_ => OnPlayerDead())
    //    //    .AddTo(this);
    //    _secondPlayerPrefab.PlayerDeadAsync
    //            .Subscribe(_ =>
    //            {
    //                var childGameObjects = _stock.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Last();
    //                Destroy(childGameObjects);
    //            }).AddTo(this);
    //}
}
