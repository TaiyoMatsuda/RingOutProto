using UnityEngine;
using System.Collections;
using StarterAssets;

public class SearchCharacter : MonoBehaviour
{

    private EnemyController _controller;

    void Start()
    {
        _controller = GetComponentInParent<EnemyController>();
    }

    void OnTriggerStay(Collider col)
    {
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            //�@�G�L�����N�^�[�̏�Ԃ��擾
            EnemyController.EnemyState state = _controller.GetState();
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
            if (state != EnemyController.EnemyState.Chase)
            {
                Debug.Log("�v���C���[����");
                _controller.SetState(EnemyController.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            _controller.SetState(EnemyController.EnemyState.Wait);
        }
    }
}