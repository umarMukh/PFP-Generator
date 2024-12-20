using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public GameObject[] AllgameObjects; // Array of GameObjects to activate
    public string[] texts;           // Array of strings to display
    public TextMeshProUGUI uiText;             // UI Text to display the string
   

    private int currentIndex = 0;   // Current index in the arrays

   void OnEnable ()
    {

        currentIndex = 0;
        // Ensure arrays are properly aligned
        if (AllgameObjects.Length != texts.Length)
        {
            Debug.LogError("GameObjects array and texts array must have the same length.");
            return;
        }

        // Initialize the first item
        UpdateUIAndGameObjects();

      
    }

    void UpdateUIAndGameObjects()
    {
        // Deactivate all GameObjects
        foreach (GameObject obj in AllgameObjects)
        {
            obj.SetActive(false);
        }

        // Activate the current GameObject
        if (AllgameObjects.Length > 0 && currentIndex < AllgameObjects.Length)
        {
            AllgameObjects[currentIndex].SetActive(true);
        }

        // Update the UI text
        if (texts.Length > 0 && currentIndex < texts.Length)
        {
            uiText.text = texts[currentIndex];
        }
    }

    public void TaptoNext()
    {
        // Increment the index
        currentIndex++;

        // If the index reaches the maximum, deactivate this GameObject
        if (currentIndex >= AllgameObjects.Length)
        {
           
            gameObject.SetActive(false);
            return;
        }

        // Update UI and GameObjects
        UpdateUIAndGameObjects();
    }




}
