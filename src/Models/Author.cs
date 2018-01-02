using System;
using System.Collections.Generic;
using JsonApiDotNetCore.Models;

namespace ApiDemo.Models
{
    public class Author : Identifiable
    {
        public Author(String firstName, String lastName)
        {
            if (String.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First name must not be null or empty.", nameof(firstName));
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public Author()
        {
        }

        [Attr("first-name")]
        public String FirstName { get; set; }
        
        [Attr("last-name")]
        public String LastName { get; set; }

        ///<summary>
        /// A collection of <see cref="Book"/>s authored by the <see cref="Author"/>. 
        ///</summary>
        [HasMany("books")]
        public List<Book> Books { get; set; }
    }
}