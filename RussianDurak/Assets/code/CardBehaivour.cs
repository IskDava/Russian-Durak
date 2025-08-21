using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CardBehaivour : MonoBehaviour
{
    public bool canHover = true;
    [SerializeField] public float HoverEffect = 0.1f, duration = 0.5f;
    [SerializeField] public BattlefieldManagment battlefielder;
    public Card card;
    public Vector3 startPos, endPos, currentPos;
    public bool IsMoving = false;
    private void Start()
    {
        if (battlefielder == null) battlefielder = FindFirstObjectByType<BattlefieldManagment>();

        startPos = transform.position;
        currentPos = startPos;
        endPos = new Vector3(startPos.x, startPos.y + HoverEffect, startPos.z);
    }
    private void OnMouseEnter()
    {
        if (canHover && !IsMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveRoutine(currentPos, endPos, duration));
        }
    }
    public IEnumerator MoveRoutine(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            currentPos = transform.position;
            yield return null;
        }
        transform.position = endPos;
    }
    public IEnumerator ResizeRoutine(Vector3 startSize, Vector3 endSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            transform.localScale = Vector3.Lerp(startSize, endSize, t);
            currentPos = transform.localScale;
            yield return null;
        }
        transform.localScale = endSize;
    }
    private void OnMouseExit()
    {
        if (canHover && !IsMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveRoutine(currentPos, startPos, duration));
        }
    }
    private void OnMouseDown()
    {
        if (!IsMoving)
        {
            IsMoving = true;
            bool success = battlefielder.PutCard(gameObject, card);
            if (!success) { IsMoving = false;}
        }
    }
}
