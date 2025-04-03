using UnityEngine;
using DG.Tweening;

public class CatButton : MonoBehaviour
{

    SpriteRenderer myRenderer;

    public Color idleColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color pressedColor;

    public float idleScale = 1;
    public float hoverScale = 1.1f;
    public float pressScale = .9f;

    public Tweener colorTween, scaleTween;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnMouseEnter()
    {

        // interrompo eventuali tween in progress
        colorTween?.Kill();
        scaleTween?.Kill();

        colorTween = myRenderer.DOColor(hoverColor, .2f);

        scaleTween = transform.DOScale(hoverScale, .2f);
    }

    private void OnMouseExit()
    {

        colorTween?.Kill();
        scaleTween?.Kill();

        colorTween = myRenderer.DOColor(idleColor, .3f);

        scaleTween = transform.DOScale(idleScale, .3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
