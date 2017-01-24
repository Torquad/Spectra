using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectraLib {

	public class SpecLib
	{
		private static float fadeAmount;

		public static float Fade(float currentTime, float timeReq, SpriteRenderer rend)
		{
			currentTime += Time.deltaTime;

			if (currentTime <= timeReq)
			{
				fadeAmount = rend.color.a - (currentTime / timeReq);
				rend.color = new Color (1, 1, 1, fadeAmount);
			} 
			return currentTime;
		}
	}
}
