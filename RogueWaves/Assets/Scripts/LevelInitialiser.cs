using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitialiser : MonoBehaviour
{
    public GameObject[] crewmateObjects; 

    public Button[] buttonObjects;

    public string[] crewmateIDs;

    public Sprite[] sprites;

    void Update()
    {
        for (int i = 0; i < crewmateObjects.Length; i++){

            if (CrewManager.Instance.HasCrewmate(crewmateIDs[i])){

                //crewmateObjects[i].SetActive(true);
                buttonObjects[i].enabled = true;
                if (crewmateIDs[i] == "Crewmate1")
                {
                    buttonObjects[0].GetComponent<Image>().sprite = sprites[0];

                }
                else if (crewmateIDs[i] == "Crewmate2")
                {
                    buttonObjects[1].GetComponent<Image>().sprite = sprites[1];

                }
                else if (crewmateIDs[1] == "Crewmate3")
                {

                    buttonObjects[2].GetComponent<Image>().sprite = sprites[2];

                }
                else if (crewmateIDs[i] == "Crewmate4")
                {
                    buttonObjects[3].GetComponent<Image>().sprite = sprites[3];

                }

            }
            else
            {
                buttonObjects[i].enabled = false;

                buttonObjects[i].GetComponent<Image>().sprite = sprites[4];


            }


            //crewmateObjects[i].SetActive(false);
           // buttonObjects[i].enabled = false;




                


                // if creewmateID is "crewmate1" then sprite[0]
                // if crewmateID is "crewmate2" then sprite[1] etc etc

            }
        }
    }

