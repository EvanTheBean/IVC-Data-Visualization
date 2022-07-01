using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRCamera : MonoBehaviour
{
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean shutterAction;
    private Texture2D _screenShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shutterAction.GetStateDown(targetSource))
        {
            string filename = System.DateTime.Now.ToString();
            /*
            RenderTexture rt = new RenderTexture(1920, 1080, 24);
            Camera.main.targetTexture = rt;
            _screenShot = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
            Camera.main.Render();
            RenderTexture.active = rt;
            _screenShot.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0);
            _screenShot.Apply();
            Camera.main.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            byte[] bytes = _screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/screenshots/" + filename, bytes);

            Debug.Log(string.Format("Took screenshot to: {0}", filename));

            ScreenCapture.CaptureScreenshot("Assets/Resources/screenshots/" + filename + "1.png");
            */
            ScreenCapture.CaptureScreenshot(filename + ".png");
        }
    }
}
