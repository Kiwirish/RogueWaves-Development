using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE, IDLE }


public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public Text dialogueText;

    public GameObject player;
    public GameObject enemy;

    Unit playerUnit;
    Unit enemyUnit;

    [SerializeField] public EnemyMovement enemyMove;
    [SerializeField] public EnemyShooting enemyShoot;

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

    public void gameIdle(){
        state = BattleState.IDLE;
    }

    void playerTurn()
    {
        dialogueText.text = "Player Turn";
        state = BattleState.PLAYERTURN;
    }

    public IEnumerator endPlayerTurn()
    {
        
        //dialogueText.text = "Player Fires";

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            //win
            state = BattleState.WIN;
            StartCoroutine(EndBattle());
        }
        else
        {
            //enemyTurn
            state = BattleState.ENEMYTURN;
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
            StartCoroutine(EndBattle());
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

        // enemyMove = enemy.GetComponent<EnemyMovement>();
        // enemyShoot = enemy.GetComponent<EnemyShooting>();

        enemyMove.EnemyMove();

        yield return new WaitForSeconds(2f);

        dialogueText.text = "Enemy Fires";

        enemyShoot.EnemyShoot();

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        if (isDead)
        {
            //lose
            state = BattleState.LOSE;
            StartCoroutine(EndBattle());
        }
        else
        {
            //enemyTurn
            state = BattleState.PLAYERTURN;
            playerTurn();
        }

    }



    public IEnumerator EndBattle()
    {
        if (state == BattleState.WIN){
            dialogueText.text = "You Win";
        }else if(state == BattleState.LOSE){
            dialogueText.text = "You Lose";
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }

}
