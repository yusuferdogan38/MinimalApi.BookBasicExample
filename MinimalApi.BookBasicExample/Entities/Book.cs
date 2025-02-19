﻿namespace MinimalApi.BookBasicExample.Entities
{
    public class Book
    {

        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int  PageSize { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
