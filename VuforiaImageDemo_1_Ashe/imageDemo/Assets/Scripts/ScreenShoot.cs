using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
///截图
/// </summary>
public class ScreenShoot : MonoBehaviour 
{
    public void OnScreenShotClicked()
    {
        System.DateTime now = System.DateTime.Now;
        string times = now.ToString();
        times = times.Trim();
        times = times.Replace("/", "-");

        string fileName = "ARScreenShot" + times + ".png";

        if (Application.platform == RuntimePlatform.Android)
        {
            //包含UI
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            byte[] bytes = texture.EncodeToPNG();

            string destination = "/sdcard/DCIM/Screenshots";

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            string pathSave = destination + "/" + fileName;

            File.WriteAllBytes(pathSave, bytes);

        }
    }
}
 

