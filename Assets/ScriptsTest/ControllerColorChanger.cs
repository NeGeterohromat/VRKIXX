using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class ControllerColorChanger : MonoBehaviour
{
    [SerializeField] private XRInputValueReader<float> leftPrimeButton;



    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material greenMaterial;

    public enum CurrentMaterial
    {
        Green,
        Yellow
    }

    private CurrentMaterial currentMaterial;
    private ButtonStates lastLeftPrimeState;

    private void Update()
    {
        if (leftPrimeButton != null)
        {
            var leftPrimeState = (ButtonStates)leftPrimeButton.ReadValue();
            if (leftPrimeState != lastLeftPrimeState)
            {
                lastLeftPrimeState = leftPrimeState;
                if (leftPrimeState == ButtonStates.On)
                {
                    if (currentMaterial == CurrentMaterial.Yellow)
                    {
                        lineRenderer.material = greenMaterial;
                        currentMaterial = CurrentMaterial.Green;
                    }
                    else
                    {
                        lineRenderer.material = yellowMaterial;
                        currentMaterial = CurrentMaterial.Yellow;
                    }
                }    
            }

        }
    }
}

public enum ButtonStates
{
    Off = 0,
    On = 1
}
