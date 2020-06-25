using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zadatak_1
{
    /// <summary>
    /// This class is responsible for adding songs and ads to collections, and their perfoming.
    /// </summary>
    class AudioPlayer
    {
        public List<Song> Songs { get; set; } = new List<Song>();
        public List<string> Ads { get; set; } = new List<string>();
        /// <summary>
        /// This method sets id of song and adds song to collection.
        /// </summary>
        /// <param name="newSong"></param>
        public void AddSong(Song newSong)
        {
            int maxId = 0;
            if (Songs.Count > 0)
            {
                maxId = Songs.Max(x => x.ID);
            }
            newSong.ID = maxId + 1;
            Songs.Add(newSong);
            WriteSongsToTxt();
            Console.WriteLine("Successfully added song.");
        }
        /// <summary>
        /// This method reads songs from file and after that puts them in collection.
        /// </summary>
        public void ReadSongsFromTxt()
        {
            try
            {
                string[] lines = File.ReadAllLines(@"../../Music.txt");
                List<string> list = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    Song song = new Song();
                    list = lines[i].Split(' ').ToList();
                    song.Author = list[0].Trim(':');
                    song.Name = list[1];
                    TimeSpan.TryParse(list[2], out TimeSpan interval);
                    song.Duration = interval;
                    int maxId = 0;
                    if (Songs.Count > 0)
                    {
                        maxId = Songs.Max(x => x.ID);
                    }
                    song.ID = maxId + 1;
                    Songs.Add(song);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file was not found.");
            }
        }
        /// <summary>
        /// This method writes songs to file.
        /// </summary>
        public void WriteSongsToTxt()
        {
            StreamWriter writer = new StreamWriter(@"../../Music.txt");
            string output;
            foreach (Song song in Songs)
            {
                output = song.Author + ": " + song.Name + " " + song.Duration;
                writer.WriteLine(output);
            }
            writer.Close();
        }
        /// <summary>
        /// This method displays all songs to console.
        /// </summary>
        public void ViewAll()
        {
            foreach (Song song in Songs)
            {
                Console.WriteLine(song.ID + ". " + song.Author + ": " + song.Name + " " + song.Duration);
            }
            if (!Songs.Any())
            {
                Console.WriteLine("There are no songs.");
            }
        }
    }
}
