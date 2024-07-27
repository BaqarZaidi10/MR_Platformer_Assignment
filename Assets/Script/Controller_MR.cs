using StarterAssets;
using UnityEngine;
using UnityEngine.XR;

public class Controller_MR : MonoBehaviour
{
    Vector2 moveVector = Vector2.zero;
    Vector2 moveVectorCam = Vector2.zero;
    bool jumping = false;
    float sprint = 0f;
    bool rightGripPressed = false;

    [SerializeField]
    StarterAssetsInputs starterAssetsInputs;
    
    [SerializeField]
    GameObject objectToMove; // Assign the GameObject you want to move in the inspector

    [SerializeField]
    float objectMoveSpeed = 1f; // Adjust this to change the object's movement speed

    private void Update()
    {
        var leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        var rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveVector);
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveVectorCam);
        rightController.TryGetFeatureValue(CommonUsages.trigger, out sprint);
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out jumping);
        rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripPressed);

        if (rightGripPressed)
        {
            MoveObject();
        }
        else
        {
            starterAssetsInputs.MoveInput(moveVector);
            starterAssetsInputs.JumpInput(jumping);
            starterAssetsInputs.SprintInput(sprint != 0);
        }
    }
    
    private void MoveObject()
    {
        if (objectToMove != null)
        {
            // Move forward and backward with left controller joystick
            Vector3 forwardMovement = new Vector3(moveVector.x, 0, moveVector.y) * objectMoveSpeed * Time.deltaTime;
            
            // Move up and down with right controller joystick
            Vector3 verticalMovement = new Vector3(0, moveVectorCam.y, 0) * objectMoveSpeed * Time.deltaTime;
            
            // Combine the movements
            Vector3 movement = forwardMovement + verticalMovement;
            
            objectToMove.transform.Translate(movement);
        }
        else
        {
            Debug.LogWarning("No object assigned to move!");
        }
    }
}
