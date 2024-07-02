﻿// See https://aka.ms/new-console-template for more information

//Structures
using Services;

List<string> list = new List<string>();
string inputFileLocation = "input.txt"; //Default values
int lengthToSearch = 6; //Default values
var arguments = Environment.GetCommandLineArgs();

//Using CommandLine Variables
if (arguments.Length == 3) // DLL + LengthToSearch + InputFile Location
{
    if (!int.TryParse(arguments[1], out lengthToSearch))
    {
        Console.WriteLine("Cannot Parse!");
        lengthToSearch = 6;
    }
    inputFileLocation = arguments[2];
}

try
{
    // Open the text file using a stream reader.
    using StreamReader reader = new(inputFileLocation);

    string line;

    // Loop over every line in the file.
    while ((line = reader.ReadLine()) != null)
    {
        // Read the stream as a string and trim potential unwanted spaces. --> Can we skip some input here?
        string text = reader.ReadLine().Trim();

        if (text.Length <= lengthToSearch) //Skip input that is too long
        {
            list.Add(text);
        }
    }

}
catch (IOException e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}


Console.WriteLine($"<code>");

WordFinderService wfservice = new WordFinderService();
var output = wfservice.FindWordsInList(list, lengthToSearch);

foreach (var s in output)
{
    Console.WriteLine(s);
}
Console.WriteLine($"</code>");


