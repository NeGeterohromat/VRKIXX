using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class JumpTest : MonoBehaviour
{
    [SerializeField] private XRInputValueReader<float> rightPrimeButton;
    [SerializeField] private XRInputValueReader<Vector2> leftAxis;

    [SerializeField] private CharacterController playerController;

    [SerializeField]private float jumpHeight = 1f;
    private Vector3 currentJumpVelocity = Vector3.zero;
    private float gravityValue = Physics.gravity.y;

    private int grabCount = 0;

    public void OnStartGrabbing() => grabCount += 1;

    public void OnFinishGrabbing() => grabCount -= 1;

    private void Update()
    {
        var isGrounded = playerController.isGrounded;

        if (rightPrimeButton != null)
        {
            var leftAxisState = Vector2.zero;
            if (leftAxis != null)
                leftAxisState = leftAxis.ReadValue();
            var rightPrimeState = (ButtonStates)rightPrimeButton.ReadValue();
            if (rightPrimeState == ButtonStates.On && isGrounded)
                currentJumpVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            if (leftAxisState == Vector2.zero)
                currentJumpVelocity.y += gravityValue * Time.deltaTime;
            if (grabCount == 0)
                playerController.Move(currentJumpVelocity * Time.deltaTime);
        }
    }
}
