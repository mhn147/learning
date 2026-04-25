int[] QuickSort(int[] arr)
{
    if (arr.Length < 2)
    {
        return arr;
    }

    var pivot = arr[0];
    var less = arr.Where(el => el < pivot).ToArray();
    var greater = arr.Where(el => el > pivot).ToArray();
    return [.. QuickSort(less), pivot, .. QuickSort(greater)];
}

var arr1 = new int[] { 1, 15, 7, 25, 88, 45, 102 };
var sorted = QuickSort(arr1);
Console.WriteLine($"Result: {sorted}");