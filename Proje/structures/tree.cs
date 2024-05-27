using System.Text;
using System;

namespace structures
{
    public class treeNode<T,V>
    {
        public T self;
        public linkedList<V> chlidren;

        public treeNode(T value)
        {
            self = value;
            chlidren = new linkedList<V>();
        }

        /// <summary>
        /// link listine children eklenir
        /// O(1) 
        /// </summary>
        /// <param name="newV"></param>
        public void addChildren(V newV)
        {
            chlidren.add(newV);
        }
        
        /// <summary>
        /// link listten children çıkarma işlemi
        /// O(N) 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public V removeChildren(int index)
        {
            V removed = chlidren.remove(index);

            return removed;
        }

        /// <summary>
        /// linked listten index ile childerını alma
        /// O(N)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public V getChildren(int index)
        {
            //System.Console.WriteLine(index);
            V get = chlidren.get(index);

            return get;
        }
        
        public void Print(int space)
        {
            Console.WriteLine(this.ToString());
        }

        /// <summary>
        /// nodu stringe dönüştürür
        /// Karmaşıklığı Max(self.ToString(),O(N) * children.ToString()) 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(self.ToString());

            sb.AppendLine("  " + chlidren.ToString().Replace(Environment.NewLine, Environment.NewLine + "  "));

            sb.Remove(sb.Length-1,1);

            return sb.ToString();
        }
    }


    /// <summary>
    /// çocukları treeNode< T,V > olan kendisi R olan bir treenode dur aslında 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="R"></typeparam>
    public class tree<T,V,R>
    {
        public treeNode<R , treeNode<T,V>> root;

        public tree(R name)
        {
            root = new treeNode<R, treeNode<T, V>>(name);
        }

        /// <summary>
        /// yeni bir alt ağaç ekler
        /// O(1)
        /// </summary>
        /// <param name="newT"></param>
        public void add(treeNode<T,V> newT)
        {
            root.addChildren( newT );
        }

        /// <summary>
        /// ağaçtan bir alt ağaç çıkarır ve bunu döndürür
        /// O(N)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public treeNode<T, V> remove(int index)
        {
            treeNode<T, V> removed = root.removeChildren(index);

            return removed;
        }

        /// <summary>
        /// indexe göre bir alt ağaç döndürür
        /// O(N)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public treeNode<T, V> get(int index)
        {
            treeNode<T, V> get = root.removeChildren(index);

            return get;
        }

        /// <summary>
        /// ToString() değerini yazdırır
        /// Karmaşıklığı ToString() ile aynıdır 
        /// </summary>
        public void Print()
        {
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// nodu stringe dönüştürür
        /// Karmaşıklığı Max(self.ToString(),O(N) * children.ToString()) 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return root.ToString();
        }
    }
}