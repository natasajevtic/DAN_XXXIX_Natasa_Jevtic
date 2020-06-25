using System;
using System.Globalization;

namespace Zadatak_1
{
    /// <summary>
    /// This class is responsible for creating new songs.
    /// </summary>
    class Song
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public int ID { get; set; }

        public Song() { }

        public Song(string author, string name, TimeSpan duration)
        {
            Author = author;
            Name = name;
            Duration = duration;
        }
        /// <summary>
        /// This method creates new song with properties based on user input.
        /// </summary>
        /// <returns>Created object of class Song.</returns>
        public Song Create()
        {
            Console.WriteLine("Enter a name of song:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter a author of song:");
            string author = Console.ReadLine();
            Console.WriteLine("Enter a duration of song (hours:minutes:seconds):");
            string duration = Console.ReadLine();
            bool conversion = TimeSpan.TryParseExact(duration, "hh\\:mm\\:ss", CultureInfo.CurrentCulture, out TimeSpan interval);
            while (!conversion)
            {
                Console.WriteLine("Invalid input. Try again:");
                duration = Console.ReadLine();
                conversion = TimeSpan.TryParseExact(duration, "hh\\:mm\\:ss", CultureInfo.CurrentCulture, out interval);
            }
            Song newSong = new Song(author, name, interval);
            return newSong;
        }
    }
}
