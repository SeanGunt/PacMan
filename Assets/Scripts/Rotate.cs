using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(0,0,50 * Time.deltaTime);
    }
}
