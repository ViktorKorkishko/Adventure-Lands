using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
    
    [Header("Lifetime Stats")]
    [SerializeField] private float lifeTime;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
