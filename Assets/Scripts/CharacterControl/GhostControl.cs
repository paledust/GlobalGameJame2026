using UnityEngine;

public class GhostControl : MonoBehaviour
{
    [SerializeField] private Animator ghostAnimator;
    [SerializeField] private Vector2 boringIntervalRange;
    private static readonly string[] ANIME_BORINGS = { "boring_1", "boring_2", "boring_3" };
    private float boringTimer = 0f;

    void Start()
    {
        boringTimer = Random.Range(boringIntervalRange.x, boringIntervalRange.y);
    }

    void Update()
    {
        boringTimer -= Time.deltaTime;
        if(boringTimer <= 0f)
        {
            ghostAnimator.SetTrigger(ANIME_BORINGS[Random.Range(0, ANIME_BORINGS.Length)]);
            boringTimer = Random.Range(boringIntervalRange.x, boringIntervalRange.y);
        }        
    }
}
