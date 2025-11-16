namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BlogPosts
    {
        [Key]
        public int Uid { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int AuthorUid { get; set; }

        public int CategoryUid { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual BlogCategories BlogCategories { get; set; }

        public virtual Users Users { get; set; }
    }
}
