using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class Mode_0
{
    public MemesStringClass[] Memes;
    
    public GameObject[] Mode_UI;

}
[Serializable]
public class MemesStringClass
{
    public string Line1;
    public string Line2;
    
}

[Serializable]
public class Mode_1
{

    public GameObject[] Mode_UI;

}




public class Cheers_ModeSelector : MonoBehaviour
{

   
    public TMP_Dropdown DD_Menu;
    public TMP_InputField Inputf;
    public Mode_0 PM_Mode;
    public Mode_1 Cust_Mode;
    public TextMeshProUGUI MemeText;
    public TextMeshProUGUI MemeText2;
    public GameObject Cameraview;
    public Toggle CamToggler;
    public Slider ZoomSlider;

    // Start is called before the first frame update
    void Start()
    {
        OnDropdownValueChanged();


    }

  


    public void OnDropdownValueChanged()
    {

        int index = DD_Menu.value;
        switch (index)
        {
            case 0:
                PremodeMode();
                break;
            case 1:
                CustomMode();
                break;
            default:
                Debug.LogWarning("Unexpected dropdown index.");
                break;
        }
    }
    public void PremodeMode()
    {
        currentIndex = 0;
        MemeText.text = PM_Mode.Memes[currentIndex].Line1;
       // MemeText2.text = PM_Mode.Memes[currentIndex].Line2;
        Debug.Log("Mode 1 manually executed!");
        // Add your Mode 1 logic here

        foreach (var item in Cust_Mode.Mode_UI)
        {
            item.SetActive(false);
        }
        foreach (var item in PM_Mode.Mode_UI)
        {
            item.SetActive(true);
        }



    }

    public void CustomMode()
    {
        Inputf.text = "";

        Inputf.ActivateInputField();
        Debug.Log("Mode 2 manually executed!");
        // Add your Mode 2 logic here
        MemeText.text="";

        foreach (var item in PM_Mode.Mode_UI)
        {
            item.SetActive(false);
        }
        foreach (var item in Cust_Mode.Mode_UI)
        {
            item.SetActive(true);
        }


    }


    private int currentIndex = 0;

    public void LeftArrow()
    {
        if (PM_Mode.Memes == null || PM_Mode.Memes.Length == 0) return;

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = PM_Mode.Memes.Length - 1; // Wrap around to the last element
        }

        MemeText.text = PM_Mode.Memes[currentIndex].Line1;
        MemeText2.text = PM_Mode.Memes[currentIndex].Line2;
    }

    // Method to move to the next meme in the array
    public void RightArrow()
    {
        if (PM_Mode.Memes == null || PM_Mode.Memes.Length == 0) return;

        currentIndex++;
        if (currentIndex >= PM_Mode.Memes.Length)
        {
            currentIndex = 0; // Wrap around to the first element
        }

        MemeText.text = PM_Mode.Memes[currentIndex].Line1;
        MemeText2.text = PM_Mode.Memes[currentIndex].Line2;

    }

    public void ToggleTheCamera() 
    {
       
      
            Cameraview.SetActive(CamToggler.isOn);
        ZoomSlider.gameObject.SetActive(CamToggler.isOn);
       
    
    }

}
