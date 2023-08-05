using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonPresser : MonoBehaviour
{
    private GameObject[] buttonsObjArray; 
    private Dictionary<ButtonColor, Button> ColorToButtonDictionary;
    public Dictionary<ButtonColor, SimonButton> ColorToButtonScriptDictionary;
    private float pressTime = 0.15f;

    public void ButtonPresserInit()
    {
        // initialize and assign buttonsObjArray
        buttonsObjArray = GameObject.FindGameObjectsWithTag("SimonButton");

        // initialize and assign ColorToButtonDictionary & ColorToButtonScriptDictionary
        ColorToButtonDictionary = new Dictionary<ButtonColor, Button>();
        ColorToButtonScriptDictionary = new Dictionary<ButtonColor, SimonButton>();

        for (int i = 0; i < buttonsObjArray.Length; i++)
        {
            ButtonColor currColor = buttonsObjArray[i].GetComponent<SimonButton>().GetColor();
            Button currButton = buttonsObjArray[i].GetComponent<Button>();
            SimonButton currScript = buttonsObjArray[i].GetComponent<SimonButton>();

            ColorToButtonDictionary.Add(currColor, currButton);
            ColorToButtonScriptDictionary.Add(currColor, currScript);
        }
    }

    public void pressSimonButton(ButtonColor color)
    {
        if (ColorToButtonDictionary.ContainsKey(color) && ColorToButtonScriptDictionary.ContainsKey(color))
        {
            PressButtonVisual(color);                            // simulate the visual press
            ColorToButtonScriptDictionary[color].ButtonSound();  // trigger the SimonButton method for sound
        }
        else
        {
            Debug.LogError("Invalid color");
        }
    }

    private void PressButtonVisual(ButtonColor color)
    {
        // Create a new PointerEventData
        var pointer = new PointerEventData(EventSystem.current);

        // Set the press
        ExecuteEvents.Execute(ColorToButtonDictionary[color].gameObject, pointer, ExecuteEvents.pointerDownHandler);

        // Start a Coroutine that waits 'pressTime' seconds then releases the button
        StartCoroutine(ReleaseButton(color));
    }

    private IEnumerator ReleaseButton(ButtonColor color)
    {
        // Wait for 'pressTime' seconds
        yield return new WaitForSeconds(pressTime);

        // Create a new PointerEventData
        var pointer = new PointerEventData(EventSystem.current);

        // Set the release
        ExecuteEvents.Execute(ColorToButtonDictionary[color].gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }

    public void ButtonsDisabler()
    {
        foreach (KeyValuePair<ButtonColor, Button> kvp in ColorToButtonDictionary)
        {
            kvp.Value.interactable = false;
        }
    }

    public void ButtonsEnabler()
    {
        foreach (KeyValuePair<ButtonColor, Button> kvp in ColorToButtonDictionary)
        {
            kvp.Value.interactable = true;
        }
    }
}
