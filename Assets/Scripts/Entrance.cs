using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] private string toScene;
    private bool isLoading = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Service.playerTag)
        {
            if(!isLoading)
            {
                isLoading = true;
                GameManager.Instance.SwitchingScene(toScene);
            }
        }
    }
}
