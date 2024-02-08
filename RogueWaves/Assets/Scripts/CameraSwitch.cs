using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    
    public GameObject FollowCamera;
    public GameObject MainCamera;

    public Canvas follow;
    public Canvas main;

    public Button follow_zoom;
    public Button main_zoom;

    public bool ZoomView = true;

    public CinemachineVirtualCamera cam;

    Text follow_dialogue, main_dialogue;
    Slider follow_enemyHealth, follow_playerHealth, main_enemyHealth, main_playerHealth;

    void Start(){
        follow_dialogue = follow.transform.Find("dialogueText").gameObject.GetComponent<Text>();
        follow_enemyHealth = follow.transform.Find("enemyHealth").gameObject.GetComponent<Slider>();
        follow_playerHealth = follow.transform.Find("playerHealth").gameObject.GetComponent<Slider>();

        main_dialogue = main.transform.Find("dialogueText").gameObject.GetComponent<Text>();
        main_enemyHealth = main.transform.Find("enemyHealth").gameObject.GetComponent<Slider>();
        main_playerHealth = main.transform.Find("playerHealth").gameObject.GetComponent<Slider>();

        // main_zoom.enabled = false;
        // follow_zoom.enabled = true;

    }

    void Update(){
        ManageCamera();
    }

    void ManageCamera(){
        if (Input.GetKeyDown(KeyCode.Z)){
            ActivateMainCamera();
        }
        if (Input.GetKeyUp(KeyCode.Z)){
            ActivateFollowCamera();
        }

    }

    public void ActivateMainCamera(){

        main_dialogue.text = follow_dialogue.text;
        main_enemyHealth.value = follow_enemyHealth.value;
        main_playerHealth.value = follow_playerHealth.value;

        // main_zoom.enabled = true;
        // follow_zoom.enabled = false;

        FollowCamera.SetActive(false);
        MainCamera.SetActive(true);

        ZoomView = false;
    }

    public void ActivateFollowCamera(){

        // main_zoom.enabled = false;
        // follow_zoom.enabled = true;

        FollowCamera.SetActive(true);
        MainCamera.SetActive(false);

        StartCoroutine(wait(2f));

        ZoomView = true;
    }

    IEnumerator wait(float time){
        yield return new WaitForSeconds(time);
    }
}
