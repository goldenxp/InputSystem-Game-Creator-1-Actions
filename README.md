# InputSystem-Game-Creator-1-Actions
Game Creator Actions (and other classes) for Unity's Input System

## Instructions
* Install Game Creator.
* Use Unity's Package Manager to install Input System 1.0.2 from the Unity Registry
* Download this code into your Assets folder.
* (Recommended) Add a Player Input component to your scene and use it to create a default Input Actions asset.

## Documentation
### Input Action Triggered
This helps with quick ad-hoc hook ups like keys for testing or inputs that only work in some situations. For actual user-playable actions, consider using ....

### Input Action Triggered (Reference)
This checks Input Actions that are stored in an asset. This allows for better sharing and opens up other possibilities like rebinding.

### Player Input Dispatcher
This takes a Player Input component and routes all performed Input Actions into Game Creator Events. This way you can easily use inputs with `On Event Receive` triggers.

Note: You can actually use the PlayerInput component to invoke Unity Events that can directly invoke Triggers but this class helps skip an extra step.

### Switch Map Action
Takes the provided Player Input component and lets you switch to a different Action Map (enabling the new and disabling prior set of inputs)
