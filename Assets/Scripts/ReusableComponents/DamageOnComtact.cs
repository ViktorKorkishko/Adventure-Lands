using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnComtact : MonoBehaviour
{
    [SerializeField] private string otherTag;
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            GenericHealth temp = GetComponent<GenericHealth>();
            if (temp)
            {
                temp.Damage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
