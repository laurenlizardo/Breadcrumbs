using UnityEngine;
using UnityEngine.InputSystem;

public class BreadcrumbLauncher : MonoBehaviour
{
    [SerializeField] private Breadcrumb _breadcrumb;
    [SerializeField] private float _launchPower;
    [SerializeField] private GameObject _tiltFivePrefab;
    
    public void LaunchBreadcrumb()
    {
        _breadcrumb.IsActive = true;
        _breadcrumb.Rigidbody.AddForce(transform.forward * _launchPower, ForceMode.Force);
    }

    public void RetrieveBreadcrumb()
    {
        _breadcrumb.IsActive = false;
    }

    private void Start()
    {
        if (!_tiltFivePrefab.activeSelf)
        {
            _breadcrumb.transform.position = new Vector3(0, 5, -3);
        }
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