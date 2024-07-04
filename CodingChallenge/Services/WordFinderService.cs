namespace Services;

using System;
using IWordFinderService;

public class WordFinderService : IWordFinderService
{
    private SplittingWordService splittingWordService = new SplittingWordService();

    public List<string> FindWordsInList(List<string> words, int wordToFindLength)
    {
        (Dictionary<int, HashSet<string>> dictionaryOfHashSets, Dictionary<string, int> dictionaryOfDuplicates) = CreateDictionaryOfHashSetsAndDictionaryOfDuplicates(words, wordToFindLength);
        List<string> output = new();

        foreach (string currentWord in dictionaryOfHashSets[wordToFindLength])
        {
            var splittedWordList = splittingWordService.SplitWordIntoParts(currentWord);

            foreach (var splittedWord in splittedWordList)
            {

                List<string> wordSplits = [.. splittedWord.Split('+')];
                bool correct = true;
                Dictionary<string, int> dictionaryOfAlreadyFoundPartsAndCounter = new();
                foreach (var wordSplit in wordSplits)
                {
                    //Elke split gaan nakijken in de hashtables of deze voorkomt.
                    if (!dictionaryOfHashSets[wordSplit.Length].Contains(wordSplit) || dictionaryOfAlreadyFoundPartsAndCounter.ContainsKey(wordSplit) && !dictionaryOfDuplicates.ContainsKey(wordSplit) || (dictionaryOfAlreadyFoundPartsAndCounter.ContainsKey(wordSplit) && dictionaryOfDuplicates.ContainsKey(wordSplit) && dictionaryOfAlreadyFoundPartsAndCounter[wordSplit] > dictionaryOfDuplicates[wordSplit]))
                    {
                        correct = false;
                        break;
                    }
                    if (dictionaryOfAlreadyFoundPartsAndCounter.ContainsKey(wordSplit))
                    {
                        dictionaryOfAlreadyFoundPartsAndCounter[wordSplit]++;
                    }
                    else
                    {
                        dictionaryOfAlreadyFoundPartsAndCounter.Add(wordSplit, 1);
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

    private (Dictionary<int, HashSet<string>>, Dictionary<string, int>) CreateDictionaryOfHashSetsAndDictionaryOfDuplicates(List<string> words, int wordToFindLength)
    {
        Dictionary<int, HashSet<string>> dictionaryOfHashSets = new();
        Dictionary<string, int> duplicateDictionary = new();
        // Creating the hashsets
        for (int i = 1; i <= wordToFindLength; i++)
        {
            dictionaryOfHashSets.Add(i, new HashSet<string>());
        }

        //Filling the hashsets buckets.
        foreach (string text in words)
        {
            if (text.Length <= wordToFindLength)
            {
                if (!dictionaryOfHashSets[text.Length].Add(text))
                {
                    if (duplicateDictionary.ContainsKey(text))
                    {
                        duplicateDictionary[text]++;
                    }
                    else
                    {
                        duplicateDictionary.Add(text, 2);
                    }
                }
            }
        }

        return (dictionaryOfHashSets, duplicateDictionary);
    }
}
