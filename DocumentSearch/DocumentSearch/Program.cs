using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace DocumentSearchNS
{
    public class DocumentSearch
    {
        private static readonly string IndexedFolderName = "LuceneIndex";
        private enum Results
        {
            LowerBound,
            StringMatch,
            Regex,
            Indexed,
            UpperBound
        }

        private static void Main(string[] args)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                string text1 = File.ReadAllText(Path.Combine(currentDirectory, "Sample_Text", "french_armed_forces.txt"));
                string text2 = File.ReadAllText(Path.Combine(currentDirectory, "Sample_Text", "hitchhikers.txt"));
                string text3 = File.ReadAllText(Path.Combine(currentDirectory, "Sample_Text", "warp_drive.txt"));

                StartSearch(text1, text2, text3);

                Console.WriteLine("Press key to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: " + ex?.InnerException);
                Console.WriteLine("Closing application, press key to continue");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Starts the Searching process by first prompting the user, and then
        /// processing the user's input. If the inputs are valid, the user will
        /// be able to see if their term is within the sample files. If the input
        /// is not valid, they will be prompted to try again.
        /// </summary>
        /// <param name="text1">french_armed_forces.txt contents</param>
        /// <param name="text2">hitchhikers.txt contents</param>
        /// <param name="text3">warp_drive.txt contents</param>
        public static void StartSearch(string text1, string text2, string text3)
        {
            Console.WriteLine("Enter the search term: ");
            var termToSearch = Console.ReadLine();

            Console.WriteLine("Search Method: 1.) String Match 2.) Regular Expression 3.) Indexed");
            var selection = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(termToSearch)
                && int.TryParse(selection, out int result)
                && (int)Results.LowerBound < result && result < (int)Results.UpperBound)
            {
                var noCharsTerm = Regex.Replace(termToSearch, @"[^\w\s]", string.Empty).ToLower();

                Console.WriteLine("Processing...");

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                if (result == (int)Results.StringMatch)
                {
                    Console.WriteLine("french_armed_forces.txt - {0} matches", StringSearch(text1, noCharsTerm));
                    Console.WriteLine("hitchhikers.txt - {0} matches", StringSearch(text2, noCharsTerm));
                    Console.WriteLine("warp_drive.txt - {0} matches", StringSearch(text3, noCharsTerm));
                }
                else if (result == (int)Results.Regex)
                {
                    Console.WriteLine("french_armed_forces.txt - {0} matches", RegexSearch(text1, noCharsTerm));
                    Console.WriteLine("hitchhikers.txt - {0} matches", RegexSearch(text2, noCharsTerm));
                    Console.WriteLine("warp_drive.txt - {0} matches", RegexSearch(text3, noCharsTerm));
                }
                else
                {
                    Console.WriteLine("french_armed_forces.txt - {0} matches", IndexedSearch(text1, noCharsTerm));
                    Console.WriteLine("hitchhikers.txt - {0} matches", IndexedSearch(text2, noCharsTerm));
                    Console.WriteLine("warp_drive.txt - {0} matches", IndexedSearch(text3, noCharsTerm));
                }

                stopwatch.Stop();
                Console.WriteLine("Elapse time: {0}h {1}m {2}s {3}ms", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes,
                    stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            }
            else
            {
                Console.WriteLine("Not a valid input");
                StartSearch(text1, text2, text3);
            }
        }

        /// <summary>
        /// String Match search using simple string comparision
        /// </summary>
        /// <param name="text">The text to be searched</param>
        /// <param name="pattern">The term to be searched for</param>
        /// <returns>The number of results found</returns>
        public static int StringSearch(string text, string pattern)
        {
            int matchCount = 0;

            var noChars = Regex.Replace(text, @"[^\w\s]", string.Empty).ToLower();
            string[] stringSearch = noChars.Split(" ");

            for (int i = 0; i < stringSearch.Length; i++)
            {
                if (string.Equals(stringSearch[i], pattern))
                {
                    matchCount++;
                }
            }

            return matchCount;
        }

        /// <summary>
        /// Regular Expression search using Regular Expressions
        /// </summary>
        /// <param name="text">The text to be searched</param>
        /// <param name="pattern">The term to be searched for</param>
        /// <returns>The number of results found</returns>
        public static int RegexSearch(string text, string pattern)
        {
            int matchCount = 0;
            var searchPattern = @$"{pattern}\w*";

            Regex r = new Regex(searchPattern, RegexOptions.IgnoreCase);
            Match m = r.Match(text);

            while (m.Success)
            {
                matchCount++;
                m = m.NextMatch();
            }

            return matchCount;
        }

        /// <summary>
        /// Indexed Search using Lucene.net Nuget Package. This code is based on
        /// the tutorial from http://lucenenet.apache.org/ and https://gist.github.com/jonsagara/1502416
        /// </summary>
        /// <param name="text">The text to be searched</param>
        /// <param name="pattern">The term to be searched for</param>
        /// <returns>The number of results found</returns>
        public static int IndexedSearch(string text, string pattern)
        {
            var noChars = Regex.Replace(text, @"[^\w\s]", string.Empty).ToLower();

            // Creates folder to store the Lucene indexes if one does not exist
            string folderPath = System.IO.Directory.GetCurrentDirectory() + "\\" + IndexedFolderName;
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(IndexedFolderName);
            }

            // Initialize the Directory and the IndexWriter.
            var dirInfo = new DirectoryInfo(IndexedFolderName);
            Lucene.Net.Store.Directory directory = FSDirectory.Open(dirInfo);
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

            // Add Documents to the Index.
            Document doc = new Document();
            doc.Add(new Field("id", "1", Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("postBody", noChars, Field.Store.YES, Field.Index.ANALYZED));
            writer.AddDocument(doc);

            writer.Dispose();

            // Create the Query.
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "postBody", analyzer);
            Query query = parser.Parse(pattern);

            // Pass the Query to the IndexSearcher.
            IndexSearcher searcher = new IndexSearcher(directory, readOnly: false);
            TopDocs hits = searcher.Search(query, 200); /* top 200 hits */
            int resultsCount = hits.ScoreDocs.Length;

            // Return the search result count
            return resultsCount;
        }
    }
}
