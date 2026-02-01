using DG.Tweening;
using PlayerInteraction;
using SimpleAudioSystem;
using UnityEngine;

public class MaskSun : Interactable
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationSnap = 10f;
    [SerializeField] private SpriteRenderer sunSpriteRenderer;
    [SerializeField] private Sprite eyeCloseSprite;
    [SerializeField] private Sprite eyeOpenSprite;
    [SerializeField] private string sfxHover;
    private float angle;
    public override void OnHover(PlayerController player)
    {
        base.OnHover(player);
        AudioManager.Instance.PlaySoundEffect(sfxHover, 0.25f);
        sunSpriteRenderer.sprite = eyeCloseSprite;
        sunSpriteRenderer.transform.localScale = Vector3.one;
        sunSpriteRenderer.transform.DOKill();
        sunSpriteRenderer.transform.DOPunchScale(Vector3.one *0.2f, 0.2f, 2);
    }
    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        float rotAngle = Mathf.Round(angle / rotationSnap) * rotationSnap;
        transform.eulerAngles = new Vector3(0, 0, rotAngle);
    }
    public override void OnExitHover()
    {
        base.OnExitHover();
        sunSpriteRenderer.sprite = eyeOpenSprite;
    }
}
