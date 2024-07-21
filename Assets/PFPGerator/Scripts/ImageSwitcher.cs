using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{

    public bool IsCityName;
    public SwitcherSet[] switcherSets;  // Array of switcher sets.
    public SpriteData spriteData;       // Reference to the ScriptableObject containing the sprites.

    public int currentIndex = 0;        // Index of the current sprite.
   
    public string CityName;
    public void Start()
    {
        if (spriteData == null || spriteData.sprites.Length == 0)
        {
            Debug.LogError("SpriteData is not assigned or contains no sprites.");
            return;
        }

        foreach (var set in switcherSets)
        {
            // Initialize the preview and big images with the first sprite.
            InitializeToStart(set);

            // Add listeners to the buttons.
            set.leftButton.onClick.AddListener(() => OnLeftButtonClick(set));
            set.rightButton.onClick.AddListener(() => OnRightButtonClick(set));
        }
    }

    public void OnLeftButtonClick(SwitcherSet set)
    {
        // Move to the previous sprite index, wrapping around if necessary.
        currentIndex = (currentIndex - 1 + spriteData.sprites.Length) % spriteData.sprites.Length;
        UpdateImage(set);
    }

    public void OnRightButtonClick(SwitcherSet set)
    {
        // Move to the next sprite index, wrapping around if necessary.
        currentIndex = (currentIndex + 1) % spriteData.sprites.Length;
        UpdateImage(set);
    }

    public void UpdateImage(SwitcherSet set)
    {

        
        // Update both the preview and big images to the current sprite.
        if (IsCityName)
        {
            Debug.Log("City Name Is : "+ spriteData.sprites[currentIndex].name);
            ImageSwitcherManager.Instance.CityNameTxt.text = spriteData.sprites[currentIndex].name;

        }
        
        set.previewImage.sprite = spriteData.SmallSprites[currentIndex];
        set.bigImage.sprite = spriteData.sprites[currentIndex];
    }

    public void InitializeToStart(SwitcherSet set)
    {

        Debug.Log("City Name Is : " + spriteData.sprites[0].name);
        // Set both the preview and big images to the first sprite.
        set.previewImage.sprite = spriteData.SmallSprites[0];
        set.bigImage.sprite = spriteData.sprites[0];
    }

    public void ReachToIndex(int Rand)
    {
        currentIndex = Rand;
        foreach (var set in switcherSets)
        {
            UpdateImage(set);
        }
    }
}
