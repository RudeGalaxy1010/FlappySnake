using System;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    public event Action<int> CoinsValueChanged;

    private int _coinsValue;

    public void Collect(Coin coin)
    {
        _coinsValue += coin.Value;
        CoinsValueChanged?.Invoke(_coinsValue);
    }
}
