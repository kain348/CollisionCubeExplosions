using System.Collections.Generic;
using UnityEngine;

internal class Exploder : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField, Min(0f)] private float _baseExplosionRadius = 5f;
    [SerializeField, Min(0f)] private float _baseExplosionForce = 50f;
    [SerializeField, Min(0f)] private float _upwardsModifier = 0f;

    [Header("Size Scaling")]
    [SerializeField] private bool _scaleForceWithSize = false; 
    [SerializeField, Min(0.1f)] private float _minSizeMultiplier = 0.5f; 
    [SerializeField, Min(1f)] private float _maxSizeMultiplier = 2f;
    [SerializeField] private float _noScalingMultiplier = 1f;

    public void Explode(Vector3 explosionPosition, Vector3 objectSize, List<Rigidbody> fragments)
    {
        if (fragments == null || fragments.Count == 0)
            return;

        float sizeMultiplier = GetSizeMultiplier(objectSize);
        float actualForce = _baseExplosionForce * sizeMultiplier;
        float actualRadius = _baseExplosionRadius * sizeMultiplier;

        foreach (var rigidbody in fragments)
        {
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(
                    actualForce,
                    explosionPosition,
                    actualRadius,
                    _upwardsModifier,
                    ForceMode.Impulse
                    );
            }
        }
    }

    private float GetSizeMultiplier(Vector3 objectSize)
    {
        if (_scaleForceWithSize == false)
            return _noScalingMultiplier;

        float sizeMagnitude = objectSize.magnitude;
        return Mathf.Clamp(sizeMagnitude, _minSizeMultiplier, _maxSizeMultiplier);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.3f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, _baseExplosionRadius);
    }
}