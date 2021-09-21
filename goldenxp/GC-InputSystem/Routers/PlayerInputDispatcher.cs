using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameCreator.Core;

[AddComponentMenu("Game Creator/Input/Player Input Dispatcher")]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputDispatcher : MonoBehaviour
{
	[Tooltip("Prefix for Event Names. Allows event name customization for the Game Creator's Event Manager.")]
	public string prefix = "On";
	
	private PlayerInput playerInput;
	// Event names for the Event Dispatch Manager (names are built off the Action Names so clashes could occur)
	private Dictionary<Guid, string> eventNames;
	
	public void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		BuildEventNames();
	}
	
	public void BuildEventNames()
	{
		if (eventNames == null)
			eventNames = new Dictionary<Guid, string>();
		else
			eventNames.Clear();
		
		foreach (var action in playerInput.actions)
		{
			eventNames[action.id] = prefix + action.name;
		}
	}
	
	public void OnEnable()
	{
		foreach (var actionMap in playerInput.actions.actionMaps)
		{
			actionMap.actionTriggered += HandleActionTriggered;
		}
		
	}
	
	public void OnDisable()
	{
		foreach (var actionMap in playerInput.actions.actionMaps)
		{
			actionMap.actionTriggered -= HandleActionTriggered;
		}
	}
	
	protected void HandleActionTriggered(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		
		var action = context.action;
		var name = eventNames[action.id];
		EventDispatchManager.Instance.Dispatch(name, gameObject);
	}
}
