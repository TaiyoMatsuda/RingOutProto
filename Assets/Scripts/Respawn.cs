using UnityEngine;

public class Respawn : MonoBehaviour
{
	[SerializeField] 
	GameObject[] players;
	[SerializeField] 
	float appearPlayerNextTime;
	[SerializeField] 
	int maxNumOfPlayers;
	private int _numberOfPlayers;
	private float elapsedTime;

	[SerializeField]
	private GameObject effectObject;
	[SerializeField] 
	float appearEffectNextTime;
	[SerializeField]
	private float deleteTime;
	[SerializeField]
	private float offset;
	private bool _isNoneEffect = true;
	private GameObject _instatiateEffect;

	// Use this for initialization
	void Start()
	{
		_numberOfPlayers = 0;
		elapsedTime = 0f;
	}

	void Update()
	{
		if (_numberOfPlayers >= maxNumOfPlayers)
		{
			return;
		}
		//　経過時間を足す
		elapsedTime += Time.deltaTime;
		
		if (elapsedTime > appearEffectNextTime && _isNoneEffect)
		{
			_isNoneEffect = false;
			_instatiateEffect = GameObject.Instantiate(effectObject, transform.position + new Vector3(0f, offset, 0f), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
		}

		if (elapsedTime > appearPlayerNextTime)
		{
			_isNoneEffect = true;
			elapsedTime = 0f;

			AppearEnemy();
			Destroy(_instatiateEffect, deleteTime);
		}
	}

	//　敵出現メソッド
	void AppearEnemy()
	{
		var randomValue = Random.Range(0, players.Length);
		var randomRotationY = Random.value * 360f;
		GameObject.Instantiate(players[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));

		_numberOfPlayers++;
		elapsedTime = 0f;
	}
}
