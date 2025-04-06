using DG.Tweening;
using UnityEngine;

public class RollingLoadCircle : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, -360), 1.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}