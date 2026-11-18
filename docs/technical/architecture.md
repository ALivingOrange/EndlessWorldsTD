# Architecture
[↑Index](../index.md)

## All Game States
`MainMenu`
`Loading`
`Play`
`Paused` (both player pausing and during World Choice)
`GameOver`

## GameManager
### Purpose
Coordinator responsible for tracking shared states and values which many other scripts are dependent upon. Always loaded regardless of active scenes (in its own persistent scene)

### Main Responsibilities
- Tracking Game State
- Tracking active worlds
- Carrying inventory (since inventory is shared across worlds)
- Tracking world progression (how many completed)
- Instantiating and removing new worlds

### Communication
Raised events: OnGameStateChanged, OnWorldFocus, OnWorldCompleted, OnWorldLost
Public functions: ResourceSpend/ResourceGain, FocusWorld, StartWorld (from a world after generation when it's ready), Pause
