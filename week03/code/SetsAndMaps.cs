using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        HashSet<string> seenWords = []; //collect words we've seen, we use hash for O(1) lookups 

        List<string> result = []; //collect results

        foreach (string word in words)
        {
            string reverse = new([word[1], word[0]]); //reverse the word
            if (seenWords.Contains(reverse)) //check if we've seen the reverse
            {
                result.Add($"{word} & {reverse}"); //add the pair to results
            }
            else //case we haven't seen the reverse
            {
                seenWords.Add(word); //add the word to seenWords
            }
        }
        return [.. result]; //convert list to array and return
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            if (fields.Length >3) // ensure there are enough fields
            {
                string degree = fields[3].Trim(); //we now get the degree from the 4th column
                if (degrees.TryGetValue(degree, out int currentCount)) //check if degree already exists in dictionary, used TryGetValue for efficiency over ContainsKey, this way we only do one lookup instead of two
                {
                    degrees[degree] = currentCount + 1;
                }
                else
                {
                    degrees.Add(degree, 1);
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        //array to count occurrences of each letter, this fixed array represents 'a' to 'z'
        //Index 0 = 'a', Index 1 = 'b', ..., Index 25 = 'z'
        //the advantage of using a fixed array is that it uses less memory and is faster than a dictionary for this specific case, like we talked about in class
        int[] letterCounts = new int[26];

        void ProcessWord(string word, bool increment)
        {
            foreach (char c in word)
            {
                if (c == ' ') continue; //ignore spaces
                int index = char.ToLower(c) - 'a'; //convert char to index
                if (index >= 0 && index < 26) //ensure it's a letter
                {
                    if (increment) letterCounts[index]++; //increment for word1
                    else letterCounts[index]--; //decrement for word2
                }
            }
        }
        ProcessWord(word1, true); //process first word, incrementing counts
        ProcessWord(word2, false); //process second word, decrementing counts

        foreach (int count in letterCounts)
        {
            if (count != 0) return false; //if any count is not zero, they are not anagrams
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        //create list to hold results
        List<string> results = new List<string>();
        //iterate through features and extract place and magnitude
        foreach (var feature in featureCollection.Features)
        {
            var mag = feature.Properties.Mag; //get magnitude
            var place = feature.Properties.Place; //get place
            if (mag.HasValue && !string.IsNullOrEmpty(place)) //check for valid data
            {
                results.Add($"{place} - Mag {mag.Value}"); //format and add to results
            }
        }
        return results.ToArray(); //convert list to array and return
    }
}