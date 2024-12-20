using UnityEngine;
using UnityEngine.UI;

public class WebGLWebcam : MonoBehaviour
{
    public RawImage webcamDisplay; // Reference to the RawImage in the UI
    public AspectRatioFitter aspectFitter; // Optional: Adjust aspect ratio
    public float zoom = 1f; // Public variable to control zoom level (default = 1, no zoom)
    public Slider SliderZoom;
    private WebCamTexture webCamTexture;

    private void OnEnable()
    {
        StartWebcam();
    }

    private void OnDisable()
    {
        StopWebcam();
    }

    private void StartWebcam()
    {
        // Check if a webcam is available
        if (WebCamTexture.devices.Length > 0)
        {
            // If the WebCamTexture is not initialized, create it
            if (webCamTexture == null)
            {
                WebCamDevice device = WebCamTexture.devices[0];
                webCamTexture = new WebCamTexture(device.name);
            }

            // Assign the webcam texture to the RawImage
            webcamDisplay.texture = webCamTexture;

            // Start the webcam
            webCamTexture.Play();

            // Adjust the aspect ratio if needed
            if (aspectFitter != null)
            {
                aspectFitter.aspectRatio = (float)webCamTexture.width / webCamTexture.height;
            }

            // Apply zoom
            ApplyZoom();
        }
        else
        {
            Debug.LogError("No webcam detected!");
        }
    }

    private void StopWebcam()
    {
        // Stop the webcam if it is active
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }

    private void Update()
    {
        // Update the rotation of the RawImage to match the webcam's orientation
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            float rotation = -webCamTexture.videoRotationAngle;
            webcamDisplay.rectTransform.localEulerAngles = new Vector3(0, 0, rotation);

            // Flip the image horizontally if the webcam is mirrored
            bool isMirrored = webCamTexture.videoVerticallyMirrored;
            webcamDisplay.rectTransform.localScale = new Vector3(isMirrored ? -1 : 1, 1, 1);
        }

        // Continuously apply zoom in case it's changed at runtime
        ApplyZoom();
    }

    private void ApplyZoom()
    {
        if (webcamDisplay != null)
        {

            
            // Adjust the scale of the RawImage based on the zoom value
            webcamDisplay.rectTransform.localScale = new Vector3(SliderZoom.value, SliderZoom.value, 1);
        }
    }

    private void OnDestroy()
    {
        // Stop and clean up the webcam when the object is destroyed
        StopWebcam();
        if (webCamTexture != null)
        {
            webCamTexture = null;
        }
    }
}
