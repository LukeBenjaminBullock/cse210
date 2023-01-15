// ! Classes 

        public class Entry 
        {

            public string _response = "";

            public string _prompt = "";
            
            public string _date = "";

            public Entry()
            {

            }
        


            public string AddToEnteries()
            {
                string newEntry = string.Concat("Date: " + _date + Environment.NewLine + "Prompt: " + _prompt + Environment.NewLine + _response);
                return newEntry;
            }

        }

        public class Journal
        {
            public List<string> _entries = new List<string>();

            public string _textFileName = "";

            public Journal()
            {

            }

            public void DisplayList()
            {
                foreach (string item in _entries)
                {
                    Console.WriteLine(item);
                }
            }

            public void Load()
            {
                Console.Write("Where would you like to retrive past entries from? ");
                string filepath = Console.ReadLine();
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        _entries.Add(line);
                    }
                }
            }

            public void Save()
            {
                Console.Write("What is the file you would like to Save to? ");
                string filepath = Console.ReadLine();
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    foreach (string line in _entries)
                    {
                        writer.WriteLine(line);
                    }
                }
            }

        }