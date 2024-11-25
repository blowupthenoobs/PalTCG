using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenshotTaker : MonoBehaviour
{
    public bool captureOnStart = false;
    
    [Header("Image")]
    public bool transparent = false;

    [Header("Setup")]
    [Tooltip("4k = 3840 x 2160, 1080p = 1920 x 1080")]
    public Vector2Int resolution = new Vector2Int(1920, 1920); // 4k = 3840 x 2160   1080p = 1920 x 1080

    // optional game object to hide during screenshots (usually your scene canvas hud)
    public GameObject hideGameObject;

    // optimize for many screenshots will not destroy any objects so future screenshots will be fast
    public bool optimizeForManyScreenshots = true;

    // configure with raw, jpg, png, or ppm (simple raw format)
    public enum Format { RAW, JPG, PNG, PPM };
    public Format format = Format.PNG;

    // folder to write output (defaults to data path)
    public string folder = "Assets/Screenshots";


    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    private int counter = 0; // image #

    // commands
    private bool captureScreenshot = false;
    private bool captureVideo = false;

    private int i;

    public void Start()
    {
        if (captureOnStart)
            CaptureScreenshot();
    }

    // create a unique filename using a one-up variable
    private string uniqueFilename(int width, int height)
    {
        // if folder not specified by now use a good default
        if (folder == null || folder.Length == 0)
        {
            folder = Application.dataPath;
            if (Application.isEditor)
            {
                // put screenshots in folder above asset path so unity doesn't index the files
                var stringPath = folder + "/..";
                folder = Path.GetFullPath(stringPath);
            }
            folder += "/screenshots";

            // make sure directoroy exists
            System.IO.Directory.CreateDirectory(folder);

            // count number of files of specified format in folder
            string mask = string.Format("screen_{0}x{1}@{3:yyyy-MM-dd_HH-mm-ss-fff}*.{2}", width, height, format.ToString().ToLower(), DateTime.Now);
            counter = Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly).Length;
        }

        // use width, height, and counter for unique file name
        var filename = string.Format("{0}/screen_{1}x{2}_{3}at{5:yyyy-MM-dd_HH-mm-ss-fff}.{4}", folder, width, height, counter, format.ToString().ToLower(), DateTime.Now);

        // up counter for next call
        ++counter;

        // return unique filename
        return filename;
    }

    void CaptureScreenshot()
    {
        // if (prefabsToScreenshot.Length > i)
        // {
        //     foreach (Transform _child in prefabsParent)
        //     {
        //         Destroy(_child.gameObject);
        //     }
        //
        //     Instantiate(prefabsToScreenshot[i], prefabsParent.transform.position, prefabsParent.transform.rotation)
        //         .transform.parent = prefabsParent;
        //
        //     i++;
        // }
        
        captureScreenshot = false;

            // hide optional game object if set
            if (hideGameObject != null) hideGameObject.SetActive(false);

            // create screenshot objects if needed
            if (renderTexture == null)
            {
                // creates off-screen render texture that can rendered into
                rect = new Rect(0, 0, resolution.x, resolution.y);
                renderTexture = new RenderTexture(resolution.x, resolution.y, 24);

                screenShot = new Texture2D(resolution.x, resolution.y, TextureFormat.ARGB32, false);
            }

            // get main camera and manually render scene into rt
            Camera camera = this.GetComponent<Camera>(); // NOTE: added because there was no reference to camera in original script; must add this script to Camera
            camera.targetTexture = renderTexture;

            if (transparent)
            {
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = Color.clear;
            }
            camera.Render();

            // read pixels will read from the currently active render texture so make our offscreen 
            // render texture active and then read the pixels
            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);

            // reset active camera texture and render texture
            camera.targetTexture = null;
            RenderTexture.active = null;

            // get our unique filename
            string filename = uniqueFilename((int)rect.width, (int)rect.height);

            // pull in our file header/data bytes for the specified image format (has to be done from main thread)
            byte[] fileHeader = null;
            byte[] fileData = null;
            if (format == Format.RAW)
            {
                fileData = screenShot.GetRawTextureData();
            }
            else if (format == Format.PNG)
            {
                fileData = screenShot.EncodeToPNG();
            }
            else if (format == Format.JPG)
            {
                fileData = screenShot.EncodeToJPG();
            }
            else // ppm
            {
                // create a file header for ppm formatted file
                string headerStr = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
                fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
                fileData = screenShot.GetRawTextureData();
            }

            // create new thread to save the image to file (only operation that can be done in background)
            new System.Threading.Thread(() =>
            {
                // create file and write optional header with image bytes
                var f = System.IO.File.Create(filename);
                if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
                f.Write(fileData, 0, fileData.Length);
                f.Close();
                Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
            }).Start();

            // unhide optional game object if set
            if (hideGameObject != null) hideGameObject.SetActive(true);

            // cleanup if needed
            if (optimizeForManyScreenshots == false)
            {
                Destroy(renderTexture);
                renderTexture = null;
                screenShot = null;
            }

            // if (prefabsToScreenshot.Length > i)
            // {
            //     yield return new WaitForEndOfFrame();
            //     StartCoroutine(CaptureScreenshot());
            // }
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            CaptureScreenshot();
        }
  
    }
}