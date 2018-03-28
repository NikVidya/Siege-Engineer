using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlAction : BaseNarrativeAction
{
    [Header("Camera Control")]
    [Tooltip("The entity name of the object to start at. Null to start from current position")]
    public string StartObjectName = null;
    [Tooltip("The entity name of the object to end at")]
    public string TargetObjectName = "";
    [Tooltip("The time in seconds it should take the camera to reach it's target")]
    public float LerpTime = 1.0f;
    [Tooltip("Should the camera return to it's old position after the sequence is over")]
    public bool ReturnToPosition = false;

    private Vector3 oldPosition;
    private Vector3 lerpStart;
    private Transform lerpTarget;
    private Camera mainCamera;
    private Transform oldCameraParent;
    private float elapsedLerpTime = 0.0f;

    public override void OnSequenceEnd()
    {
        if(mainCamera == null)
        {   // This sequence never really ran
            return;
        }
        // Return the camera to it's parent
        mainCamera.transform.parent = oldCameraParent;

        if (ReturnToPosition)
        {   // Camera should go back from whence it came!
            mainCamera.transform.position = oldPosition;
        }
        else
        {   // Camera doesn't need to return, so make sure it's where it's supposed to be
            mainCamera.transform.position = new Vector3(lerpTarget.position.x, lerpTarget.position.y, mainCamera.transform.position.z);
        }
    }

    public override void OnSequenceStart()
    {
        elapsedLerpTime = 0.0f;

        mainCamera = Camera.main;
        oldCameraParent = mainCamera.gameObject.transform.parent;
        lerpStart = oldPosition = mainCamera.gameObject.transform.position;

        // Pull the camera from it's parent so we're in control
        mainCamera.transform.parent = null;

        // Get the start position
        if (!string.IsNullOrEmpty(StartObjectName))
        {   // Start name was specified, try and set that to be the start position
            GameObject startTarget = GameObject.Find(StartObjectName);
            if(startTarget)
            {
                lerpStart = new Vector3(startTarget.transform.position.x, startTarget.transform.position.y, mainCamera.transform.position.z);
            }
        }

        // Get the target transform
        if (!string.IsNullOrEmpty(TargetObjectName))
        {
            GameObject endTarget = GameObject.Find(TargetObjectName);
            if(endTarget)
            {
                lerpTarget = endTarget.transform;
            }
        }
    }

    public override void OnUpdate()
    {
        elapsedLerpTime += Time.deltaTime;

        if(lerpTarget != null && LerpTime > 0)
        {
            Vector3 target = new Vector3(lerpTarget.position.x, lerpTarget.position.y, mainCamera.transform.position.z);
            // Lerp
            mainCamera.transform.position = Vector3.Lerp(lerpStart, target, elapsedLerpTime / LerpTime);
        }
    }
}
