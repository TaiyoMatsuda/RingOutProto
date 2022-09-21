using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SecondPlayerView : MonoBehaviour
{

    [SerializeField]
    private GameObject _stock;

    [SerializeField]
    private Text _damage;

    // Start is called before the first frame update
    void Start()
    {
        SetDamage(Convert.ToInt32(_damage.text));
    }

    public void DeleteStock()
    {
        var childGameObjects = _stock.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Last();
        Destroy(childGameObjects);
    }

    public void SetDamage(int value)
    {
        int test = Convert.ToInt32(_damage.text) + value;
        _damage.text = value.ToString();
    }
}
