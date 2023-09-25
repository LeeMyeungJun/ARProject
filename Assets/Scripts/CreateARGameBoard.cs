using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;      	
using UnityEngine.XR.ARSubsystems;
public class CreateARGameBoard : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;     
    private ARPlaneManager arPlaneManager;

    private bool bFlag = false;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

#if UNITY_EDITOR

    private void Update()
    {
        if (Input.touchCount == 0 || bFlag) return;

        Touch touch = Input.GetTouch(0);
        if (arRaycastManager.Raycast(touch.position,hits,TrackableType.PlaneWithinPolygon))
        {
            bFlag = true;
            Pose pose = hits[0].pose;
            GameManager.Instance.SetGameParentObjActive(true, pose.position, pose.rotation);
        }

    }
#endif
}
