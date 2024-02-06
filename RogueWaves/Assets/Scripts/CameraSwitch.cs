using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSwitch : MonoBehaviour
{
    
    public GameObject FollowCamera;
    public GameObject MainCamera;
    public bool ZoomView = true;

    public CinemachineVirtualCamera cam;

    void Update(){
        ManageCamera();
    }

    void ManageCamera(){
        if (Input.GetKeyDown(KeyCode.Z)){
            ActivateMainCamera();
            ZoomView = false;
        }
        if (Input.GetKeyUp(KeyCode.Z)){
            ActivateFollowCamera();
            ZoomView = true;
        }

    }

    void ActivateMainCamera(){
        FollowCamera.SetActive(false);
        MainCamera.SetActive(true);
    }

    void ActivateFollowCamera(){
        FollowCamera.SetActive(true);
        MainCamera.SetActive(false);
    }
}
