namespace structures
{
    public class Hash
    {
        /// <summary>
        /// Burada hash veri yapısını kullanıyoruz N boyutunda, hem silinmiş yerlerin adreslerini 
        /// hem de tablonun kendisini tutuyoruz.
        /// </summary>
        int[] table;
        bool[] deleted;
        int N;

        public Hash(int N)
        { 
            table = new int[N];
            deleted = new bool[N];
            this.N = N;
        }
        
        /// <summary>
        /// Burada string değerleri hash tablomuza kaydederken uygulayacağımız fonksiyon
        /// O(value.lenght)
        /// </summary>
        /// <param name="value"> burada value değerini alıyoruz</param>
        /// <returns>Gelen value'nun adresini (int) döndürüyoruz</returns>
        public int HashMethod(string value)
        {
            int i = 0;
            int position = 0;

            for(i = 0;i<value.Length;i++){
                position = 39*position;
            }
            position = position%N;
            return position;
        }
        /// <summary>
        /// Burada gelen int değerleri tablomuza kaydederken uygulayacağımız fonksiyon mevcut
        /// O(1)
        /// </summary>
        /// <param name="value">int bir value değeri alıp</param>
        /// <returns>İnt</returns>
        public int HashMethod(int value)
        {
            return value%N;
        }
        /// <summary>
        /// Burada bize verilen int değerlerin adresini buluyoruz
        /// O(1)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int HashSearch(int value)
        {
            int address;
            address = HashMethod(value);
            while(table[address]!= null)
            {
                if(!(deleted[address]) && table[address] == value)
                    break;
                address = (address + 1) % N;
            }
            return table[address];
        }
        /// <summary>
        /// Burada bize verilen int değeri biz hash fonksiyonundan sonra hash tablosuna ekliyoruz
        /// O(1)
        /// </summary>
        /// <param name="newValue"> BU bizim fonksiyonumuzun parametresi ve eklenecek değer.</param>
        public void HashAdd(int newValue)
        {
            int address;
            address = HashMethod(newValue);
            while(table[address]!= null && !(deleted[address])){
                address = (address + 1) % N;
            }
            if(table[address]!= null)
                deleted[address] = false;
            table[address] = newValue;
        }
        /// <summary>
        /// Burada bize verien değeri hash tablomuzdan siliyoruz.
        /// O(1)
        /// </summary>
        /// <param name="deletedValue">Fonksiyonumuzun parametresi ve silinecek değer.</param>
        public void HashDelete(int deletedValue)
        {
            int address;
            address = HashMethod(deletedValue);
            while(table[address]!=null){
                if(!(deleted[address])&&table[address] == deletedValue)
                    break;
                address = (address+1)%N;
            }
            deleted[address] = true;
        }
    }
}