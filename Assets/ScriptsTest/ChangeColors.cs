using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using TMPro;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class ChangeColors : MonoBehaviour
{
    [SerializeField] private XRInputValueReader<bool> leftPrimeButton;

    [SerializeField] private Rigidbody playerBody;

    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material orangeMaterial;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Action OnLeftTriggerPressed;
    [SerializeField] private Action OnLeftTriggerOmitted;

    [SerializeField] private Action OnLeftGripPressed;
    [SerializeField] private Action OnLeftGripOmitted;

    [SerializeField] private Action OnLeftPrimePressed;
    [SerializeField] private Action OnLeftPrimeOmitted;

    [SerializeField] private Action OnLeftSecondPressed;
    [SerializeField] private Action OnLeftSecondOmitted;

    [SerializeField] private Action OnLeftJoystickChange;
    [SerializeField] private Action OnLeftJoystickZero;

    [SerializeField] private Action OnRightTriggerPressed;
    [SerializeField] private Action OnRightTriggerOmitted;

    [SerializeField] private Action OnRightGripPressed;
    [SerializeField] private Action OnRightGripOmitted;

    [SerializeField] private Action OnRightPrimePressed;
    [SerializeField] private Action OnRightPrimeOmitted;

    [SerializeField] private Action OnRightSecondPressed;
    [SerializeField] private Action OnRightSecondOmitted;

    [SerializeField] private Action OnRightJoystickChange;
    [SerializeField] private Action OnRightJoystickZero;

    private Dictionary<(InputDevice Device, InputFeatureUsage<bool> Usage), (bool LastValue, Action On, Action Off)> XRControllerButtons;
    private Dictionary<(InputDevice Device, InputFeatureUsage<Vector2> Usage), (Vector2 LastValue, Action Change, Action Zero)> XRController2dAxis;

    private InputDevice leftController;
    private InputDevice rightController;

    private void GetXRControllers()
    {
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void SetControllerUsagesDictionaries()
    {
        XRControllerButtons = new Dictionary<(InputDevice Device, InputFeatureUsage<bool> Usage), (bool LastValue, Action On, Action Off)>()
        {
            { (leftController,CommonUsages.primaryButton),(false,OnLeftPrimePressed,OnLeftPrimeOmitted) },
            { (leftController,CommonUsages.secondaryButton),(false,OnLeftSecondPressed,OnLeftSecondOmitted) },
            { (rightController,CommonUsages.primaryButton),(false,OnRightPrimePressed,OnRightSecondOmitted) }
        };
        XRController2dAxis = new Dictionary<(InputDevice Device, InputFeatureUsage<Vector2> Usage), (Vector2 LastValue, Action Change, Action Zero)>();
    }

    private void SetControllerUsagesEvents()
    {
        OnLeftPrimePressed = () => lineRenderer.material = yellowMaterial;
        OnLeftSecondPressed = () => lineRenderer.material = orangeMaterial;
        OnRightPrimePressed = () => playerBody.AddForce(new Vector3(0,1,0));
    }

    private void Awake()
    {
        SetControllerUsagesEvents();
        GetXRControllers();
        SetControllerUsagesDictionaries();
    }

    private void Update()
    {
        if (!leftController.isValid || !rightController.isValid)
        {
            GetXRControllers();
            SetControllerUsagesDictionaries();
        }
        

        foreach (var buttonData in XRControllerButtons.ToArray())
        {
            bool currentValue;
            bool isSucsess = buttonData.Key.Device.TryGetFeatureValue(buttonData.Key.Usage, out currentValue);
            if (isSucsess && (currentValue!=buttonData.Value.LastValue))
            {
                XRControllerButtons[buttonData.Key] = (currentValue, buttonData.Value.On, buttonData.Value.Off);
                if (currentValue)
                    buttonData.Value.On?.Invoke();
                else
                    buttonData.Value.Off?.Invoke();
            }
        }
        foreach (var axisData in XRController2dAxis)
        {
            Vector2 currentValue;
            if (axisData.Key.Device.TryGetFeatureValue(axisData.Key.Usage, out currentValue) && (currentValue!=axisData.Value.LastValue))
            {
                XRController2dAxis[axisData.Key] = (currentValue, axisData.Value.Change, axisData.Value.Zero);
                if (currentValue != Vector2.zero)
                    axisData.Value.Change.Invoke();
                else
                    axisData.Value.Zero.Invoke();
            }
        }
    }
}
