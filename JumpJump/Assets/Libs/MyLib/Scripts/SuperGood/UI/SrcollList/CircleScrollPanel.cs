using UnityEngine;
using System.Collections;
using UI.UIComponent.ScrollList;

public class CircleScrollPanel : SGScrollPanel
{

    public float pauseTime = 1f;
    public float vectory = 0.15f;

    protected override void myUpdate ()
    {
        base.myUpdate ();
//        moveLeftToRight ();
        moveRightToLeft ();
    }

    private void moveLeftToRight ()
    {
        if (AutoScroll && GetElementCount () > 1) {
            myscrollBar.value -= Time.deltaTime * vectory * standCount / GetElementCount ();
            if (myscrollBar.value <= index * 1f / (GetElementCount () - 1)) {
                AutoScroll = false;
                float nextValue = index * 1f / (GetElementCount () - 1);
                myscrollBar.value = nextValue;
                index --;
                bool reset = false;
                if (myscrollBar.value <= 0) {
                    index = (GetElementCount () - 1) / 2;
                    nextValue = index * 1f / (GetElementCount () - 1);
                    reset = true;
                }
                StartCoroutine (delayTime (reset, pauseTime));
            }
        }
    }

    private void moveRightToLeft ()
    {
        if (AutoScroll && GetElementCount () > 1) {
            myscrollBar.value += Time.deltaTime * vectory * standCount / GetElementCount ();
            if (myscrollBar.value >= index * 1f / (GetElementCount () - 1)) {
                AutoScroll = false;
                float nextValue = index * 1f / (GetElementCount () - 1);
                myscrollBar.value = nextValue;
                index ++;
                bool reset = false;
                if (myscrollBar.value >= 1) {
                    index = (GetElementCount () - 1) / 2;
                    nextValue = index * 1f / (GetElementCount () - 1);
                    reset = true;
                }
                StartCoroutine (delayTime (reset, pauseTime));
            }
        }
    }
   
    internal void GeneralPointerDownAction()
    {
        PlayClickSound();
    }
    
    public void PlayClickSound()
    {
    }

   
    protected override void onBeginDrag (UnityEngine.EventSystems.PointerEventData eventData)
    {
//        PlayClickSound ();
        base.onBeginDrag (eventData);
        StopAllCoroutines ();
    
//        if (myscrollBar.value > (1 - 0.5f / (GetElementCount () - 1))) {
//            for (int i=0; i<(GetElementCount ())/2; i++) {
//                myScrollList.transform.GetChild (0).SetSiblingIndex (GetElementCount () - 1);
//            }
//            index = (GetElementCount () - 1) / 2;
//            myscrollBar.value = (index) * 1f / (GetElementCount () - 1);
//        } else if (myscrollBar.value < (0.5f / (GetElementCount () - 1))) {
//            for (int i=0; i<(GetElementCount ())/2; i++) {
//                myScrollList.transform.GetChild (0).SetSiblingIndex (GetElementCount () - 1);
//            }
//            index = (GetElementCount () - 1) / 2;
//            myscrollBar.value = (index + 1) * 1f / (GetElementCount () - 1);
//        }
        lastDragPosition.x = eventData.position.x;
        lastDragPosition.y = eventData.position.y;
        lastTime = Time.time;
    }

    protected override void onDrag (UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.onDrag (eventData);
        if (Time.time > lastTime+0.1f) {
            lastDragPosition.x = eventData.position.x;
            lastDragPosition.y = eventData.position.y;
            lastTime = Time.time;
        }
    }

    protected override void onEndDrag (UnityEngine.EventSystems.PointerEventData eventData)
    {
        index = (int)(myscrollBar.value * (GetElementCount () - 1));
        endDragPosition.x = eventData.position.x;
        endDragPosition.y = eventData.position.y;
        StartCoroutine (delayTime (pauseTime));
    }

    protected override void initScrollSize ()
    {
        base.initScrollSize ();
    }

    public override void SetPosition ()
    {
        if (GetElementCount () > 1) {
            index = (GetElementCount () - 1) / 2;
            myscrollBar.value = index * 1f / (GetElementCount () - 1);
        } else {
            index = 0;
            myscrollBar.value = 0;
        }

    }
    
    IEnumerator delayTime (bool reset, float delayTime)
    {
        yield return new WaitForSeconds (delayTime);
        if (!((SGScrollRect)myScrollRect).IsDraging) {
            AutoScroll = true;
            if (reset) {
                R2L ();
            }
        }
    }

    private void L2R ()
    {
        for (int i=0; i<(GetElementCount ())/2; i++) {
            myScrollList.transform.GetChild (0).SetSiblingIndex (GetElementCount () - 1);
        }
        myscrollBar.value = (index + 1) * 1f / (GetElementCount () - 1);
    }

    private void R2L ()
    {

        if (GetElementCount () == 2) {
             myScrollList.transform.GetChild (GetElementCount () - 1).SetSiblingIndex (0);
        } else {
            for (int i=GetElementCount ()-1; i>(GetElementCount ())/2; i--) {
                myScrollList.transform.GetChild (GetElementCount () - 1).SetSiblingIndex (0);
            }
        }


        myscrollBar.value = (index - 1) * 1f / (GetElementCount () - 1);
    }

    IEnumerator delayTime (float delayTime)
    {

        float next = (index + 1) * 1f / (GetElementCount () - 1);
        float delta = next - myscrollBar.value;
        float moveTime = 0.5f;
        if (endDragPosition.x < lastDragPosition.x) {
  
            while (myscrollBar.value <next) {
                yield return 0;
                myscrollBar.value += delta * Time.deltaTime / moveTime;
            }
            index ++;
        } else {
            next = (index) * 1f / (GetElementCount () - 1);
            delta = myscrollBar.value - next;
            while (myscrollBar.value >next) {
                yield return 0;
                myscrollBar.value -= delta * Time.deltaTime / moveTime;
            }

        }
       
        if (!((SGScrollRect)myScrollRect).IsDraging) {
//            AutoScroll = true;
        }
    }

    public override void OnSubPointDown ()
    {
        base.OnSubPointDown ();
        PlayClickSound ();
    }
    
    public override void OnSubPointUp ()
    {
        base.OnSubPointUp ();
    }

    public override void NextElement ()
    {
        if (GetElementCount () <= 1)
            return;
        AutoScroll = true;
    }
    
    public override void PreElement ()
    {
        if (GetElementCount () <= 1)
            return;
        AutoScroll = true;
    }

    Vector2 lastDragPosition = new Vector2();
    Vector2 endDragPosition  = new Vector2();
    float lastTime =0;
}
