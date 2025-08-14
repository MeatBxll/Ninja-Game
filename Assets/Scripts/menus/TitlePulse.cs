using UnityEngine;
using TMPro;

public class TitlePulse : MonoBehaviour
{
  public float scaleAmount = 0.05f;
  public float pulseSpeed = 2f;

  private Vector3 originalScale;
  private TextMeshProUGUI tmpText;
  private Material tmpMaterial;

  void Start()
  {
    originalScale = transform.localScale;
    tmpText = GetComponent<TextMeshProUGUI>();

    if (tmpText != null && tmpText.fontMaterial != null)
    {
      tmpMaterial = Instantiate(tmpText.fontMaterial);
      tmpText.fontMaterial = tmpMaterial;
    }
  }

  void Update()
  {
    float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * scaleAmount;
    transform.localScale = originalScale * scale;

    if (tmpMaterial != null && tmpMaterial.HasProperty("_GlowPower"))
    {
      float flicker = Mathf.PingPong(Time.time * 2f, 0.2f) + 0.1f;
      tmpMaterial.SetFloat("_GlowPower", flicker);
    }
  }
}
