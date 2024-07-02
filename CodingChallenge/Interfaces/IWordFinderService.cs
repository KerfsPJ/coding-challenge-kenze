namespace IWordFinderService;

public interface IWordFinderService
{
    public List<string> FindWordsInList(List<string> words, int wordToFindLength);
}