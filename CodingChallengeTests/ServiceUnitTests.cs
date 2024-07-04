namespace CodingChallengeTests;

using Services;

public class ServiceUnitTests
{


    public static IEnumerable<object[]> WordFinderService_CorrectInputData()
    {
        yield return new object[] { new List<string> { "a", "nthem", "anthem" }, 6, new List<string> { "a+nthem=anthem" }, 1 };
        yield return new object[] { new List<string> { "an", "them", "anthem" }, 6, new List<string> { "an+them=anthem" }, 1 };
        yield return new object[] { new List<string> { "ant", "hem", "anthem" }, 6, new List<string> { "ant+hem=anthem" }, 1 };
        yield return new object[] { new List<string> { "anth", "em", "anthem" }, 6, new List<string> { "anth+em=anthem" }, 1 };
        yield return new object[] { new List<string> { "anthe", "m", "anthem" }, 6, new List<string> { "anthe+m=anthem" }, 1 };
    }


    [Theory]
    [MemberData(nameof(WordFinderService_CorrectInputData))]
    public void WordFinderService_WithCorrectInputs_ShouldReturnCorrectResults(List<string> words, int wordToFindLength, List<string> expected, int numberOfCorrectAnswers)
    {
        //Arrange
        var wordFindingService = new WordFinderService();

        //Act
        var ListWithOutputs = wordFindingService.FindWordsInList(words, wordToFindLength);
        Console.WriteLine(ListWithOutputs.Count);
        //Assert
        Assert.Equivalent(ListWithOutputs, expected);
        Assert.Equal(numberOfCorrectAnswers, ListWithOutputs.Count);
    }

    public static IEnumerable<object[]> WordFinderService_IncorrectInputData()
    {
        yield return new object[] { new List<string> { "a", "the", "anthem" }, 6, new List<string>(), 0 };
        yield return new object[] { new List<string> { "a", "the", "anthems" }, 6, new List<string>(), 0 };
        yield return new object[] { new List<string> { "a", "t", "anthem" }, 6, new List<string>(), 0 };
    }


    [Theory]
    [MemberData(nameof(WordFinderService_IncorrectInputData))]
    public void WordFinderService_WithIncorrectInputs_ShouldReturnNoResults(List<string> words, int wordToFindLength, List<string> expected, int numberOfCorrectAnswers)
    {
        //Arrange
        var wordFindingService = new WordFinderService();

        //Act
        var ListWithOutputs = wordFindingService.FindWordsInList(words, wordToFindLength);
        Console.WriteLine(ListWithOutputs.Count);
        //Assert
        Assert.Equivalent(ListWithOutputs, expected);
        Assert.Equal(numberOfCorrectAnswers, ListWithOutputs.Count);
    }


    [Fact]
    public void WordFinderService_WithLongerDataInputThanReQuested_ShouldNotBreakCode()
    {
        //Arrange
        var wordFindingService = new WordFinderService();
        List<string> words = new List<string> { "a", "the", "anthe", "anthemmmmm" };

        //Act
        var ListWithOutputs = wordFindingService.FindWordsInList(words, 6);

        //Assert
        Assert.Empty(ListWithOutputs);
    }


    public static IEnumerable<object[]> SplittingWordService_CorrectInputData()
    {
        yield return new object[] { "a", new List<string>() };
        yield return new object[] { "an", new List<string> { "a+n" } };
        yield return new object[] { "ant", new List<string> { "a+nt", "an+t", "a+n+t" } };
        yield return new object[] { "anth", new List<string> { "a+nth", "an+th", "ant+h", "a+n+th", "a+nt+h", "an+t+h", "a+n+t+h" } };
        yield return new object[] { "anthe", new List<string> { "a+nthe", "an+the", "ant+he", "anth+e", "a+n+the", "a+nt+he", "a+nth+e", "an+t+he", "an+th+e", "ant+h+e", "a+n+t+he", "a+n+th+e", "a+nt+h+e", "an+t+h+e", "a+n+t+h+e" } };
        yield return new object[] { "anthem", new List<string> { "a+nthem", "an+them", "ant+hem", "anth+em", "anthe+m", "a+n+them", "a+nt+hem", "a+nth+em", "a+nthe+m", "an+t+hem", "an+th+em", "an+the+m", "ant+h+em", "ant+he+m", "anth+e+m", "a+n+t+hem", "a+n+th+em", "a+n+the+m", "a+nt+h+em", "a+nt+he+m", "a+nth+e+m", "an+t+h+em", "an+t+he+m", "an+th+e+m", "ant+h+e+m", "a+n+t+h+em", "a+n+t+he+m", "a+n+th+e+m", "a+nt+h+e+m", "an+t+h+e+m", "a+n+t+h+e+m" } };
    }

    [Theory]
    [MemberData(nameof(SplittingWordService_CorrectInputData))]
    public void SplittingWordService_WithCorrectInput_ShouldReturnCorrectResults(string word, List<string> expected)
    {
        //Arrange
        var splittingWordService = new SplittingWordService();

        //Act
        var ListWithSplittedWords = splittingWordService.SplitWordIntoParts(word);

        //Assert
        Assert.Equivalent(ListWithSplittedWords, expected);
    }
}