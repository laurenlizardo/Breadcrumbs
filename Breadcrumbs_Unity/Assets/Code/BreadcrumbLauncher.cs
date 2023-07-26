using UnityEngine;

public class BreadcrumbLauncher : MonoBehaviour
{
    [SerializeField] private Breadcrumb _breadcrumb;
    [SerializeField] private Animal _animal;
    
    private void StartOver()
    {
        _breadcrumb.Retrieve();
        _breadcrumb.Clear();
        _animal.ResetPosition();
    }

    private void Update()
    {
        if (TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right,
                out TiltFive.WandDevice wandDevice))
        {
            if (wandDevice.One.wasPressedThisFrame)
            {
                if (!_breadcrumb.IsActive)
                {
                    _breadcrumb.Launch(); 
                }
            }

            if (wandDevice.Two.wasPressedThisFrame)
            {
                if (_breadcrumb.IsActive)
                {
                    _breadcrumb.Retrieve();
                }
            }
            
            if (wandDevice.X.wasPressedThisFrame)
            {
                StartOver();
            }

            if (wandDevice.B.wasPressedThisFrame)
            {
                Application.Quit();
            }
        }
    }
}