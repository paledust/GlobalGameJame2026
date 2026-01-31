using UnityEngine;

public abstract class PlayerDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == Service.playerTag)
        {
            OnFindPlayer(collider.gameObject);
        }
    }
    protected abstract void OnFindPlayer(GameObject player);
}
