using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class TapToPlane : MonoBehaviour
{
    [SerializeField] GameObject objectToPlace;
    private ARRaycastManager _arRaycastManager;

    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject spawnedObject;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool tryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (spawnedObject == null)
            {
                //spawnedObject = Instantiate(objectToPlace);
                //float yOffSet = spawnedObject.transform.position.y / 3;
                //spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x, 
                //spawnedObject.transform.position.y + yOffSet, spawnedObject.transform.position.z);
                spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            }
            else
            {
                //spawnedObject.transform.position = hitPose.position;
            }
        }


    }
}
