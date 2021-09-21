namespace GameCreator.Core
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.InputSystem;
	#if UNITY_EDITOR
	using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionSwitchActionMap : IAction
	{
		public PlayerInput playerInput;
		public string actionMap;

		public override bool InstantExecute(GameObject target, IAction[] actions, int index)
		{
			playerInput.SwitchCurrentActionMap(actionMap);
			return true;
		}

		#if UNITY_EDITOR
		public static new string NAME = "Input System/Switch Action Map";
		private const string NODE_TITLE = "Change {0} to {1} Action Map";
			
		private const string PROP_PI = "playerInput";
		private const string PROP_AMN = "actionMap";
		
		private SerializedProperty spPlayerInput;
		private SerializedProperty spActionMap;
		private readonly GUIContent labelActionMap = new GUIContent("Action Map");
		private GUIContent[] options;
		private int selectedActionMapIndex;
		private bool initialized;
		
		public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE, 
				playerInput != null ? playerInput.name : "???",
				(options != null && options.Length > 0 && selectedActionMapIndex != -1) ? options[selectedActionMapIndex].text : "???");
		}
		
		protected override void OnEnableEditorChild()
		{
			spPlayerInput = serializedObject.FindProperty(PROP_PI);
			spActionMap = serializedObject.FindProperty(PROP_AMN);
		}
		
		protected override void OnDisableEditorChild()
		{
			spPlayerInput = null;
			spActionMap = null;
		}
		
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(spPlayerInput);
			if (EditorGUI.EndChangeCheck() || !initialized)
			{
				serializedObject.ApplyModifiedProperties();
				PlayerInput pi = (PlayerInput) spPlayerInput.objectReferenceValue;
				if (pi == null || pi.actions == null || pi.actions.actionMaps.Count == 0)
				{
					options = null;
					selectedActionMapIndex = -1;
				}
				else
				{
					var maps = pi.actions.actionMaps;
					options = new GUIContent[maps.Count];
					InputActionMap sMap = pi.actions.FindActionMap(spActionMap.stringValue);
					if (sMap == null) sMap = pi.currentActionMap;
					int i = 0;
					foreach (InputActionMap map in maps)
					{
						options[i] = new GUIContent(map.name);
						if (map == sMap) selectedActionMapIndex = i;
						i++;
					}
				}
				serializedObject.Update();
				initialized = true;
			}
			
			if (playerInput != null && playerInput.actions != null && options != null && options.Length > 0)
			{
				int selection = EditorGUILayout.Popup(labelActionMap, selectedActionMapIndex, options);
				if (selection != selectedActionMapIndex)
				{
					// We display the name in the inspector but actually store the id for flexibility
					InputActionMap map = playerInput.actions.FindActionMap(options[selection].text);
					if (map != null)
						spActionMap.stringValue = map.id.ToString();
					selectedActionMapIndex = selection;
				}
			}
			else
			{
				EditorGUILayout.HelpBox("Please add a Player Input with an Actions Asset first.", MessageType.None);
			}
			
			serializedObject.ApplyModifiedProperties();
		}
		
		#endif
	}
}
