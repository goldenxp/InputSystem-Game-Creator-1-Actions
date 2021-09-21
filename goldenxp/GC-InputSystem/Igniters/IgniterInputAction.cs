namespace GameCreator.Core
{
	using UnityEngine.InputSystem;

	public class IgniterInputAction : Igniter 
	{
		#if UNITY_EDITOR
		public new static string NAME = "Input System/Input Action Triggered";
		public new static string COMMENT = "Check if assigned Input Action has been triggered (or not)";
		#endif

		public InputAction action;
		public bool negate;
		
		protected new void OnEnable()
		{
			base.OnEnable();
			action.Enable();
		}
		
		protected void OnDisable()
		{
			action.Disable();
		}

		private void Update()
		{
			if (negate ^ action.triggered)
			{
				this.ExecuteTrigger(gameObject);
			}
		}
	}
}