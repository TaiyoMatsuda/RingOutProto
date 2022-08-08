using UnityEngine;
using System.Collections;
using StarterAssets;

public class SearchCharacter : MonoBehaviour
{

    private EnemyController moveEnemy;

    void Start()
    {
        moveEnemy = GetComponentInParent<EnemyController>();
    }

    void OnTriggerStay(Collider col)
    {
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            //�@�G�L�����N�^�[�̏�Ԃ��擾
            EnemyController.EnemyState state = moveEnemy.GetState();
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
            if (state != EnemyController.EnemyState.Chase)
            {
                Debug.Log("�v���C���[����");
                moveEnemy.SetState(EnemyController.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            moveEnemy.SetState(EnemyController.EnemyState.Wait);
        }
    }
}