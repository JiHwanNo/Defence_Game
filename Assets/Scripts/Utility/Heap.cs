using System;

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}

public class Heap<T> where T : IHeapItem<T>
{
    readonly T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        currentItemCount = 1;
        items = new T[maxHeapSize + currentItemCount];
    }

    public int Count { get => currentItemCount - 1; }
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    void SortUp(T item)
    {
        int parentIndex = 0;

        while ((parentIndex = GetParentIndex(item.HeapIndex))!= 0)
        {
            T parentNode = items[parentIndex];

            //item�� �켱������ ������ ��庸�� ���ٸ�
            if (item.CompareTo(parentNode) < 0)
            {
                //�ٲ㼭 �����Ѵ�.
                Swap(item, parentNode);
            }
            else
                break;
        }
    }
    public T RemoveFirst()
    {
        // ù��° ��
        T firstItem = items[1];
        currentItemCount--;
        items[1] = items[currentItemCount];
        items[1].HeapIndex = 1;
        SortDown(items[1]);

        return firstItem;
    }
    void SortDown(T item)
    {
        int childIndex = 0;
        while((childIndex = GetChildIndex(item.HeapIndex))!=0)
        {
            T childItem = items[childIndex];

            //item�� �켱���̰� 
            if (item.CompareTo(childItem) > 0)
                Swap(item, childItem);
            else
                break;
        }
    }



    private void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        int tempIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = tempIndex;
    }

    private int GetParentIndex(int selfIndex)
    {
        return selfIndex / 2;
    }

    private int GetChildIndex(int selfIndex)
    {
        if (GetLeftChildIndex(selfIndex) > Count)
            return 0;
        else if (GetLeftChildIndex(selfIndex) == Count)
            return GetLeftChildIndex(selfIndex);
        else
        {
            if(items[GetLeftChildIndex(selfIndex)].CompareTo(items[GetRightChildIndex(selfIndex)])<0)
            {
                return GetLeftChildIndex(selfIndex);
            }
            else
            {
                return GetRightChildIndex(selfIndex);
            }
        }
    }
    int GetLeftChildIndex(int selfIndex)
    {
        return selfIndex * 2;
    }
   int GetRightChildIndex(int selfIndex)
    {
        return selfIndex * 2 + 1;
    }
}
