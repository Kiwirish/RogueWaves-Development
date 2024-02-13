using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PVPState { START, PLAYER1SHOOT, PLAYER1MOVE, PLAYER2MOVE, PLAYER2SHOOT, WIN, LOSE, IDLE }


public class PVPBattleSystem : MonoBehaviour
{

    public CrewManager crewmanager;

    public PVPState state;

    // public WorldMapPlayer worldmap; 

    public Text dialogueText;
    public Font customFont;

    public GameObject player1;
    public GameObject player2;

    public CameraSwitch cam;

    public int turnvalue = 1; 

    Unit player1Unit;
    Unit player2Unit;

    // Start is called before the first frame update
    void Start()
    {
        if (customFont != null)
        {
            dialogueText.font = customFont;
        }

        state = PVPState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {   

        cam.ActivateMainCamera();
        yield return new WaitForSeconds(3f);

        cam.ActivateFollowCamera();

        GameObject player1GO = player1;
        player1Unit = player1GO.GetComponent<Unit>();

        GameObject player2GO = player2;
        player2Unit = player2GO.GetComponent<Unit>();

        dialogueText.text = "Battle Begins!";

        yield return new WaitForSeconds(3f);
        Player1Shoot();
    }

    public void gameIdle()
    {
        state = PVPState.IDLE;
    }

    void Player1Shoot()
    {
        dialogueText.text = "Player 1 Shoot";
        state = PVPState.PLAYER1SHOOT;
    }

    public IEnumerator endPlayer1Turn()
    {
        bool isDead = player2Unit.TakeDamage(player1Unit.damage);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            //win
            state = PVPState.WIN;
            StartCoroutine(EndBattle());
        }
        else
        {
            //Player1Turn
            StartCoroutine(player1Move());
        }

    }

    IEnumerator player1Move()
    {   
        dialogueText.text = "Player 1s Move";
        state = PVPState.PLAYER1MOVE;

        yield return new WaitForSeconds(3f);
        Player2Shoot();
    }

    void Player2Shoot()
    {
        dialogueText.text = "Player 2 Shoot";
        state = PVPState.PLAYER2SHOOT;
    }

    public IEnumerator endPlayer2Turn()
    {
        bool isDead = player1Unit.TakeDamage(player2Unit.damage);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            //win
            state = PVPState.LOSE;
            StartCoroutine(EndBattle());
        }
        else
        {
            //Player1Turn
            StartCoroutine(player2Move());
        }

    }

    IEnumerator player2Move()
    {   
        dialogueText.text = "Player 2s Move";
        state = PVPState.PLAYER2MOVE;

        yield return new WaitForSeconds(3f);
        Player1Shoot();
    }

    public IEnumerator EndBattle()
    {
        if (state == PVPState.WIN){
            dialogueText.text = "Player 1 Wins";
        }else if(state == PVPState.LOSE){
            dialogueText.text = "Player 2 Wins";
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }

}
