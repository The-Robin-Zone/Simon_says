using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleReactTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    Image backgroundImage;
    Image handleImage;

    Color backgroundDefaultColor;
    Color handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        handlePosition = uiHandleReactTransform.anchoredPosition;
        backgroundImage = uiHandleReactTransform.parent.GetComponent<Image>();
        handleImage = uiHandleReactTransform.GetComponent<Image>();
        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;
        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

 
    void OnSwitch(bool on)
    {
        if (on)
        {
            uiHandleReactTransform.anchoredPosition = handlePosition * (-1);
        }
        else
        {
            uiHandleReactTransform.anchoredPosition = handlePosition;
        }

        backgroundImage.color = on ? Color.black : handleDefaultColor;
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
