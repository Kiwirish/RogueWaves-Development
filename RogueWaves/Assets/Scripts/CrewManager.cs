using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    public static CrewManager Instance { get; private set; }
    public GameObject crewmate1;
    public GameObject crewmate2;
    public GameObject crewmate3;
    public GameObject crewmate4;

    private List<GameObject> crewmatesGained = new List<GameObject>();

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
    }

    public void GainCrewmate(GameObject crewmate)
    {
        if (!crewmatesGained.Contains(crewmate))
        {
            crewmatesGained.Add(crewmate);
            crewmate.SetActive(true);
        }
    }

    public bool HasCrewmate(GameObject crewmate)
    {
        return crewmatesGained.Contains(crewmate);
    }
}