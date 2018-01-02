using System;
using System.Collections.Generic;
using JsonApiDotNetCore.Models;
namespace ApiDemo.Models
{
    public class Book : Identifiable
    {
        private readonly List<Review> _reviews = new List<Review>();

        public Book(String title, String isbn, DateTime publishDate) 
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title must not be null or empty.", nameof(title));
            }

            Title = title; 
            ISBN = isbn; 
            PublishDate = publishDate;
        }

        public Book()
        {
        }

        [Attr("title")]
        public String Title { get; set; }

        [Attr("isbn")]
        public String ISBN { get; set; }
        
        [Attr("publish-date")]
        public DateTime PublishDate { get; set; }

        public Int32 AuthorId { get; set; }
        
        // Todo: A book could have many authors, convert this to a many-to-many relationship
        // (see https://github.com/json-api-dotnet/JsonApiDotNetCore/issues/151).
        [HasOne("author")]
        public Author Author { get; set; }

        ///<summary>
        /// A collection of reviews for the book.
        ///</summary>
        [HasMany("reviews")]
        public List<Review> Reviews { get => _reviews; }
    }
}