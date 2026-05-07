using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private Text messageText;

	public void ShowMessage()
	{
		if (messageText != null)
		{
			messageText.text = "Button clicked!";
			messageText.gameObject.SetActive(true);
		}
		else
		{
			Debug.LogWarning("LevelManager: messageText is not assigned.");
		}
	}

	public void EndGame()
	{
		Debug.Log("EndGame called.");
		Application.Quit();
	}
    
}
