using UnityEngine;

public class Bowser : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f; 

    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
