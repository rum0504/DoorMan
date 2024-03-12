using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class ButtonScript2 : Button
    {
        Tweener tweener = null;
        new void Start()
        {
            base.Start();

            // ボタンアニメーション
            onClick.AddListener(() =>
            {
                // 再生中のアニメーションを停止/初期化
                if (tweener != null)
                {
                    tweener.Kill();
                    tweener = null;
                    transform.localScale = Vector3.one;
                }
                tweener = transform.DOPunchScale(
                    punch: Vector3.one * 0.5f,
                    duration: 0.2f,
                    vibrato: 1
                ).SetEase(Ease.OutExpo);
            });
        }
    }

