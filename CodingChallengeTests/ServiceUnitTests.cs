namespace CodingChallengeTests;

using Services;

public class ServiceUnitTests
{


    public static IEnumerable<object[]> CorrectInputData()
    {
        yield return new object[] { new List<string> { "a", "nthem", "anthem" }, 6, new List<string> { "a+nthem=anthem" }, 1 };
        yield return new object[] { new List<string> { "an", "them", "anthem" }, 6, new List<string> { "an+them=anthem" }, 1 };
        yield return new object[] { new List<string> { "ant", "hem", "anthem" }, 6, new List<string> { "ant+hem=anthem" }, 1 };
        yield return new object[] { new List<string> { "anth", "em", "anthem" }, 6, new List<string> { "anth+em=anthem" }, 1 };
        yield return new object[] { new List<string> { "anthe", "m", "anthem" }, 6, new List<string> { "anthe+m=anthem" }, 1 };
    }



    [Theory]
    [MemberData(nameof(CorrectInputData))]
    public void WordFinderService_WithCorrectInputs_ShouldReturnCorrectResults(List<string> words, int wordToFindLength, List<string> expected, int numberOfCorrectAnswers)
    {
        //Arrange
        var wordFindingService = new WordFinderService();
        //List<string> words = new List<string> { "a", "the", "anthe", "anthemmmmm" };

        //Act
        var ListWithOutputs = wordFindingService.FindWordsInList(words, wordToFindLength);
        Console.WriteLine(ListWithOutputs.Count);
        //Assert
        Assert.Equivalent(ListWithOutputs, expected);
        Assert.Equal(numberOfCorrectAnswers, ListWithOutputs.Count);
    }

    public static IEnumerable<object[]> IncorrectInputData()
    {
        yield return new object[] { new List<string> { "a", "the", "anthem" }, 6, new List<string>(), 0 };
        yield return new object[] { new List<string> { "a", "the", "anthems" }, 6, new List<string>(), 0 };
        yield return new object[] { new List<string> { "a", "t", "anthem" }, 6, new List<string>(), 0 };
    }


    [Theory]
    [MemberData(nameof(IncorrectInputData))]
    public void WordFinderService_WithIncorrectInputs_ShouldReturnNoResults(List<string> words, int wordToFindLength, List<string> expected, int numberOfCorrectAnswers)
    {
        //Arrange
        var wordFindingService = new WordFinderService();
        //List<string> words = new List<string> { "a", "the", "anthe", "anthemmmmm" };

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
}