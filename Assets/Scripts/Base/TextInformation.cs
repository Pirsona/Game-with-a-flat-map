using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInformation : MonoBehaviour
{
    [SerializeField] private Base _base;
    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _base.ValueOreChanged += UpdateText;
    }

    private void OnDisable()
    {
        _base.ValueOreChanged -= UpdateText;
    }

    private void UpdateText()
    {
        _text.text = $"Ore collection: {_base.OreCount}";
    }
}