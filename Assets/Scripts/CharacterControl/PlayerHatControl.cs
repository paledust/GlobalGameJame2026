using DG.Tweening;
using UnityEngine;

public class PlayerHatControl : MonoBehaviour
{
    [SerializeField] private Transform hatRootTrans;
    [SerializeField] private float angleRange;
    public void OnGrowHat(GameObject prefab)
    {
        var hat = Instantiate(prefab, hatRootTrans);
        hat.transform.localPosition = Vector3.zero;
        hat.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-angleRange, angleRange));
        hat.transform.localScale = Vector3.zero;
        hat.transform.DOScale(Vector3.one*Random.Range(0.75f, 1.25f), 0.25f).SetEase(Ease.OutBack);
    }
}
