using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIOutlineExact : MonoBehaviour
{
  public Color outlineColor = Color.black;
  public float outlineScale = 1.05f;

  private GameObject outline;

  private void Awake()
  {
    CreateOutline();
  }

  private void CreateOutline()
  {
    Image originalImg = GetComponent<Image>();
    if (originalImg == null || originalImg.sprite == null)
      return;

    GameObject parentEmpty = new GameObject("ImgWithBorder", typeof(RectTransform));
    RectTransform parentRT = parentEmpty.GetComponent<RectTransform>();
    parentRT.SetParent(transform.parent, false);

    RectTransform originalRT = originalImg.GetComponent<RectTransform>();
    parentRT.anchorMin = originalRT.anchorMin;
    parentRT.anchorMax = originalRT.anchorMax;
    parentRT.anchoredPosition = originalRT.anchoredPosition;
    parentRT.sizeDelta = originalRT.sizeDelta;
    parentRT.localScale = Vector3.one;
    parentEmpty.transform.SetSiblingIndex(originalRT.GetSiblingIndex());

    transform.SetParent(parentRT, true);
    transform.SetSiblingIndex(1);

    outline = new GameObject("Outline", typeof(RectTransform));
    RectTransform outlineRT = outline.GetComponent<RectTransform>();
    outlineRT.SetParent(parentRT, false);
    outlineRT.anchorMin = Vector2.zero;
    outlineRT.anchorMax = Vector2.one;
    outlineRT.anchoredPosition = Vector2.zero;
    outlineRT.sizeDelta = Vector2.zero;
    outlineRT.localScale = Vector3.one * outlineScale;

    outline.transform.SetSiblingIndex(0);

    Image outlineImg = outline.AddComponent<Image>();
    outlineImg.sprite = originalImg.sprite;
    outlineImg.type = originalImg.type;
    outlineImg.preserveAspect = originalImg.preserveAspect;
    outlineImg.color = outlineColor;
    outlineImg.raycastTarget = false;
  }
}
