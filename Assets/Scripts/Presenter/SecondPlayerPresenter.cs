using UnityEngine;

public class SecondPlayerPresenter : MonoBehaviour
{
    [SerializeField]
    private SecondPlayerView _secondPlayerView;
    public int StockNum{get;set;}
    public void OnPlayerDead()
    {
        _secondPlayerView.DeleteStock();
        StockNum = _secondPlayerView.StockNum;
    }

    public void OnPlayerDamage(int damage)
    {
        _secondPlayerView.SetDamage(damage);
    }
}
