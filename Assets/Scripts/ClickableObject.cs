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

    private Renderer _cachedRenderer;
    private Rigidbody _cachedRigidbody;
    private Collider _cachedCollider;

    public Collider ObjectCollider => _cachedCollider;

    public Vector3 Position
    {
        get => _prefabDefaultPosition;
        set
        {
            _prefabDefaultPosition = value;
            transform.position = _prefabDefaultPosition;
        }
    }
    public Vector3 Size
    {
        get => _prefabDefaultSize;
        set
        {
            _prefabDefaultSize = value;
            transform.localScale = _prefabDefaultSize;
        }
    }
    public Material Material
    {
        get => _prefabDefaultMaterial;
        set
        {
            _prefabDefaultMaterial = value;
            if (_prefabDefaultMaterial != null)
                _cachedRenderer.material = _prefabDefaultMaterial;
        }
    }
    public int SplitChance { get; private set; }

    public event System.Action<ClickableObject> OnObjectClicked;

    private void Awake()
    {
        _cachedRenderer = GetComponent<Renderer>();
        _cachedRigidbody = GetComponent<Rigidbody>();
        _cachedCollider = GetComponent<Collider>();

        Position = transform.position;
        Size = transform.localScale;
        Material = _cachedRenderer.material;
        SplitChance = _prefabSplitChance;
    }

    public void Initialize(Vector3 position, Vector3 size, Material material, int splitChance)
    {
        Position = position;
        Size = size;
        Material = material;
        SplitChance = splitChance;
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
}