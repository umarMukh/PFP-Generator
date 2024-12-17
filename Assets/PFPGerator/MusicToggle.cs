using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioSource musicSource;    // Attach your AudioSource here
    public Button musicOnButton;       // Attach your "Music On" button here
    public Button musicOffButton;      // Attach your "Music Off" button here

    private void Start()
    {
        // Set up listeners for each button
      //  musicOnButton.onClick.AddListener(TurnMusicOn);
       // musicOffButton.onClick.AddListener(TurnMusicOff);

        // Set initial states
      //  UpdateButtonVisibility();
    }

    // Method to turn music on
    public void TurnMusicOn()
    {
        
        {
            musicSource.Play();
        }
        UpdateButtonVisibility(true);
    }

    // Method to turn music off
    public void TurnMusicOff()
    {
     
        {
            musicSource.Pause();
        }
        UpdateButtonVisibility(false);
    }

    // Toggle button visibility based on music state
    public void UpdateButtonVisibility(bool status)
    {
        musicOnButton.gameObject.SetActive(status);
        musicOffButton.gameObject.SetActive(!status);
    }
}
