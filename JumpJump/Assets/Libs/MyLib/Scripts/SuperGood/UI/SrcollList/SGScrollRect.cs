using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.UIComponent.ScrollList
{
    public class SGScrollRect : ScrollRect
    {
        public delegate void SkyOnBeginDrag (UnityEngine.EventSystems.PointerEventData eventData);

        public SkyOnBeginDrag mySkyOnBeginDrag;

        public delegate void SkyOnEndDrag (UnityEngine.EventSystems.PointerEventData eventData);

        public SkyOnEndDrag mySkyOnEndDrag;


        public delegate void SkyOnDrag (UnityEngine.EventSystems.PointerEventData eventData);
        
        public SkyOnDrag mySkyOnDrag;
        
        public override void OnBeginDrag (UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) {
                return;
            }
            if (!this.IsActive ()) {
                return;
            }
            mySkyOnBeginDrag (eventData);
            base.OnBeginDrag (eventData);
                        
            _isDraging = true;
        }

        public override void OnDrag (UnityEngine.EventSystems.PointerEventData eventData)
        {   

            if (eventData.button != PointerEventData.InputButton.Left) {
                return;
            }
            if (!this.IsActive ()) {
                return;
            }
            base.OnDrag (eventData);
            mySkyOnDrag (eventData);

        }

        public override void OnEndDrag (UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) {
                return;
            }
            base.OnEndDrag (eventData);
            mySkyOnEndDrag (eventData);
            _isDraging = false;
        }

        public override void OnScroll (UnityEngine.EventSystems.PointerEventData data)
        {
                        
        }
     
        public bool IsDraging {
            get { return _isDraging;}
        }

        private bool _isDraging;
    }
}
