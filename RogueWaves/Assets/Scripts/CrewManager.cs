using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    public static CrewManager Instance { get; private set; }
    //public GameObject crewmate1;
    //public GameObject crewmate2;
    //public GameObject crewmate3;
    //public GameObject crewmate4;

    public HashSet<string> crewmatesGained = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        GainCrewmate("Crewmate1"); // remove later (for testing)
        GainCrewmate("Crewmate2"); // remove later (for testing)
    }

    public void GainCrewmate(string crewmateID)
    {
        if (!crewmatesGained.Contains(crewmateID))
        {
            crewmatesGained.Add(crewmateID);
            Debug.Log($"Crewmate gained: {crewmateID}");

            //crewmate.SetActive(true);
        }
    }

    public bool HasCrewmate(string crewmateID)
    {
        return crewmatesGained.Contains(crewmateID);
    }
}