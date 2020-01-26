using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits;
    public GameObject PlayerPrefab;

    // Start is called before the first frame update
    void Start()
    {

        raycastManager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if(raycastManager.Raycast(touch.position,hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    Instantiate(PlayerPrefab, pose.position, pose.rotation);
                }
            }
        }
    }
}
