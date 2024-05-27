using System.Text;
using System;

namespace structures
{
    
        public class queueNode<T>
        {
            /// <summary>
            /// Burada bir kuyruk modülü oluşturuz
            /// Kuyruk modülünün 
            /// </summary>
            public T veri;
            public queueNode<T> ileri;

            public queueNode(T veri)
            {
                this.veri = veri;
                ileri = null;
            }

            /// <summary>
            /// T türünün stringe dönüştürmesi
            /// Karmaşıklığı T türünün ToString() fonkiyonunun karmaşıklığı kadardır.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return veri.ToString();
            }
        }

        public class AQueue<T>
        {
            public queueNode<T> bas;
            public queueNode<T> son;

            public AQueue()
            {
                bas = null;
                son = null;
            }
            
            /// <summary>
            /// Kuyruk nun boş olup olmadığını kontrol eder
            /// O(1) 
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                if(bas == null)
                    return true;
                else
                    return false;
            }


            /// <summary>
            /// Kuyruk'a verilen elemanı kuyruğa ekler.
            /// O(1) 
            /// </summary>
            /// <param name="veri"></param>
            public void Enqueue(T veri)
            {
                queueNode<T> node = new queueNode<T>(veri);

                if(!IsEmpty())
                    son.ileri = node;
                else
                    bas = node;
                son = node;
            }

            /// <summary>
            /// Queue dan eleman çıkarır
            /// O(1)
            /// </summary>
            /// <returns></returns>
            public T Dequeue()
            {
                queueNode<T> temp;
                temp = bas;

                if(!IsEmpty())
                {
                    bas = bas.ileri;
                    if(bas == null)
                        son = null;
                }
                return temp.veri;
            }

            /// <summary>
            /// Burada bize verilen ifadeyi string'e çeviriyoruz.
            /// O(N)
            /// </summary>
            /// <returns>Burada ToString() metodunu ezip tekrar yazdık.</returns>
            public override string ToString()
            {
                //System.Console.WriteLine("asdasdasd");

                StringBuilder sb = new StringBuilder();
                queueNode<T> temp = bas;
                int index = 1;
                while (temp != null)
                {
                    sb.AppendLine($"[{index}] {temp.veri.ToString()}");
                    temp = temp.ileri;
                    index++;
                }

                sb.Remove(sb.Length - 1, 1);

                return sb.ToString();
            }
        }
}