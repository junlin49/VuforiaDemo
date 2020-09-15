using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
///
/// </summary>
public class CameraSetting : MonoBehaviour 
{
    bool openFlash = true;
    private void Start()
    {
        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);// Vuforia启动完成时的回调函数
        vuforia.RegisterOnPauseCallback(OnVuforiaPaused);//Vuforia暂停时的回调函数
    }

    private void OnVuforiaStarted()
    {
        //程序启动，自动对焦
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);//连续自动对焦
    }

    private void OnVuforiaPaused(bool isPaused)
    {

    }

    public void SetCameraFocus()//手动单次对焦
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);//手动单次对焦
    }


    //public void SwitchCameraDevice()
    //{
    //    CameraDevice.Instance.Stop();//停用摄像头
    //    CameraDevice.Instance.Deinit();//取消初始化

    //    CameraDevice.Instance.Init(CameraDevice.camerad)
    //}

    public void SetFlash()//设置闪光灯
    {
        CameraDevice.Instance.SetFlashTorchMode(openFlash);
        openFlash = !openFlash;
    }
}
 

