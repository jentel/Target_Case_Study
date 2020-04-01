# Target Case Study - Document Search
The goal of this exercise was to create a working program to search a set of documents for the given search term or phrase (single token) and return results in order of relevance. 

Three methods were created for searching the documents: 
*	Simple string matching
*	Text search using regular expressions
*	Preprocess the content and then search the index

This program prompts the user to enter a search term and search method, execute the search, and return results. 

This program is not case sensitive and will convert all inputs to lower case for easier matching. The program also does not search for special characters and will remove them if inputted for easier searching. This includes words seperated by spaces.

Three sample texts have already been provided.

### Prerequisites
* Visual Studio

### Installation
1. Open the solution in Visual Studio
2. Ensure that **DocumentSearch** project is set as the Start-up project
3. Press F5 to build and run the project. Once finished, the console application will open.

### Running Unit Tests
1. Open the solution in Visual Studio
2. Right click on the **DocumentSearchTests**
3. Select "Run Tests" in the menu

### Known Issue
Indexed Search currently only finds the first result if one exits, and only returns a count of 1.
