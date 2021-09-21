namespace GameCreator.Core
{
	using UnityEngine;
	using UnityEngine.InputSystem;

	public class IgniterInputActionReference : Igniter 
	{
		#if UNITY_EDITOR
		public new static string NAME = "Input System/Input Action Triggered (Reference)";
		public new static string COMMENT = "Check if the referenced Input Action has been triggered (or not). " +
			"Does not control its enabled state.";
		#endif
		
		public InputActionReference actionRef;
		public bool negate;

		private void Update()
		{
			if (negate ^ actionRef.action.triggered)
			{
				this.ExecuteTrigger(gameObject);
			}
		}
	}
}