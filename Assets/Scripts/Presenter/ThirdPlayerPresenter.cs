using UnityEngine;

public class ThirdPlayerPresenter : MonoBehaviour
{
    [SerializeField]
    private ThirdPlayerView _secondPlayerView;
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
