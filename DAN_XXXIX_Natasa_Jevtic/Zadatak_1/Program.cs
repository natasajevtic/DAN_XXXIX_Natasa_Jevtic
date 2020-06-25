using System;
using System.Linq;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        public static EventWaitHandle songIsRunning = new ManualResetEvent(false);
        static bool againChoose;
        static EventWaitHandle againPlaySong = new AutoResetEvent(false);
        /// <summary>
        /// This method waits for signal that song is runned and then for every 1000 miliseconds of song duration displays that song is running 
        /// </summary>
        /// <param name="song"></param>
        static void IsSongRunning(Song song)
        {
            //waiting signal that song started
            songIsRunning.WaitOne();
            //converting duration of song to miliseconds
            double counter = song.Duration.TotalMilliseconds;
            while (counter != 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Song is running...");
                counter -= 1000;
            }
            if (counter == 0)
            {
                Console.WriteLine("Song finished.");
                Console.WriteLine("Do you still want to listen music (yes/no)?");
                string answer = Console.ReadLine();
                while (answer.ToUpper() != "YES" && answer.ToUpper() != "NO")
                {
                    Console.WriteLine("Invalid input. Try again:");
                    answer = Console.ReadLine();
                }
                if (answer.ToUpper() == "YES")
                {
                    againPlaySong.Set();
                }
                else if (answer.ToUpper() == "NO")
                {
                    againChoose = false;
                }
            }
        }
        /// <summary>
        /// This method waits for signal if user choice to listen music again.
        /// </summary>
        static void PlayAgain()
        {
            againPlaySong.WaitOne();
            againChoose = true;
        }
        /// <summary>
        /// This method displays user different options and performs actions based on user choice.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            AudioPlayer player = new AudioPlayer();
            player.ReadSongsFromTxt();
            player.ReadAdsFromTxt();
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
                        do
                        {
                            player.ViewAll();
                            Console.WriteLine("Choose which song want to listen:");
                            string songNumber = Console.ReadLine();
                            bool conversion = Int32.TryParse(songNumber, out int songID);
                            //checking for existing song
                            while (!conversion || songID <= 0 || songID > player.Songs.Max(x => x.ID))
                            {
                                Console.WriteLine("Invalid input. Try again:");
                                songNumber = Console.ReadLine();
                                conversion = Int32.TryParse(songNumber, out songID);
                            }                            
                            Song findedSong = player.FindingSong(songID);
                            player.PlayingSong(findedSong);
                            Thread isSongRunning = new Thread(() => IsSongRunning(findedSong));
                            Thread runningAds = new Thread(() => player.RunningAds(findedSong));
                            Thread playAgain = new Thread(PlayAgain);
                            playAgain.Start();
                            runningAds.Start();
                            isSongRunning.Start();
                            runningAds.Join();
                            isSongRunning.Join();
                        } while (againChoose == true);                        
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
