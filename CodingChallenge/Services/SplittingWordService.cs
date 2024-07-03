namespace Services;

using ISplittingWordService;

public class SplittingWordService : ISplittingWordService
{
    public List<string> SplitWordIntoParts(string word)
    {
        List<int> combinations = new List<int>();
        for (int i = 0; i < word.Length; i++)
        {
            GenerateCombinations(0, i, word.Length, combinations);
        }

        List<string> SplittedWordList = new();
        foreach (int combo in combinations)
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
            SplittedWordList.Add(splittedWord);
        }
        return SplittedWordList;
    }

    void GenerateCombinations(int start, int remainingDigits, int wordLength, List<int> result, string current = "")
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