

using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using Rect = UnityEngine.Rect;

public class FaceReplacement : MonoBehaviour
{
    public RectTransform headPlaceholder;

    private WebCamTexture webcamTexture;
    private CascadeClassifier faceCascade;
    private Mat reusableFrame;
    private Texture2D croppedFaceTexture;

    void Start()
    {
        faceCascade = new CascadeClassifier(Application.streamingAssetsPath + "/haarcascade_frontalface_default.xml");

        if (WebCamTexture.devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(WebCamTexture.devices[0].name, 320, 240); // Lower resolution
            webcamTexture.Play();

            reusableFrame = new Mat();
            croppedFaceTexture = new Texture2D(100, 100); // Initialize cropped texture
        }
        else
        {
            Debug.LogWarning("No webcam detected!");
        }
    }

    void Update()
    {
        if (webcamTexture != null && webcamTexture.didUpdateThisFrame)
        {
            // Convert WebCamTexture to Texture2D
            Texture2D texture2D = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
            texture2D.SetPixels(webcamTexture.GetPixels());
            texture2D.Apply();

            // Convert Texture2D to OpenCV Mat
            reusableFrame = OpenCvSharp.Unity.TextureToMat(texture2D);

            // Perform face detection
            var faces = faceCascade.DetectMultiScale(reusableFrame, 1.1, 4, HaarDetectionType.DoRoughSearch, new Size(100, 100));

            if (faces.Length > 0)
            {
                var face = faces[0];
                Rect faceRect = new Rect(face.X, face.Y, face.Width, face.Height);
                Texture2D croppedFaceTexture = GetCroppedFaceTexture(webcamTexture, faceRect);

                // Update the UI placeholder
                headPlaceholder.GetComponent<Image>().sprite = Sprite.Create(
                    croppedFaceTexture,
                    new Rect(0, 0, croppedFaceTexture.width, croppedFaceTexture.height),
                    new Vector2(0.5f, 0.5f)
                );
            }
        }
    }


    Texture2D GetCroppedFaceTexture(WebCamTexture webcamTexture, Rect faceRect)
    {
        croppedFaceTexture.Reinitialize((int)faceRect.width, (int)faceRect.height); // Reuse existing texture
        croppedFaceTexture.SetPixels(webcamTexture.GetPixels((int)faceRect.x, (int)faceRect.y, (int)faceRect.width, (int)faceRect.height));
        croppedFaceTexture.Apply();
        return croppedFaceTexture;
    }

    void OnDisable()
    {
        webcamTexture?.Stop();
        reusableFrame?.Dispose();
    }
}
