using UnityEngine;

public class Timer : MonoBehaviour
{
    float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > 8f) Destroy(gameObject);
    }
}
