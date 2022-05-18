using System;
using System.Collections;
using System.Collections.Generic;

// prostokątna macierz bitów o wymiarach m x n
public partial class BitMatrix : IEquatable<BitMatrix>, IEnumerable<int>, ICloneable
{
    private BitArray data;
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;

    // tworzy prostokątną macierz bitową wypełnioną `defaultValue`
    public BitMatrix(int numberOfRows, int numberOfColumns, int defaultValue = 0)
    {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new ArgumentOutOfRangeException("Incorrect size of matrix");
        data = new BitArray(numberOfRows * numberOfColumns, BitToBool(defaultValue));
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }

    public static int BoolToBit(bool boolValue) => boolValue ? 1 : 0;
    public static bool BitToBool(int bit) => bit != 0;

    public override string ToString()
    {
        string all = "";
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] == false)
            {
                all += 0;
            }
            else
            {
                all += 1;
            }
            if ((i + 1) % NumberOfColumns == 0)
            {
                all += "\r\n";
            }
        }
        return all;
    }

    public BitMatrix(int numberOfRows, int numberOfColumns, params int[] bits)
    {

        if (bits == null || bits.Length == 0)
        {
            data = new BitArray(numberOfRows * numberOfColumns, BitToBool(0));
        }
        else
        {
            int count = numberOfColumns * numberOfRows;
            bool[] arrbits = new bool[count];
            if (count == bits.Length)
            {

                for (int i = 0; i < bits.Length; i++)
                {
                    arrbits[i] = BitToBool(bits[i]);
                }
            }
            else if (bits.Length < count)
            {
                for (int i = 0; i < bits.Length; i++)
                {
                    arrbits[i] = BitToBool(bits[i]);
                }
                for (int i = bits.Length; i == count; i++)
                {
                    arrbits[i] = false;
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    arrbits[i] = BitToBool(bits[i]);
                }
            }
            data = new BitArray(arrbits);
        }
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }

    public BitMatrix(int[,] bits)
    {
        if (bits == null)
        {
            throw new NullReferenceException();
        }
        else if (bits.Length == 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            NumberOfRows = bits.GetLength(0);
            NumberOfColumns = bits.GetLength(1);
            bool[] arrbits = new bool[bits.Length];
            int x = 0;
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    arrbits[x] = BitToBool(bits[i, j]);
                    x++;
                }
            }
            data = new BitArray(arrbits);

        }
    }

    public BitMatrix(bool[,] bits)
    {
        if (bits == null)
        {
            throw new NullReferenceException();
        }
        else if (bits.Length == 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            NumberOfRows = bits.GetLength(0);
            NumberOfColumns = bits.GetLength(1);
            bool[] arrbits = new bool[bits.Length];
            int x = 0;
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    arrbits[x] = bits[i, j];
                    x++;
                }
            }
            data = new BitArray(arrbits);

        }
    }

    public bool Equals(BitMatrix other)
    {
        if (other == null) return false;
        if (this.NumberOfColumns != other.NumberOfColumns || this.NumberOfRows != other.NumberOfRows)
        {
            return false;
        }
        int lenght = this.NumberOfColumns * this.NumberOfRows;

        for (int i = 0; i < lenght; i++)
        {
            if (this.data[i] != other.data[i])
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj is BitMatrix)
        {
            return this.Equals(obj);
        }
        else
        {
            return false;
        }
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public IEnumerator<int> GetEnumerator()
    {
        int[] arrbits = new int[this.NumberOfColumns * this.NumberOfRows];
        for (int i = 0; i < this.NumberOfColumns * this.NumberOfRows; i++)
        {
            if (this.data[i] == false)
            {
                arrbits[i] = 0;
            }
            else
            {
                arrbits[i] = 1;
            }
        }
        foreach (var item in arrbits)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        int[] arrbits = new int[this.NumberOfColumns * this.NumberOfRows];
        for (int i = 0; i < this.NumberOfColumns * this.NumberOfRows; i++)
        {
            if (this.data[i] == false)
            {
                arrbits[i] = 0;
            }
            else
            {
                arrbits[i] = 1;
            }
        }
        foreach (var item in arrbits)
        {
            yield return item;
        }
    }

    public int this[int index1, int index2]
    {
        get
        {
            if (index1 >= 0 && index2 >= 0 && index1 <= this.NumberOfColumns && index2 <= this.NumberOfRows)
            {
                int place = 0;
                if (index1 == 0)
                {
                    place = index2;
                }
                else
                {
                    place = (index1 * this.NumberOfColumns) + index2;
                    if (place < 0)
                    {
                        place = 0;
                    }
                }
                if (place <= (this.NumberOfColumns * this.NumberOfRows))
                {
                    if (this.data[place] == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        set
        {
            int place = 0;
            if (index1 == 0)
            {
                place = index2;
            }
            else
            {
                place = (index1 * this.NumberOfColumns) + index2;
            }
            if (place < 0)
            {
                place = 0;
            }
            if (place <= (this.NumberOfColumns * this.NumberOfRows))
            {
                if (value > 0)
                {
                    this.data[place] = true;
                }
                else
                {
                    this.data[place] = false;
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    public object Clone()
    {
        int[] arr = new int[this.NumberOfColumns * this.NumberOfRows];
        for (int i = 0; i < this.NumberOfColumns * this.NumberOfRows; i++)
        {
            if (this.data[i] == false)
            {
                arr[i] = 0;
            }
            else
            {
                arr[i] = 1;
            }
        }
        return new BitMatrix(this.NumberOfRows, this.NumberOfColumns, arr);
    }
    object ICloneable.Clone()
    {
        return this.MemberwiseClone();
    }

    public static BitMatrix Parse(string s)
    {
        if (s == null || s.Length == 0)
        {
            throw new ArgumentNullException();
        }

        var ints = new List<int>();
        int rows = 0;
        int cols = -1;
        var lines = s.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            foreach (char c in line.Trim())
            {
                var toAdd = (int)char.GetNumericValue(c);
                if (toAdd != 0 && toAdd != 1)
                {
                    throw new FormatException();
                }
                ints.Add(toAdd);
            }
            rows++;

            if (cols != -1 && cols != line.Trim().Length)
            {
                throw new FormatException();
            }
            cols = line.Trim().Length;
        }

        return new BitMatrix(rows, cols, ints.ToArray());
    }

    public static bool TryParse(string s, out BitMatrix result)
    {
        result = new BitMatrix(1, 1);
        if (s == null || s.Length == 0)
        {
            return false;
        }

        var ints = new List<int>();
        int rows = 0;
        int cols = -1;
        var lines = s.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            foreach (char c in line.Trim())
            {
                var toAdd = (int)char.GetNumericValue(c);
                if (toAdd != 0 && toAdd != 1)
                {
                    return false;
                }
                ints.Add(toAdd);
            }
            rows++;

            if (cols != -1 && cols != line.Trim().Length)
            {
                return false;
            }
            cols = line.Trim().Length;
        }

        result = new BitMatrix(rows, cols, ints.ToArray());
        return true;
    }

    public static explicit operator BitMatrix(int[,] array)
    {
        if (array == null)
        {
            throw new NullReferenceException();
        }
        if (array.Length == 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return new BitMatrix(array);
    }
    public static explicit operator BitMatrix(bool[,] array)
    {
        if (array == null)
        {
            throw new NullReferenceException();
        }
        if (array.Length == 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return new BitMatrix(array);
    }
    public static implicit operator int[,](BitMatrix bm)
    {
        var array = new int[bm.NumberOfRows, bm.NumberOfColumns];
        for (int i = 0; i < bm.NumberOfRows; i++)
        {
            for (int j = 0; j < bm.NumberOfColumns; j++)
            {
                array[i, j] = bm[i, j];
            }
        }
        return array;
    }
    public static implicit operator bool[,](BitMatrix bm)
    {
        var array = new bool[bm.NumberOfRows, bm.NumberOfColumns];
        for (int i = 0; i < bm.NumberOfRows; i++)
        {
            for (int j = 0; j < bm.NumberOfColumns; j++)
            {
                array[i, j] = BitToBool(bm[i, j]);
            }
        }
        return array;
    }
    public static explicit operator BitArray(BitMatrix bm) => new BitArray(bm.data);
}