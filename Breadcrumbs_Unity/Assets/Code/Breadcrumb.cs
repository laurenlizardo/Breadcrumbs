using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Breadcrumb : MonoBehaviour
{
    // States:
    // 1. Ready: Breadcrumb is at the tip of the wand ready to launch
    // 2. Launched: Player pressed the button to release the breadcrumb
    //    and the breadcrumb is "in the air"
    // 3. Landed: Breadcrumb is on the ground
    // 4. Eaten: Animal reached the breadcrumb, ate it, and now the
    //    breadcrumb is gone/eaten

    [SerializeField] private BreadcrumbLauncher _breadcrumbLauncher;
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    public bool IsActive;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (IsActive)
        {
            _rigidbody.useGravity = true;
        }
        else
        {
            _rigidbody.useGravity = false;
            transform.position = _breadcrumbLauncher.transform.position;
            transform.rotation = _breadcrumbLauncher.transform.rotation;
        }
    }
}