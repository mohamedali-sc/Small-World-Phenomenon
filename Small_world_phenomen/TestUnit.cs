using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_world_phenomen
{
   static  class TestUnit
    {


        public static List<KeyValuePair<string, string>> readQueries(string path)
        {
            List<KeyValuePair<string, string>> actors_pairs = new List<KeyValuePair<string, string>>(10);
            char[] seprators = { '/' };
            StreamReader reader = new StreamReader(path);

            KeyValuePair<string, string> actor_pair;
            while (!reader.EndOfStream)
            {

                string[] values = reader.ReadLine().Split(seprators);

                for(int i = 0; i <values.Length -1;i++)
                {
                    actor_pair = new KeyValuePair<string, string>(values[i], values[i+1]);

                    actors_pairs.Add(actor_pair);
                }
                

            }


            return actors_pairs;
        }
        public static Dictionary<string, List<string>> readMovies(string path)
        {
            //Movie //num //actor / actor2

            char[] seprators = {  '/' };
            Dictionary<string, List<string>> movieData = new Dictionary<string, List<string>>();

            StreamReader reader = new StreamReader(path);

            int indexAfterFilmName = 1;
            
            while(!reader.EndOfStream)
            {
                List<string> actor_list = new List<string>();

                string line = reader.ReadLine();

               string [] input =  line.Split(seprators);

                //As film name can contain "," so this to prevent error.
                try
                {
                    movieData.Add(input[0], actor_list);

                }

                catch(ArgumentException e)
                {

                    //Console.WriteLine(input[0] + input[1]);
                    movieData.Add(input[0] + input[1], actor_list);
                    indexAfterFilmName = 2;
                   
                }
                for (int i = indexAfterFilmName; i <input.Length;i++)
                {
                    actor_list.Add(input[i]);
                }

                indexAfterFilmName = 1;
            }

            // return 


            return movieData;
        }

        public static bool testCode()
        {


            return true;
        }


    }
}
