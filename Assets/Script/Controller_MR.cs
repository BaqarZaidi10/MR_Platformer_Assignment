using StarterAssets;
using UnityEngine;
using UnityEngine.XR;

public class Controller_MR : MonoBehaviour
{

    Vector2 moveVector;
    bool jumping;
    float sprint;

    [SerializeField]
    StarterAssetsInputs starterAssetsInputs;

    private void Update()
    {
        var leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        var rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveVector);
        rightController.TryGetFeatureValue(CommonUsages.trigger, out sprint);
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out jumping);

        starterAssetsInputs.MoveInput(moveVector);
        starterAssetsInputs.JumpInput(jumping);
        starterAssetsInputs.SprintInput(sprint != 0);
    }

}