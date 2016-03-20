using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace UI.UIComponent.ScrollList
{
		public class SGElementBase : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
		{

				protected SGScrollPanel MySkyScrollPanel;
			
				public virtual bool Init (int index, SGScrollPanel mySkyScrollPanel)
				{
						this.MySkyScrollPanel = mySkyScrollPanel;
                        return true;
				}

				public virtual void OnPointerDown (PointerEventData eventData)
				{
						MySkyScrollPanel.OnSubPointDown ();
				}

				public virtual void OnPointerUp (PointerEventData eventData)
				{
						MySkyScrollPanel.OnSubPointUp ();
				}

				public virtual void OnPointerClick (PointerEventData eventData)
				{
						//Debug.Log ("OnPointerClick");
				}
		}
}
