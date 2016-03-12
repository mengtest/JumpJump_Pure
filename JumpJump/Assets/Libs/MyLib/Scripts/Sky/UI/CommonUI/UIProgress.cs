using UnityEngine;
using System.Collections;
using UnityEngine.UI;



	public class UIProgress : MonoBehaviour {

		public float Total = 100f;
		public float Current = 0f;
		public float Speed = 100;

		public Image FrontImage = null;
		private float RealCurrent = -1;

		public void SetTotal(float newValue)
		{
			Total = newValue;
		}

		public void SetCurrent(float newValue)
		{
			Current = newValue;
		}

		void ValueChanged()
		{
			if (FrontImage == null) 
			{
				Debug.LogError("UIProgress FrontImage not set");
				return;
			}
		
			if (Total == 0)
				Total = 100f;

			float progress = RealCurrent / Total;
			FrontImage.type = Image.Type.Filled;
			FrontImage.fillMethod = Image.FillMethod.Horizontal;
			FrontImage.fillAmount = progress;
		}

		void Update()
		{
			if (RealCurrent != Current) 
			{
				float nextProgress = RealCurrent;
				if(RealCurrent < Current)
				{
					nextProgress = RealCurrent + Time.deltaTime * Speed;
					nextProgress = nextProgress > Current? Current:nextProgress;
				}
				else if(RealCurrent > Current)
				{
					nextProgress = RealCurrent - Time.deltaTime * Speed;
					nextProgress = nextProgress > Current? nextProgress:Current;
				}
				else
				{
					nextProgress = RealCurrent;
				}
				RealCurrent = nextProgress;
				ValueChanged();
			}
		}
	}




