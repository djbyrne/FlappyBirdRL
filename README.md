# FlappyBirdRL
Reinforcement Learning environment for the game Flappy Bird using the Unity ML Agents library

<img src="flappy_rl_demo.gif" height="500">

## Requirements
- Unity version 2019.1.0f2
- ml-agents 0.8.1

## Action Space
 - 1 discrete action "flap": 1 = move up, 0 = do nothing (fall down)
  Note: once the agent has "flapped" they must do nothing for the next action. This simulates the clicking functionality of the original game
 
 ## State Space
 State space comprises of 6 feature vectors normalized between [0,1]
 - agent height
 - agent Y velocity
 - last action taken
 - height of the next top pipe
 - height of the next bottom pipe
 - X distance to next pipe
 
 ## Info
 The game runs at x20 normal speed in order to speed up training. 
