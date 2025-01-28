using _0_Framework.Domain;
using ShopManagment.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ShopManagment.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public ProductCategory(string name, string title, string description, string picture, string pictureAlt,
            string pictureTitle, string metaDescription, string keywords, string slug)
        {
            Name = name;
            Title = title;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Keywords = keywords;
            Slug = slug;
        }

        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string Keywords { get; private set; }
        public string Slug { get; private set; }
        public List<Product> Products { get; private set; }
        public ProductCategory()
        {
          Products = new List<Product>();
        }
        public void Edit(string name, string title, string description, string picture, string pictureAlt,
            string pictureTitle, string metaDescription, string keywords, string slug)
        {
            Name = name;
            Title = title;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Keywords = keywords;
            Slug = slug;
        }
    }
}
