using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPresser : MonoBehaviour
{
    private GameObject[] buttonsObjArray;
    private float pressTime = 0.15f;
    private Dictionary<eButtonColor, Button> ColorToButtonDictionary;
    public Dictionary<eButtonColor, SimonButton> ColorToButtonScriptDictionary;

    /// <summary>
    /// This method initializes the dictionaries used to simulate the Simon buttons sequense.
    /// </summary>
    public void ButtonPresserInit()
    {
        // initialize and assign buttonsObjArray
        buttonsObjArray = GameObject.FindGameObjectsWithTag("SimonButton");

        // initialize and assign ColorToButtonDictionary & ColorToButtonScriptDictionary
        ColorToButtonDictionary = new Dictionary<eButtonColor, Button>();
        ColorToButtonScriptDictionary = new Dictionary<eButtonColor, SimonButton>();

        for (int i = 0; i < buttonsObjArray.Length; i++)
        {
            eButtonColor currColor = buttonsObjArray[i].GetComponent<SimonButton>().GetColor();
            Button currButton = buttonsObjArray[i].GetComponent<Button>();
            SimonButton currScript = buttonsObjArray[i].GetComponent<SimonButton>();

            ColorToButtonDictionary.Add(currColor, currButton);
            ColorToButtonScriptDictionary.Add(currColor, currScript);
        }
    }

    /// <summary>
    /// This method presses a Simon button or logs an error if invalid color was provided.
    /// </summary>
    /// <param name="i_Color">Color of the button to be pressed.</param>
    public void pressSimonButton(eButtonColor i_Color)
    {
        if (ColorToButtonDictionary.ContainsKey(i_Color) && ColorToButtonScriptDictionary.ContainsKey(i_Color))
        {
            PressButtonVisual(i_Color);                            // simulate the visual press
            ColorToButtonScriptDictionary[i_Color].ButtonSound();  // trigger the SimonButton method for sound
        }
        else
        {
            Debug.LogError("Invalid color");
        }
    }

    /// <summary>
    /// This method simulate the visual press of a Simon button by color.
    /// </summary>
    /// <param name="i_Color">Color of the button to be pressed.</param>
    private void PressButtonVisual(eButtonColor i_Color)
    {
        // Create a new PointerEventData
        var pointer = new PointerEventData(EventSystem.current);

        // Set the press
        ExecuteEvents.Execute(ColorToButtonDictionary[i_Color].gameObject, pointer, ExecuteEvents.pointerDownHandler);

        // Start a Coroutine that waits 'pressTime' seconds then releases the button
        StartCoroutine(ReleaseButton(i_Color));
    }

    /// <summary>
    /// This method simulate the visual release of a Simon button by color.
    /// </summary>
    /// <param name="i_Color">Color of the button to be released.</param>
    private IEnumerator ReleaseButton(eButtonColor i_Color)
    {
        // Wait for 'pressTime' seconds
        yield return new WaitForSeconds(pressTime);

        // Create a new PointerEventData
        var pointer = new PointerEventData(EventSystem.current);

        // Set the release
        ExecuteEvents.Execute(ColorToButtonDictionary[i_Color].gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }

    /// <summary>
    /// This method disables all Simon buttons.
    /// </summary>
    public void ButtonsDisabler()
    {
        foreach (KeyValuePair<eButtonColor, Button> kvp in ColorToButtonDictionary)
        {
            kvp.Value.interactable = false;
        }
    }

    /// <summary>
    /// This method disables all Simon buttons.
    /// </summary>
    public void ButtonsEnabler()
    {
        foreach (KeyValuePair<eButtonColor, Button> kvp in ColorToButtonDictionary)
        {
            kvp.Value.interactable = true;
        }
    }
}
