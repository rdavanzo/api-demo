using System;
using JsonApiDotNetCore.Models;

namespace ApiDemo.Models
{
    public class Review : Identifiable
    {
        public Review(String reviewerName, String body)
        {
            if (String.IsNullOrEmpty(reviewerName))
            {
                throw new ArgumentException("Reviewer name must not be null or empty.", nameof(reviewerName));
            }
            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentException("Body must not be null or empty.", nameof(body));
            }

            ReviewerName = reviewerName;
            Body = body; 
        }

        public Review()
        {
        }

        [Attr("reviewer-name")]
        public String ReviewerName { get; set; }

        [Attr("body")]
        public String Body { get; set; }

        public Int32 BookId { get; set; }

        [HasOne("book")]
        public Book Book { get; set; }
    }
}
