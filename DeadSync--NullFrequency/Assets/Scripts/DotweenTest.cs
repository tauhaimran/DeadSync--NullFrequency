using UnityEngine;
using DG.Tweening;  // <-- REQUIRED

public class DotweenTest : MonoBehaviour
{
    void Start()
    {
        // Move object to (3,1,0) over 1.5 sec
        transform.DOMove(new Vector3(3f, 1f, 0f), 1.5f)
                 .SetEase(Ease.OutBounce);

        // Scale Punch effect
        transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 10, 1);

        // Rotate continuously
        transform.DORotate(new Vector3(0,360,0), 2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1);
    }
}
