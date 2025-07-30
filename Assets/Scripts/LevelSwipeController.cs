using UnityEngine;

/// <summary>
/// Controls swipe-based navigation between level pages.
/// Allows moving to next or previous level selection pages with smooth tweening.
/// </summary>
public class LevelSwipeController : MonoBehaviour
{
    [Header("Page Configuration")]
    [Tooltip("Maximum number of pages available")]
    [SerializeField] int maxPage;

    int currentPage;
    Vector3 targetPos;

    [Tooltip("The distance to move between each page")]
    [SerializeField] Vector3 pageStep;

    [Tooltip("The RectTransform of the level pages container")]
    [SerializeField] RectTransform levelPagesRect;

    [Header("Tween Settings")]
    [Tooltip("Time taken to transition between pages")]
    [SerializeField] float tweenTime;

    [Tooltip("Type of tweening used for page movement")]
    [SerializeField] LeanTweenType tweenType;

    /// <summary>
    /// Initializes the first page and sets the initial position.
    /// </summary>
    public void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    /// <summary>
    /// Moves to the next page if not already on the last page.
    /// </summary>
    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    /// <summary>
    /// Moves to the previous page if not already on the first page.
    /// </summary>
    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    /// <summary>
    /// Applies tweening to move the level page container to the target position.
    /// </summary>
    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }
}
