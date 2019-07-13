namespace p2p.Models
{
    public class CategoriesData
    {
        public int Id { get; set;}
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public CategoriesData Clone()
        {
            return new CategoriesData()
            {
                Id = Id,
                ParentId = ParentId,
                Name = Name,
                Description = Description
            };

        }
    }
}
