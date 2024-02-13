using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public enum BattleState { START, PLAYERSHOOT, PlAYERMOVE, ENEMYTURN,  WIN, LOSE, IDLE }


public class BattleSystem : MonoBehaviour
{

    public CrewManager crewmanager;

    public LevelSelectManager lvl;

    public BattleState state;

    public WorldMapPlayer worldmap;

    public Text dialogueText;
    public Font customFont; 

    public GameObject player;
    public GameObject enemy;

    public CameraSwitch cam;

    public int turnvalue = 1;

    // to be moved to another script for code tidiness, here temporarily for functionality

    public GameObject tick1; 

    //public GameObject gainedCrewmate;

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

        cam.ActivateMainCamera();
        yield return new WaitForSeconds(3f);

        cam.ActivateFollowCamera();

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
        StartCoroutine(EnemyTurn());
    }



    IEnumerator EnemyTurn()
    {
      
        // enemyMove = enemy.GetComponent<EnemyMovement>();
        // enemyShoot = enemy.GetComponent<EnemyShooting>();

        state = BattleState.ENEMYTURN;
        dialogueText.text = "Enemy Shoot";
        yield return StartCoroutine(enemyShoot.EnemyShoot());

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        if (isDead)
        {
            //lose
            state = BattleState.LOSE;
            yield return StartCoroutine(EndBattle());
        }else{
            dialogueText.text = "Enemy Moves";
            yield return StartCoroutine(enemyMove.EnemyMove());
            turnvalue++;
            crewmanager.resetPowerupsForNextTurn();
            playerShoot();
            enemy.GetComponent<Unit>().damage = 1;
        }
    }

 

    public IEnumerator EndBattle()
    {

        if (state == BattleState.WIN)
        {
            dialogueText.text = "You Win";

            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "Level"){
                //int levelNum = 1;

                CrewManager.Instance.GainCrewmate("Crewmate1");
                //LevelSelectManager.Instance.MarkLevelComplete(levelNum - 1);
                //tick1.SetActive(true);
                //LevelSelectManager.Instance.GainTick("Level");
                //LevelSelectManager.ShowTickForLevel(sceneName);
                //worldmap.level1.SetActive(false);

            }else if (sceneName == "Level2") {

                CrewManager.Instance.GainCrewmate("Crewmate2");
                //worldmap.level2.SetActive(false);


            }
            else if (sceneName == "Level3"){

                CrewManager.Instance.GainCrewmate("Crewmate3");
                //worldmap.level3.SetActive(false);


            }
            else if (sceneName == "Level4"){

                CrewManager.Instance.GainCrewmate("Crewmate4");
                //worldmap.level4.SetActive(false);

            }


        }
        else if (state == BattleState.LOSE)
        {
            dialogueText.text = "";
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("LevelSelect");
    }

}
