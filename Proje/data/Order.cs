    using structs;
using structures;
using CLI;

namespace structs
{
    /// <summary>
    /// Burada kullanıcan bir sipariş alıyoruz ve bir sipariş sınıfı oluşturuyoruz.
    /// </summary>
    public class Order
    {

        public Food[] foods;

        public int price;
        public string str;

        public Order(Food[] foods)
        {
            this.foods = foods;

            Update();
        }

        /// <summary>
        /// Burada siparişimizi güncelliyoruz.
        /// Karmaşıklıklığı gelen siparişin içerdiği yemek sayısı kadardır.
        /// O(n) denilebilir.
        /// </summary>
        public void Update()
        {
            string val ="";
            int pr = 0;

            int len = foods.Length;
            int ctr = 0;
            foreach (Food food in foods)
            {
                pr += food.price;
                val += food.name;
                if (++ctr != len)
                    val += ", ";
            }

            str = val;
            price = pr;
        }

        /// <summary>
        /// Burada bize gelen siparişi düzenleyip string hale getiriyoruz.
        /// O(1)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}₺] {1}", price, str);
        }
    }
    /// <summary>
    /// Burada bir sipariş kuyruğu oluşturuyoruz.
    /// 
    /// </summary>
    public class OrderQueue
    {
        AQueue<Order> queue;

        public Menu menu;

        public OrderQueue(Menu menu)
        {
            queue = new AQueue<Order>();
            this.menu = menu;
        }
        /// <summary>
        /// Gelen sipariş stringini içerdiği yemeklere göre bölüp onları siparişlere ekler.
        /// Karmaşıklığı gelen sipariş stringinin uzunluğuna göre değişir ancak O(n) denilebilir.
        /// </summary>
        /// <param name="orderString"></param>
        public void AddOrder(string orderString)
        {

            // "1,1 1,2 2,3 ..."
            string[] orders = orderString.Split(' ');
            int pairCount = orders.Length;

            //"1,1" , "1,2" , "2,3"

            Food[] pairs = new Food[pairCount]; 
            for (int i = 0; i < pairCount; i++)
            {
                string[] t = orders[i].Split(',');
                //System.Console.WriteLine(orders[i]);
                int a = Convert.ToInt32(t[0]) - 1;
                int b = Convert.ToInt32(t[1]) - 1;
                //System.Console.WriteLine("{0}, {1}",a,b);
                pairs[i] = menu.getFood(a,b);
            }

            Order newOrder = new Order(pairs);

            queue.Enqueue(newOrder);
        }

        /// <summary>
        /// Burada gelen siparişi tamamlanıyoruz ve tamamlandığını ekrana yazdırıyoruz.
        /// Karmaşıklığı Dequeue() fonksiyonunun karmaşıklığı kadardır.
        /// O(1) denilebilir.
        /// </summary>
        public void OrderComplete()
        {
            Order complete = queue.Dequeue();

            Console.WriteLine("Complete: {0}", complete.ToString());
        }

        /// <summary>
        /// Burada siparişi ilk önce string ifadeye dönüştüyoruz. Ondan sonra ekrana yazdırıyoruz.
        /// Karmaşıklığı ToString() metodunun karmaşıklığı kadardır.
        /// O(1)
        /// </summary>
        public void Print()
        {
            Console.WriteLine(ToString());
        }
        /// <summary>
        /// Burada gelen sipariş kuyruğunu string ifadeye çevirir()
        /// O(1)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return queue.ToString();
        }
    }
}