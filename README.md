# Endless Runner (Unity 6)

A 3-lane endless runner built in Unity 6 with C#. Dodge obstacles, jump, and chase a high score.

This is the foundation for a reinforcement learning project: a Unity ML-Agents agent will learn to play the game, and its score will be compared against a human baseline.

**Status:** playable prototype. Slide and overhead barriers are in progress. The RL agent is planned.

## Demo

<!-- Replace with a 10-15 second clip: running, switching lanes, jumping, crashing -->
![Gameplay](docs/gameplay.gif)

## Features

- Constant forward run with smooth 3-lane switching
- Endless road: the floor follows the player, so the run never ends
- Obstacles spawn ahead of the player in random lanes and despawn behind it
- Jump with hand-written gravity
- Crash detection: hitting an obstacle restarts the scene
- Distance score with a best score that persists between runs

## Controls

| Action | Keys |
| --- | --- |
| Move left / right | `A` / `D` or Left / Right arrows |
| Jump | `Space`, `W` or Up arrow |

## Getting started

**Requirements:** Unity 6 (6000.0.77f1) with the URP template.

1. Clone the repo.
2. In Unity Hub, choose **Add → Add project from disk** and select the cloned folder.
3. Open `Assets/Scenes/SampleScene`.
4. Press **Play**, then **click inside the Game view** so it receives keyboard input.

**Troubleshooting**

- Input error in the Console: set **Edit → Project Settings → Player → Other Settings → Active Input Handling** to **Both**.
- Restart does nothing after a crash: add the scene under **File → Build Profiles → Scene List → Add Open Scenes**.

## How it works

| Script | Role |
| --- | --- |
| `PlayerController` | Forward motion, lane switching, jump physics, crash handling |
| `ObstacleSpawner` | Places obstacles ahead of the player in a random lane and destroys them after a fixed lifetime |
| `Obstacle` | Marker component that identifies what counts as a crash |
| `FloorFollow` | Keeps the road under the player so it never ends |
| `ScoreDisplay` | Shows distance travelled and stores the best score with `PlayerPrefs` |

**Design notes**

- The player is a **kinematic Rigidbody** moved by script. Unity's gravity doesn't apply, so jump and gravity are integrated manually in `Update`.
- Obstacles use **trigger colliders**. Unity only raises `OnTriggerEnter` when at least one of the two objects has a Rigidbody, which is why the player carries one.
- Score is distance travelled along Z since the start of the run.
- Obstacles are spawned from a prefab, so changes to the prefab apply to every future spawn.

## Tunable parameters

Exposed in the Inspector on the relevant components.

| Parameter | Component | Default |
| --- | --- | --- |
| Forward Speed | `PlayerController` | 10 |
| Lane Width | `PlayerController` | 2 |
| Lane Change Speed | `PlayerController` | 15 |
| Jump Velocity | `PlayerController` | 13 |
| Gravity | `PlayerController` | 30 |
| Spawn Interval | `ObstacleSpawner` | 1 s |
| Spawn Distance Ahead | `ObstacleSpawner` | 60 |

## Problems solved

- **No keyboard input in Play mode:** the Game view needs focus before Unity sends it keys.
- **Collisions never fired:** the obstacle collider wasn't a trigger and the `Obstacle` script was missing from the prefab. Fixed on the prefab itself, then **Overrides → Apply All**.
- **Compile error on `SceneManager`:** it lives in `UnityEngine.SceneManagement`, which needs its own `using` line.
- **Road ran out after about 50 seconds:** replaced the fixed floor with a floor that follows the player.

## Roadmap

- [x] Forward run and 3-lane movement
- [x] Obstacle spawner with crash and restart
- [x] Endless floor
- [x] Jump
- [x] Distance score and best score
- [ ] Slide and overhead barriers
- [ ] Unity ML-Agents agent with discrete actions (lane, jump, slide) and a survival reward
- [ ] Evaluation: untrained agent vs trained agent vs human best score
- [ ] Trained model exported to ONNX and run as inference in the game

## Project structure

```
Assets/
  Scenes/      SampleScene
  Scripts/     PlayerController, ObstacleSpawner, Obstacle, FloorFollow, ScoreDisplay
  Prefabs/     Obstacle
Packages/
ProjectSettings/
```

## Author

Raj Verma · [github.com/raaajjjjj](https://github.com/raaajjjjj)
