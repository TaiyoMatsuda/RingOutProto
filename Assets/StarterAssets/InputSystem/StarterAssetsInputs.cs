using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		public Vector2 Move { get; set; }
        public Vector2 Look { get; set; }
        public bool Jump { get; set; }
        public bool LeftPunch { get; set; }
        public bool RightPunch { get; set; }
        public bool Block { get; private set; }
		public bool AirRun { get; set; }

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnLeftPunch(InputValue value)
		{
			LeftPunchInput(value.isPressed);
		}

		public void OnRightPunch(InputValue value)
		{
			RightPunchInput(value.isPressed);
		}

        public void OnBlock(InputValue value)
        {
            BlockInput(value.isPressed);
        }

        public void OnAirRun(InputValue value)
        {
            AirRunInput(value.isPressed);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
		{
            Move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
            Look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
            Jump = newJumpState;
		}

		public void RightPunchInput(bool newRightPunchState)
		{
            RightPunch = newRightPunchState;
		}

		public void LeftPunchInput(bool newLeftPunchState)
		{
            LeftPunch = newLeftPunchState;
		}

        public void BlockInput(bool newBlockState)
        {
            Block = newBlockState;
        }

        public void AirRunInput(bool newAirRunState)
        {
            AirRun = newAirRunState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}