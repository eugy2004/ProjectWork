using UnityEngine;
using DG.Tweening;
using Unity.UI;
using UnityEngine.EventSystems;

public class CanvasButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    SpriteRenderer myRenderer;

    public float idleScale = 1;
    public float hoverScale = 1.1f;
    public float pressScale = .9f;

    public Tweener scaleTween;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnMouseEnter()
    {

        // interrompo eventuali tween in progress
     
        scaleTween?.Kill();

       

        scaleTween = transform.DOScale(hoverScale, .2f);
    }

    private void OnMouseExit()
    {

       
        scaleTween?.Kill();

       

        scaleTween = transform.DOScale(idleScale, .3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        scaleTween?.Kill();

        scaleTween = transform.DOScale(hoverScale, .2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        scaleTween?.Kill();

        scaleTween = transform.DOScale(hoverScale, .2f);
    }
}
