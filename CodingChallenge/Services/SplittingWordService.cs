namespace Services;

using ISplittingWordService;

public class SplittingWordService : ISplittingWordService
{

    private Dictionary<int, List<int>> dictionaryOfCombinationsForEachLength = new();
    public List<string> SplitWordIntoParts(string word)
    {
        if (!dictionaryOfCombinationsForEachLength.ContainsKey(word.Length))
        {
            dictionaryOfCombinationsForEachLength.Add(word.Length, GenerateCombinationsForLength(word.Length));
        }


        List<string> splittedWordList = new();
        foreach (int combo in dictionaryOfCombinationsForEachLength[word.Length])
        {
            string splittedWord = word;
            int currentCombo = combo;
            while (currentCombo / 10 > 0)
            {
                var rest = currentCombo % 10;
                splittedWord = splittedWord.Insert(rest, "+");
                currentCombo /= 10;
            }
            splittedWord = splittedWord.Insert(currentCombo, "+");
            splittedWordList.Add(splittedWord);
        }
        return splittedWordList;
    }

    private List<int> GenerateCombinationsForLength(int length)
    {
        List<int> combinations = new List<int>();
        for (int i = 0; i < length; i++)
        {
            GenerateCombinations(0, i, length, combinations);
        }
        return combinations;
    }

    private void GenerateCombinations(int start, int remainingDigits, int wordLength, List<int> result, string current = "")
    {
        if (remainingDigits == 0 && !string.IsNullOrEmpty(current))
        {
            result.Add(int.Parse(current));
            return;
        }

        for (int i = start + 1; i <= wordLength - 1; i++)
        {
            GenerateCombinations(i, remainingDigits - 1, wordLength, result, current + i);
        }
    }
}