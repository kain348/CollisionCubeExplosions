using System.Collections.Generic;
using UnityEngine;

internal class ObjectDestructionController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Separator _separator;
    [SerializeField] private ObjectCreator _objectCreator;
    [SerializeField] private RaycastClickHandler _clickHandler;
    [SerializeField] private Exploder _exploder;

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
            List<Rigidbody> newObjectRigidbodies = _objectCreator.CreateFragments(clickedObject);

            if (newObjectRigidbodies.Count > 0)
            {
                _exploder.Explode(clickedObject.Position, clickedObject.Size, newObjectRigidbodies);
            }
        }

        Destroy(clickedObject.gameObject);
    }
}