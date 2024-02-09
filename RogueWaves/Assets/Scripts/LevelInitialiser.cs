using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitialiser : MonoBehaviour
{
    public GameObject[] crewmateObjects; 

    public Button[] buttonObjects;

    public string[] crewmateIDs;

    public Sprite sprite;

    void Update()
    {
        for (int i = 0; i < crewmateObjects.Length; i++){

            if (CrewManager.Instance.HasCrewmate(crewmateIDs[i])){

                //crewmateObjects[i].SetActive(true);
                buttonObjects[i].enabled = true;

            }else{

                //crewmateObjects[i].SetActive(false);
                buttonObjects[i].enabled = false;
                buttonObjects[i].GetComponent<Image>().sprite = sprite;
            }
        }
    }
}
