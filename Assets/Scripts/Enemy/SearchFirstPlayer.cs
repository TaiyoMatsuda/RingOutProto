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
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            //�@�G�L�����N�^�[�̏�Ԃ��擾
            SecondPlayerController.SecondPlayerState state = _controller.GetState();
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
            if (state != SecondPlayerController.SecondPlayerState.Chase)
            {
                Debug.Log("�v���C���[����");
                _controller.SetState(SecondPlayerController.SecondPlayerState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            _controller.SetState(SecondPlayerController.SecondPlayerState.Wait);
        }
    }
}