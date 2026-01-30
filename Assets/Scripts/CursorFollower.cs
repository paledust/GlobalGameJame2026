using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerManager.Instance.GetCursorWorldPos(10f);
    }
}