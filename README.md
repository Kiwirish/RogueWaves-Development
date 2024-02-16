**Rogue Waves - Team Minotaur **

**Game Description: **

_RogueWaves: A Swashbuckling Turn-Based Adventure_
Set sail on a thrilling journey across the high seas in RogueWaves, a captivating 2D pirate-themed game. Command your very own pirate ship and engage in exhilarating turn-based combat against rival vessels. With projectile shooting and ship movement phases, each battle promises a strategic and immersive experience.

_Main Goal:_
Your mission is to defeat enemy ships at each level and rescue lost crewmates. With every crewmember added, your ship gains powerful upgrades, enhancing your abilities to face the fiercest challenges on the high seas. But beware, the dreaded final boss of the seven seas awaits!

_Gameplay Mechanics:_
RogueWaves features a unique turn-based combat system, with both player and enemy having distinct shooting and movement phases. The battle system is designed to change the game's state, allowing or executing specific code based on the current phase. Players have limited time to move their ship and must strategically plan their shots to emerge victorious.

_Crewmates and Power Ups:_
Rescuing crewmates is at the heart of RogueWaves. Each crewmate brings a unique powerup, such as healing abilities, doubling damage, reducing received damage, or making the enemy AI less accurate. These powerups are integral to your strategy as you progress through increasingly difficult levels.

_Shooting Mechanics:_
Inspired by games like Angry Birds and Raft Wars, the shooting mechanic is intuitive yet challenging. Players can aim and fire their cannon using the mouse, with a dynamic trajectory arc showing the shot's path. Enemy AI uses randomness and calculated shots to keep the battles fair and unpredictable.

_Art, Story, and Sound:_
RogueWaves features charming pixel art, a light-hearted story, and upbeat music to create a positive atmosphere. The game's art style is designed to be accessible to all ages, with varied backgrounds and a unique font that complements the pirate theme.

_Multiplayer Modes:_
Experience the thrill of competition in PVP and PVC game modes, where you can test your skills against other players or the computer.

Embark on a swashbuckling adventure in RogueWaves, where strategy, skill, and a bit of luck are your keys to triumph on the high seas!

**Instructions:**

Click Start then Campaign to begin your journey, Levels can be accessed in any order. however there is a suggested path based on the difficulty indicator to guide you through the levels. 

Choose a level to begin rescuing the crewmates.  

Each turn is split into a Shooting and Movement phase

To shoot, place your cursor around the ship, hold down the left mouse button and drag your cursor backwards (to the left) to fire a shot which will go through the white arc. Vertical movement changes the angle, while horizontal movement changes the power of the shot.

After shooting you may move. Use A/D or arrow keys to move horizontally. During the player movement phase, you get 3 seconds to change positions.

Hold and drag left mouse button to shoot

A/D or arrow keys to move

Power Ups are a game mechanic that are used during the firing phase and can add effects to the Player and Enemy.

To activate a crewmate powerup, click on the face icon during your shooting phase and it will turn green. When the turn finishes, it will turn red, signalling it cannot be used and is on cooldown. When the face regains its natural colour it means the powerup is usable again.

Gain crewmates and learn about their abilities through trial and error!

**Known Issues**

- error: ‘Occasional NullReferenceException for CrewManager.GainCrewmate()’. Sometimes on level win, crewmate doesn't instantly replace question mark crewmate, but is saved as a gained crewmate and still usable.

- Final protective pete crewmate doesn't halve enemy damage.

- On game run error: ‘The variable level1Tick of LevelSelectManager has not been assigned.’ when it’s identical to other ‘tick’ objects in assignment.

- Pause menu only works when zoomed in.

- Crewmate objects are saved only once, so if all gained instantly from PVP, they stay across other levels. If only some are gained on other levels, only those crewmates are available in PVC.

- PVP only works when it’s the first scene run. After any other levels, it’s broken. Must re-run game to work. 





RogueWaves is pirate-themed 2D game, you command your very own pirate ship in battles against rival vessels. Prepare to engage in turn-based combat with projectile shooting and ship movement phases. The main goal of the RogueWaves is to defeat each level's enemy ship and collect crewmates after each victory. With every crewmember added, your ship gains powerful upgrades to face the fiercest challenges on the high seas. During shooting phase drag and aim your cannon and let go to fire. During the movement phase use a/d or arrow keys to move left and right. Select crewmate abilities during the shooting phase to activate them.
