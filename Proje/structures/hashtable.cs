namespace structures
{
    public class Hash
    {
        int[] table;
        bool[] deleted;
        int N;

        public Hash(int N)
        { 
            table = new int[N];
            deleted = new bool[N];
            this.N = N;
        }
        
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
        public int HashMethod(int value)
        {
            return value%N;
        }
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