using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{   

    [SerializeField] public EnemyShooting enemyShooting;
    [SerializeField] public Launcher2 playerShooting;
    [SerializeField] public CrewManager crewmanager;

    [SerializeField] public int numberOfMaps = 8;

    private int map, difficulty, powerups;
    private GameObject[] backgrounds;

    public bool isPVP;
    

    void Start(){

        map = PlayerPrefs.GetInt("Map", 0);
        difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        powerups = PlayerPrefs.GetInt("Powerups", 0);
        Debug.Log("Selected map: " + map + ", difficulty: " + difficulty + ", powerups: " + powerups);

        backgrounds = new GameObject[numberOfMaps];

        backgrounds[0] = GameObject.Find("FlatOcean"); // BOTH PVP/PVC
        backgrounds[1] = GameObject.Find("RockyShores"); // BOTH PVP/PVC
        backgrounds[2] = GameObject.Find("SmallHighWaterfall"); //BOTH PVP/PVC
        backgrounds[3] = GameObject.Find("SmallLowWaterfall"); // BOTH PVP/PVC
        backgrounds[4] = GameObject.Find("TwoWaterfalls"); // BOTH PVP/PVC (maybe not if I'm using angular drag?)
        backgrounds[5] = GameObject.Find("RockierShores"); // PVP ONLY
        backgrounds[6] = GameObject.Find("TallHighWaterfall"); // PVP ONLY
        backgrounds[7] = GameObject.Find("TallLowWaterfall"); // PVP ONLY

        ResetMaps();
        ApplyMap();
        if(!isPVP){
            ApplyDifficulty();
            ApplyPowerups();
        }
        
    }

    void ApplyMap(){
        backgrounds[map].SetActive(true);
    }

    void ApplyDifficulty(){
        
        switch (difficulty){
            case 0:
                enemyShooting.xOffset = 10f;
                break;
            case 1:
                enemyShooting.xOffset = 8f;
                break;
            case 2:
                enemyShooting.xOffset = 5f;
                break;
            case 3:
                enemyShooting.xOffset = 1f;
                break;
            default:
                break;
        }
    }

    void ApplyPowerups(){
        switch (powerups){
            case 0:
                // do nothing
                break;
            case 1:
                Debug.Log("Done");
                crewmanager.GainCrewmate("Crewmate1"); // remove later (for testing)
                crewmanager.GainCrewmate("Crewmate2"); // remove later (for testing)
                crewmanager.GainCrewmate("Crewmate3"); // remove later (for testing)
                crewmanager.GainCrewmate("Crewmate4"); // remove later (for testing)
                break;
            default:
                break;
        }
    }

    void ResetMaps(){
        for(int i = 0; i < numberOfMaps; i++){
            backgrounds[i].SetActive(false);
        }
    }
}
