
using UnityEngine;
using System.Collections;

public class WebGLScreenshot : MonoBehaviour
{
    public Canvas canvas;
    // public Camera SS_Cam;

    public void CaptureAndDownload()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // Wait for end of frame to capture the screenshot
        yield return new WaitForEndOfFrame();

        // Calculate the dimensions of the active UI content
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        float originalWidth = canvasRectTransform.rect.width;
        float originalHeight = canvasRectTransform.rect.height;

        // Adjust the dimensions to capture slightly more area
        float width = originalWidth / 2 + 10;  // Adjust width as needed
        float height = originalHeight + 10;    // Adjust height as needed

        // Create a RenderTexture
        RenderTexture rt = new RenderTexture((int)originalWidth, (int)originalHeight, 24);
        Texture2D screenShot = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);

        // Render the UI to the RenderTexture
        Camera.main.targetTexture = rt;
        Camera.main.Render();
        RenderTexture.active = rt;

        // Read pixels from the right half of the RenderTexture with adjustments
        screenShot.ReadPixels(new Rect((int)(originalWidth / 2) - 5, -5, (int)width, (int)height), 0, 0);
        screenShot.Apply();

        // Reset the RenderTexture
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Encode texture into PNG
        byte[] bytes = screenShot.EncodeToPNG();
        string encodedText = System.Convert.ToBase64String(bytes);

        // Send the encoded PNG to JavaScript
        Application.ExternalCall("DownloadImage", encodedText);
    }
}
