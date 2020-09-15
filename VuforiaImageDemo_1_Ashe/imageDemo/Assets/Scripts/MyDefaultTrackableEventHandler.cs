/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class MyDefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public GameObject aixiPrefab;
    public GameObject bloodPrefab;
    public GameObject tondanPrefab;
    private AudioSource backg;
    public AudioClip wel;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        backg = this.GetComponent<AudioSource>();
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + 
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS


    //发现目标物后采取的行动
    protected virtual void OnTrackingFound()
    {
        if (!backg.isPlaying)
        {
            backg.Play();
        }

        //出现aixi
        GameObject aixi = GameObject.Instantiate(aixiPrefab);
        aixi.transform.position = this.transform.position - new Vector3(0, 2.4f, 0);
        aixi.transform.parent = this.transform;
        aixi.transform.rotation = this.transform.rotation;
       
        //出现血噬特效
        GameObject teXiao1 = GameObject.Instantiate(bloodPrefab,this.transform.position,Quaternion.identity);
        teXiao1.transform.parent = this.transform;
        Destroy(teXiao1, 7);//7s后销毁
        //出现火焰特效
        GameObject teXiao2 = GameObject.Instantiate(tondanPrefab, this.transform.position, Quaternion.identity);
        teXiao2.transform.parent = this.transform;
        Destroy(teXiao2, 7);//7s后销毁

        Invoke("playAudioClip", 6);//6s后开启声音
    }

    private void playAudioClip()
    {
        AudioSource.PlayClipAtPoint(wel, this.transform.position);
    }

    protected virtual void OnTrackingLost()//丢失销毁
    {
        Destroy(GameObject.Find("ashe@attack1(Clone)"));
        Destroy(GameObject.Find("RFX_Blood_Puddle(Clone)"));
        Destroy(GameObject.Find("RFX_Tonado_Flame(Clone)"));
    }

    #endregion // PROTECTED_METHODS
}
