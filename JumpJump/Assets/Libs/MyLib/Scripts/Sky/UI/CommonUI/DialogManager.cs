using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	public class DialogManager:MonoBehaviour
	{
		public Canvas dialogCanvas = null;
		private static DialogManager singletonInstance = null;
		private List<GameObject> dialogList = new List<GameObject>();
		private DialogManager()
		{

		}

		void Start (){
			singletonInstance = this;
			//StartCoroutine (DialogLoop ());
		}

		public static DialogManager GetInstance()
		{
			return singletonInstance;
		}

		public void AddDialog(GameObject dialogToShow)
		{
			if (!dialogList.Contains (dialogToShow)) 
			{
				RectTransform dialogTransform = dialogToShow.GetComponent<RectTransform>();
				dialogList.Add (dialogToShow);
				dialogTransform.SetParent (dialogCanvas.GetComponent<RectTransform>(),false);

				if(dialogToShow.GetComponent<UIDialog>().bShowWhenAdded)
					dialogToShow.SetActive(true);
				else
					dialogToShow.SetActive(false);
			}
		}

		public void RemoveDialog(GameObject dialogToRemove)
		{
			if(dialogList.Contains(dialogToRemove))
			{
				dialogList.Remove(dialogToRemove);
			}
		}

		public void ShowLastDialog()
		{
			this.ShowDialogAtIndex (dialogList.Count - 1);
		}

		public void ShowAllDialog()
		{
			for (int index = 0; index < dialogList.Count; index ++)
				ShowDialogAtIndex (index);
		}

		public void ShowDialogAtIndex(int dialogIndex)
		{
			if (dialogIndex < 0 || dialogIndex >= dialogList.Count)
				return;

			GameObject specificDialog = dialogList[dialogIndex];
			if(!specificDialog.activeSelf)
				specificDialog.SetActive(true);
		}

		public int TotalDialog()
		{
			return dialogList.Count;
		}

		public void PopUpDialogWhenNessary()
		{
			if (dialogList.Count > 0) 
			{
				GameObject specificDialog = dialogList [dialogList.Count - 1];
				if(specificDialog.GetComponent<UIDialog>().bPopupWhenNessary)
					this.ShowLastDialog ();
			}
		}

		void Update()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				UIDialog activeDialog = GetActiveDialog ().GetComponent<UIDialog>();
				if(activeDialog && activeDialog.bResponseBackButton)
				{
					if(Input.GetKeyUp(KeyCode.Escape))
					{
						activeDialog.ShowOut();
					}
				}
			}

		}

		private GameObject GetActiveDialog()
		{
			for (int index = dialogList.Count; index > 0; index --) 
			{
				if(dialogList[index].activeSelf)
					return dialogList[index];
			}
			return null;
		}

		IEnumerator DialogLoop()
		{
			while (true) 
			{
				yield return new WaitForSeconds (0.1f);
			}
		}
	}




