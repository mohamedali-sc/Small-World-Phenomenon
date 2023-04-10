using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_world_phenomen
{

    //Expermintal Class
    //public class Actor_vertex // Actor with films that participate in 
    //{
    //    string actorName;

    //   List<string> movies;
    //}
    public enum COLORS { BLACK, WHITE, GRAY };
    class Graph
    {

        public Dictionary<string, Dictionary<string, List<string>>> adjcencyList; // key : ActorName ,Value :  Actors connected to (with films) 
        public Dictionary<string, COLORS> colors;
        public Dictionary<string, List<string>> parents;
        public Dictionary<string, int> distances;

        public HashSet<string> films;
        public Graph()
        {
            adjcencyList = new Dictionary<string, Dictionary<string, List<string>>>();
            colors = new Dictionary<string, COLORS>();
            parents = new Dictionary<string, List<string>>();
            distances = new Dictionary<string, int>();

            films = new HashSet<string>();

            
        }

       
        public int path(string source , string destination ,int n,string adj,int max)
        {


            foreach (var film in adjcencyList[destination][adj])
            {
                
                films.Add(film);

              //  Console.Write(film + "  ");
            }

         

            //Console.WriteLine();
            n += adjcencyList[destination][adj].Count;

            destination = adj;

            if (adj == source)
                return films.Count;

            
            foreach (var parent in parents[destination])
            {
               max = Math.Max( path(source, destination, n, parent,max)  , max) ;

            }

            return max;
        }
        public void BFS(string s, string d, Dictionary<string, Dictionary<string, List<string>>> adjList)
        {
            colors = new Dictionary<string, COLORS>();
            parents = new Dictionary<string, List<string>>();
            distances = new Dictionary<string, int>();
            Queue<string> vertices = new Queue<string>();
            films = new HashSet<string>();
            int distance = 0;

            foreach (var actor in adjList)
            {
                colors.Add(actor.Key, COLORS.WHITE);
                distances.Add(actor.Key, int.MaxValue);

                List<string> parent = new List<string>();
            }


            colors[s] = COLORS.GRAY;
            distances[s] = 0;

            vertices.Enqueue(s);

            string v = "";

            while (vertices.Count != 0)
            {
                v = vertices.Dequeue();
                foreach (var adj in adjList[v].Keys)
                {

                    if (colors[adj] == COLORS.WHITE || colors[adj] == COLORS.GRAY)
                    {
                        colors[adj] = COLORS.GRAY;
                        if (distances[adj] > distances[v] + 1)
                            distances[adj] = distances[v] + 1;
                        if (parents.ContainsKey(adj))
                        {
                            if(! (parents[adj].Contains(v)) && adjcencyList[v][adj].Count == 0)
                                parents[adj].Add(v);
                        }
                        else
                        {

                            List<string> parent = new List<string>();
                            parent.Add(v);
                            parents[adj] = parent;
                        }

                        vertices.Enqueue(adj);

                    }
                    colors[v] = COLORS.BLACK;
                }

                if (distances[d] != int.MaxValue && !(vertices.Contains(d)))
                {
                    return;
                }
            }
            return;
        }
        public void constract_graph(Dictionary<string, List<string>> moviesData)
        {
            foreach (var movie in moviesData)
            {
                //  Console.Write("key : " +  + "  value: ");
                foreach (var actor in movie.Value)
                {
                    foreach (var friend in movie.Value)
                    {

                        if (actor != friend)
                        {
                            if (!adjcencyList.ContainsKey(actor))
                            {

                                List<string> films = new List<string>();
                                films.Add(movie.Key);

                                Dictionary<string, List<string>> friendInfo = new Dictionary<string, List<string>>();
                                friendInfo.Add(friend, films);
                                adjcencyList.Add(actor, friendInfo);
                            }
                            else
                            {
                                if (!adjcencyList[actor].ContainsKey(friend))
                                {
                                    List<string> films = new List<string>();
                                    films.Add(movie.Key);
                                    adjcencyList[actor].Add(friend, films);
                                }
                                else
                                {
                                    adjcencyList[actor][friend].Add(movie.Key);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
