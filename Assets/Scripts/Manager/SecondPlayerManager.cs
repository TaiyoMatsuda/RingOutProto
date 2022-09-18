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
    private SecondPlayerStockPresenter _presenter;
    private SecondPlayerController _currentPlayer;

    private void Start()
    {
        CreatePlayer();
    }

    public void OnPlayerDead()
    {
        //_currentPlayer = null;
        _presenter.OnPlayerDead();

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        _currentPlayer = Instantiate(_secondPlayerPrefab);

        _currentPlayer
            .PlayerDeadAsync
            .Subscribe(_ => OnPlayerDead())
            .AddTo(this);
    }
}
