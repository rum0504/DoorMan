using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; //DOTween使用に必要
using UnityEngine.UI;

public class ButtonScript :
    MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    ISelectHandler,
    IDeselectHandler,
    ISubmitHandler
{
    CanvasGroup canvasGroup;
    Image image;
    Text text;
    Button button;


    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        text = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        button = GetComponent<Button>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        Debug.Log("クリック！");
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        transform.DOScale(0.9f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
        canvasGroup.DOFade(0.8f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1.2f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
        canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1.2f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
        image.color = new Color(1f, 0f, 0f);
        text.color = new Color(1f, 1f, 1f);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
        image.color = new Color(1f, 1f, 1f);
        text.color = new Color(0f, 0f, 0f);
    }
    void ISelectHandler.OnSelect(BaseEventData data)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1.2f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
        image.color = new Color(1f, 0f, 0f);
        text.color = new Color(1f, 1f, 1f);
        Debug.Log("選択！");
    }
    void IDeselectHandler.OnDeselect(BaseEventData data)
    {
        if (!button.interactable) { return; }
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic).SetLink(gameObject);
        image.color = new Color(1f, 1f, 1f);
        text.color = new Color(0f, 0f, 0f);
        Debug.Log("選択解除！");
    }
    void ISubmitHandler.OnSubmit(BaseEventData data)
    {
        if (!button.interactable) { return; }
        var sequence = DOTween.Sequence();
        sequence
        .Append(transform.DOScale(0.9f, 0.1f).SetEase(Ease.OutCubic))
        .Join(canvasGroup.DOFade(0.8f, 0.1f).SetEase(Ease.OutCubic))
        .AppendCallback(() => { Debug.Log("決定！"); })
        .Append(transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutCubic))
        .Join(canvasGroup.DOFade(1f, 0.1f).SetEase(Ease.OutCubic)).SetLink(gameObject);
    }
}