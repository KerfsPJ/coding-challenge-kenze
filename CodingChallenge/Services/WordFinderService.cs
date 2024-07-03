namespace Services;

using System;
using IWordFinderService;

public class WordFinderService : IWordFinderService
{
    public List<string> FindWordsInList(List<string> words, int wordToFindLength)
    {
        Dictionary<int, HashSet<string>> DictionaryOfHashSets = CreateDictionaryOfHashSets(words, wordToFindLength);
        List<string> output = new();


        //Only works on 2 wordparts.
        foreach (string t in DictionaryOfHashSets[wordToFindLength])
        {
            for (int firstWordLength = 1; firstWordLength < wordToFindLength; firstWordLength++)
            {
                int secondWordLength = wordToFindLength - firstWordLength;

                var firstWord = t.Substring(0, firstWordLength);
                var secondWord = t.Substring(firstWordLength);

                if (DictionaryOfHashSets[firstWordLength].Contains(firstWord) && DictionaryOfHashSets[secondWordLength].Contains(secondWord))
                {
                    output.Add($"{firstWord}+{secondWord}={t}");
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
