using System.Collections;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
	[SerializeField] 
	GameObject[] _enemys;
	[SerializeField] 
	int _residueNum;

	[SerializeField]
	private GameObject effectObject;
	[SerializeField]
	private float deleteTime;
	[SerializeField]
	private float offset;

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

		yield break;
	}

	void RespawnCharacter()
	{
		var randomValue = Random.Range(0, _enemys.Length);
		var randomRotationY = Random.value * 360f;
		GameObject.Instantiate(_enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));

		_residueNum--;
	}
}
