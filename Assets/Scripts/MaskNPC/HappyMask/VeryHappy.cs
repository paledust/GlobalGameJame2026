using UnityEngine;

public class VeryHappy : MonoBehaviour
{
    [SerializeField] private Vector2 happyTimeRange;
    private HappyMask happyMask;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        happyMask = GetComponent<HappyMask>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > Random.Range(happyTimeRange.x, happyTimeRange.y))
        {
            timer = 0;
            happyMask.OnFindPlayer(null);
        }
    }
}
