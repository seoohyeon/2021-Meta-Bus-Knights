using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour {

	public void GameOver()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

}
