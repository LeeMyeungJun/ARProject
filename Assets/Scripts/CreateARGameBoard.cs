using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;      	
using UnityEngine.XR.ARSubsystems;
public class CreateARGameBoard : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;     
    private ARPlaneManager arPlaneManager;

    private bool bFlag = false;
    private Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
    }
#if !UNITY_EDITOR

    private void Update()
    {
        if (Input.touchCount == 0 && bFlag) return;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(mainCam.ScreenPointToRay(Input.GetTouch(0).position), hits, TrackableType.PlaneWithinBounds))
        {
            bFlag = true;
            Pose pose = hits[0].pose;
            GameManager.Instance.SetGameParentObjActive(true, pose.position, pose.rotation);
        }

    }
#endif
}
