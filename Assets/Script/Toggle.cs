using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// トグル
/// </summary>
public class Toggle : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private RectTransform handle;
    [SerializeField] private bool onAwake;

    /// <summary>
    /// トグルの値
    /// </summary>
    [NonSerialized] public bool Value;
    
    private float handlePosX;
    private Sequence sequence;
    
    private static readonly Color OFF_BG_COLOR = new Color(0.2f, 0.2f, 0.92f);
    private static readonly Color ON_BG_COLOR = new Color(0.2f, 0.84f, 0.3f);

    private const float SWITCH_DURATION = 0.36f;
    
    private void Start()
    {
        handlePosX = Mathf.Abs(handle.anchoredPosition.x);
        Value = onAwake;
        UpdateToggle(0);
    }

    /// <summary>
    /// トグルのボタンアクションに設定しておく
    /// </summary>
    public void SwitchToggle()
    {
        Value = !Value;
        UpdateToggle(SWITCH_DURATION);
        GManager.instance.PlayMode = Value;
    }

    /// <summary>
    /// 状態を反映させる
    /// </summary>
    private void UpdateToggle(float duration)
    {
        var bgColor = Value ? ON_BG_COLOR : OFF_BG_COLOR;
        var handleDestX = Value ? handlePosX : -handlePosX;

        sequence?.Complete();
        sequence = DOTween.Sequence();
        sequence.Append(backgroundImage.DOColor(bgColor, duration))
            .Join(handle.DOAnchorPosX(handleDestX, duration / 2));
    }
}