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

    private int _tempDamageValue;

    void Start()
    {
        SetDamage(Convert.ToInt32(_damage.text));
    }

    public void DeleteStock()
    {
        var childGameObjects = _stock.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Last();
        Destroy(childGameObjects);

        _damage.text = "0 %";
    }

    public void SetDamage(int value)
    {
        DOTween.To(() => _tempDamageValue,
            tempValue =>
            {
                _damage.text = $"{tempValue.ToString()} %";
                var color = value >= 100 ? Color.red : Color.black;
                _damage.DOColor(color, 0f);
            },
            value, 0.35f);
    }
}
