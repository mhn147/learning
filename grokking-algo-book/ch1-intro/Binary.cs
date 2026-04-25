// Logarithms are about what number you need to raise to, to get another number
// 2**x = 16 ==> x = 4
// 2**x = 16 equiv to log(2)16=x
// log(3)81 = x equiv to 3**x=81
// log(n)n**x=x log(2)2**3=3 log(2)2**5=5
// worst case: log(2)128=7
var namesArr = File.ReadAllLines("sorted-names.txt");
Console.WriteLine($"Searching for names in sorted-names.txt. n = {namesArr.Length}");

var iterations = 0;
for (var i = 0; i < 10; i++)
{
    var index = (new Random()).Next(0, namesArr.Length);
    var name = namesArr[index];
    Console.WriteLine($"#{i + 1} - Random name selected: '{name}'. index = '{index}'");
    binarySearch(namesArr, name);
    Console.WriteLine($"#{i + 1} - Item '{name}' found in '{iterations}' iterations\n");
    iterations = 0;
}

bool binarySearch(string[] namesArr, string item)
{
    var low = 0;
    var high = namesArr.Length - 1;

    while (low <= high)
    {
        iterations++;
        var mid = (low + high) / 2;
        var guess = namesArr[mid];

        var compare = string.Compare(guess, item);
        if (compare == 0)
        {
            return true;
        }
        if (compare > 0)
        {
            high = mid - 1;
        }
        else
        {
            low = mid + 1;
        }
    }

    return false;
}