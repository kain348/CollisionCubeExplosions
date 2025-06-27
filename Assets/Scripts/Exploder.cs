using UnityEngine;

internal class Exploder : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField, Min(0f)] private float _baseExplosionRadius = 5f;
    [SerializeField, Min(0f)] private float _baseExplosionForce = 50f;
    [SerializeField, Min(0f)] private float _upwardsModifier = 0f;

    [Header("Size Scaling")]
    [SerializeField] private bool _scaleForceWithSize = false; // <---- Новая настройка
    [SerializeField, Min(0.1f)] private float _minSizeMultiplier = 0.5f; // <---- Добавлено
    [SerializeField, Min(1f)] private float _maxSizeMultiplier = 2f; // <---- Добавлено
    [SerializeField] private float _noScalingMultiplier = 1f; // <---- Явное значение вместо 1f

    public void Enable(Vector3 explosionPosition, Vector3 objectSize)
    {
        float sizeMultiplier = _scaleForceWithSize
            ? Mathf.Clamp(objectSize.magnitude, _minSizeMultiplier, _maxSizeMultiplier)
            : _noScalingMultiplier; // <---- Учет размера

        float actualForce = _baseExplosionForce * sizeMultiplier;
        float actualRadius = _baseExplosionRadius * sizeMultiplier;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, actualRadius);

        foreach (var collider in colliders)
        {
            if (collider.attachedRigidbody != null)
            {
                collider.attachedRigidbody.AddExplosionForce(
                    actualForce,
                    explosionPosition,
                    actualRadius,
                    _upwardsModifier,
                    ForceMode.Impulse
                    );
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.3f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, _baseExplosionRadius);
    }
}