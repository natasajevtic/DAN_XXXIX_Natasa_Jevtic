using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        /// <summary>
        /// This method displays user different options and performs actions based on user choice.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            AudioPlayer player = new AudioPlayer();
            player.ReadSongsFromTxt();
            Song song = new Song();
            string option = "";
            do
            {
                Console.WriteLine("\n1. Add a new song");
                Console.WriteLine("2. View all songs");
                Console.WriteLine("3. Open audio player");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Choose option:");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Song newSong = song.Create();
                        player.AddSong(newSong);
                        break;
                    case "2":
                        player.ViewAll();
                        break;
                    case "3":                        
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again:");
                        break;
                }
            } while (option != "4");
        }
    }
}
