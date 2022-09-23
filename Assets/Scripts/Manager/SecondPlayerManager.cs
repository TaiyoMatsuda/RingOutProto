using Cysharp.Threading.Tasks;
using StarterAssets;
using UniRx;
using UnityEngine;

public class SecondPlayerManager : MonoBehaviour
{
    public IReactiveCollection<int> SecondPlayersCount => _secondPlayersCount;

    private readonly ReactiveCollection<int> _secondPlayersCount = new ReactiveCollection<int>();

    [SerializeField]
    private SecondPlayerController _secondPlayerPrefab;
    [SerializeField] 
    private SecondPlayerPresenter _presenter;
    private SecondPlayerController _currentPlayer;

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
        //_currentPlayer = null;
        _presenter.OnPlayerDead();

        if (_presenter.StockNum <= 1)
        {
            return;
        }
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        _currentPlayer = Instantiate(_secondPlayerPrefab);

        _currentPlayer.DamageSum.Subscribe(damage => OnPlayerDamage(damage)).AddTo(this);
        _currentPlayer.PlayerDeadAsync.Subscribe(_ => OnPlayerDead()).AddTo(this);
    }
}
