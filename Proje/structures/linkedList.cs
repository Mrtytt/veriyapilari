using System.Text;
using System;

namespace structures
{
    public class listNode<T>
    {
        public T self;
        public listNode<T> next;

        public listNode(T item)
        {
            self = item;
            next = null;
        }
    }

    public class linkedList<T>
    {

        public listNode<T> head;
        public listNode<T> end;

        public linkedList()
        {
            head = null;
            end = null;
        }

        /// <summary>
        /// listenin boş mu olup olmadığını kontrol eder
        /// O(1)
        /// </summary>
        public bool isEmpty
        {
            get {
                return head == null;
            }
        }

        /// <summary>
        /// listenin tek elemanı olma özel durumunu kontrol eder
        /// O(1)
        /// </summary>
        public bool hasOneElement
        {
            get {
                return head == end;
            }
        }

        /// <summary>
        /// listenini uzunluğunu döndürür
        /// O(N)
        /// </summary>
        public int Lenght
        {
            get {

                int count = 0;
                listNode<T> temp = head;

                while(temp != null)
                {
                    temp = temp.next;
                    count++;
                }

                return count;
            }
        }

        /// <summary>
        /// listeye yeni bir eleman ekler
        /// O(1)
        /// </summary>
        /// <param name="item"></param>
        public void add(T item)
        {
            listNode<T> newNode = new listNode<T>(item);

            //System.Console.WriteLine("called 1");

            //this.Print(2);

            if (isEmpty)
            {
                head = newNode;
                end = newNode;
            }
            else if(head == end)
            {
                head.next = newNode;
                end = newNode;
            }
            else 
            {
                //System.Console.WriteLine("  called 2");
                end.next = newNode;
                end = newNode;
            }
        }


        /// <summary>
        /// listeden indexine göre eleman çıkarır
        /// O(N)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T remove(int index)
        {
            //System.Console.WriteLine(index);

            if (index == 0)
            {
                listNode<T> removedHead = head;
                head = head.next;
                return removedHead.self;
            }

            listNode<T> temp = head;
            int count = 0;

            while (temp != null && count < index - 1)
            {
                temp = temp.next;
                count++;
            }

            listNode<T> removed = temp.next;

            // Remove the node from the list
            temp.next = temp.next.next;

            return removed.self;
        }

        /// <summary>
        /// listede index değerine göre elemanı döndürür
        /// O(N)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T get(int index)
        {
            listNode<T> temp = head;

            //System.Console.WriteLine(index);

            int count = 0;
            while (count++ < index)
            {
                temp = temp.next;
            }

            listNode<T> get = temp;

            return get.self;
        }

        /// <summary>
        /// ToString() değerini yazdırır
        /// O(N)
        /// </summary>
        /// <param name="space"></param>
        public void Print(int space)
        {
            Console.WriteLine(this.ToString());
        }

        /// <summary>
        /// listeyi arraye çevirir
        /// O(N) 
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] val =  new T[Lenght];
            listNode<T> temp = head;

            int index = 0;
            while (temp != null)
            {   
                val[index++] = temp.self;
                temp = temp.next;
            }

            return val;
        }

        /// <summary>
        /// listeyi stringe çevirir
        /// O(N)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            listNode<T> temp = head;
            int index = 1;
            while (temp != null)
            {

                if (temp.self == null)
                {
                    sb.AppendLine("-----------------");
                    temp = temp.next;
                    index++;
                    continue;
                }

                sb.AppendLine($"[{index}] {temp.self.ToString()}");
                temp = temp.next;
                index++;
            }
            //System.Console.WriteLine(index);

            if (sb.Length - 1 < 0)
                sb.AppendLine(" ");

            sb.Remove(sb.Length - 1,1);

            return sb.ToString();
        }
    }
}