using System;
using TMPro;
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

    [SerializeField] private bool _isActive;

    [SerializeField] private TMP_Text _tmpText;
    
    public bool IsActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsActive)
        {
            FollowLauncher();
        }
        
        // if (transform.position.y <= .05f)
        // {
        //     transform.position = new Vector3(transform.position.x, .05f, transform.position.z);
        // }
    }

    public void MakeActive()
    {
        _rigidbody.useGravity = true;
        IsActive = true;
    }

    public void MakeInactive()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        IsActive = false;
    }

    public void FollowLauncher()
    {
        transform.position = _breadcrumbLauncher.transform.position;
        transform.rotation = _breadcrumbLauncher.transform.rotation;
    }

    public void UpdateTmpText(string text)
    {
        _tmpText.text = string.Format("Breadcrumbs eaten:\n{0}", text);
    }
}