using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleReactTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;
    private Toggle toggle;
    private Image backgroundImage;
    private Image handleImage;
    private Color handleDefaultColor;
    private Vector2 handlePosition;

    void Awake()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        handlePosition = uiHandleReactTransform.anchoredPosition;
        backgroundImage = uiHandleReactTransform.parent.GetComponent<Image>();
        handleImage = uiHandleReactTransform.GetComponent<Image>();
        handleDefaultColor = handleImage.color;
        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    /// <summary>
    /// This method handles the visual effects of the toggle button.
    /// </summary>
    /// <param name="i_On">The state of the toggle.</param>
    void OnSwitch(bool i_On)
    {
        if (i_On)
        {
            uiHandleReactTransform.anchoredPosition = handlePosition * (-1);
        }
        else
        {
            uiHandleReactTransform.anchoredPosition = handlePosition;
        }

        backgroundImage.color = i_On ? Color.grey : handleDefaultColor;
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
