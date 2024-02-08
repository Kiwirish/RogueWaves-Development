using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitialiser : MonoBehaviour
{
    public GameObject[] crewmateObjects; 
    public string[] crewmateIDs;

    void Update()
    {
        for (int i = 0; i < crewmateObjects.Length; i++){

            if (CrewManager.Instance.HasCrewmate(crewmateIDs[i])){

                crewmateObjects[i].SetActive(true);

            }else{

                crewmateObjects[i].SetActive(false);
            }
        }
    }
}
