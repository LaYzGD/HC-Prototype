using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LavaBlock : Block
{
    [SerializeField] private float _explosionRadius;
    protected override void DestroyBlock()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var collider in colliders)
        {
            if (collider.transform.parent != null && collider.transform.parent.TryGetComponent(out IExplosionable explosionableObject))
            {
                explosionableObject.DestroyObject();
            }
        }
        base.DestroyBlock();
    }
}
