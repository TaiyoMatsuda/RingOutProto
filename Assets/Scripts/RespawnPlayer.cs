using Cinemachine;
using System.Collections;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
	[SerializeField] 
	GameObject player;
	[SerializeField] 
	int _residueNum;

	[SerializeField]
	private GameObject effectObject;
	[SerializeField]
	private float deleteTime;
	[SerializeField]
	private float offset;
    [SerializeField]
    private CinemachineVirtualCamera _playerFollowCamera;

    public void Respawn()
	{
		StartCoroutine("RespawnHandle");
	}

	private IEnumerator RespawnHandle()
	{
		yield return new WaitForSeconds(2.0f);

		if (_residueNum < 1)
		{
			yield break;
		}
		var instatiateEffect = GameObject.Instantiate(effectObject, transform.position + new Vector3(0f, offset, 0f), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;

		yield return new WaitForSeconds(1.0f);

		RespawnCharacter();
		Destroy(instatiateEffect, deleteTime);

		var CinemachineTarget = GameObject.FindGameObjectsWithTag("CinemachineTarget");
		_playerFollowCamera.Follow = CinemachineTarget[0].transform;
		yield break;
	}

	void RespawnCharacter()
	{
		GameObject.Instantiate(player, transform.position, Quaternion.Euler(0f, 0f, 0f));
		_residueNum--;
	}
}
