#!/usr/bin/env dotnet

Console.WriteLine("Hello!");

var people = new Dictionary<string, string[]>
{
  { "You", new string[] {"Bob", "Claire", "Alice"} },
  { "Claire", new string[] {"Thom", "Jonny" }},
  { "Thom", new string[] {}},
  { "Jonny", new string[] {}},
  { "Bob", new string[] {"ANUJ-seller", "Peggy" }},
  { "ANUJ-seller", new string[] {}},
  { "Peggy", new string[] {} },
  { "Alice", new string[] {"Peggy"} },
};


var found = BreadthFirstSearch(people, "You");
if (string.IsNullOrEmpty(found))
{
    Console.WriteLine("Seller was not found.");
}

string BreadthFirstSearch(Dictionary<string, string[]> people, string name)
{
    var searchQueue = new Queue<string>();

    var x = people[name];
    if (x?.Length > 0)
    {
        foreach (var y in x)
        {
            searchQueue.Enqueue(y);
        }
    }

    var searched = new HashSet<string>();

    while (searchQueue.Any())
    {
        var person = searchQueue.Dequeue();
        if (person == null || searched.Contains(person))
        {
            continue;
        }

        var personIsSeller = person?.EndsWith("-seller");
        if (personIsSeller.HasValue && personIsSeller.Value == true)
        {
            return person;
        }

        x = people[person];
        if (x?.Length > 0)
        {
            foreach (var y in x)
            {
                searchQueue.Enqueue(y);
            }
        }
        searched.Add(person);
    }

    return null;
}