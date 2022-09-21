using UnityEngine;

public class SecondPlayerPresenter : MonoBehaviour
{
    [SerializeField]
    private SecondPlayerView _secondPlayerView;

    public void OnPlayerDead()
    {
        _secondPlayerView.DeleteStock();
    }

    public void OnPlayerDamage(int damage)
    {
        _secondPlayerView.SetDamage(damage);
    }
}
