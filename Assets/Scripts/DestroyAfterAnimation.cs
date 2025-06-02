using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float delay = 0.5f;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
