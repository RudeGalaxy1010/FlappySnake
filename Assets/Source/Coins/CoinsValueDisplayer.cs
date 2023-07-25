using TMPro;
using UnityEngine;

public class CoinsValueDisplayer : MonoBehaviour
{
    private CoinsCollector _coinsCollector;
    private TMP_Text _valueText;

    public void Construct(CoinsCollector coinsCollector, TMP_Text valueText)
    {
        _coinsCollector = coinsCollector;
        _valueText = valueText;
        _coinsCollector.CoinsValueChanged += OnCoinsValueChanged;
    }

    private void OnDestroy()
    {
        if (_coinsCollector != null)
        {
            _coinsCollector.CoinsValueChanged -= OnCoinsValueChanged;
        }
    }

    private void OnCoinsValueChanged(int value)
    {
         _valueText.text = value.ToString();
    }
}
