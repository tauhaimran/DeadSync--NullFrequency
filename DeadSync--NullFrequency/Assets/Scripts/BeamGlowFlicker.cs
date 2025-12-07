using UnityEngine;

public class BeamGlowFlicker : MonoBehaviour
{
    [Header("Glow Settings")]
    public Renderer beamRenderer;
    public string emissionProperty = "_EmissionColor";
    public Color glowColor = Color.white;
    public float minIntensity = 0.5f;
    public float maxIntensity = 3f;
    public float flickerSpeed = 10f;

    [Header("Life")]
    public bool autoDestroy = false;
    public float lifeTime = 1f;

    float t = 0f;

    void Start()
    {
        if (beamRenderer != null)
        {
            // REQUIRED for Unity to show emission
            beamRenderer.material.EnableKeyword("_EMISSION");
        }

        if (autoDestroy)
            Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (beamRenderer == null) return;

        // Smooth sine flicker
        t += Time.deltaTime * flickerSpeed;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(t) + 1f) * 0.5f);

        beamRenderer.material.SetColor(emissionProperty, glowColor * intensity);
    }
}
