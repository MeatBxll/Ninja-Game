using UnityEngine;
using TMPro;
public class SizeFitter : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    private RectTransform background;
    private Vector2 padding = new Vector2(15f, 15f);

    void Start()
    {
        background = gameObject.GetComponent<RectTransform>();
    }
    void Update()
    {
        if (targetText == null || background == null)
            return;

        float width = targetText.preferredWidth + padding.x;
        float height = targetText.preferredHeight + padding.y;

        background.sizeDelta = new Vector2(width, height);

        background.position = targetText.transform.position;
    }
}


