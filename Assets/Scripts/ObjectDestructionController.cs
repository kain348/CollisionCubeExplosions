using UnityEngine;

internal class ObjectDestructionController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Separator _separator;
    [SerializeField] private ObjectCreator _objectCreator;
    [SerializeField] private RaycastClickHandler _clickHandler;

    private void OnEnable()
    {
        if (_clickHandler != null)
            _clickHandler.OnClickableObjectClicked += HandleObjectClick;
    }

    private void OnDisable()
    {
        if (_clickHandler != null)
            _clickHandler.OnClickableObjectClicked -= HandleObjectClick;
    }

    private void HandleObjectClick(ClickableObject clickedObject)
    {
        if (clickedObject == null) return;

        if (_separator.ShouldSplit(clickedObject.SplitChance))
        {
            _objectCreator.CreateFragments(clickedObject);
        }

        Destroy(clickedObject.gameObject);
    }
}