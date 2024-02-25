using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ClickStreakAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform[] streakTexts;

    [Header("Animation Settings")]
    [Header("Rotation Animation")]
    [SerializeField]
    private float rotationDuration;
    [SerializeField]
    private Ease rotationEase;
    List<Sequence> rotationSequences = new List<Sequence>(3);
    [Header("Scale Animation")]
    [SerializeField]
    private float scaleDuration;
    [SerializeField]
    private float[] TextsTargetScale;
    [SerializeField]
    private Ease scaleEase;

    void Start()
    {
        for (int i=0; i < streakTexts.Length; i++)
        {
            ScaleTween(streakTexts[i], TextsTargetScale[i]);
            RotationTween(streakTexts[i]);
        }
    }
    private void OnDestroy()
    {
        foreach (Sequence sequence in rotationSequences)
        {
            sequence.Kill();
        }
        for (int i = 0; i < streakTexts.Length; i++)
        {
            streakTexts[i].DOKill();
        }      
        rotationSequences.Clear();
    }


    public void ActivateStreakText(int textNumber)
    {
        streakTexts[textNumber].DOPlay();
        rotationSequences[textNumber].Play();
    }
    public void DeactivateStreakText(int textNumber)
    {
        streakTexts[textNumber].DORewind();
        rotationSequences[textNumber].Rewind();
    }

    void RotationTween(Transform objectTransform)
    {
        Sequence rotationSequence = DOTween.Sequence();

        rotationSequence.Append(objectTransform.DORotate(new Vector3(0, 0, 25), rotationDuration));
        rotationSequence.Append(objectTransform.DORotate(new Vector3(0, 0, 0), rotationDuration));
        rotationSequence.Append(objectTransform.DORotate(new Vector3(0, 0, -25), rotationDuration));
        rotationSequence.Append(objectTransform.DORotate(new Vector3(0, 0, 0), rotationDuration));
        rotationSequence.SetLoops(-1, LoopType.Restart)
            .SetEase(rotationEase);
        rotationSequences.Add(rotationSequence);
    }

    void ScaleTween(Transform objectTransform, float targetScale)
    {
        objectTransform.DOScale(targetScale, scaleDuration)
            .SetEase(scaleEase);
    }

}
