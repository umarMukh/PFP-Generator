using UnityEngine;

public class ImageSwitcherManager : MonoBehaviour
{
    public ImageSwitcher[] imageSwitchers;  // Array of ImageSwitchers.
    public WebGLScreenshot webGLScreenshot;
    void Start()
    {
        //foreach (var switcher in imageSwitchers)
        //{
        //    switcher.Start();
        //}
    }


    public void RandomCombination()
    {
        foreach (var switcher in imageSwitchers)
        {
            int rand= Random.Range(0, switcher.spriteData.sprites.Length);
            switcher.ReachToIndex(rand);
        }
    }

     public void ResetCombination()
    {
        foreach (var switcher in imageSwitchers)
        {
            switcher.ReachToIndex(0);

        }
    }

    
   

   
    public void DownloadImage() 
    {
        webGLScreenshot.CaptureAndDownload();



    }


}
