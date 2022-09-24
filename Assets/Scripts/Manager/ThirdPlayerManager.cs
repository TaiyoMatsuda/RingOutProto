using Cysharp.Threading.Tasks;
using StarterAssets;
using UniRx;
using UnityEngine;

public class ThirdPlayerManager : MonoBehaviour
{
    [SerializeField]
    private ThirdPlayerController _thirdPlayerPrefab;
    [SerializeField]
    private ThirdPlayerPresenter _presenter;

    private void Start()
    {
        CreatePlayer();
    }

    private void OnPlayerDamage(int damage)
    {
        _presenter.OnPlayerDamage(damage);
    }

    public void OnPlayerDead()
    {
        _presenter.OnPlayerDead();

        if (_presenter.StockNum <= 1)
        {
            return;
        }
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        var currentPlayer = Instantiate(_thirdPlayerPrefab);
        currentPlayer.DamageSum.Subscribe(damage => OnPlayerDamage(damage)).AddTo(this);
        currentPlayer.PlayerDeadAsync.Subscribe(_ => OnPlayerDead()).AddTo(this);
    }
}
