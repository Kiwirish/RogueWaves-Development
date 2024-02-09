using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{   

    [SerializeField] public int numberOfMaps = 8;

    [SerializeField] public int map = 0;
    [SerializeField] public int difficulty;

    GameObject[] backgrounds;



    //private int mapValue;
    //private int difficultyValue;

    private void Start()
    {   
        backgrounds = new GameObject[numberOfMaps];

        backgrounds[0] = GameObject.Find("FlatOcean");
        backgrounds[1] = GameObject.Find("RockyShores");
        backgrounds[2] = GameObject.Find("RockierShores");
        backgrounds[3] = GameObject.Find("SmallHighWaterfall");
        backgrounds[4] = GameObject.Find("SmallLowWaterfall");
        backgrounds[5] = GameObject.Find("TallHighWaterfall");
        backgrounds[6] = GameObject.Find("TallLowWaterfall");
        backgrounds[7] = GameObject.Find("TwoWaterfalls");

        ResetMaps();
        ApplyMap();

        //mapValue = PlayerPrefs.GetInt("Map");
        //difficultyValue = PlayerPrefs.GetInt("Difficulty");

        //ApplySavedValues();
    }

    private void ApplySavedValues()
    {

        Debug.Log("Map type: " + map);
        Debug.Log("Dropdown 2 Value: " + difficulty);

    }

    void ApplyDifficulty(){

    }

    void ApplyMap(){
        //if(map != null){
            backgrounds[map].SetActive(true);
        //}
    }

    void ResetMaps(){
        for(int i = 0; i < numberOfMaps; i++){
            backgrounds[i].SetActive(false);
        }
    }

    void SetMaps(){
        for(int i = 0; i < numberOfMaps; i++){
            backgrounds[i].SetActive(true);
        }
    }
}
