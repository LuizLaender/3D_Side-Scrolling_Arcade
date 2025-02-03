using UnityEngine;

public class RepeatingMovement : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] float duration = 2f; // Time to reach endPos

    float elapsedTime = 0f;

    void Start()
    {
        transform.position = startPos;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;

        if (t >= 1f) // Reached end position
        {
            transform.position = startPos; // Teleport back
            elapsedTime = 0f; // Reset timer
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }
    }
}
