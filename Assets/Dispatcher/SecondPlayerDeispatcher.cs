using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerDeispatcher : MonoBehaviour
{
    // Model��񋟂���Manager
    [SerializeField] private SecondPlayerManager _playerManager;

    // Player��Presenter
    [SerializeField] private SecondPlayerStockPresenter _presenter;

    // View��Prefab
    //[SerializeField] private PlayerView _viewPrefab;

    private void Start()
    {
        // �����X�g�ɂ�����Dispatch
        //foreach (var p in _playerManager.Players)
        //{
        //    Dispatch(p);
        //}

        //// �ȍ~�V�K�쐬���ꂽ���̂�Dispatch
        //_playerManager.Players.ObserveAdd().Subscribe(x => Dispatch(x.Value)).AddTo(this);
    }

    private void Dispatch(SecondPlayerController player)
    {
        //// Player�̎q�v�f�Ƃ���View���쐬
        //var view = Instantiate(_viewPrefab, player.transform, true);

        //// �ʒu�𒲐�
        //view.transform.localPosition = Vector3.up * 1.5f;

        // Presenter�ɑg�ݍ��킹�Ēʒm
        //_presenter.OnCreatePlayer(player, view);
    }
}
