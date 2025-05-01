using UnityEngine;

public class Boss : MonoBehaviour
{
    private Damageable damageable;

    void Start()
    {
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        if (damageable != null && !damageable.IsAlive)
        {
            
        }
    }
}

