using BoilerTools.Procedural;
using UnityEngine;


public class TextGeneratorDemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var g = new ProcTextGenerator();
        g.AddCorpus("PrincessOfMars");

        Debug.Log(g.GenerateSentence());
        Debug.Log(g.GenerateSentence());
        Debug.Log(g.GenerateSentence());
        Debug.Log(g.GenerateSentence());

    }

}
