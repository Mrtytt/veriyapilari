using structures;
using Newtonsoft.Json;
using App;

namespace structs
{
    /// <summary>
    /// Burada Menu varlığımızı oluşturuyoruz. Bu menü varlığı bir kategori listesine sahip
    /// ve bir 
    /// </summary>
    public class Menu
    {
        
        public Category[] categories;

        [JsonIgnore]
        public tree<Category,Food,string> menuTree;

        /// <summary>
        /// Menü dosyadan okunduktan sonra bunun ağaca yazdırır
        /// Kategori sayısı N
        /// Kategorilerdeki yemek sayısı M
        /// O(N*M)
        /// </summary>
        public void Init()
        {
            menuTree = new tree<Category,Food,string>("Menü");
            // create tree

            foreach (Category category in categories)
            {
                Category newct = new Category(category.name,category.id);
                newct.foods = category.foods;
                //System.Console.WriteLine(category.foods.Length);


                newct.UpdateBranch();

                menuTree.add(newct.branch);
            }
        }

        /// <summary>
        /// Yeni kategori ekler
        /// O(N) 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void addCategory(string name,int id)
        {
            /*
            Category newct = new Category(category.name);
            newct.foods = category.foods;
            newct.UpdateBranch();

            menuTree.add(newct.branch);
            */

            Category newct = new Category(name, id);
            newct.foods = new Food[0];
            newct.UpdateBranch();

            // update tree

            //newct.branch.chlidren.Print(2);

            //string str = newct.branch.self.ToString();
            //System.Console.WriteLine(str);

            //menuTree.root.chlidren.Print(2);

            menuTree.add(newct.branch);

            //menuTree.root.chlidren.Print(2);


            // update array
            Category[] newCategories = new Category[categories.Length + 1];
            int index = 0;
            foreach (Category category in categories)
            {
                //System.Console.WriteLine("{0}, {1}",index,category.name);
                newCategories[index++] = category;
            }
            newCategories[index] = newct;

            categories = newCategories;

            //System.Console.WriteLine("{0}, {1}", index, newct.name);


        }

        /// <summary>
        /// Id ye göre kategori çıkarır , arrayi kaydetme için günceller
        /// O(N) 
        /// </summary>
        /// <param name="id"></param>
        public void removeCategory(int id)
        {
            // update array

            Category[] newCategories = new Category[categories.Length - 1];
            int index = 0;
            foreach (Category category in categories)
            {
                if(index != id)
                    newCategories[index++] = category;
            }

            categories = newCategories;

            // update tree
            object obj = menuTree.root.removeChildren(id);
        }


        /// <summary>
        /// id ye göre category döndürür
        /// O(N)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category getCategory(int id)
        {
            Category val = new Category("",0);

            int index = 0;
            foreach (Category category in categories)
            {
                if (index++ == id)
                    val = category;
            }

            return val;
        }

        /// <summary>
        /// seçilen yemeği çıkarır 
        /// categori sayısı N
        /// yemek sayısı M
        /// O(N * M)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Food removeFood(int categoryId, int id)
        {
            
            Food removed = menuTree.root.getChildren(categoryId).removeChildren(id);

            return removed;
        }

        /// <summary>
        /// id lere göre yemek döndürür
        /// kategori sayısı M
        /// yemek sayısı N
        /// O(N*M) 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Food getFood(int categoryId, int id)
        {
            Category cc = getCategory(categoryId);

            Food get = cc.GetFood(id);

            return get;
        }

        public void Print()
        {
            menuTree.Print();
        }

        /// <summary>
        /// stringe çevirir
        /// kategori sayısı N
        /// yemek sayısı M
        /// O(N*M)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return menuTree.ToString();
        }

        /// <summary>
        /// Menüyü dosyaya json a kaydeder
        /// Karmaşıkşığı Jsona çevirme kadardır 
        /// </summary>
        /// <param name="path"></param>
        public void SaveMenu(string path = Globals.MENU_PATH)
        {
            string data = ToJson();

            try
            {
                File.WriteAllText(path, data);
                Console.WriteLine("Başarıyla kaydedildi.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"HATA: {e.Message}");
            }
        }

        /// <summary>
        /// objeyi json stringine çevirir kaydetmek için
        /// karmaşıklığı Jsona çevirme kadardır 
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }


        /// <summary>
        /// dosya uzantısından menü objesi oluşuturur
        /// karmaşıklığı FromJson kadardır
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Menu FromPath(string path = Globals.MENU_PATH)
        {

            try
            {
                // Read the entire file contents as a string
                string fileContents = File.ReadAllText(path);

                // Display the file contents
                Console.WriteLine($"Okundu");

                return FromJson(fileContents);
            }
            catch (Exception e)
            {
                Console.WriteLine($"HATA: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// dosyanın okunması stringini objeye çevirir
        /// karmaşıklığı dönüştürme kadardır 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Menu FromJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Menu>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing menu: {ex.Message}");
                return null;
            }
        }
    }

    public class Category
    {
        public int id;
        public string name;
        public Food[] foods;

        [JsonIgnore]
        public treeNode<Category,Food> branch;
        [JsonIgnore]
        public bool lastUpdatedIsBranch = false;

        public Category(string name, int id)
        {
            this.name = name;
            this.id = id;

            lastUpdatedIsBranch = true;
            //UpdateBranch();
        }

        /// <summary>
        /// yeni bir yemek ekler kategoriye
        /// O(N) 
        /// </summary>
        /// <param name="newFood"></param>
        public void AddFood(Food newFood)
        {
            branch.addChildren(newFood);
            UpdateArray();
        }

        /// <summary>
        /// kategoriden yemek çıkarır
        /// O(N)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Food RemoveFood(int index)
        {
            Food removed = branch.removeChildren(index);

            //System.Console.WriteLine("TEST");
            branch.Print(2);
            UpdateArray();

            return removed;
        }

        /// <summary>
        /// id ye göre yemeği döndürür
        /// O(N) 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Food GetFood(int index)
        {
            if (branch == null)
            {
                UpdateBranch();
            }

            //System.Console.WriteLine(index);

            //branch.chlidren.Print(2);

            Food get = branch.getChildren(index);

            //ystem.Console.WriteLine(get.ToString());

            return get;
        }

        /// <summary>
        /// katgoriyi günceller ağacı yada arrayi
        /// O(N) 
        /// </summary>
        public void Update()
        {
            if (!lastUpdatedIsBranch)
            {
                UpdateBranch();
                lastUpdatedIsBranch = true;
            }
            else
            {
                UpdateArray();
                lastUpdatedIsBranch = false;
            }
        }

        /// <summary>
        /// normal değerleri alır ve ağacı günceller
        /// O(N)
        /// </summary>
        public void UpdateBranch()
        {
            if (branch == null)
            {
                branch = new treeNode<Category, Food>(this);
            }

            branch.self = this;

            branch.chlidren =  new linkedList<Food>();

            foreach (Food food in foods)
            {
                branch.chlidren.add(food);
            }
        }

        /// <summary>
        /// Arrayi günceller 
        /// O(N) 
        /// </summary>
        public void UpdateArray()
        {
            foods = branch.chlidren.ToArray();
        }

        /// <summary>
        /// ismini döndürür
        /// O(1) 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }

    }

    public class Food
    {
        public int id;
        public string name;
        public string description;
        public int price;

        /// <summary>
        /// stringe çevirir
        /// O(1) 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string val = string.Format("[{0}₺] ({3}) {1} : {2}",price, name, description, id);
            return val;
        }
    }
}