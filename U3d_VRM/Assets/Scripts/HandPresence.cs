using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    private InputDevice _rightController;
    private InputDevice _leftController;

    public bool rightController;
    public bool leftController;

    public bool rightGrip;
    public bool leftGrip;
    public float rightGripValue;
    public float leftGripValue;
    
    public bool rightTrigger;
    public bool leftTrigger;
    public float rightTriggerValue;
    public float leftTriggerValue;
    
    public Vector2 rightP2Daxis;
    public Vector2 leftP2Daxis;
    
    public bool leftPrimaryButtonPressed;
    public bool rightPrimaryButtonPressed;

    private void Start()
    {
        var characteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        StartCoroutine(RepeatGetDevice(d => { _rightController = d; }, characteristics));

        characteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        StartCoroutine(RepeatGetDevice(d => { _leftController = d; }, characteristics));
    }

    private void Update()
    {
        rightController = _rightController != null;
        leftController = _leftController != null;

        _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out  rightPrimaryButtonPressed);
        _rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGrip);
        _rightController.TryGetFeatureValue(CommonUsages.grip, out rightGripValue);
        _rightController.TryGetFeatureValue(CommonUsages.triggerButton, out rightTrigger);
        _rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue);
        _rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightP2Daxis);

        _leftController.TryGetFeatureValue(CommonUsages.primaryButton, out leftPrimaryButtonPressed);
        _leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGrip);
        _leftController.TryGetFeatureValue(CommonUsages.grip, out leftGripValue);
        _leftController.TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger);
        _leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue);
        _leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftP2Daxis);
    }

    private IEnumerator RepeatGetDevice(Action<InputDevice> device, InputDeviceCharacteristics characteristics)
    {
        var devices = new List<InputDevice>();

        do
        {
            yield return null;
            InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
            if (devices.Count > 0)
                device?.Invoke(devices[0]);
        } while (devices.Count == 0);

        Debug.Log($"{devices[0].name} : {devices[0].characteristics}");
    }
}