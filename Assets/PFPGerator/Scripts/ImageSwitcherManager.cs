using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcherManager : MonoBehaviour
{

    public static ImageSwitcherManager Instance;

    public Text CityNameTxt;
    public ImageSwitcher[] imageSwitchers;  // Array of ImageSwitchers.
    public WebGLScreenshot webGLScreenshot;
    void Start()
    {


        Instance = this;
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
