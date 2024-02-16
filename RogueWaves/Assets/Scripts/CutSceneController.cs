using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public float cutsceneDuration = 25;

    void Start()
    {
        StartCoroutine(EndCutsceneAfterTime(cutsceneDuration));
    }

    IEnumerator EndCutsceneAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu"); 
    }
}
