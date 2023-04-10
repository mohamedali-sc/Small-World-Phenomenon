using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_world_phenomen
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultSample = "Test/Sample/";
            const string defaultComplete = "Test/Complete/small/Case2/";
            Dictionary<string, List<string>> moviesData = TestUnit.readMovies(defaultComplete + "movies187.txt");
            Graph graph = new Graph();
            graph.constract_graph(moviesData);

            

            List<KeyValuePair<string, string>> queries = TestUnit.readQueries(defaultComplete + "queries50.txt");
            queries.ForEach(pair =>
            {
                graph.BFS(pair.Key, pair.Value, graph.adjcencyList);
                Console.WriteLine(graph.distances[pair.Value]);
                int max = int.MinValue;
                Console.WriteLine("Strength: "+graph.path(pair.Key, pair.Value, 0, graph.parents[pair.Value][0],max) );


                foreach(var film in graph.films)
                {
                    Console.Write(film + " ");

                }

                Console.WriteLine();
            });


            Console.Read();
        }
    }
}
