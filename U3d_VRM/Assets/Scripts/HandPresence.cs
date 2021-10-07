using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    private InputDevice _rightController;
    private InputDevice _leftController;
    
    void Start()
    {
        var characteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        StartCoroutine(RepeatGetDevice(_rightController, characteristics));
        
        characteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        StartCoroutine(RepeatGetDevice(_leftController, characteristics));
    }

    private IEnumerator RepeatGetDevice(InputDevice device, InputDeviceCharacteristics characteristics )
    {
        var devices = new List<InputDevice>();

        do
        {
            yield return null;
            InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
            if (devices.Count > 0)
                device = devices[0];
        } while (devices.Count == 0);
        
        Debug.Log($"{device.name} : {device.characteristics}");
    }
}