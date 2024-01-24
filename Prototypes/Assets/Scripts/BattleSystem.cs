using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE}

public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        
    }

}
