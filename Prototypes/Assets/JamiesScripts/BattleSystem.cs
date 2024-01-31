using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE, IDLE }


public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public Text dialogueText;

    public GameObject player;
    public GameObject enemy;

    Unit playerUnit;
    Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = player;
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = enemy;
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Battle Begins!";

        yield return new WaitForSeconds(3f);
        playerTurn();
    }

    void playerTurn()
    {
        dialogueText.text = "Player Turn";
        state = BattleState.PLAYERTURN;
    }

    public IEnumerator endPlayerTurn()
    {
        state = BattleState.IDLE;
        dialogueText.text = "Player Fires";

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //win
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            //enemyTurn
            enemyTurn();
        }

    }

    void enemyTurn()
    {
        dialogueText.text = "Enemy Turn";
        state = BattleState.ENEMYTURN;

        StartCoroutine(EnemyAttack());
    }

    IEnumerator PlayerAttack()
    {

        dialogueText.text = "Player Fires";

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //win
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            //enemyTurn
            state = BattleState.ENEMYTURN;
            enemyTurn();
        }

    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(2f);

        dialogueText.text = "Enemy Fires";
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //lose
            state = BattleState.LOSE;
            EndBattle();
        }
        else
        {
            //enemyTurn
            state = BattleState.PLAYERTURN;
            playerTurn();
        }

    }



    void EndBattle()
    {
        if (state == BattleState.WIN)
        {
            dialogueText.text = "Player Wins";
        }
    }

}
