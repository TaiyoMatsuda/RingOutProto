using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "CreateStatus")]
public class Status : ScriptableObject
{
	[SerializeField]
	private int hp;

	public int SetHp(int hp)
	{
		if (hp <= 0)
		{
			hp = 0;
		}
		this.hp = hp;
		return this.hp;
	}

	public int GetHp()
	{
		return this.hp;
	}

	public void Reset()
	{
		this.hp = 10;
	}
}
