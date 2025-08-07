using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TMPButtonHoverEffects : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    ISelectHandler, IDeselectHandler
{
  [Header("UI References")]
  public Image borderImage;
  public TMP_Text tmpText;

  [Header("Colors")]
  public Color normalColor = new Color(1f, 0.65f, 0.15f);
  public Color hoverColor = new Color(1f, 0.83f, 0.31f);

  [Header("Scale Settings")]
  public Vector3 normalScale = Vector3.one;
  public Vector3 hoverScale = Vector3.one * 1.05f;
  public float scaleSpeed = 10f;

  [Header("Audio (Optional)")]
  public AudioSource hoverSound;

  private bool isHovered = false;

  void Start()
  {
    RemoveHover();
  }
  void Update()
  {
    transform.localScale = Vector3.Lerp(transform.localScale,
        isHovered ? hoverScale : normalScale,
        Time.unscaledDeltaTime * scaleSpeed);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    ApplyHover();
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    RemoveHover();
  }

  public void OnSelect(BaseEventData eventData)
  {
    ApplyHover();
  }

  public void OnDeselect(BaseEventData eventData)
  {
    RemoveHover();
  }

  private void ApplyHover()
  {
    if (borderImage != null)
      borderImage.color = hoverColor;

    if (tmpText != null)
      tmpText.color = hoverColor;

    if (hoverSound != null)
      hoverSound.Play();

    isHovered = true;
  }

  private void RemoveHover()
  {
    if (borderImage != null)
      borderImage.color = normalColor;

    if (tmpText != null)
      tmpText.color = normalColor;

    isHovered = false;
  }
}
