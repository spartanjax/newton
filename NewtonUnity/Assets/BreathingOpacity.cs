using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BreathingOpacity : MonoBehaviour
{
    [Range(0f, 1f)] public float minAlpha = 0.3f;  
    [Range(0f, 1f)] public float maxAlpha = 1f;    
    public float speed = 2f;                       

    private Material mat;
    private Color baseColor;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;

        // Make sure material supports transparency
        SetMaterialTransparent();
    }

    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * speed) + 1f) / 2f);
        mat.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
    }

    void SetMaterialTransparent()
    {
        // Switch material render mode to transparent (important!)
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
