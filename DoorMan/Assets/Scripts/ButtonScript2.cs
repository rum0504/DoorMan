using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class ButtonScript2 : Button
    {
        Tweener tweener = null;
        new void Start()
        {
            base.Start();

            // �{�^���A�j���[�V����
            onClick.AddListener(() =>
            {
                // �Đ����̃A�j���[�V�������~/������
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

