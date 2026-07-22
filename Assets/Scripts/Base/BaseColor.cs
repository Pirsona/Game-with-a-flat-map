using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BaseColor : MonoBehaviour
{
    [SerializeField] private Color _selectingColor;

    private Renderer _renderer;

    public Color SelectingColor => _selectingColor;

    public Color StandardColor { get; private set; }


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        StandardColor = _renderer.material.color;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
