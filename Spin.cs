using Unity.Mathematics;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float rotation = 64;

    void Update()
    {
        transform.Rotate(0, rotation * Time.deltaTime, 0);        
    }
}
