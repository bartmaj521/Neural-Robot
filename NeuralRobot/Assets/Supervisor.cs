using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class Supervisor : MonoBehaviour {

	// Use this for initialization
    private NeuralNetwork _n1, _n2;

    private Transform bb;

    public Transform bb8;

    public float bestScore = 0;

    private float currentScore = 0;

    public Text bestScoreText;

    public int[] neuronLayers;

	void Start () {
		_n1 = new NeuralNetwork(neuronLayers);
	    _n2 = new NeuralNetwork(_n1);

	    bb = Instantiate(bb8, new Vector3(0, -3, 0), transform.rotation);
	    bb.GetComponent<Mover>().Nn = _n1;

	    currentScore = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var headRotation = GameObject.Find("Object004").transform.rotation.eulerAngles.x;

	    currentScore += Time.deltaTime;

        if (headRotation < 180f || headRotation > 360)
	    {
            restart();
	    }
	}

    private float getScore()
    {

        return GameObject.Find("Box001").transform.position.x;
    }

    private void restart()
    {
        if (getScore() > bestScore)
        {
            bestScore = getScore();
            _n2 = new NeuralNetwork(_n1);
            FileManager.SaveNetworkToFile(_n2, @"C:\Users\Kuba\Desktop\network.json");
            var x = FileManager.ReadNetworkFromFile(@"C:\Users\Kuba\Desktop\network.json");
            Debug.Log(x);
        }
        else
        {
            _n1 = new NeuralNetwork(_n2);
        }
        _n1.Mutate();
        Destroy(bb.gameObject);
        bb = Instantiate(bb8, new Vector3(0, -3, 0), transform.rotation);
        bb.GetComponent<Mover>().Nn = _n1;
        Debug.Log("Best Score: " + bestScore);
        //bestScoreText.text = bestScore.ToString();
        //Debug.Log("Current Score: " + getScore());
        currentScore = 0;
    }
}
