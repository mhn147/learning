using System;
using System.Collections.Generic;

public class MyHashTable<TKey, TValue>
{
    private class Entry
    {
        public TKey Key;
        public TValue Value;
        public Entry Next;
    }

    private Entry[] buckets;
    private int size;
    private const float LoadFactor = 0.75f;

    private void EnsureCapacity()
    {
        if ((float)size / buckets.Length < LoadFactor)
            return;

        Resize();
    }

    private void Resize()
    {
        Entry[] oldBuckets = buckets;

        buckets = new Entry[oldBuckets.Length * 2];
        size = 0;

        foreach (var head in oldBuckets)
        {
            Entry current = head;

            while (current != null)
            {
                Put(current.Key, current.Value); // re-inserts using new hash mapping
                current = current.Next;
            }
        }
    }

    public HashTable(int capacity = 16)
    {
        buckets = new Entry[capacity];
    }

    private int GetBucketIndex(TKey key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        int hash = key.GetHashCode();
        hash = hash & 0x7fffffff; // make it positive
        return hash % buckets.Length;
    }

    public void Put(TKey key, TValue value)
    {
        int index = GetBucketIndex(key);

        Entry current = buckets[index];

        // update if key already exists
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                current.Value = value;
                return;
            }
            current = current.Next;
        }

        // insert new entry at head (chaining)
        Entry newEntry = new Entry
        {
            Key = key,
            Value = value,
            Next = buckets[index]
        };

        buckets[index] = newEntry;
        size++;
    }

    public bool TryGet(TKey key, out TValue value)
    {
        int index = GetBucketIndex(key);

        Entry current = buckets[index];

        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                value = current.Value;
                return true;
            }
            current = current.Next;
        }

        value = default;
        return false;
    }

    public bool Remove(TKey key)
    {
        int index = GetBucketIndex(key);

        Entry current = buckets[index];
        Entry prev = null;

        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                if (prev == null)
                    buckets[index] = current.Next;
                else
                    prev.Next = current.Next;

                size--;
                return true;
            }

            prev = current;
            current = current.Next;
        }

        return false;
    }

    public int Count => size;
}