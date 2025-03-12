using UnityEngine;

public class Raycastable : MonoBehaviour, IRaycastable
{
    Outline _outline;
    float _outlineWidth = 6.0f;
    Color _color = new Color(1.0f, 0.5f, 0.0f);

    void Start()
    {
        _outline = GetComponent<Outline>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_outline != null)
        {
            _outline.OutlineWidth = _outlineWidth;
            _outline.OutlineColor = _color;
            _outline.OutlineMode = Outline.Mode.OutlineAll;
        }
    }

    public void SwitchOutline()
    {
        if (_outline != null)
        {
            _outline.enabled = true;
        }
    }
    public void SwitchOffOutline()
    {
        if (_outline != null)
        {
            _outline.enabled = false;
        }
    }
}
