using UnityEngine;

internal class ObjectsManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Separator _separator;
    [SerializeField] private ObjectCreator _objectCreator;
    [SerializeField] private Exploder _exploder;

    private void OnEnable() => ClickableObject.OnObjectClicked += HandleObjectClick;

    private void OnDisable() => ClickableObject.OnObjectClicked -= HandleObjectClick;

    private void HandleObjectClick(ClickableObject clickedObject)
    {
        if (clickedObject == null) return;

        if (_separator.ShouldSplit(clickedObject.SplitChance))
        {
            _objectCreator.CreateFragments(clickedObject);
        }

        Destroy(clickedObject.gameObject);
        _exploder.Enable(clickedObject.Position, clickedObject.Size);
    }
}