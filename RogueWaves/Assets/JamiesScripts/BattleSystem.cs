using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum BattleState { START, PLAYERSHOOT, PlAYERMOVE, ENEMYSHOOT, ENEMYMOVE,  WIN, LOSE, IDLE }


public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public Text dialogueText;
    public Font customFont; 

    public GameObject player;
    public GameObject enemy;

    public GameObject gainedCrewmate;


    Unit playerUnit;
    Unit enemyUnit;

    [SerializeField] public EnemyMovement enemyMove;
    [SerializeField] public EnemyShooting enemyShoot;

    // Start is called before the first frame update
    void Start()
    {
        // checking if the font is assigned &
        // if so, set future text pop ups to that font 
        if(customFont != null)
        {
            dialogueText.font = customFont;
        }

        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = player;
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = enemy;
        enemyUnit = enemyGO.GetComponent<Unit>();
        //dialogueText.font = customFont;
        dialogueText.text = "Battle Begins!";

        yield return new WaitForSeconds(1f);
        playerShoot();
    }

    public void gameIdle()
    {
        state = BattleState.IDLE;
    }

    void playerShoot()
    {
        dialogueText.text = "Player Shoot";
        state = BattleState.PLAYERSHOOT;
    }

    public IEnumerator endPlayerShoot()
    {

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
            //PlayerMove
            StartCoroutine(playerMove());
        }
    }

    IEnumerator playerMove()
    {
        dialogueText.text = "Player Move";
        state = BattleState.PlAYERMOVE;

        yield return new WaitForSeconds(3f);

        StartCoroutine(enemyTurn());
    }

     IEnumerator enemyTurn(){

        yield return StartCoroutine(EnemyShoot());
        yield return StartCoroutine(EnemyMove());
        playerShoot();
    }

 


    IEnumerator EnemyShoot()
    {
      
        // enemyMove = enemy.GetComponent<EnemyMovement>();
        // enemyShoot = enemy.GetComponent<EnemyShooting>();

        state = BattleState.ENEMYSHOOT;
        dialogueText.text = "Enemy Shoot";
        yield return StartCoroutine(enemyShoot.EnemyShoot());

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        if (isDead)
        {
            //lose
            state = BattleState.LOSE;
            yield return StartCoroutine(EndBattle());
        }
    }

    IEnumerator EnemyMove(){
            dialogueText.text = "Enemy Moves";
            yield return StartCoroutine(enemyMove.EnemyMove());
    }

    public IEnumerator EndBattle()
    {
        if (state == BattleState.WIN)
        {
            dialogueText.text = "You Win";

            if (gainedCrewmate != null && !gainedCrewmate.activeSelf)
            {

                gainedCrewmate.SetActive(true);
                CrewManager.Instance.GainCrewmate(gainedCrewmate);

            }



        }
        else if (state == BattleState.LOSE)
        {
            dialogueText.text = "You Lose";
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("LevelSelect");
    }

}
