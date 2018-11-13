using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTutorial : MonoBehaviour
{
	public CanvasGroup fade;

	public void End(){
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut(){
		while (true){
			if (fade.alpha > 0.99f){
				yield break;
			}else{
				fade.alpha = Mathf.Lerp(fade.alpha, 1, Time.deltaTime * 0.5f);
			}

			yield return null;
		}
	}
}
