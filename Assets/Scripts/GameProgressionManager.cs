using UnityEngine;

public class GameProgressionManager : Singleton<GameProgressionManager>
{
    public bool isIntroOver{get; private set;} = false;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    public void EndIntro()
    {
        isIntroOver = true;
    }
}