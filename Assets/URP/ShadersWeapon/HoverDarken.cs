using UnityEngine;

public class HoverDarken : MonoBehaviour
{
    private Renderer rend;
    private MaterialPropertyBlock mpb;

    public float hoverValue = 0f;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    public void SetHover(bool state)
    {
        hoverValue = state ? 1f : 0f;
        UpdateShader();
    }

    private void UpdateShader()
    {
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat("_Hover", hoverValue);
        rend.SetPropertyBlock(mpb);
    }
}
