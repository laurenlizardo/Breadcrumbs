using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreadcrumbLauncher : MonoBehaviour
{
    [SerializeField] private Breadcrumb _breadcrumb;
    [SerializeField] private Animal _animal;
    [SerializeField] private float _launchPower;
    
    public void LaunchBreadcrumb()
    {
        _breadcrumb.MakeActive();
        _breadcrumb.Rigidbody.AddForce(transform.forward * _launchPower, ForceMode.Force);
    }

    public void RetrieveBreadcrumb()
    {
        _breadcrumb.MakeInactive();
    }

    public void Restart()
    {
        _animal.ResetBreadcrumbs();
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

            if (wandDevice.X.wasPressedThisFrame)
            {
                RetrieveBreadcrumb();
                Restart();
            }

            if (wandDevice.B.wasPressedThisFrame)
            {
                Application.Quit();
            }
        }
    }
}