namespace Services;

using System;
using IWordFinderService;

public class WordFinderService : IWordFinderService
{
    private SplittingWordService splittingWordService = new SplittingWordService();

    public List<string> FindWordsInList(List<string> words, int wordToFindLength)
    {
        Dictionary<int, HashSet<string>> DictionaryOfHashSets = CreateDictionaryOfHashSets(words, wordToFindLength);
        List<string> output = new();

        foreach (string currentWord in DictionaryOfHashSets[wordToFindLength])
        {
            var splittedWordList = splittingWordService.SplitWordIntoParts(currentWord);

            foreach (var splittedWord in splittedWordList)
            {

                List<string> wordSplits = [.. splittedWord.Split('+')];
                bool correct = true;
                foreach (var wordSplit in wordSplits)
                {
                    //Elke split gaan nakijken in de hashtables of deze voorkomt.
                    if (!DictionaryOfHashSets[wordSplit.Length].Contains(wordSplit))
                    {
                        correct = false;
                        break;
                    }
                }
                if (correct)
                {
                    output.Add($"{splittedWord}={currentWord}");
                }
            }
        }
        return output;
    }

    private Dictionary<int, HashSet<string>> CreateDictionaryOfHashSets(List<string> words, int wordToFindLength)
    {
        Dictionary<int, HashSet<string>> DictionaryOfHashSets = new();
        // Creating the hashsets
        for (int i = 1; i <= wordToFindLength; i++)
        {
            DictionaryOfHashSets.Add(i, new HashSet<string>());
        }

        //Filling the hashsets buckets.
        foreach (string text in words)
        {
            if (text.Length <= wordToFindLength)
            {
                DictionaryOfHashSets[text.Length].Add(text);
            }
        }

        return DictionaryOfHashSets;
    }
}
