using UnityEngine;
using System.Collections;


	public class UIDialog : UIWindow 
	{
		public bool bResponseBackButton = false;
		public bool bPopupWhenNessary = true;
		public bool bShowWhenAdded = true;

		void OnDestroy()
		{
			DialogManager.GetInstance ().RemoveDialog (gameObject);
			DialogManager.GetInstance ().PopUpDialogWhenNessary ();
		}
	}

