using TMPro;
using UnityEngine;

public class Breadcrumb : MonoBehaviour
{
    [SerializeField] private BreadcrumbLauncher _breadcrumbLauncher;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private float _launchPower;
    [SerializeField] private bool _isActive;

    private int _breadcrumbsEaten;
    
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
    
    private void Start()
    {
        MakeInactive();
    }

    private void Update()
    {
        if (!IsActive)
        {
            FollowLauncher();
        }
    }

    private void FollowLauncher()
    {
        transform.position = _breadcrumbLauncher.transform.position;
        transform.rotation = _breadcrumbLauncher.transform.rotation;
    }

    public void MakeActive()
    {
        _rigidbody.useGravity = true;
        IsActive = true;
    }

    private void MakeInactive()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        IsActive = false;
    }

    private void UpdateTmpText(string text)
    {
        _tmpText.text = string.Format("Breadcrumbs\n{0}", text);
    }

    public void Launch()
    {
        MakeActive();
        _rigidbody.AddForce(transform.forward * _launchPower, ForceMode.Force);
    }

    public void Retrieve()
    {
        MakeInactive();
    }
    
    public void Add()
    {
        _breadcrumbsEaten++;
        UpdateTmpText(_breadcrumbsEaten.ToString());
    }

    public void Clear()
    {
        _breadcrumbsEaten = 0;
        UpdateTmpText(_breadcrumbsEaten.ToString());
    }
}