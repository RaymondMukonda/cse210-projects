using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("C# Tutorial Basics", "Alice", 600);
        video1.AddComment(new Comment("John", "Great explanation!"));
        video1.AddComment(new Comment("Sarah", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Mike", "Clear and easy to follow."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Introduction to OOP", "Bob", 720);
        video2.AddComment(new Comment("Anna", "Loved the examples."));
        video2.AddComment(new Comment("James", "Can you make one on inheritance?"));
        video2.AddComment(new Comment("Lily", "This was perfect for my homework."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Advanced C# Collections", "Charlie", 840);
        video3.AddComment(new Comment("David", "Exactly what I needed."));
        video3.AddComment(new Comment("Emma", "Collections were confusing, now I get it!"));
        video3.AddComment(new Comment("Sophia", "Please do one on LINQ next."));
        videos.Add(video3);

        // Display all videos and comments
        foreach (Video video in videos)
        {
            video.Display();
        }

        //  i dont know if i was supposed to have all the work in one file .
    }
}
