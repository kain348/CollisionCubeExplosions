using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]

public class ClickableObject : MonoBehaviour
{
    [Header("Prefab Defaults (For Editor Only)")]
    [SerializeField] private Vector3 _prefabDefaultPosition = Vector3.zero;
    [SerializeField] private Vector3 _prefabDefaultSize = Vector3.one;
    [SerializeField] private Material _prefabDefaultMaterial;
    [SerializeField, Range(0, 100)] private int _prefabSplitChance = 100;

    private Vector3 _currentPosition;
    private Vector3 _currentSize;
    private Material _currentMaterial;
    private int _currentSplitChance;

    private Renderer _cachedRenderer;
    private Rigidbody _cachedRigidbody;
    private Collider _cachedCollider;

    public Vector3 Position => _currentPosition;
    public Vector3 Size => _currentSize;
    public Material Material => _currentMaterial;
    public int SplitChance => _currentSplitChance;
    public Collider ObjectCollider => _cachedCollider;

    public static event System.Action<ClickableObject> OnObjectClicked;

    private void Awake()
    {
        _cachedRenderer = GetComponent<Renderer>();
        _cachedRigidbody = GetComponent<Rigidbody>();
        _cachedCollider = GetComponent<Collider>();

        _currentPosition = transform.position;
        _currentSize = transform.localScale;
        _currentMaterial = _cachedRenderer.material;
        _currentSplitChance = _prefabSplitChance;
    }

    public void Initialize(Vector3 position, Vector3 size, Material material, int splitChance)
    {
        _currentPosition = position;
        _currentSize = size;
        _currentMaterial = material;
        _currentSplitChance = splitChance;

        ApplyRuntimeSettings();
    }

    private void ApplyRuntimeSettings()
    {
        transform.position = _currentPosition;
        transform.localScale = _currentSize;

        if (_currentMaterial != null)
        {
            _cachedRenderer.material = _currentMaterial;
        }
    }
        
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {            
            transform.position = _prefabDefaultPosition;
            transform.localScale = _prefabDefaultSize;

            if (_prefabDefaultMaterial != null)
            {
                GetComponent<Renderer>().material = _prefabDefaultMaterial;
            }
        }
    }

    public void HandleClick()
    {
        OnObjectClicked?.Invoke(this);
    }
}
