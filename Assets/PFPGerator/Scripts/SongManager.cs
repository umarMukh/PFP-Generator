using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongManager : MonoBehaviour
{
    public List<AudioClip> songs; // List of songs
    public Text songNameText; // UI Text to display song name
    public Button leftButton; // Button to navigate left
    public Button rightButton; // Button to navigate right
    public WebGLScreenshot WGS;
    private int currentIndex = 0; // Current index in the song list
    private AudioSource audioSource; // AudioSource to play the song


    public GameObject SongOnBtn;
 //   public GameObject SongOffBtn;

    void Start()
    {
        if (songs.Count > 0)
        {
            audioSource = GetComponent<AudioSource>();
         //   UpdateSong();
        }

        leftButton.onClick.AddListener(PreviousSong);
        rightButton.onClick.AddListener(NextSong);

        leftButton.gameObject.SetActive(false);
       rightButton.gameObject.SetActive(false);

    }

    void UpdateSong()
    {
        if (songs.Count > 0)
        {

            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            SongOnBtn.SetActive(false);
            WGS.associatedAudioClip= songs[currentIndex];
            songNameText.text = songs[currentIndex].name;
            audioSource.clip = songs[currentIndex];
            audioSource.Play();
        }
    }

    void PreviousSong()
    {
        if (songs.Count > 0)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = songs.Count - 1;
            }
            UpdateSong();
        }
    }

    void NextSong()
    {
        if (songs.Count > 0)
        {
            currentIndex++;
            if (currentIndex >= songs.Count)
            {
                currentIndex = 0;
            }
            UpdateSong();
        }
    }

    public void EnableSong(bool status) 
    {
       
       // SongOffBtn.SetActive(status);
        if (status)
        {
            UpdateSong();
            

        }
        else
        {
            audioSource.Stop();


        }


       

    }

    public void ResetAndRandomSong(bool random)
    {

        if (random)
        {
            if (songs.Count > 0)
            {
                currentIndex = Random.Range(0, songs.Count);
                UpdateSong();
            }

        }
        else
        {
            if (songs.Count > 0)
            {
                currentIndex = 1;
                UpdateSong();
            }


        }
       
    }

}
