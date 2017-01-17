using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BoilerTools.Extensions;

/* usage
* 
* var g = new TextGenerator();
* g.AddCorpus( Resource ); g.AddCorpus( WWW );
* g.GenerateSentence();
* g.GenerateParagraph();
* g.GenerateTitle();
*/


namespace BoilerTools.Procedural {

    /// <summary>
    /// Generate mostly meaningless text using a set of corpora.
    /// </summary>
    public class ProcTextGenerator {

        HashSet<TextAsset> corpora;
        Dictionary<string, HashSet<string>> wordMap;

        public ProcTextGenerator() {
            wordMap = new Dictionary<string, HashSet<string>>(); // Todo: Consider making this a list, when we add to our word list, we probably want multiple entries to add to frequency
            corpora = new HashSet<TextAsset>();
        }

        public bool AddCorpus( string resourcePath)
        {
            TextAsset asset = Resources.Load<TextAsset>(resourcePath);

            // RETURNS: False, asset can't be loaded.
            if (!asset || asset.text == "") { Debug.Log("Corpus " + resourcePath + " could not be loaded!"); return false; }

            // RETURNS: True, asset already loaded.
            if (corpora.Contains(asset)) { return true; }

            corpora.Add(asset);
            DigestCorpus(asset);

            return true;
        }

        public string GenerateSentence()
        {
            string output = "";

            // Pick a random word
            int random = Random.Range(0, wordMap.Count);

            var entry = wordMap.Keys.ElementAt(random);
            output += entry.ToLower().FirstLetterToUpper().Trim();
            output += " ";

            if (wordMap[entry] != null)
            {
                string followingWord = wordMap[entry].ElementAt(Random.Range(0, wordMap[entry].Count));

                for (int i = 0; i < 9; i++)
                {
                    if (wordMap[followingWord] == null) { break; } // BREAK : No following words

                    var fword = wordMap[followingWord].ElementAt(Random.Range(0, wordMap[followingWord].Count));
                    output += " " + followingWord.ToLower().Trim() + " ";
                    followingWord = fword;
                    if (fword.Contains('.') || fword.Contains('!') || fword.Contains('?'))
                    {
                        break;
                    }
                }
            }

            output += ".";
            return output;
        }

        private void DigestCorpus(TextAsset asset)
        {
            string[] text = asset.text.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < text.Length; i++)
            {

                string currentWord = text[i];
                currentWord = currentWord.ToLower();
                currentWord = currentWord.Replace(System.Environment.NewLine, "");
                currentWord = currentWord.Replace("-", "").Trim();

                string nextWord = "";
                if(i < text.Length - 1)
                {
                    nextWord = text[i+1].ToLower().Replace(System.Environment.NewLine, "").Replace("-", "").Trim();
                }

                // CONTINUE : Found new word and new word has a following word, add them
                if (!wordMap.ContainsKey(currentWord))
                {
                    if (i < text.Length - 1) // There is  a following word
                    {
                        HashSet<string> wordList = new HashSet<string>();
                        wordList.Add(nextWord);
                        wordMap.Add(currentWord, wordList);
                        continue;
                    }
                    else
                    {
                        wordMap.Add(currentWord, null);
                    }
                }

                // CONTINUE : Found word that already exists, but it doesnt have the following word mapped to it, so add it!
                if (wordMap.ContainsKey(currentWord) && i < text.Length - 1 && !wordMap[currentWord].Contains(nextWord))
                {
                    wordMap[currentWord].Add(nextWord);
                    continue;
                }
            }
        }

        public void ToString()
        {
            foreach(var word in wordMap)
            {
                string output = "";
                output += word.Key + " : ";
                foreach(var followingword in word.Value)
                {
                    output += followingword + " , ";
                }

                Debug.Log( output );
            }

        }
    }
}


