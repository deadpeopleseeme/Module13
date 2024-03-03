using System.Linq;

namespace Module13
{
    public class ListsToTextReader
    {
        public string PathToFile { get; set; }
        public List<string> TestList { get; set; }
        public LinkedList<string> TestLinkedList { get; set; }

        public bool IsPathOK { get; private set; }

        public StreamReader Reader { get; set; }

        public void TextToListReader()
        {

            Reader = new(PathToFile);
            string? line;
            while ((line = Reader.ReadLine()) != null)
            {
                TestList.Add(line);
            }
        }

        public void TextToLinkedListReader()
        {

            Reader = new(PathToFile);
            string? line;
            while ((line = Reader.ReadLine()) != null)
            {
                TestLinkedList.AddLast(line);
            }
        }


        public ListsToTextReader(string path)
        {
            PathToFile = path;
            try
            {
                Reader = new(PathToFile);
                IsPathOK = true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                IsPathOK = false;
            }
            TestList = [];
            TestLinkedList = [];
        }
    }
}
