using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BreadcrumbLauncher : MonoBehaviour
{
    [SerializeField] private Breadcrumb _breadcrumb;
    [SerializeField] private float _launchPower;
    
    public void LaunchBreadcrumb()
    {
        _breadcrumb.IsActive = true;
        _breadcrumb.Rigidbody.AddForce(transform.forward * _launchPower, ForceMode.Force);
    }

    public void RetrieveBreadcrumb()
    {
        _breadcrumb.IsActive = false;
    }

    private void Update()
    {
        if (TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right,
                out TiltFive.WandDevice wandDevice))
        {
            if (wandDevice.One.wasReleasedThisFrame)
            {
                LaunchBreadcrumb();
            }

            if (wandDevice.Two.wasPressedThisFrame)
            {
                RetrieveBreadcrumb();
            }
        }
    }
}