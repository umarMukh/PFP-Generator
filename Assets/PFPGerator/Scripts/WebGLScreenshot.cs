using System.Collections;
using System.IO;
using UnityEngine;

public class WebGLScreenshot : MonoBehaviour
{

    public bool IsPepeGen = false;
    public Canvas canvas;
    public AudioClip associatedAudioClip;

    public void CaptureAndDownload()
    {
        StartCoroutine(CaptureScreenshotAndAudio());
    }

    private IEnumerator CaptureScreenshotAndAudio()
    {
        yield return new WaitForEndOfFrame();

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        float originalWidth = canvasRectTransform.rect.width;
        float originalHeight = canvasRectTransform.rect.height;

        RenderTexture rt = new RenderTexture((int)originalWidth, (int)originalHeight, 24);
        Texture2D screenShot = new Texture2D((int)originalWidth, (int)originalHeight, TextureFormat.RGB24, false);

        Camera.main.targetTexture = rt;
        Camera.main.Render();
        RenderTexture.active = rt;

        screenShot.ReadPixels(new Rect(0, 0, (int)originalWidth, (int)originalHeight), 0, 0);
        screenShot.Apply();

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] imageBytes = screenShot.EncodeToPNG();
        string screenshotPath = Path.Combine(Application.persistentDataPath, "screenshot.png");
        File.WriteAllBytes(screenshotPath, imageBytes);



        // Trigger download for the screenshot
        string screenshotUrl = $"data:image/png;base64,{System.Convert.ToBase64String(imageBytes)}";
        // Application.ExternalCall("DownloadFile", screenshotUrl, "screenshot.png");
        Application.ExternalEval($"DownloadFile('{screenshotUrl}', 'screenshot.png');");



        // Trigger download for the audio file
        if (IsPepeGen)
        {
            string audioPath = Path.Combine(Application.persistentDataPath, "audio.wav");
            SaveAudioClipToWAV(associatedAudioClip, audioPath);
            byte[] audioBytes = File.ReadAllBytes(audioPath);
            string audioUrl = $"data:audio/wav;base64,{System.Convert.ToBase64String(audioBytes)}";
            Application.ExternalCall("DownloadFile", audioUrl, "audio.wav");
        }

      
    }

    private void SaveAudioClipToWAV(AudioClip clip, string path)
    {
        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            int headerSize = 44;
            fileStream.Seek(headerSize, SeekOrigin.Begin);
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            short[] intData = new short[samples.Length];
            byte[] bytesData = new byte[samples.Length * 2];
            int rescaleFactor = 32767;

            for (int i = 0; i < samples.Length; i++)
            {
                intData[i] = (short)(samples[i] * rescaleFactor);
                byte[] byteArr = System.BitConverter.GetBytes(intData[i]);
                byteArr.CopyTo(bytesData, i * 2);
            }

            fileStream.Write(bytesData, 0, bytesData.Length);

            fileStream.Seek(0, SeekOrigin.Begin);

            byte[] header = new byte[headerSize];
            System.Array.Copy(System.Text.Encoding.ASCII.GetBytes("RIFF"), 0, header, 0, 4);
            System.Array.Copy(System.BitConverter.GetBytes(fileStream.Length - 8), 0, header, 4, 4);
            System.Array.Copy(System.Text.Encoding.ASCII.GetBytes("WAVE"), 0, header, 8, 4);
            System.Array.Copy(System.Text.Encoding.ASCII.GetBytes("fmt "), 0, header, 12, 4);
            System.Array.Copy(System.BitConverter.GetBytes(16), 0, header, 16, 4);
            System.Array.Copy(System.BitConverter.GetBytes((short)1), 0, header, 20, 2);
            System.Array.Copy(System.BitConverter.GetBytes((short)clip.channels), 0, header, 22, 2);
            System.Array.Copy(System.BitConverter.GetBytes(clip.frequency), 0, header, 24, 4);
            System.Array.Copy(System.BitConverter.GetBytes(clip.frequency * clip.channels * 2), 0, header, 28, 4);
            System.Array.Copy(System.BitConverter.GetBytes((short)(clip.channels * 2)), 0, header, 32, 2);
            System.Array.Copy(System.BitConverter.GetBytes((short)16), 0, header, 34, 2);
            System.Array.Copy(System.Text.Encoding.ASCII.GetBytes("data"), 0, header, 36, 4);
            System.Array.Copy(System.BitConverter.GetBytes(fileStream.Length - headerSize), 0, header, 40, 4);

            fileStream.Write(header, 0, headerSize);
        }
    }

    private void Update()
    {
        
    }
}
