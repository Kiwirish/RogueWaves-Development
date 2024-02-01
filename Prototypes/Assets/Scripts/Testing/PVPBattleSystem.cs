using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PVPState { START, PLAYER1TURN, PLAYER2TURN, WIN, LOSE, IDLE }


public class PVPBattleSystem : MonoBehaviour
{

    public PVPState state;

    public Text dialogueText;

    public GameObject player1;
    public GameObject player2;

    Unit player1Unit;
    Unit player2Unit;

    // Start is called before the first frame update
    void Start()
    {

        state = PVPState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {
        GameObject player1GO = player1;
        player1Unit = player1GO.GetComponent<Unit>();

        GameObject player2GO = player2;
        player2Unit = player2GO.GetComponent<Unit>();

        dialogueText.text = "Battle Begins!";

        yield return new WaitForSeconds(3f);
        player1Turn();
    }

    void player1Turn()
    {
        dialogueText.text = "Player 1s Turn";
        state = PVPState.PLAYER1TURN;
    }

    void player2Turn()
    {
        dialogueText.text = "Player 2s Turn";
        state = PVPState.PLAYER2TURN;
    }

    public void player1Attacks(){
        dialogueText.text = "Player 1 Fires";
    }

    public void player2Attacks(){
        dialogueText.text = "Player 2 Fires";
    }


    public IEnumerator endPlayer1Turn()
    {
        bool isDead = player2Unit.TakeDamage(player1Unit.damage);
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //win
            state = PVPState.WIN;
            EndBattle();
        }
        else
        {
            //enemyTurn
            state = PVPState.PLAYER2TURN;
            player2Turn();
        }

    }

    public IEnumerator endPlayer2Turn()
    {   

        bool isDead = player1Unit.TakeDamage(player2Unit.damage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //lose
            state = PVPState.LOSE;
            EndBattle();
        }
        else
        {
            //enemyTurn
            state = PVPState.PLAYER1TURN;
            player1Turn();
        }

    }

    void EndBattle()
    {
        if (state == PVPState.WIN){
            dialogueText.text = "Player 1 Wins";
        }else if(state == PVPState.LOSE){
            dialogueText.text = "Player 2 Wins";
        }else{
            Debug.Log("Something went wrong");
        }
        SceneManager.LoadScene("MainMenu");
    }

}
